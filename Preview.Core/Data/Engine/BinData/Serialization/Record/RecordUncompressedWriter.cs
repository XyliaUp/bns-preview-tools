namespace Xylia.Preview.Data.Engine.BinData.Serialization;
internal class RecordUncompressedWriter
{
	private int _recordCountOffset;
	private int _beginning = -1;
	private int _recordsBeginning = -1;
	private int _recordCount;
	private bool _is64Bit;

	public void SetRecordCountOffset(int recordCountOffset)
	{
		_recordCountOffset = recordCountOffset;
	}

	public void BeginWrite(DataArchiveWriter writer, bool is64Bit)
	{
		_is64Bit = is64Bit;
		_beginning = (int)writer.Position;
		writer.Write(0); // Size
		writer.Write(false); // Is compressed
		writer.Write(0); // Record count
		if (is64Bit)
			writer.Write(0); // Unknown
		writer.Write(0); // Records size + padding
		writer.Write(0); // Lookup size
		writer.Write((byte)1); // Always 1
		_recordsBeginning = (int)writer.Position;
		_recordCount = 0;
	}

	public void EndWrite(DataArchiveWriter writer, ReadOnlySpan<byte> padding, MemoryStream lookupBuffer)
	{
		writer.Write(padding.ToArray());
		lookupBuffer.Seek(0, SeekOrigin.Begin);
		lookupBuffer.CopyTo(writer);
		var positionEnd = (int)writer.Position;
		writer.Seek(_beginning, SeekOrigin.Begin);

		writer.Write(positionEnd - _beginning - sizeof(int)); // Size
		writer.Write(false); // Is compressed
		writer.Write(_recordCount + _recordCountOffset); // Record count
		if (_is64Bit)
			writer.Write(0); // Unknown
		writer.Write((int)(positionEnd - _recordsBeginning - lookupBuffer.Length)); // Records size + padding (without lookup!)
		writer.Write((int)lookupBuffer.Length); // Lookup size
		writer.Write((byte)1); // Always 1

		writer.Seek(positionEnd, SeekOrigin.Begin);
	}

	public void WriteRecord(DataArchiveWriter writer, ReadOnlySpan<byte> data)
	{
		writer.Write(data.ToArray());
		_recordCount++;
	}
}