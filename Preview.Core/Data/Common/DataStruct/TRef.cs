using System.Runtime.InteropServices;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct TRef
{
	public int Table;
	public Ref Ref;

	public TRef(ushort table, int id, int variant = 0)
	{
		Table = table;
		Ref = new Ref(id, variant);
	}

	public TRef(ushort table, Ref Ref) : this(table, Ref.Id, Ref.Variant)
	{
	}

	public TRef(Record record)
	{
		if (record is null) return;

		Table = record.Owner.Type;
		Ref = record.PrimaryKey;
	}



	public override string ToString() => $"{Table}:{Ref}";

	public static bool operator ==(TRef a, TRef b)
	{
		return a.Table == b.Table && a.Ref == b.Ref;
	}

	public static bool operator !=(TRef a, TRef b)
	{
		return !(a == b);
	}

	public bool Equals(TRef other)
	{
		return Table == other.Table && Ref == other.Ref;
	}

	public override bool Equals(object obj) => obj is TRef other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(Table, Ref);
}