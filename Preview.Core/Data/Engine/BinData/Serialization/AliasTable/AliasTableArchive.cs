using System.Text;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;
internal class AliasTableArchive : AliasTable
{
	private Dictionary<string, Ref> _table;

    public override Dictionary<string, Ref> Table
    {
        get
        {
            if (_table is null)
            {
                _table = [];

				// load data
				Read(this);
				CreateNode(RootEntry, string.Empty);
            }

            return _table;
        }
    }

	#region Read
	private static readonly Encoding KoreanEncoding = CodePagesEncodingProvider.Instance.GetEncoding(949);

	public static AliasTable LazyLoad(DataArchive reader)
	{
		var position = reader.Position;

		var globalStringTable = new AliasTableArchive();
		globalStringTable.RootEntry.Begin = reader.Read<uint>();
		globalStringTable.RootEntry.End = reader.Read<uint>();

		var entryCount = reader.Read<int>();

		if (reader.Is64Bit) reader.Seek(entryCount * 16, SeekOrigin.Current);
		else reader.Seek(entryCount * 12, SeekOrigin.Current);


		var stringTableSize = reader.Read<int>();
		reader.Seek(stringTableSize, SeekOrigin.Current);

		globalStringTable.Source = reader.OffsetedSource(position, reader.Position - position);

		return globalStringTable;
	}

	public static void Read(AliasTableArchive table)
	{
		var reader = table.Source;
		table.RootEntry.Begin = reader.Read<uint>();
		table.RootEntry.End = reader.Read<uint>();

		var entryCount = reader.Read<int>();
		table.Entries = new List<AliasTableArchiveEntry>(entryCount);

		for (var i = 0; i < entryCount; i++)
		{
			table.Entries.Add(reader.Is64Bit ? ReadEntry64(reader) : ReadEntry(reader));
		}

		var stringTableSize = reader.Read<int>(); // Total size of string table
		var stringTable = reader.ReadBytes(stringTableSize);

		var memoryReader = new BinaryReader(new MemoryStream(stringTable), Encoding.ASCII);

		Span<byte> buffer = stackalloc byte[256];

		foreach (var entry in table.Entries)
		{
			memoryReader.BaseStream.Seek(entry.StringOffset, SeekOrigin.Begin);
			entry.String = ReadAliasString(memoryReader, buffer);
		}
	}

	private static string ReadAliasString(BinaryReader reader, Span<byte> buffer)
	{
		var position = reader.BaseStream.Position;
		var size = 0;

		while (true)
		{
			if (reader.ReadByte() == 0)
				break;

			size++;
		}

		buffer = buffer[..size];
		reader.BaseStream.Seek(position, SeekOrigin.Begin);
		reader.Read(buffer);
		reader.ReadByte();

		return KoreanEncoding.GetString(buffer);
	}

	private static AliasTableArchiveEntry ReadEntry(DataArchive reader)
	{
		return new AliasTableArchiveEntry
		{
			StringOffset = reader.Read<int>(),
			Begin = reader.Read<uint>(),
			End = reader.Read<uint>()
		};
	}

	private static AliasTableArchiveEntry ReadEntry64(DataArchive reader)
	{
		return new AliasTableArchiveEntry
		{
			StringOffset = reader.Read<long>(),
			Begin = reader.Read<uint>(),
			End = reader.Read<uint>()
		};
	}
	#endregion

	#region Methods
	public DataArchive Source { get; internal set; }

	public AliasTableArchiveEntry RootEntry { get; } = new AliasTableArchiveEntry();

	public List<AliasTableArchiveEntry> Entries { get; internal set; }

	private void CreateNode(AliasTableArchiveEntry entry, string path)
	{
		path += entry.String;

		if (entry.IsLeaf)
		{
			for (uint i = entry.Begin >> 1; i <= entry.End; i++)
				CreateNode(Entries[(int)i], path);
		}
		else
		{
			Add(entry.ToRef(), path);
		}
	}
	#endregion
}