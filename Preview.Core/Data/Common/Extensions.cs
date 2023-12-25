using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common;
public static class Extensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe string GetNStringUTF16(this byte[] array, int offset)
	{
		fixed (byte* memory = array)
			return new string((char*)(memory + offset));
	}


	// Getters
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe T Get<T>(this Record record, int offset) where T : unmanaged
	{
		fixed (byte* ptr = record.Data)
		{
			return *(T*)(ptr + offset);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe T Get<T>(this byte[] data, int offset) where T : unmanaged
	{
		fixed (byte* ptr = data)
		{
			return *(T*)(ptr + offset);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe T Get<T>(this Span<byte> data, int offset) where T : unmanaged
	{
		fixed (byte* ptr = data)
		{
			return *(T*)(ptr + offset);
		}
	}


	// Setters
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe void Set<T>(this Record record, int offset, T value) where T : unmanaged
	{
		fixed (byte* ptr = record.Data)
		{
			*(T*)(ptr + offset) = value;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe void Set(this Record record, int offset, object value)
	{
		fixed (byte* ptr = record.Data)
		{
			var ptr2 = new nint(ptr + offset);
			Marshal.StructureToPtr(value, ptr2, true);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe void Set<T>(this byte[] data, int offset, T value) where T : unmanaged
	{
		fixed (byte* ptr = data)
		{
			*(T*)(ptr + offset) = value;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static unsafe void Set(this byte[] data, int offset, object value)
	{
		fixed (byte* ptr = data)
		{
			var ptr2 = new nint(ptr + offset);
			Marshal.StructureToPtr(value, ptr2, true);
		}
	}
}