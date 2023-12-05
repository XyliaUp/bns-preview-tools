using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using CUE4Parse.Compression;

using Ionic.Zlib;

using Xylia.Preview.Data.Engine.DatData.Third;

using static Xylia.Preview.Data.Engine.DatData.Third.MySpport;

using AesProvider = System.Security.Cryptography.Aes;

namespace Xylia.Preview.Data.Engine.DatData;
public sealed class BNSDat : IDisposable
{
	#region Fields
	public string Path;

	public bool Bit64;

	public KeyInfo KeyInfo = new();
	#endregion

	#region Constructor
	public BNSDat(string DatPath, bool? Is64 = null, byte[] AES = null)
	{
		Bit64 = Is64 ?? DatPath.Judge64Bit();
		Path = DatPath;

		if (AES != null) KeyInfo.AES_KEY = AES;
	}


	public static implicit operator BNSDat(FileInfo file) => file != null ? new(file.FullName) : null;

	public static implicit operator BNSDat(string path) => File.Exists(path) ? new(path) : null;
	#endregion


	#region DatInfo
	public byte[] Magic = "UOSEDALB"u8.ToArray();

	public uint Version;
	public byte[] Unknown_001;
	public byte[] Unknown_002;

	public bool IsCompressed;
	public bool IsEncrypted;

	public byte[] Signature;


	private List<FileTableEntry> _files;

	public List<FileTableEntry> FileTable
	{
		private set => _files = value;
		get
		{
			if (_files is null)
				this.Read();

			return _files;
		}
	}

	public IEnumerable<FileTableEntry> EnumerateFiles(string searchPattern)
	{
		var regex = new Regex(searchPattern
			.Replace("/", "\\")
			.Replace("\\", "\\\\")
			.Replace("*", ".*?"), RegexOptions.IgnoreCase);

		return FileTable.Where(f => regex.Match(f.FilePath).Success);
	}
	#endregion


	#region Methods
	public static byte[] Decrypt(byte[] buffer, int size, byte[] AES)
	{
		var aes = AesProvider.Create();
		aes.Mode = CipherMode.ECB;
		aes.Padding = PaddingMode.None;

		return aes.CreateDecryptor(AES, null).TransformFinalBlock(buffer, 0, buffer.Length);
	}

	public static byte[] Encrypt(byte[] buffer, int size, out int sizePadded, byte[] AESKey)
	{
		int AES_BLOCK_SIZE = AESKey.Length;
		sizePadded = size + (AES_BLOCK_SIZE - size % AES_BLOCK_SIZE);

		byte[] output = new byte[sizePadded];
		byte[] temp = new byte[sizePadded];


		Array.Copy(buffer, 0, temp, 0, buffer.Length);
		buffer = null;

		var aes = AesProvider.Create();
		aes.Mode = CipherMode.ECB;

		ICryptoTransform encrypt = aes.CreateEncryptor(AESKey, new byte[16]);
		encrypt.TransformBlock(temp, 0, sizePadded, output, 0);

		temp = null;
		return output;
	}

	public static byte[] Inflate(byte[] buffer, int sizeDecompressed, byte? compressionLevel = null)
	{
		if (sizeDecompressed == 0)
			sizeDecompressed = buffer.Length;

		var output = new MemoryStream();

		ZlibStream zs = new(output, CompressionMode.Compress, (CompressionLevel)(compressionLevel ?? 6), true);
		zs.Write(buffer, 0, sizeDecompressed);
		zs.Flush();
		zs.Close();

		return output.ToArray();
	}


	public static byte[] Unpack(byte[] buffer, int sizeStored, int sizeSheared, int sizeUnpacked, bool isEncrypted, bool isCompressed, KeyInfo KeyInfo)
	{
		var output = buffer;
		if (isEncrypted) output = Decrypt(buffer, sizeStored, KeyInfo.AES_KEY);

		var uncompressedBuffer = new byte[sizeUnpacked];
		Compression.Decompress(output, uncompressedBuffer, isCompressed ? CompressionMethod.Zlib : CompressionMethod.None);
		return uncompressedBuffer;
	}

