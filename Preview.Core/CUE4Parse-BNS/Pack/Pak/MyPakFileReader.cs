using System.Runtime.CompilerServices;
using System.Security.Cryptography;

using CUE4Parse.BNS.Pack;
using CUE4Parse.Compression;
using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider.Objects;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Pak.Objects;
using CUE4Parse.UE4.Readers;
using CUE4Parse.UE4.Versions;
using CUE4Parse.UE4.VirtualFileSystem;
using CUE4Parse.Utils;

namespace CUE4Parse.BNS.Pak;
public class MyPakFileReader : AbstractAesVfsReader
{
	public readonly FArchive Ar;

	public MyFPakInfo Info;

	public override string MountPoint { get; protected set; }

	public sealed override long Length { get; set; }

	public override bool HasDirectoryIndex => true;

	public override FGuid EncryptionKeyGuid => Info.EncryptionKeyGuid;

	public override bool IsEncrypted => Info.EncryptedIndex;


	public override byte[] Extract(VfsEntry entry) => throw new NotImplementedException();

	public override IReadOnlyDictionary<string, GameFile> Mount(bool caseInsensitive = false) => throw new NotImplementedException();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected override byte[] ReadAndDecrypt(int length) => ReadAndDecrypt(length, Ar, IsEncrypted);

	public override byte[] MountPointCheckBytes()
	{
		Ar.Position = Info.IndexOffset;
		return Ar.ReadBytes((4 + MAX_MOUNTPOINT_TEST_LENGTH * 2).Align(CUE4Parse.Encryption.Aes.Aes.ALIGN));
	}

	public override void Dispose()
	{
		Files = null;
		Ar?.Dispose();
	}







	public MyPakFileReader(string MountPoint) : base("", new() { Game = EGame.GAME_BladeAndSoul })
	{
		this.AesKey = new FAesKey(GameFileProvider._aesKey);
		this.Info = new MyFPakInfo();
		this.MountPoint = MountPoint;
	}


	public void Add(string filePath, string VfsPath = null, CompressionMethod Method = CompressionMethod.None, CompressedStatus Status = CompressedStatus.None)
	{
		//实际上 VfsPath 是完整路径（MountPoint + FileName）
		//由于输出时有一个写 FileName 的操作，因此这里直接传递 FileName 部分
		VfsPath ??= System.IO.Path.GetFileName(filePath);
		VfsPath = VfsPath.Replace("\\", "/");


		if (Method != CompressionMethod.None && Status == CompressedStatus.None)
			Status = CompressedStatus.IsCompressed;

		var Files = (Dictionary<string, GameFile>)this.Files;
		Files.Add(filePath, new MyFPakEntry(this, filePath, VfsPath, Method, Status));
	}

	public void Write(BinaryWriter writer)
	{
		#region init
		this.AesKey ??= new FAesKey(GameFileProvider._aesKey);
		if (!this.Files.Any()) throw new ArgumentNullException(nameof(Files), "至少需要一个文件");

		// MountPoint
		if ((MountPoint.IsUnicode() && (MountPoint.Length + 1) * 2 > MAX_MOUNTPOINT_TEST_LENGTH) ||
		   (!MountPoint.IsUnicode() && ((MountPoint.Length + 1) > MAX_MOUNTPOINT_TEST_LENGTH)))
			throw new ArgumentOutOfRangeException(nameof(MountPoint), "挂载点过长");

		MountPoint = MountPoint.Replace("\\", "/");
		if (!MountPoint.EndsWith("/")) MountPoint += "/";

		// Version.Compression
		if (this.Info.Version >= EPakFileVersion.PakFile_Version_FNameBasedCompressionMethod)
		{
			this.Info.CompressionMethods = Files.Select(f => f.Value.CompressionMethod).Distinct().ToList();
			this.Info.CompressionMethods.Insert(0, CompressionMethod.None);
		}
		#endregion

		#region File Data Section
		long offset = 0;
		foreach (var file in Files.Values.Cast<MyFPakEntry>())
		{
			file.Offset = offset;

			#region StructSize
			var tempWriter = new BinaryWriter(new MemoryStream());
			file.WriteInfo(Info, tempWriter, false);

			var StructSize = (int)tempWriter.BaseStream.Length;
			for (int i = 0; i < file.CompressionBlocks.Length; i++)
			{
				file.CompressionBlocks[i].CompressedStart += StructSize;
				file.CompressionBlocks[i].CompressedEnd += StructSize;
			}
			#endregion


			file.WriteInfo(Info, writer, false);

			if (!file.IsCompressed) writer.Write(file.Data);
			else Array.ForEach(file.CompressionBlocksData, block => writer.Write(block));

			offset = writer.BaseStream.Position;
		}
		#endregion

		#region File Index Section
		this.Info.IndexOffset = writer.BaseStream.Position;

		using var stream = new MemoryStream();
		var IndexWriter = new BinaryWriter(stream);

		IndexWriter.WriteFString("../../../" + this.MountPoint);
		IndexWriter.Write(this.Files.Count);

		foreach (var file in Files.Values.Cast<MyFPakEntry>())
		{
			IndexWriter.WriteFString(file.Path);
			file.WriteInfo(this.Info, IndexWriter, true);
		}

		byte[] temp = stream.GetBuffer();
		var IndexData = this.EncryptIfEncrypted(temp);
		writer.Write(IndexData);

		this.Info.IndexSize = IndexData.Length;
		this.Info.IndexHash = SHA1.HashData(temp);

		temp = null;
		IndexData = null;
		#endregion

		this.Info.Write(writer);

		writer.Flush();
		writer.Close();
		writer.Dispose();
	}



	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public byte[] EncryptIfEncrypted(byte[] bytes) => EncryptIfEncrypted(bytes, IsEncrypted);

	public byte[] EncryptIfEncrypted(byte[] bytes, bool isEncrypted)
	{
		if (!isEncrypted) return bytes;
		if (AesKey != null)   //序列化时不需要校验密钥
		{
			AesEncrypt.Align(ref bytes);
			return bytes.Encrypt(AesKey);
		}

		throw new InvalidAesKeyException("Encrypt data requires a valid aes key");
	}
}