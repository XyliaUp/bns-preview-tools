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
		var data = File.ReadAllBytes(Params.PackagePath);
		using var archive = new DataArchive(data, Bit64);
		Magic = archive.ReadBytes(8);
		Version = archive.Read<uint>();
		Unknown_001 = archive.ReadBytes(5);
		var FileDataSizePacked = archive.ReadLongInt();
		var FileCount = archive.ReadLongInt();
		IsCompressed = archive.Read<bool>();
		IsEncrypted = archive.Read<bool>();

		// Update 200429																
		if (Version == 3)
		{
			var signature = archive.ReadBytes(128);

			var rsa = new RSACryptoServiceProvider();
			rsa.ImportParameters(Params.RSA_KEY);
			rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
			var x = rsa.Decrypt(signature, false);
		}

		Unknown_002 = archive.ReadBytes(62);

		var FileTableSizePacked = archive.ReadLongInt();
		var FileTableSizeUnpacked = archive.ReadLongInt();
		var FileTablePacked = archive.ReadBytes((int)FileTableSizePacked);

		// not trust value, read the current position
		var OffsetGlobal = archive.ReadLongInt();
		OffsetGlobal = archive.Position;
		#endregion

		#region files
		var archive2 = new DataArchive(Unpack(FileTablePacked, FileTableSizePacked, FileTableSizePacked, FileTableSizeUnpacked, IsEncrypted, IsCompressed, Params.AES_KEY), Bit64);

		var files = new FileTableEntry[FileCount];
		for (int i = 0; i < FileCount; i++)
		{
			var item = files[i] = new FileTableEntry(this, archive2);
			item.DataArchive = archive.OffsetedSource(OffsetGlobal + item.FileDataOffset, item.FileDataSizeStored);
		}

		this._files = [.. files];
		#endregion
	}

	public void Write(bool Is64bit, CompressionLevel level)
	{
		#region head
		using var writer = new DataArchiveWriter(Is64bit);
		writer.Write(Magic);
		writer.Write(Version);
		writer.Write(Unknown_001);

		long FileDataSizePacked = 0;
		writer.WriteLongInt(FileDataSizePacked);  // size
		writer.WriteLongInt(_files.Count);        // count
		writer.Write(IsCompressed);
		writer.Write(IsEncrypted);

		// Signature
		long pos = writer.Position;
		if (Version == 3) writer.Write(new byte[128]);

		writer.Write(Unknown_002);
		#endregion

		#region file summary
		long FileDataOffset = 0;
		using MemoryStream ms = new();
		using BinaryWriter mosTable = new(ms);
		foreach (FileTableEntry item in _files)
		{
			item.WriteHeader(mosTable, Is64bit, level, ref FileDataOffset);
		}

		long FileTableSizeUnpacked = mosTable.BaseStream.Length;
		var FileTablePacked = Pack(ms.ToArray(), FileTableSizeUnpacked, out var FileTableSizeSheared, out var FileTableSizePacked, IsEncrypted, IsCompressed, level, Params.AES_KEY);
		writer.WriteLongInt(FileTableSizePacked);
		writer.WriteLongInt(FileTableSizeUnpacked);
		writer.Write(FileTablePacked);
		#endregion

		#region file data
		var OffsetGlobal = writer.Position + (Is64bit ? 8 : 4);
		writer.WriteLongInt(OffsetGlobal);

		foreach (FileTableEntry item in _files)
		{
			using var stream = item.DataArchive.CreateStream();
			stream.CopyTo(writer);
		}

		writer.Position = pos;
		var rsa = new RSACryptoServiceProvider();
		rsa.ImportParameters(Params.RSA_KEY);
		writer.Write(rsa.Encrypt(SHA256.HashData(writer.ToArray()), false));

		writer.Position = 17;
		writer.WriteLongInt(writer.Length);
		#endregion


		#region final
		using var fileStream = new FileStream(Params.PackagePath, FileMode.Create);
		writer.Seek(0, SeekOrigin.Begin);
		writer.CopyTo(fileStream);
		writer.Dispose();

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
	public static byte[] Decrypt(byte[] buffer, long size, byte[] AES)
	{
		var aes = Aes.Create();
		aes.Mode = CipherMode.ECB;
		aes.Padding = PaddingMode.None;

		return aes.CreateDecryptor(AES, null).TransformFinalBlock(buffer, 0, buffer.Length);
	}

	public static byte[] Encrypt(byte[] buffer, long size, out long sizePadded, byte[] AESKey)
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
		encrypt.TransformBlock(temp, 0, (int)sizePadded, output, 0);

		temp = null;
		return output;
	}

	public static byte[] Inflate(byte[] buffer, long sizeDecompressed, CompressionLevel compression)
	{
		if (sizeDecompressed == 0)
			sizeDecompressed = buffer.Length;

		var level = (Ionic.Zlib.CompressionLevel)((byte)compression * 3);
		var output = new MemoryStream();
		var zs = new ZlibStream(output, CompressionMode.Compress, level, true);
		zs.Write(buffer, 0, (int)sizeDecompressed);
		zs.Flush();
		zs.Close();

		return output.ToArray();
	}

	public static byte[] Unpack(byte[] buffer, long sizeStored, long sizeSheared, long sizeUnpacked, bool isEncrypted, bool isCompressed, byte[] key)
	{
		var output = buffer;
		if (isEncrypted) output = Decrypt(buffer, sizeStored, key);

		var uncompressedBuffer = new byte[sizeUnpacked];
		Compression.Decompress(output, uncompressedBuffer, isCompressed ? CompressionMethod.Zlib : CompressionMethod.None);
		return uncompressedBuffer;
	}

	public static byte[] Pack(byte[] buffer, long sizeUnpacked, out long sizeSheared, out long sizeStored, bool encrypt, bool compress, CompressionLevel compressionLevel, byte[] key)
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