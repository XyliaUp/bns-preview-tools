using System.Text;

namespace Xylia.Preview.Data.Engine.DatData;
public class FileTableEntry
{
	#region Fields
	public string FilePath;
	public byte Unknown_001;
	public byte Unknown_002;
	public bool IsCompressed;
	public bool IsEncrypted;

	public int FileDataOffset;        // (relative) offset
	public int FileDataSizeSheared;   // without padding for AES
	public int FileDataSizeStored;
	public int FileDataSizeUnpacked;

	public byte[] Padding;
	#endregion

	#region Constructor
	private BNSDat Owner { get; init; }

	internal FileTableEntry(BNSDat owner, BinaryReader reader, bool is64)
	{
		Owner = owner;

		int FilePathLength = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FilePath = Encoding.Unicode.GetString(reader.ReadBytes(FilePathLength * 2));

		Unknown_001 = reader.ReadByte();
		IsCompressed = reader.ReadByte() == 1;
		IsEncrypted = reader.ReadByte() == 1;
		Unknown_002 = reader.ReadByte();
		FileDataSizeUnpacked = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FileDataSizeSheared = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FileDataSizeStored = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FileDataOffset = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		Padding = reader.ReadBytes(60);
	}

	internal FileTableEntry(BNSDat owner, string path, byte[] data)
	{
		Owner = owner;

		FilePath = path;
		IsCompressed = true;
		IsEncrypted = true;
		Unknown_001 = 2;
		Unknown_002 = 0;
		Padding = new byte[60];
		Data = data;
	}
	#endregion


	#region DATA
	private byte[] _data;


	public byte[] CompressedBuffer;
	public byte[] Data
	{
		set => _data = value;
		get
		{
			if (CompressedBuffer != null)
			{
				byte[] UncompressedBuffer = BNSDat.Unpack(CompressedBuffer, FileDataSizeStored, FileDataSizeSheared, FileDataSizeUnpacked, IsEncrypted, IsCompressed, Owner.Params.AES_KEY);
				CompressedBuffer = null;

				if (FilePath.EndsWith(".xml") || FilePath.EndsWith(".x16"))
				{
					var Xml = new BXML_CONTENT(Owner.Params.XOR_KEY);
					Xml.Read(UncompressedBuffer);

					_data = Xml.ConvertToString();
					UncompressedBuffer = null;
				}
				else _data = UncompressedBuffer;
			}

			return _data;
		}
	}
	#endregion





	public void WriteHeader(BinaryWriter writer, bool Is64bit, CompressionLevel level, ref int FileDataOffset)
	{
		// Lazy
		if (CompressedBuffer is null)
		{
			var data = _data;
			if (FilePath.EndsWith(".xml") || FilePath.EndsWith(".x16"))
			{
				var bns_xml = new BXML_CONTENT(Owner.Params.XOR_KEY);
				bns_xml.ConvertFrom(data);
				data = bns_xml.Write();
			}

			FileDataSizeUnpacked = data.Length;
			CompressedBuffer = BNSDat.Pack(data, FileDataSizeUnpacked, out FileDataSizeSheared, out FileDataSizeStored, IsEncrypted, IsCompressed, level, Owner.Params.AES_KEY);
		}

		byte[] _filePath = Encoding.Unicode.GetBytes(FilePath);
		if (Is64bit) writer.Write((long)FilePath.Length);
		else writer.Write(FilePath.Length);
		writer.Write(_filePath);

		writer.Write(Unknown_001);
		writer.Write(IsCompressed);
		writer.Write(IsEncrypted);
		writer.Write(Unknown_002);

		if (Is64bit)
		{
			writer.Write((long)FileDataSizeUnpacked);
			writer.Write((long)FileDataSizeSheared);
			writer.Write((long)FileDataSizeStored);
			writer.Write((long)FileDataOffset);
		}
		else
		{
			writer.Write(FileDataSizeUnpacked);
			writer.Write(FileDataSizeSheared);
			writer.Write(FileDataSizeStored);
			writer.Write(FileDataOffset);
		}

		writer.Write(Padding);
		FileDataOffset += FileDataSizeStored;
	}
}