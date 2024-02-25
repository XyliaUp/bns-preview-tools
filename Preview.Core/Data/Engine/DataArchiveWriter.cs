using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Preview.Tests")]
namespace Xylia.Preview.Data.Engine;
internal class DataArchiveWriter : MemoryStream
{
	public bool Is64Bit { get; set; }

	public DataArchiveWriter(bool is64Bit = false)
	{
		Is64Bit = is64Bit;
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual void Write<T>(T value) where T : struct
	{
		var size = Unsafe.SizeOf<T>();
		var data = new byte[size];

		unsafe
		{
			fixed (byte* p = &Unsafe.As<T, byte>(ref value))
			{
				using UnmanagedMemoryStream ms = new UnmanagedMemoryStream(p, size);
				ms.Read(data, 0, data.Length);
			}
		}

		this.Write(data, 0, size);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual void Write(byte[] data)
	{
		this.Write(data, 0, data.Length);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteString(string value)
	{
		var buffer = Encoding.UTF8.GetBytes(value);

		Write(buffer.Length);
		Write(buffer);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteLongInt(long value)
	{
		if (Is64Bit) this.Write((long)value);
		else this.Write((int)value);
	}
}