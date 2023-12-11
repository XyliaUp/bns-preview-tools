using System.Runtime.InteropServices;

using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.Readers;
public class TableArchive
{
	private readonly RecordCompressedReader _recordCompressedReader;
	private readonly RecordUncompressedReader _recordUncompressedReader;
	private readonly bool _is64Bit;
	private DatafileArchive Source;

	public TableArchive(
		RecordCompressedReader recordCompressedReader = null,
		RecordUncompressedReader recordUncompressedReader = null,
		bool is64bit = false)
	{
		_recordUncompressedReader = recordUncompressedReader ?? new RecordUncompressedReader();
		_recordCompressedReader = recordCompressedReader ?? new RecordCompressedReader();
		_is64Bit = is64bit;
	}

	public static Table LazyLoad(DatafileArchive reader, bool is64Bit)
	{
		var table = new Table();
		var tableStart = reader.Position;
		table.ReadHeaderFrom(reader);
		table.Archive = new TableArchive(is64bit: is64Bit) { Source = reader.OffsetedSource(tableStart, table.Size + 11), };
		reader.Seek(table.Size - 1, SeekOrigin.Current);

		return table;
	}

	public Stream LazyStream() => Source.CreateStream();


	public void ReadFrom(Table table)
	{
		table.ReadHeaderFrom(Source);

		if (table.IsCompressed) ReadCompressed(Source, table);
		else ReadUncompressed(Source, table);
	}

	private unsafe void ReadCompressed(DatafileArchive reader, Table table)
	{
		var records = new List<Record>();

		if (!_recordCompressedReader.Initialize(reader, _is64Bit))
			throw new Exception("Failed to initialize compressed record reader");

		var rowMemory = new RecordMemory();

		while (_recordCompressedReader.Read(reader, ref rowMemory))
		{
			var row = new Record
			{
				Owner = table,
				Data = new byte[rowMemory.DataSize],
				StringLookup = new StringLookup { IsPerTable = false, Data = new byte[rowMemory.StringBufferSize] },
			};

			Marshal.Copy((IntPtr)rowMemory.DataBegin, row.Data, 0, rowMemory.DataSize);
			Marshal.Copy((IntPtr)rowMemory.StringBufferBegin, row.StringLookup.Data, 0, rowMemory.StringBufferSize);

			records.Add(row);
		}

		table.Records = records;
	}

	private unsafe void ReadUncompressed(DatafileArchive reader, Table table)
	{
		var records = new List<Record>();

		if (!_recordUncompressedReader.Initialize(reader,
			_is64Bit && !table.IsCompressed && table.ElementCount == 1))
			throw new Exception("Failed to initialize uncompressed record reader");

		var rowMemory = new RecordMemory();
		var stringLookup = new StringLookup { IsPerTable = true };

		while (_recordUncompressedReader.Read(reader, ref rowMemory))
		{
			if (rowMemory.DataSize == 6)
			{
				continue;
			}

			var row = new Record
			{
				Owner = table,
				Data = new byte[rowMemory.DataSize],
				StringLookup = stringLookup
			};

			Marshal.Copy((IntPtr)rowMemory.DataBegin, row.Data, 0, rowMemory.DataSize);
			records.Add(row);
		}

		table.RecordCountOffset = _recordUncompressedReader.GetRecordCountOffset();

		if (rowMemory.StringBufferBegin != null)
		{
			stringLookup.Data = new byte[rowMemory.StringBufferSize];
			Marshal.Copy((IntPtr)rowMemory.StringBufferBegin, stringLookup.Data, 0, rowMemory.StringBufferSize);
		}

		_recordUncompressedReader.GetPadding(out var padding);
		if (padding.Length > 0)
			table.Padding = padding.ToArray();

		table.Records = records;
	}
}