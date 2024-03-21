using System.Runtime.InteropServices;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct IconRef
{
	public readonly Ref IconTextureRef;
	public readonly int IconTextureIndex;

	public IconRef(int iconTextureRecordId, int iconTextureVariantId = 0, ushort iconTextureVariantIndex = 1)
	{
		IconTextureRef = new Ref(iconTextureRecordId, iconTextureVariantId);
		IconTextureIndex = iconTextureVariantIndex;
	}

	public IconRef(Ref @ref, int iconTextureVariantIndex = 1)
	{
		IconTextureRef = @ref;
		IconTextureIndex = iconTextureVariantIndex;
	}

	public IconRef(Record record, ushort index)
	{
		if (record is null) return;

		IconTextureRef = record.PrimaryKey;
		IconTextureIndex = index;
	}



	public override string ToString()
	{
		return $"(Ref: {IconTextureRef}, Index.: {IconTextureIndex})";
	}

	public static bool operator ==(IconRef a, IconRef b)
	{
		return
			a.IconTextureRef == b.IconTextureRef &&
			a.IconTextureIndex == b.IconTextureIndex;
	}

	public static bool operator !=(IconRef a, IconRef b)
	{
		return !(a == b);
	}

	public bool Equals(IconRef other)
	{
		return IconTextureRef == other.IconTextureRef && 
			IconTextureIndex == other.IconTextureIndex;
	}

	public override bool Equals(object obj)
	{
		return obj is IconRef other && Equals(other);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(IconTextureRef, IconTextureIndex);
	}
}