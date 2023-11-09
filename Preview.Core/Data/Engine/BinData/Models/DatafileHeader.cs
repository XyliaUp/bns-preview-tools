using System.Text;

using Xylia.Preview.Data.Engine.Readers;

using Version = Xylia.Preview.Data.Common.DataStruct.Version;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public class DatafileHeader
{
    public string Magic { get; set; }
    public byte DatafileVersion { get; set; }
    public Version ClientVersion { get; set; }
	public long TotalTableSize { get; set; }
    public long ReadTableCount { get; private set; }
    public long AliasMapSize { get; set; }
    public long AliasCount { get; set; }
    public long MaxBufferSize { get; set; }
    public DateTime CreatedAt { get; set; }
    public byte[] Reserved { get; set; }

    public void ReadHeaderFrom(DatafileArchive reader, bool is64Bit)
    {
        Magic = Encoding.ASCII.GetString(reader.ReadBytes(8));
        DatafileVersion = reader.Read<byte>();
		ClientVersion = reader.Read<Version>();

		if (is64Bit)
        {
            TotalTableSize = reader.Read<long>();
            ReadTableCount = reader.Read<long>();
			AliasMapSize = reader.Read<long>();
			AliasCount = reader.Read<long>();
			MaxBufferSize = reader.Read<long>();
		}
        else
        {
            TotalTableSize = reader.Read<int>();
			ReadTableCount = reader.Read<int>();
			AliasMapSize = reader.Read<int>();
			AliasCount = reader.Read<int>();
			MaxBufferSize = reader.Read<int>();
		}

        CreatedAt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(reader.Read<uint>());
        Reserved = reader.ReadBytes(58);
    }

    public Action<long> WriteHeaderTo(BinaryWriter writer, long tableCount, long aliasCount, bool is64Bit)
    {
        writer.Write(Encoding.ASCII.GetBytes(Magic));
        writer.Write(DatafileVersion);

        writer.Write(ClientVersion.Major);
		writer.Write(ClientVersion.Minor);
		writer.Write(ClientVersion.Build);
		writer.Write(ClientVersion.Revision);


        Action<long> overwriteSize;

        if (is64Bit)
        {
            writer.Write(TotalTableSize);
            writer.Write(tableCount);
            var offset = writer.BaseStream.Position;
            overwriteSize = x =>
            {
                var oldPosition = writer.BaseStream.Position;
                writer.BaseStream.Seek(offset, SeekOrigin.Begin);
                writer.Write(x);
                writer.BaseStream.Seek(oldPosition, SeekOrigin.Begin);
            };
            writer.Write((long)0);
            writer.Write(aliasCount);
            writer.Write(MaxBufferSize);
        }
        else
        {
            writer.Write((int)TotalTableSize);
            writer.Write((int)tableCount);
            var offset = writer.BaseStream.Position;
            overwriteSize = x =>
            {
                var oldPosition = writer.BaseStream.Position;
                writer.BaseStream.Seek(offset, SeekOrigin.Begin);
                writer.Write((int)x);
                writer.BaseStream.Seek(oldPosition, SeekOrigin.Begin);
            };
            writer.Write(0);
            writer.Write((int)aliasCount);
            writer.Write((int)MaxBufferSize);
        }

        writer.Write((int)(CreatedAt - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
        writer.Write(Reserved);
        return overwriteSize;
    }
}