using System.Runtime.CompilerServices;

namespace Xylia.Preview.Common.Extension;
public static class BinaryExtension
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Read<T>(this BinaryReader reader)
	{
		var size = Unsafe.SizeOf<T>();
		var buffer = reader.ReadBytes(size);
		return Unsafe.ReadUnaligned<T>(ref buffer[0]);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Write<T>(this BinaryWriter writer, T value) where T : struct
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

		writer.Write(data);
	}
}