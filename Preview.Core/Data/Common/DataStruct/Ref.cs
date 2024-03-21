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


	#region Static Methods
	public static Ref Prase(string input) => TryPrase(input, out var key) ? key : throw new ArgumentException("Invalid Ref string input");

	public static bool TryPrase(string input, out Ref key)
	{
		key = default;

		var split = input?.Split(':', 2) ?? [];
		if (split.Length >= 1 && int.TryParse(split[0], out var id))
		{
			var variant = split.Length == 2 ? int.Parse(split[1]) : 0;
			key = new Ref(id, variant);
			return true;
		}

		return false;
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static implicit operator long(Ref r) => Unsafe.As<Ref, long>(ref r);
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static implicit operator Ref(long key) => Unsafe.As<long, Ref>(ref key);

	public static implicit operator Ref(TRef tref) => tref.Ref;
	public static implicit operator Ref(IconRef iconRef) => iconRef.IconTextureRef;

	public static bool operator ==(Ref a, Ref b) => Unsafe.As<Ref, long>(ref a) == Unsafe.As<Ref, long>(ref b);
	public static bool operator !=(Ref a, Ref b) => !(a == b);
	#endregion
}
