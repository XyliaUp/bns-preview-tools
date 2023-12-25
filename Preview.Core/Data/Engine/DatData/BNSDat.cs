using System.Security.Cryptography;
using System.Text.RegularExpressions;
using CUE4Parse.Compression;
using Ionic.Zlib;

namespace Xylia.Preview.Data.Engine.DatData;
public sealed class BNSDat(PackageParam Params) : IDisposable
{
	#region DatInfo
	internal PackageParam Params { private set; get; } = Params;
	public bool Bit64 => Params.Bit64;
	public string Path => Params.PackagePath;



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
	#endregion


	#region Methods
	private void Read()
	{
		#region head
		using var stream = new FileStream(Params.PackagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		using var br = new BinaryReader(stream);

		Magic = br.ReadBytes(8);
		Version = br.ReadUInt32();
		Unknown_001 = br.ReadBytes(5);
		var FileDataSizePacked = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		var FileCount = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		IsCompressed = br.ReadBoolean();
		IsEncrypted = br.ReadBoolean();

		// Update 200429
		if (Version == 3) Signature = br.ReadBytes(128);

		Unknown_002 = br.ReadBytes(62);

		var FileTableSizePacked = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		var FileTableSizeUnpacked = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		var FileTablePacked = br.ReadBytes(FileTableSizePacked);

		// not trust value, read the current position
		var OffsetGlobal = Bit64 ? (int)br.ReadInt64() : br.ReadInt32();
		OffsetGlobal = (int)br.BaseStream.Position;


		var rsa = new RSACryptoServiceProvider();
		rsa.ImportParameters(Params.RSA_KEY);
		var x = rsa.VerifyData(FileTablePacked, Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
		#endregion

		#region files
		byte[] FileTableUnpacked = Unpack(FileTablePacked, FileTableSizePacked, FileTableSizePacked, FileTableSizeUnpacked, IsEncrypted, IsCompressed, Params.AES_KEY);
		FileTablePacked = null;

		using BinaryReader br2 = new(new MemoryStream(FileTableUnpacked));
		FileTableUnpacked = null;

		//file info
		_files = new List<FileTableEntry>(FileCount);
		for (int i = 0; i < FileCount; i++)
			_files.Add(new FileTableEntry(this, br2, Bit64));

		Parallel.ForEach(_files, item =>
		{
			lock (br)
			{
				br.BaseStream.Position = OffsetGlobal +  item.FileDataOffset;
				item.CompressedBuffer = br.ReadBytes(item.FileDataSizeStored);
			}
		});
		#endregion
	}

	public void Write(bool Is64bit, CompressionLevel level)
	{
		#region head
		using BinaryWriter bw = new(new MemoryStream());

		bw.Write(Magic);
		bw.Write(Version);
		bw.Write(Unknown_001);

		// size
		int FileDataSizePacked = 0;
		if (Is64bit) bw.Write((long)FileDataSizePacked);
		else bw.Write(FileDataSizePacked);

		// count
		if (Is64bit) bw.Write((long)_files.Count);
		else bw.Write(_files.Count);

		bw.Write(IsCompressed);
		bw.Write(IsEncrypted);

		// Signature
		long pos = bw.BaseStream.Position;
		if (Version == 3) bw.Write(Signature);

		bw.Write(Unknown_002);
		#endregion


		#region file summary
		int FileDataOffset = 0;
		MemoryStream ms = new();
		BinaryWriter mosTable = new(ms);
		foreach (FileTableEntry item in _files)
		{
			item.WriteHeader(mosTable, Is64bit, level, ref FileDataOffset);
		}

		int FileTableSizeUnpacked = (int)mosTable.BaseStream.Length;
		int FileTableSizeSheared = FileTableSizeUnpacked;
		int FileTableSizePacked = FileTableSizeUnpacked;

		var buffer_unpacked = ms.ToArray();
		mosTable.Dispose();
		mosTable = null;

		var buffer_packed = Pack(buffer_unpacked, FileTableSizeUnpacked, out FileTableSizeSheared, out FileTableSizePacked, IsEncrypted, IsCompressed, level, Params.AES_KEY);
		buffer_unpacked = null;

		if (Is64bit) bw.Write((long)FileTableSizePacked);
		else bw.Write(FileTableSizePacked);

		if (Is64bit) bw.Write((long)FileTableSizeUnpacked);
		else bw.Write(FileTableSizeUnpacked);

		long Pos = bw.BaseStream.Position;
		bw.Write(buffer_packed);
		//buffer_packed = null;
		#endregion

		#region file data
		int OffsetGlobal = (int)bw.BaseStream.Position + (Is64bit ? 8 : 4);
		if (Is64bit) bw.Write((long)OffsetGlobal);
		else bw.Write(OffsetGlobal);

		foreach (FileTableEntry item in _files)
		{
			bw.Write(item.CompressedBuffer);
		}


		FileDataSizePacked = (int)(bw.BaseStream.Length - Pos);
		bw.BaseStream.Position = 17;

		if (Is64bit) bw.Write((long)FileDataSizePacked);
		else bw.Write(FileDataSizePacked);

		bw.BaseStream.Position = pos;
		var rsa = new RSACryptoServiceProvider();
		rsa.ImportParameters(Params.RSA_KEY);
		bw.Write(Signature = rsa.SignData(buffer_packed, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
		#endregion


		#region final
		using var fileStream = new FileStream(Params.PackagePath, FileMode.Create);
		bw.BaseStream.Seek(0, SeekOrigin.Begin);
		bw.BaseStream.CopyTo(fileStream);
		bw.Dispose();

		fileStream.Close();
		#endregion
	}


	public IEnumerable<FileTableEntry> EnumerateFiles(string searchPattern)
	{
		var regex = new Regex(searchPattern
			.Replace("/", "\\")
			.Replace("\\", "\\\\")
			.Replace("*", ".*?"), RegexOptions.IgnoreCase);

		return FileTable.Where(f => regex.Match(f.FilePath).Success);
	}

	public void Add(string path, byte[] data)
	{
		// remove
		FileTable.RemoveAll(f => f.FilePath.Equals(path, StringComparison.OrdinalIgnoreCase));

		// instance
		var file = new FileTableEntry(this, path, data);
		FileTable.Add(file);
	}

	public void Dispose()
	{
		_files?.Clear();

		GC.SuppressFinalize(this);
	}
	#endregion


	#region Methods
	public static byte[] Decrypt(byte[] buffer, int size, byte[] AES)
	{
		var aes = Aes.Create();
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

		var aes = Aes.Create();
		aes.Mode = CipherMode.ECB;

		ICryptoTransform encrypt = aes.CreateEncryptor(AESKey, new byte[16]);
		encrypt.TransformBlock(temp, 0, sizePadded, output, 0);

		temp = null;
		return output;
	}

	public static byte[] Inflate(byte[] buffer, int sizeDecompressed, CompressionLevel compression)
	{
		if (sizeDecompressed == 0)
			sizeDecompressed = buffer.Length;

		var level = (Ionic.Zlib.CompressionLevel)((byte)compression * 3);
		var output = new MemoryStream();
		var zs = new ZlibStream(output, CompressionMode.Compress, level, true);
		zs.Write(buffer, 0, sizeDecompressed);
		zs.Flush();
		zs.Close();

		return output.ToArray();
	}

	public static byte[] Unpack(byte[] buffer, int sizeStored, int sizeSheared, int sizeUnpacked, bool isEncrypted, bool isCompressed, byte[] key)
	{
		var output = buffer;
		if (isEncrypted) output = Decrypt(buffer, sizeStored, key);

		var uncompressedBuffer = new byte[sizeUnpacked];
		Compression.Decompress(output, uncompressedBuffer, isCompressed ? CompressionMethod.Zlib : CompressionMethod.None);
		return uncompressedBuffer;
	}

	public static byte[] Pack(byte[] buffer, int sizeUnpacked, out int sizeSheared, out int sizeStored, bool encrypt, bool compress, CompressionLevel compressionLevel, byte[] key)
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
			output = Encrypt(output, output.Length, out sizeStored, key);
		}

		return output;
	}



	public static void CreateFromDirectory(PackageParam param)
	{
		var package = new BNSDat(param);
		package.Version = 3;
		package.Unknown_001 = [0, 0, 0, 0, 0];
		package.IsCompressed = true;
		package.IsEncrypted = true;
		package.Signature = new byte[128];
		package.Unknown_002 = new byte[62];
		package.FileTable = Directory.EnumerateFiles(param.FolderPath, "*", SearchOption.AllDirectories).Select(x => new FileTableEntry(package,
			x.Replace(param.FolderPath, "").TrimStart('\\'),
			File.ReadAllBytes(x)))
			.ToList();

		// write
		package.Write(param.Bit64, param.CompressionLevel);
	}

	public static implicit operator BNSDat(FileInfo file)
	{
		if (!file.Exists) return null;

		var param = new PackageParam(file.FullName);
		return new BNSDat(param);
	}
	#endregion
}