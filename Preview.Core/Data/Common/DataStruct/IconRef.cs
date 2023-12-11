using System.Runtime.InteropServices;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct IconRef
{
	public int IconTextureRecordId;
	public int IconTextureVariantId;
	public int IconTextureIndex;

	public IconRef(int iconTextureRecordId, int iconTextureVariantId = 0, ushort iconTextureVariantIndex = 1)
	{
		IconTextureRecordId = iconTextureRecordId;
		IconTextureVariantId = iconTextureVariantId;
		IconTextureIndex = iconTextureVariantIndex;
	}

	public IconRef(Ref @ref, int iconTextureVariantIndex = 1)
	{
		IconTextureRecordId = @ref.Id;
		IconTextureVariantId = @ref.Variant;
		IconTextureIndex = iconTextureVariantIndex;
	}

	public IconRef(Record record, ushort index)
	{
		if (record is null) return;

		IconTextureRecordId = record.RecordId;
		IconTextureVariantId = record.RecordVariationId;
		IconTextureIndex = index;
	}



	public override string ToString()
	{
		return $"(Id: {IconTextureRecordId}, Variant: {IconTextureVariantId}, Index.: {IconTextureIndex})";
	}

	public static bool operator ==(IconRef a, IconRef b)
	{
		return
			a.IconTextureRecordId == b.IconTextureRecordId &&
			a.IconTextureVariantId == b.IconTextureVariantId &&
			a.IconTextureIndex == b.IconTextureIndex;
	}

	public static bool operator !=(IconRef a, IconRef b)
	{
		return !(a == b);
	}

	public bool Equals(IconRef other)
	{
		return IconTextureRecordId == other.IconTextureRecordId && 
			IconTextureVariantId == other.IconTextureVariantId && 
			IconTextureIndex == other.IconTextureIndex;
	}

	public override bool Equals(object obj)
	{
		return obj is IconRef other && Equals(other);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(IconTextureRecordId, IconTextureVariantId, IconTextureIndex);
	}
}