	public static byte[] Pack(byte[] buffer, int sizeUnpacked, out int sizeSheared, out int sizeStored, bool encrypt, bool compress, byte compressionLevel, byte[] Key)
	{
		byte[] output = buffer;

		sizeStored = sizeSheared = sizeUnpacked;

		if (compress)
		{
			output = Inflate(output, sizeUnpacked, compressionLevel);

			sizeSheared = output.Length;
			sizeStored = output.Length;
		}

		if (encrypt)
		{
			output = Encrypt(output, output.Length, out sizeStored, Key);
		}

		return output;
	}

	public static void Pack(PackParam param)
	{
		ArgumentNullException.ThrowIfNull(param.Aes);

		var Package = new BNSDat(param.PackagePath, true, param.Aes);
		Package.FileTable = Directory.EnumerateFiles(param.FolderPath, "*", SearchOption.AllDirectories).Select(x => new FileTableEntry()
		{
			FilePath = x.Replace(param.FolderPath, "").TrimStart('\\'),
			Unknown_001 = 2,
			IsCompressed = true,
			IsEncrypted = true,
			Unknown_002 = 0,
			FileDataSizeUnpacked = 0,
			Padding = new byte[60],
			Data = File.ReadAllBytes(x),
		}).ToList();
		Package.Magic = "UOSEDALB"u8.ToArray();
		Package.Version = 3;
		Package.Unknown_001 = new byte[5] { 0, 0, 0, 0, 0 };
		Package.IsCompressed = true;
		Package.IsEncrypted = true;
		Package.Signature = new byte[128];   //var Auth = RSA3;
		Package.Unknown_002 = new byte[62];

		// get pack data
		foreach (FileTableEntry item in Package.FileTable)
		{
			if (item.FilePath.EndsWith(".xml") || item.FilePath.EndsWith(".x16"))
			{
				var bns_xml = new BXML_CONTENT(Package.KeyInfo.XOR_KEY);
				bns_xml.ConvertFrom(item.Data);
				item.Data = bns_xml.Write();
			}
		}

		// write
		Package.Write(param.CompressionLevel);
	}
	#endregion



