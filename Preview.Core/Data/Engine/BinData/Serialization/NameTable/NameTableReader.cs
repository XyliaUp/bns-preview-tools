using System.Text;

using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;
public interface INameTableReader
{
	NameTable ReadFrom(DatafileArchive reader);
}

public class NameTableReader : INameTableReader
{
    private static readonly Encoding KoreanEncoding
        = CodePagesEncodingProvider.Instance.GetEncoding(949);

    public DatafileArchive LazyLoadSource { get; init; }

    public NameTableReader(bool is64Bit)
    {
        _is64Bit = is64Bit;
    }

    private readonly bool _is64Bit;

    public NameTable ReadFrom(DatafileArchive reader)
    {
        if (LazyLoadSource != null)
            return LazyReadFrom(reader, _is64Bit);

        var table = new NameTable();
        table.RootEntry.Begin = reader.Read<uint>();
        table.RootEntry.End = reader.Read<uint>();

		var entryCount = reader.Read<int>();

		for (var i = 0; i < entryCount; i++)
		{
			table.Entries.Add(_is64Bit ? ReadEntry64(reader) : ReadEntry(reader));
		}

        var stringTableSize = reader.Read<uint>(); // Total size of string table
        var stringTable = reader.ReadBytes(stringTableSize);

        var memoryReader = new BinaryReader(new MemoryStream(stringTable), Encoding.ASCII);

        Span<byte> buffer = stackalloc byte[256];

        foreach (var entry in table.Entries)
        {
            memoryReader.BaseStream.Seek(entry.StringOffset, SeekOrigin.Begin);
            entry.String = ReadAliasString(memoryReader, buffer);
        }

        return table;
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

    private static NameTableEntry ReadEntry(DatafileArchive reader)
    {
        return new NameTableEntry
        {
            StringOffset = reader.Read<int>(),
            Begin = reader.Read<uint>(),
            End = reader.Read<uint>()
        };
    }

    private static NameTableEntry ReadEntry64(DatafileArchive reader)
    {
        return new NameTableEntry
        {
            StringOffset = reader.Read<long>(),
			Begin = reader.Read<uint>(),
            End = reader.Read<uint>()
        };
    }

    private NameTable LazyReadFrom(DatafileArchive reader, bool is64Bit)
    {
        var position = reader.Position;

        var globalStringTable = new LazyNameTable(new NameTableReader(is64Bit));
        globalStringTable.RootEntry.Begin = reader.Read<uint>();
        globalStringTable.RootEntry.End = reader.Read<uint>();

        var entryCount = reader.Read<int>();

        if (is64Bit) reader.Seek(entryCount * 16, SeekOrigin.Current);
		else reader.Seek(entryCount * 12, SeekOrigin.Current);


		var stringTableSize = reader.Read<int>();
        reader.Seek(stringTableSize, SeekOrigin.Current);     

        globalStringTable.Source = LazyLoadSource.OffsetedSource(position, reader.Position - position);

        return globalStringTable;
    }
}