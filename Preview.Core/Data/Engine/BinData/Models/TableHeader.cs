using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public class TableHeader
{
	public byte ElementCount { get; set; }
	public short Type { get; set; }
	public ushort MajorVersion { get; set; }
	public ushort MinorVersion { get; set; }
	public int Size { get; set; }
	public bool IsCompressed { get; set; }

	public void ReadHeaderFrom(DatafileArchive reader)
	{
		ElementCount = reader.Read<byte>();
		Type = reader.Read<short>();
		MajorVersion = reader.Read<ushort>();
		MinorVersion = reader.Read<ushort>();
		Size = reader.Read<int>();
		IsCompressed = reader.Read<bool>();
	}

	public void WriteHeaderTo(BinaryWriter writer)
	{
		writer.Write(ElementCount);
		writer.Write(Type);
		writer.Write(MajorVersion);
		writer.Write(MinorVersion);
	}
}