	private void Read()
	{
		#region head
		using var br = new BinaryReader(new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
		Magic = br.ReadBytes(8);
		Version = br.ReadUInt32();
		Unknown_001 = br.ReadBytes(5);
		var FileDataSizePacked = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		var FileCount = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		IsCompressed = br.ReadBoolean();
		IsEncrypted = br.ReadBoolean();

		// Update 200429
		if (Version == 3)
		{
			Signature = br.ReadBytes(128);
		}


		Unknown_002 = br.ReadBytes(62);

		var FileTableSizePacked = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		var FileTableSizeUnpacked = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		var FileTablePacked = br.ReadBytes(FileTableSizePacked);

		//不要相信数值，请读取当前流位置
		var OffsetGlobal = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		OffsetGlobal = (int)br.BaseStream.Position;
		#endregion

		#region files
		byte[] FileTableUnpacked = Unpack(FileTablePacked, FileTableSizePacked, FileTableSizePacked, FileTableSizeUnpacked, IsEncrypted, IsCompressed, KeyInfo);
		FileTablePacked = null;

		using BinaryReader br2 = new(new MemoryStream(FileTableUnpacked));
		FileTableUnpacked = null;

		//file info
		_files = new List<FileTableEntry>();
		for (int i = 0; i < FileCount; i++)
			_files.Add(new FileTableEntry(br2, Bit64, OffsetGlobal) { KeyInfo = KeyInfo });

		//file data
		Parallel.ForEach(_files, item =>
		{
			lock (br)
			{
				br.BaseStream.Position = item.FileDataOffset;
				item.CompressedBuffer = br.ReadBytes(item.FileDataSizeStored);
			}
		});
		#endregion
	}

	public void Write(BnsCompression.CompressionLevel CompressionLevel)
	{
		var Level = (byte)(3 * (byte)CompressionLevel);


		#region head
		BinaryWriter bw = new(new MemoryStream());

		bw.Write(Magic);
		bw.Write(Version);
		bw.Write(Unknown_001);

		// size
		int FileDataSizePacked = 0;
		if (Bit64) bw.Write((long)FileDataSizePacked);
		else bw.Write(FileDataSizePacked);

		// count
		if (Bit64) bw.Write((long)_files.Count);
		else bw.Write(_files.Count);

		bw.Write(IsCompressed);
		bw.Write(IsEncrypted);

		if (Version == 3)
			bw.Write(Signature);

		bw.Write(Unknown_002);
		#endregion

		#region file summary
		int FileDataOffset = 0;
		MemoryStream ms = new();
		BinaryWriter mosTable = new(ms);
		foreach (FileTableEntry item in _files)
		{
			item.FileDataOffset = FileDataOffset;
			item.FileDataSizeUnpacked = item.Data.Length;
			item.CompressedBuffer = Pack(item.Data, item.FileDataSizeUnpacked, out item.FileDataSizeSheared, out item.FileDataSizeStored, item.IsEncrypted, item.IsCompressed, Level, KeyInfo.AES_KEY);


			byte[] FilePath = Encoding.Unicode.GetBytes(item.FilePath);
			if (Bit64) mosTable.Write((long)item.FilePath.Length);
			else mosTable.Write(item.FilePath.Length);

			mosTable.Write(FilePath);
			mosTable.Write(item.Unknown_001);
			mosTable.Write(item.IsCompressed);
			mosTable.Write(item.IsEncrypted);
			mosTable.Write(item.Unknown_002);

			if (Bit64) mosTable.Write((long)item.FileDataSizeUnpacked);
			else mosTable.Write(item.FileDataSizeUnpacked);

			if (Bit64) mosTable.Write((long)item.FileDataSizeSheared);
			else mosTable.Write(item.FileDataSizeSheared);

			if (Bit64) mosTable.Write((long)item.FileDataSizeStored);
			else mosTable.Write(item.FileDataSizeStored);

			if (Bit64) mosTable.Write((long)item.FileDataOffset);
			else mosTable.Write(item.FileDataOffset);

			FileDataOffset += item.FileDataSizeStored;

			mosTable.Write(item.Padding);
		}

		int FileTableSizeUnpacked = (int)mosTable.BaseStream.Length;
		int FileTableSizeSheared = FileTableSizeUnpacked;
		int FileTableSizePacked = FileTableSizeUnpacked;

		var buffer_unpacked = ms.ToArray();
		mosTable.Dispose();
		mosTable = null;

		var buffer_packed = Pack(buffer_unpacked, FileTableSizeUnpacked, out FileTableSizeSheared, out FileTableSizePacked, IsEncrypted, IsCompressed, Level, KeyInfo.AES_KEY);
		buffer_unpacked = null;

		if (Bit64) bw.Write((long)FileTableSizePacked);
		else bw.Write(FileTableSizePacked);

		if (Bit64) bw.Write((long)FileTableSizeUnpacked);
		else bw.Write(FileTableSizeUnpacked);

		long Pos = bw.BaseStream.Position;

		bw.Write(buffer_packed);
		buffer_packed = null;
		#endregion

		#region file data
		int OffsetGlobal = (int)bw.BaseStream.Position + (Bit64 ? 8 : 4);
		if (Bit64) bw.Write((long)OffsetGlobal);
		else bw.Write(OffsetGlobal);

		foreach (FileTableEntry item in _files)
		{
			bw.Write(item.CompressedBuffer);
		}

		FileDataSizePacked = (int)(bw.BaseStream.Length - Pos);
		bw.BaseStream.Position = 17;
		bw.Write((long)FileDataSizePacked);
		#endregion


		#region final
		FileStream fileStream;

		try
		{
			fileStream = new(Path, FileMode.OpenOrCreate);
		}
		catch
		{
			fileStream = new(Path + ".tmp", FileMode.OpenOrCreate);
			Console.WriteLine("由于文件目前正在占用，已变更为创建临时文件。待退出游戏后将.tmp后缀删除即可。\n");
		}

		bw.BaseStream.Position = 0;
		bw.BaseStream.CopyTo(fileStream);
		bw.Dispose();
		bw = null;

		fileStream.Close();
		fileStream = null;

		Console.WriteLine("完成");
		#endregion
	}



	public void Dispose()
	{
		Magic = null;
		Signature = null;
		Unknown_001 = null;
		Unknown_002 = null;

		_files?.Clear();

		GC.SuppressFinalize(this);
	}
}