using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.BinData.Serialization;

namespace Xylia.Preview.Data.Engine.Readers;
public class TableWriter
{
	public static int GlobalCompressionBlockSize = ushort.MaxValue;
	private readonly RecordCompressedWriter _recordCompressedWriter;
	private readonly RecordUncompressedWriter _recordUncompressedWriter;

	public TableWriter(int compressionBlockSize = -1)
	{
		if (compressionBlockSize == -1)
			compressionBlockSize = GlobalCompressionBlockSize;

		_recordCompressedWriter = new RecordCompressedWriter(compressionBlockSize);
		_recordUncompressedWriter = new RecordUncompressedWriter();
	}


	public void WriteTo(BinaryWriter writer, Table table, bool is64Bit)
	{
		writer.Write(table.ElementCount);
		writer.Write(table.Type);
		writer.Write(table.MajorVersion);
		writer.Write(table.MinorVersion);

		if (table.IsCompressed) WriteCompressed(writer, table);
		else WriteLoose(writer, table, is64Bit);
	}

	private void WriteCompressed(BinaryWriter writer, Table table)
	{
		var recordCompressedWriter = _recordCompressedWriter;
		recordCompressedWriter.BeginWrite(writer);

		foreach (var record in table.Records.OrderBy(x => x.RecordVariationId).ThenBy(x => x.RecordId))
		{
			recordCompressedWriter.WriteRecord(writer, record.Data, record.StringLookup.Data);
		}

		recordCompressedWriter.EndWrite(writer);
	}

	private void WriteLoose(BinaryWriter writer, Table table, bool is64Bit)
	{
		_recordUncompressedWriter.SetRecordCountOffset(table.RecordCountOffset);
		_recordUncompressedWriter.BeginWrite(writer, is64Bit && !table.IsCompressed && table.ElementCount == 1);

		foreach (var record in table.Records)
		{
			_recordUncompressedWriter.WriteRecord(writer, record.Data);
		}

		_recordUncompressedWriter.EndWrite(writer, table.Padding,
			table.Records.Count == 0
				? new MemoryStream()
				: new MemoryStream(table.Records.First().StringLookup.Data));
	}
}