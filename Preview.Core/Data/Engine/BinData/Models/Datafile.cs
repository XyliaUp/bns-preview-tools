using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Serialization;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public abstract class Datafile
{
	public bool Is64Bit { get; set; }

	public byte DatafileVersion { get; set; } = 5;
	public BnsVersion ClientVersion { get; set; }
	public DateTimeOffset CreatedAt { get; set; }

	public long AliasCount { get; set; }
	public long AliasMapSize { get; set; }
	internal AliasTable AliasTable { get; set; }

	public TableCollection Tables { get; set; }



	#region	Serialize
	protected void ReadFrom(byte[] bytes, bool is64bit)
	{
		using var reader = new DataArchive(bytes, is64bit);

		var bin = new DatafileHeader();
		bin.ReadHeaderFrom(reader);

		if (bin.ReadTableCount > 10)
		{
			this.DatafileVersion = bin.DatafileVersion;
			this.ClientVersion = bin.ClientVersion;
			this.CreatedAt = bin.CreatedAt;
			this.AliasCount = bin.AliasCount;
			this.AliasMapSize = bin.AliasMapSize;
			this.AliasTable = AliasTableArchive.LazyLoad(reader);
		}

		// TotalTableSize = bytes.Length - reader.Position - bin.ReadTableCount * 4

		for (var tableId = 0; tableId < bin.ReadTableCount; tableId++)
		{
			this.Tables.Add(TableArchive.LazyLoad(reader));
		}
	}

	protected byte[] WriteTo(Table[] tables, bool is64bit)
	{
		using var writer = new DataArchiveWriter(is64bit);

		var datafileHeader = new DatafileHeader
		{
			Magic = "TADBOSLB",
			Reserved = new byte[54],
			CreatedAt = DateTime.Now,
			DatafileVersion = DatafileVersion,
			ClientVersion = ClientVersion,

			MaxBufferSize = 0x0,   //MaxBufferSize  好像等于 AliasMapSize  但是设置任何值游戏都没影响 
			TotalTableSize = 0x1,  //TotalTableSize 好像等于               必须 >0 但是无所谓值
		};

		var overwriteNameTableSize = datafileHeader.WriteHeaderTo(writer, tables.Length, this.AliasCount, is64bit);

		if (this.AliasTable == null)
			overwriteNameTableSize(this.AliasMapSize);

		if (tables.Length > 10)
		{
			if (this.AliasTable is not AliasTableArchive alias)
				throw new NullReferenceException("NameTable was null on main datafile");

			var oldPosition = writer.Position;
			AliasTableWriter.WriteTo(writer, alias, is64bit);

			var nameTableSize = writer.Position - oldPosition;
			this.AliasMapSize = nameTableSize;
			this.AliasCount = alias.Entries.Count;
			overwriteNameTableSize(nameTableSize);
		}


		var tableWriter = new TableWriter();
		foreach (var table in tables)
		{
			tableWriter.WriteTo(writer, table, is64bit);
		}

		writer.Flush();
		return writer.ToArray();
	}
	#endregion
}