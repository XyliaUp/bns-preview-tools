using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public abstract class Datafile
{
	public bool Is64Bit { get; set; }

	public byte DatafileVersion { get; set; } = 5;
	public BnsVersion ClientVersion { get; set; }
	public DateTime CreatedAt { get; set; }

	public long AliasCount { get; set; }
	public long AliasMapSize { get; set; }

	public NameTable NameTable { get; set; }

	public TableCollection Tables { get; set; }



	#region	Serialize
	protected void ReadFrom(byte[] bytes, bool is64bit)
	{
		using var reader = new DatafileArchive(bytes);

		var bin = new DatafileHeader();
		bin.ReadHeaderFrom(reader, is64bit);

		if (bin.ReadTableCount > 10)
		{
			this.DatafileVersion = bin.DatafileVersion;
			this.ClientVersion = bin.ClientVersion;
			this.CreatedAt = bin.CreatedAt;
			this.AliasCount = bin.AliasCount;
			this.AliasMapSize = bin.AliasMapSize;
			this.NameTable = new NameTableReader(is64bit).ReadFrom(reader);
		}

		// TotalTableSize = bytes.Length - reader.Position - bin.ReadTableCount * 4

		for (var tableId = 0; tableId < bin.ReadTableCount; tableId++)
		{
			this.Tables.Add(TableArchive.LazyLoad(reader, is64bit));
		}
	}

	protected byte[] WriteTo(Table[] tables, bool is64bit)
	{
		using var memoryStream = new MemoryStream();
		using var writer = new BinaryWriter(memoryStream);

		var datafileHeader = new DatafileHeader
		{
			Magic = "TADBOSLB",
			Reserved = new byte[58],
			CreatedAt = DateTime.Now,
			DatafileVersion = DatafileVersion,
			ClientVersion = ClientVersion,

			MaxBufferSize = 0x0,   //MaxBufferSize  好像等于 AliasMapSize  但是设置任何值游戏都没影响 
			TotalTableSize = 0x1,  //TotalTableSize 好像等于               必须 >0 但是无所谓值
		};

		var overwriteNameTableSize = datafileHeader.WriteHeaderTo(writer, tables.Length,
			NameTable?.Entries.Count ?? this.AliasCount, is64bit);

		if (this.NameTable == null)
			overwriteNameTableSize(this.AliasMapSize);

		if (tables.Length > 10)
		{
			if (this.NameTable == null)
				throw new NullReferenceException("NameTable was null on main datafile");

			var oldPosition = writer.BaseStream.Position;
			var _nameTableWriter = new NameTableWriter();
			_nameTableWriter.WriteTo(writer, this.NameTable, is64bit);

			var nameTableSize = writer.BaseStream.Position - oldPosition;
			this.AliasMapSize = nameTableSize;
			this.AliasCount = this.NameTable.Entries.Count;
			overwriteNameTableSize(nameTableSize);
		}


		var tableWriter = new TableWriter();
		foreach (var table in tables)
		{
			tableWriter.WriteTo(writer, table, is64bit);
		}

		writer.Flush();
		return memoryStream.ToArray();
	}
	#endregion
}