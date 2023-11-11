using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Xylia.Extension;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;
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
	public static explicit operator ulong(Ref r) => Unsafe.As<Ref, ulong>(ref r);

	public static implicit operator Ref(TRef tref) => new Ref(tref.Id, tref.Variant);
	public static implicit operator Ref(IconRef iconRef) => new Ref(iconRef.IconTextureRecordId, iconRef.IconTextureVariantId);
	public static implicit operator int(Ref r) => r.Id;

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


/// <summary>
/// model reference
/// </summary>
/// <typeparam name="TRecord"></typeparam>
public struct Ref<TRecord> where TRecord : Record
{
	public Ref(string value, BnsDatabase database = null)
	{
		if (value is null)
		{
			throw new Exception();
		}
		else if (value.Contains(':'))
		{
			Table = value.Split(':')[0];
			Alias = value.Split(':')[1]?.Trim();
		}
		else if (typeof(TRecord) != typeof(Record))
		{
			Table = typeof(TRecord).Name;
			Alias = value;
		}

		Database = database ?? FileCache.Data;
	}

	#region Field
	private readonly BnsDatabase Database;

	public readonly string Table;
	public string Alias;
	//public int Id;
	//public int Variant;

	public bool IsNull => Alias is null || Instance is null;

	public override string ToString() => IsNull ? null : $"{Table}:{Alias}";
	#endregion

	#region	Instance
	private TRecord _instance;

	public TRecord Instance => _instance ??= CastObject<TRecord>(Table, Alias, Database);

	public static T CastObject<T>(string table, string alias, BnsDatabase data) where T : Record
	{
		if (alias is null) return null;

		// tref: need register on the database
		// TODO: change to auto create ?
		if (typeof(T) == typeof(Record))
			return (T)(data.GetValue(table, true) as Table)?[alias]?.Model.Value;

		// ref: create model table
		return data.Get<T>(table)?[alias];
	}
	#endregion


	#region Operator
	public static bool operator ==(Ref<TRecord> a, Ref<TRecord> b)
	{
		// If one is null, but not both, return false.
		if (a.GetType() != b.GetType()) return false;

		// Return true if the fields match:
		if (a.Alias is null && b.Alias is null) return false;
		else if (a.Alias != null && a.Alias.Equals(b.Alias, StringComparison.OrdinalIgnoreCase)) return true;


		return false;
	}
	public static bool operator !=(Ref<TRecord> a, Ref<TRecord> b) => !(a == b);

	public static bool operator ==(Ref<TRecord> a, TRecord b)
	{
		return a.Instance == b;
	}
	public static bool operator !=(Ref<TRecord> a, TRecord b) => !(a == b);

	public override readonly bool Equals(object other) => other is Ref<TRecord> record && this == record;
	public override readonly int GetHashCode() => base.GetHashCode();
	#endregion
}
