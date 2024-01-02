using System.Text;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public class DatafileHeader
{    
	public string Magic { get; set; }    
	public byte DatafileVersion { get; set; }
	public BnsVersion ClientVersion { get; set; }
	public long TotalTableSize { get; set; }
	public long ReadTableCount { get; private set; }
	public long AliasMapSize { get; set; }
	public long AliasCount { get; set; }
	public long MaxBufferSize { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public byte[] Reserved { get; set; }


	internal void ReadHeaderFrom(DataArchive reader)
	{
		Magic = Encoding.ASCII.GetString(reader.ReadBytes(8));
		DatafileVersion = reader.Read<byte>();
		ClientVersion = reader.Read<BnsVersion>();
		TotalTableSize = reader.ReadLongInt();
		ReadTableCount = reader.ReadLongInt();
		AliasMapSize = reader.ReadLongInt();
		AliasCount = reader.ReadLongInt();
		MaxBufferSize = reader.ReadLongInt();
		CreatedAt = DateTimeOffset.FromUnixTimeSeconds(reader.Read<long>());
		Reserved = reader.ReadBytes(54);
	}

	internal Action<long> WriteHeaderTo(DataArchiveWriter writer, long tableCount, long aliasCount, bool is64Bit)
	{
		writer.Write(Encoding.ASCII.GetBytes(Magic));
		writer.Write(DatafileVersion);
		writer.Write(ClientVersion);

		Action<long> overwriteSize;

		if (is64Bit)
		{
			writer.Write(TotalTableSize);
			writer.Write(tableCount);
			var offset = writer.Position;
			overwriteSize = x =>
			{
				var oldPosition = writer.Position;
				writer.Seek(offset, SeekOrigin.Begin);
				writer.Write(x);
				writer.Seek(oldPosition, SeekOrigin.Begin);
			};
			writer.Write((long)0);
			writer.Write(aliasCount);
			writer.Write(MaxBufferSize);
		}
		else
		{
			writer.Write((int)TotalTableSize);
			writer.Write((int)tableCount);
			var offset = writer.Position;
			overwriteSize = x =>
			{
				var oldPosition = writer.Position;
				writer.Seek(offset, SeekOrigin.Begin);
				writer.Write((int)x);
				writer.Seek(oldPosition, SeekOrigin.Begin);
			};
			writer.Write(0);
			writer.Write((int)aliasCount);
			writer.Write((int)MaxBufferSize);
		}

		writer.Write(CreatedAt.ToUnixTimeSeconds());
		writer.Write(Reserved);
		return overwriteSize;
	}
}