using System.Runtime.InteropServices;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct TRef
{
	public int Table;
	public int Id;
	public int Variant;

	public TRef(ushort table, int id, int variant = 0)
	{
		Table = table;
		Id = id;
		Variant = variant;
	}

	public TRef(ushort table, Ref Ref) : this(table, Ref.Id, Ref.Variant)
	{
	}

	public TRef(Record record)
	{
		if (record is null) return;

		Table = record.Owner.Type;
		Id = ((Ref)record).Id;
		Variant = ((Ref)record).Variant;
	}



	public override string ToString() => $"{Table}:{Id}.{Variant}";


	public static implicit operator int(TRef r) => r.Id;

	public static bool operator ==(TRef a, TRef b)
	{
		return
			a.Table == b.Table &&
			a.Id == b.Id &&
			a.Variant == b.Variant;
	}

	public static bool operator !=(TRef a, TRef b)
	{
		return !(a == b);
	}

	public bool Equals(TRef other)
	{
		return Table == other.Table && Id == other.Id && Variant == other.Variant;
	}

	public override bool Equals(object obj) => obj is TRef other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(Table, Id, Variant);
}