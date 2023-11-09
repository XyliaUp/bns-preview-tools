using System.Runtime.InteropServices;

using Xylia.Preview.Data.Common.Abstractions;

using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;

/// <summary>
/// Reads uncompressed table record by record
/// </summary>
public unsafe class RecordUncompressedReader : IRecordReader
{
    private int _stringLookupBufferSize = 0x4000;
    private nint _stringLookupBufferHandle = Marshal.AllocHGlobal(0x4000);

    private int _recordBufferSize = 0x1000;
    private nint _recordBufferHandle = Marshal.AllocHGlobal(0x1000);

    private byte* _stringLookupBuffer;
    private byte* _recordBuffer;

    private long _recordCount = -1;
    private int _recordsSize;
    private int _currentRecord;

    private int _stringLookupSize;
    private long _stringLookupStart;

    private int _paddingSize;

    public RecordUncompressedReader()
    {
        _stringLookupBuffer = (byte*)_stringLookupBufferHandle.ToPointer();
        _recordBuffer = (byte*)_recordBufferHandle.ToPointer();
    }

    ~RecordUncompressedReader()
    {
        Marshal.FreeHGlobal(_stringLookupBufferHandle);
        Marshal.FreeHGlobal(_recordBufferHandle);
    }

    public bool Initialize(DatafileArchive reader, bool is64Bit)
    {
        _currentRecord = 0;
        _recordCount = reader.Read<Int32>();

        if (is64Bit)
        {
            if (reader.Read<Int32>() != 0)
                 throw new Exception("Unexpected integer");
        }

        _recordsSize = reader.Read<Int32>();
        _stringLookupSize = reader.Read<Int32>();
        _paddingSize = 0;

        EnsureBufferSize(ref _stringLookupBufferHandle, ref _stringLookupBuffer, ref _stringLookupBufferSize, _stringLookupSize);

        if (reader.ReadByte() != 1)
             throw new Exception("Unexpected byte");

        var position = reader.Position;
        _stringLookupStart = position + _recordsSize;

        reader.Seek(_recordsSize, SeekOrigin.Current);

        var read = reader.Read(new Span<byte>(_stringLookupBuffer, _stringLookupSize));

        if (read != _stringLookupSize)
             throw new Exception("Failed to read string lookup");

        reader.Seek(position, SeekOrigin.Begin);

        return true;
    }

    public bool Read(DatafileArchive reader, ref RecordMemory recordMemory)
    {
        if (_recordCount == -1)
			throw new Exception("Uninitialized");

        if (reader.Position > _stringLookupStart)
			throw new Exception("Read past string lookup while reading records");

        if (_currentRecord != _recordCount && reader.Position != _stringLookupStart)
        {
            reader.Read(new Span<byte>(_recordBuffer, 6));
            var size = *(ushort*)(_recordBuffer + 4);

            EnsureBufferSize(ref _recordBufferHandle, ref _recordBuffer, ref _recordBufferSize, size);

            if (reader.Read(new Span<byte>(_recordBuffer + 6, size - 6)) != size - 6)
                throw new Exception("Failed to read record");

            recordMemory.Type = *(short*)(_recordBuffer + 2);
            recordMemory.DataBegin = _recordBuffer;
            recordMemory.DataSize = size;
            recordMemory.StringBufferBegin = _stringLookupBuffer;
            recordMemory.StringBufferSize = _stringLookupSize;

            _currentRecord++;
            return true;
        }

        if (reader.Position > _stringLookupStart)
             throw new Exception("Read past string lookup while reading records");

        if (reader.Position < _stringLookupStart)
        {
            var paddingSize = (int)(_stringLookupStart - reader.Position);
            _paddingSize = paddingSize;
            EnsureBufferSize(ref _recordBufferHandle, ref _recordBuffer, ref _recordBufferSize, paddingSize);

            if (reader.Read(new Span<byte>(_recordBuffer, paddingSize)) != paddingSize)
               throw new Exception("Failed to padding");
        }

        reader.Seek(_stringLookupStart + _stringLookupSize, SeekOrigin.Begin);

        return false;
    }

    public void GetPadding(out Span<byte> outPadding)
    {
        outPadding = new Span<byte>(_recordBuffer, _paddingSize);
    }

    public int GetRecordCountOffset()
    {
        return (int)(_recordCount - _currentRecord);
    }

    private static void EnsureBufferSize(ref nint intPtr, ref byte* bytePtr, ref int currentSize, int size)
    {
        if (size <= currentSize)
            return;

        size = size + 0x1000 - size % 0x1000;

        intPtr = Marshal.ReAllocHGlobal(intPtr, size);
        bytePtr = (byte*)intPtr.ToPointer();

        currentSize = size;
    }
}