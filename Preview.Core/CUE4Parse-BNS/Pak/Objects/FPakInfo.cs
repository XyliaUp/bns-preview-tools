using CUE4Parse.Compression;
using CUE4Parse.UE4.Objects.Core.Misc;

using Xylia.Preview.Common.Extension;

namespace CUE4Parse.UE4.Pak.Objects;
public class MyFPakInfo	
{
	public uint Magic;
	public EPakFileVersion Version;
	public bool IsSubVersion;
	public long IndexOffset;
	public long IndexSize;
	public byte[] IndexHash;
	// When new fields are added to FPakInfo, they're serialized before 'Magic' to keep compatibility
	// with older pak file versions. At the same time, structure size grows.
	public bool EncryptedIndex;
	public bool IndexIsFrozen;
	public FGuid EncryptionKeyGuid;
	public List<CompressionMethod> CompressionMethods;

	public MyFPakInfo()
	{
		this.EncryptionKeyGuid = new FGuid();
		this.EncryptedIndex = true;
		this.Version = EPakFileVersion.PakFile_Version_FrozenIndex;
		this.Magic = FPakInfo.PAK_FILE_MAGIC;
	}

	public void Write(BinaryWriter writer)
	{
		// New FPakInfo fields.
		writer.Write(this.EncryptionKeyGuid);

		// Do not replace by ReadFlag
		writer.Write(this.EncryptedIndex);

		// Old FPakInfo fields
		writer.Write(this.Magic);
		writer.Write((int)this.Version);

		writer.Write(this.IndexOffset);
		writer.Write(this.IndexSize);
		writer.Write(this.IndexHash);


		if (Version == EPakFileVersion.PakFile_Version_FrozenIndex)
		{
			var bIndexIsFrozen = false;
			writer.Write(bIndexIsFrozen);
		}

		if (Version < EPakFileVersion.PakFile_Version_FNameBasedCompressionMethod)
		{
			CompressionMethods = new List<CompressionMethod>
			{
				CompressionMethod.None, CompressionMethod.Zlib, CompressionMethod.Gzip, CompressionMethod.Oodle, CompressionMethod.LZ4, CompressionMethod.Zstd
			};
		}
		else
		{
			var maxNumCompressionMethods = 5;
			var bufferSize = FPakInfo.COMPRESSION_METHOD_NAME_LEN * maxNumCompressionMethods;

			var offset = writer.BaseStream.Position;
			foreach (var method in this.CompressionMethods.Where(o => o != CompressionMethod.None))
				writer.Write(System.Text.Encoding.ASCII.GetBytes(method.ToString()));

			writer.Write(new byte[bufferSize - (writer.BaseStream.Position - offset)]);
		}
	}
}