﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common;
public static class Extensions
{
    public static string ReadNString([NotNull] this BinaryReader reader)
    {
        var builder = new StringBuilder();
        var c = reader.ReadByte();

        while (c != 0)
        {
            builder.Append((char)c);
            c = reader.ReadByte();
        }

        return builder.ToString();
    }

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


	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static Record GetRef(Table table, Ref Ref)
	{
		if (Ref == default) return null;

		return table?[Ref , false];
		//if (reference != null)
		//{
		//	var alias = reference.Attributes["alias"];
		//	if (alias != null) return alias;
		//}

		//return Ref.Variant == 0 ? $"{Ref.Id}" : $"{Ref.Id}.{Ref.Variant}";
	}

    public static Record GetRef(this Record record, short type, Ref Ref)
    {
		if (Ref == default) return null;

		var table = record.Owner.Owner.Provider.Tables[type];
		return GetRef(table, Ref);
    }

	public static string GetRef(this Record record, IconRef Ref)
	{
		if (Ref == default) return null;

		var table = record.Owner.Owner.Provider.Tables["icon-texture"];
		return GetRef(table, Ref) + $",{Ref._unk_i32_0}";
	}

	public static string GetRef(this Record record, TRef Ref)
	{
		if (Ref == default) return null;

		var table = record.Owner.Owner.Provider.Tables[(short)Ref.Table];
		return $"{table.Name}:{GetRef(table, Ref)}";
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
    public static unsafe void Set<T>(this byte[] data, int offset, T value) where T : unmanaged
    {
        fixed (byte* ptr = data)
        {
            *(T*)(ptr + offset) = value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe void Set<T>(this Span<byte> data, int offset, T value) where T : unmanaged
    {
        fixed (byte* ptr = data)
        {
            *(T*)(ptr + offset) = value;
        }
    }
}