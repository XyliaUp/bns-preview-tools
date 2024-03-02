using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Common.DataStruct;
[StructLayout(LayoutKind.Sequential)]
public struct Ref : IComparable<Ref>
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

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static implicit operator long(Ref r) => Unsafe.As<Ref, long>(ref r);
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static implicit operator Ref(long key) => Unsafe.As<long, Ref>(ref key);

	public static implicit operator Ref(TRef tref) => tref.Ref;
	public static implicit operator Ref(IconRef iconRef) => iconRef.IconTextureRef;

	public static bool operator ==(Ref a, Ref b) => Unsafe.As<Ref, long>(ref a) == Unsafe.As<Ref, long>(ref b);
	public static bool operator !=(Ref a, Ref b) => !(a == b);

	public override bool Equals(object obj)
	{
		return obj is Ref other && Equals(other);
	}

	public bool Equals(Ref other)
	{
		return Unsafe.As<Ref, long>(ref this) == Unsafe.As<Ref, long>(ref other);
	}

	public readonly int CompareTo(Ref other)
	{
		return this.Variant == other.Variant ?
			this.Id - other.Id :
			this.Variant - other.Variant;
	}

	public override readonly string ToString() => Variant == 0 ? $"{Id}" : $"{Id}.{Variant}";

	public override readonly int GetHashCode() => HashCode.Combine(Id, Variant);
}
