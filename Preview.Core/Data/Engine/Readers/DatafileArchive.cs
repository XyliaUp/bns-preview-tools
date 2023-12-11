using System.Runtime.CompilerServices;

namespace Xylia.Preview.Data.Engine.Readers;
public class DatafileArchive : Stream
{
	private readonly byte[] _data;

	public DatafileArchive(byte[] data, long offset = 0, long size = -1)
	{
		_data = data;

		Position = offset;
		Length = size == -1 ? _data.Length : (offset + size);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int Read(byte[] buffer, int offset, int count)
	{
		int n = (int)(Length - Position);
		if (n > count) n = count;
		if (n <= 0)
			return 0;

		if (n <= 8)
		{
			int byteCount = n;
			while (--byteCount >= 0)
				buffer[offset + byteCount] = _data[Position + byteCount];
		}
		else
			Buffer.BlockCopy(_data, (int)Position, buffer, offset, n);
		Position += n;

		return n;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override long Seek(long offset, SeekOrigin origin)
	{
		Position = origin switch
		{
			SeekOrigin.Begin => offset,
			SeekOrigin.Current => Position + offset,
			SeekOrigin.End => Length + offset,
			_ => throw new ArgumentOutOfRangeException()
		};
		return Position;
	}
	public override void Flush() { }
	public override bool CanSeek { get; } = true;
	public override long Length { get; }
	public override long Position { get; set; }
	public override bool CanRead { get; } = true;
	public override bool CanWrite { get; } = false;
	public override void SetLength(long value) { throw new InvalidOperationException(); }
	public override void Write(byte[] buffer, int offset, int count) { throw new InvalidOperationException(); }



	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual T Read<T>()
	{
		var size = Unsafe.SizeOf<T>();
		var result = Unsafe.ReadUnaligned<T>(ref _data[Position]);
		Position += size;
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual byte[] ReadBytes(uint count)
	{
		var result = new byte[count];
		Unsafe.CopyBlockUnaligned(ref result[0], ref _data[Position], count);
		Position += count;
		return result;
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual unsafe void Serialize(byte* ptr, int length)
	{
		Unsafe.CopyBlockUnaligned(ref ptr[0], ref _data[Position], (uint)length);
		Position += length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual T[] ReadArray<T>(int length)
	{
		var size = length * Unsafe.SizeOf<T>();
		var result = new T[length];
		if (length > 0) Unsafe.CopyBlockUnaligned(ref Unsafe.As<T, byte>(ref result[0]), ref _data[Position], (uint)size);
		Position += size;
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual void ReadArray<T>(T[] array)
	{
		if (array.Length == 0) return;
		var size = array.Length * Unsafe.SizeOf<T>();
		Unsafe.CopyBlockUnaligned(ref Unsafe.As<T, byte>(ref array[0]), ref _data[Position], (uint)size);
		Position += size;
	}



	public Stream CreateStream()
	{
		return new MemoryStream(_data, (int)Position, (int)(Length - Position));
	}

	public DatafileArchive OffsetedSource(long offset, long size)
	{
		if (Position + offset > int.MaxValue)
			throw new OverflowException("Offset doesn't fit inside 32-bit integer");

		if (size > int.MaxValue)
			throw new OverflowException("Size doesn't fit inside 32-bit integer");

		return new DatafileArchive(this._data, offset, size);
	}
}