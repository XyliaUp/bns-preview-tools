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
	public FileTableEntry()
	{

	}

	public FileTableEntry(BinaryReader reader, bool is64, int OffsetGlobal = 0)
	{
		int FilePathLength = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FilePath = Encoding.Unicode.GetString(reader.ReadBytes(FilePathLength * 2));

		Unknown_001 = reader.ReadByte();
		IsCompressed = reader.ReadByte() == 1;
		IsEncrypted = reader.ReadByte() == 1;
		Unknown_002 = reader.ReadByte();
		FileDataSizeUnpacked = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FileDataSizeSheared = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FileDataSizeStored = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
		FileDataOffset = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32()) + OffsetGlobal;
		Padding = reader.ReadBytes(60);
	}
	#endregion


	#region DATA
	internal KeyInfo KeyInfo;
	private byte[] _data;


	public byte[] CompressedBuffer;
	public byte[] Data
	{
		set => _data = value;
		get
		{
			if (CompressedBuffer != null)
			{
				byte[] UncompressedBuffer = BNSDat.Unpack(CompressedBuffer, FileDataSizeStored, FileDataSizeSheared, FileDataSizeUnpacked, IsEncrypted, IsCompressed, KeyInfo);
				CompressedBuffer = null;

				if (FilePath.EndsWith(".xml") || FilePath.EndsWith(".x16"))
				{
					var Xml = new BXML_CONTENT(KeyInfo.XOR_KEY);
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
}