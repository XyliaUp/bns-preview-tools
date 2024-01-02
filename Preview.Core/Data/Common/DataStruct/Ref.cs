using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;
[StructLayout(LayoutKind.Sequential)]
public struct Ref
{
	public readonly int Id;
	public readonly int Variant;
	public Ref(int id, int variant = 0)
	{
		Id = id;
		Variant = variant;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static Ref From(long ul)
	{
		return Unsafe.As<long, Ref>(ref ul);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static Ref From(ulong ul)
	{
		return Unsafe.As<ulong, Ref>(ref ul);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static Ref From(string input)
	{
		var split = input.Split(':', 2);
		if (split.Length == 2)
		{
			var id = int.Parse(split[0]);
			var variant = int.Parse(split[1]);
			return new Ref(id, variant);
		}

		throw new ArgumentException("Invalid Ref string input");
	}


	public override string ToString() => Variant == 0 ? $"{Id}" : $"{Id}.{Variant}";

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static explicit operator long(Ref r) => Unsafe.As<Ref, long>(ref r);
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static explicit operator Ref(long key) => Unsafe.As<long, Ref>(ref key);

	public static implicit operator int(Ref r) => r.Id;
	public static implicit operator Ref(TRef tref) => new Ref(tref.Id, tref.Variant);
	public static implicit operator Ref(IconRef iconRef) => new Ref(iconRef.IconTextureRecordId, iconRef.IconTextureVariantId);
	public static implicit operator Ref(Record record)
	{
		if (record.Data is null) return default;
		return new Ref(record.RecordId, record.RecordVariationId);
	}


	public static bool operator ==(Ref a, Ref b)
	{
		return Unsafe.As<Ref, ulong>(ref a) == Unsafe.As<Ref, ulong>(ref b);
	}

	public static bool operator !=(Ref a, Ref b)
	{
		return Unsafe.As<Ref, ulong>(ref a) != Unsafe.As<Ref, ulong>(ref b);
	}

	public bool Equals(Ref other)
	{
		return Unsafe.As<Ref, ulong>(ref this) == Unsafe.As<Ref, ulong>(ref other);
	}

	public override bool Equals(object obj)
	{
		return obj is Ref other && Equals(other);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Id, Variant);
	}
}
