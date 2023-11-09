using System.Runtime.InteropServices;

using Xylia.Preview.Data.Common.Abstractions;

using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;
public unsafe class RecordCompressedReader : IRecordReader
{
	private const ushort BufferSize = ushort.MaxValue;
	private readonly nint _decompressedBufferHandle = Marshal.AllocHGlobal(BufferSize);
	private readonly byte* _decompressedBuffer;
#if ENABLE_ISA
    private readonly IntPtr _compressedBufferHandle = Marshal.AllocHGlobal(BufferSize);
    private readonly byte* _compressedBuffer;
#endif

	private int _compressedBlockCount = -1;
	private int _currentBlock = -1;
	private int _currentRecord;
	private int _recordCount;
	private int _sizeDecompressed;
	private int _position;

	public RecordCompressedReader()
	{
		_decompressedBuffer = (byte*)_decompressedBufferHandle.ToPointer();
#if ENABLE_ISA
        _compressedBuffer = (byte*) _compressedBufferHandle.ToPointer();
#endif
	}

	~RecordCompressedReader()
	{
		Marshal.FreeHGlobal(_decompressedBufferHandle);
#if ENABLE_ISA
        Marshal.FreeHGlobal(_compressedBufferHandle);
#endif
	}

	public bool Initialize(DatafileArchive reader, bool is64Bit)
	{
		_compressedBlockCount = -1;
		_currentBlock = -1;
		_currentRecord = 0;
		_recordCount = 0;
		_sizeDecompressed = 0;
		_position = 0;
		_compressedBlockCount = reader.Read<int>();

		if (reader.Read<UInt16>() != 8)
			return false;

		return BeginReadBlock(reader);
	}

	public bool Read(DatafileArchive reader, ref RecordMemory recordMemory)
	{
		if (_compressedBlockCount == -1)
			throw new Exception("Uninitialized");

		if (_currentRecord == _recordCount)
		{
			if (!BeginReadBlock(reader))
				return false;
		}

		recordMemory.DataBegin = _decompressedBuffer + _position;
		recordMemory.Type = *(short*)(recordMemory.DataBegin + 2);
		recordMemory.DataSize = *(ushort*)(recordMemory.DataBegin + 4);

		_position = _currentRecord < _recordCount - 1
			? reader.Read<UInt16>()
			: _sizeDecompressed;

		recordMemory.StringBufferBegin = recordMemory.DataBegin + recordMemory.DataSize;
		recordMemory.StringBufferSize = (ushort)(_decompressedBuffer + _position - recordMemory.StringBufferBegin);

		_currentRecord++;

		return true;
	}

	private bool BeginReadBlock(DatafileArchive reader)
	{
		if (++_currentBlock >= _compressedBlockCount)
			return false;

		// Skip startRecord - endRecord & sizeCompressed
		reader.Seek(4 * sizeof(int), SeekOrigin.Current);

		var sizeCompressed = reader.Read<UInt16>();
		var beforeDecompressingPosition = reader.Position;

#if ENABLE_ISA
        if (reader.Read(new Span<byte>(_compressedBuffer, sizeCompressed)) != sizeCompressed)
             throw new Exception("Failed to read compressed buffer");
        if (!IsaLib.zlib_decompress(_compressedBuffer, sizeCompressed, _decompressedBuffer, BufferSize))
             throw new Exception("Failed to decompress");
#else
		using (var inputStream = new InflaterInputStream(reader) { IsStreamOwner = false })
		{
			using var memoryStream = new UnmanagedMemoryStream(_decompressedBuffer, BufferSize, BufferSize, FileAccess.ReadWrite);
			inputStream.CopyTo(memoryStream);
		}
#endif

		reader.Seek(beforeDecompressingPosition + sizeCompressed, SeekOrigin.Begin);

		_sizeDecompressed = reader.Read<UInt16>();

		// Read decompressed records and lookup tables
		_recordCount = reader.Read<Int32>();
		_position = reader.Read<UInt16>();

		_currentRecord = 0;

		return true;
	}
}