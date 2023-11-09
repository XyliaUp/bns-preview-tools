using System.Runtime.CompilerServices;
using System.Security.Cryptography;

using CUE4Parse.BNS.Pack;
using CUE4Parse.Compression;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Pak.Objects;
using CUE4Parse.UE4.Readers;
using CUE4Parse.UE4.VirtualFileSystem;
using CUE4Parse.Utils;

namespace CUE4Parse.BNS.Pak;
internal class MyFPakEntry : VfsEntry
{
	private const byte Flag_None = 0x00;
	private const byte Flag_Encrypted = 0x01;
	private const byte Flag_Deleted = 0x02;

	public readonly long CompressedSize;
	public readonly long UncompressedSize;
	public sealed override CompressionMethod CompressionMethod { get; }
	public readonly FPakCompressedBlock[] CompressionBlocks = Array.Empty<FPakCompressedBlock>();
	public readonly uint Flags;
	public override bool IsEncrypted => (Flags & Flag_Encrypted) == Flag_Encrypted;
	public bool IsDeleted => (Flags & Flag_Deleted) == Flag_Deleted;
	public readonly uint CompressionBlockSize;

	public bool IsCompressed => UncompressedSize != CompressedSize && CompressionBlockSize > 0;


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override byte[] Read() => Vfs.Extract(this);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override FArchive CreateReader() => new FByteArchive(Path, Read(), Vfs.Versions);




	public long Timestamp;
	public byte[] Data;
	public byte[][] CompressionBlocksData;
	public byte[] SHAHash;
	public new long Offset { get => base.Offset; set => base.Offset = value; }

	public MyFPakEntry(MyPakFileReader reader, string FilePath, string VfsPath, CompressionMethod Method = CompressionMethod.None, CompressedStatus Status = CompressedStatus.None) : base(null)
	{
		Path = VfsPath;
		Flags |= Flag_Encrypted;

		var FileBuffer = File.ReadAllBytes(FilePath);
		this.UncompressedSize = FileBuffer.Length;
		this.CompressionBlockSize = 0;     // 不压缩时为0，压缩时为文件自身大小或1MB（1024*1024）

		if (Method == CompressionMethod.None)
		{
			this.CompressedSize = FileBuffer.Length;
			this.Data = reader.EncryptIfEncrypted(FileBuffer, this.IsEncrypted);
			this.SHAHash = SHA1.HashData(this.Data.SubByteArray((int)this.UncompressedSize));
		}
		else
		{
			this.CompressedSize = 0;
			this.CompressionMethod = Method;
			this.SHAHash = SHA1.HashData(FileBuffer);

			if (Status == CompressedStatus.None) throw new ArgumentException("参数不能指定为None");
			else if (Status == CompressedStatus.AlreadyCompressed) throw new NotImplementedException("尚未支持");
			else
			{
				var CompressionBlocks = new List<KeyValuePair<FPakCompressedBlock, byte[]>>();
				this.CompressionBlockSize = Math.Min((uint)this.UncompressedSize, 1048576);

				#region 创建压缩区块
				var tempReader = new BinaryReader(new MemoryStream(FileBuffer));
				while (tempReader.BaseStream.Position < tempReader.BaseStream.Length)
				{
					var BlockLength = (int)Math.Min(this.CompressionBlockSize, tempReader.BaseStream.Length - tempReader.BaseStream.Position);
					var BlockData = reader.EncryptIfEncrypted(Compression.Compression2.Compress(tempReader.ReadBytes(BlockLength), BlockLength, Method, 9), this.IsEncrypted);

					//这是存储临时偏移
					CompressionBlocks.Add(new(new FPakCompressedBlock()
					{
						CompressedStart = this.CompressedSize,
						CompressedEnd = this.CompressedSize + BlockData.Length,
					}, BlockData));

					this.CompressedSize += BlockData.Length;
				}
				#endregion

				this.CompressionBlocks = CompressionBlocks.Select(o => o.Key).ToArray();
				this.CompressionBlocksData = CompressionBlocks.Select(o => o.Value).ToArray();
			}
		}

		FileBuffer = null;
	}

	public void WriteInfo(MyFPakInfo info, BinaryWriter writer, bool WriteOffset)
	{
		long startOffset = writer.BaseStream.Position;

		writer.Write(WriteOffset ? this.Offset : 0L);
		writer.Write(this.CompressedSize);
		writer.Write(this.UncompressedSize);

		#region CompressionMethod
		var CompressionMethod = this.IsCompressed ? this.CompressionMethod : Compression.CompressionMethod.None;
		if (info.Version < EPakFileVersion.PakFile_Version_FNameBasedCompressionMethod)
		{
			int CompressionMethodIndex = -1;
			if (info.CompressionMethods.Contains(CompressionMethod))
				CompressionMethodIndex = info.CompressionMethods.IndexOf(CompressionMethod);

			var LegacyCompressionMethod = CompressionMethodIndex switch
			{
				0 => ECompressionFlags.COMPRESS_None,
				1 => ECompressionFlags.COMPRESS_ZLIB,
				2 => ECompressionFlags.COMPRESS_GZIP,
				4 => (ECompressionFlags)259,

				_ => ECompressionFlags.COMPRESS_None,
			};
			writer.Write((int)LegacyCompressionMethod);
		}
		else if (info.Version == EPakFileVersion.PakFile_Version_FNameBasedCompressionMethod && !info.IsSubVersion)
		{
			if (!info.CompressionMethods.Contains(CompressionMethod))
				throw new InvalidDataException();

			writer.Write((byte)info.CompressionMethods.IndexOf(CompressionMethod));
		}
		else
		{
			if (!info.CompressionMethods.Contains(CompressionMethod))
				throw new InvalidDataException();

			writer.Write(info.CompressionMethods.IndexOf(CompressionMethod));
		}
		#endregion

		#region Timestamp & Hash
		Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
		if (info.Version < EPakFileVersion.PakFile_Version_NoTimestamps)
			writer.Write(this.Timestamp);
		writer.Write(this.SHAHash);
		#endregion

		if (info.Version >= EPakFileVersion.PakFile_Version_CompressionEncryption)
		{
			if (CompressionMethod != CompressionMethod.None)
			{
				writer.Write(this.CompressionBlocks.Length);
				foreach (var CompressionBlock in this.CompressionBlocks)
				{
					if (info.Version >= EPakFileVersion.PakFile_Version_RelativeChunkOffsets)
					{
						writer.Write(CompressionBlock.CompressedStart - Offset);
						writer.Write(CompressionBlock.CompressedEnd - Offset);
					}
				}
			}

			writer.Write((byte)Flags);
			writer.Write(CompressionBlockSize);
		}

		var StructSize = (int)(writer.BaseStream.Length - startOffset);
	}
}