using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UBnsCustomBaseWidgetSlot : USerializeObject
{
	[UPROPERTY]
	public FLayoutData LayoutData;

	[UPROPERTY]
	public FPackageIndex Parent;

	[UPROPERTY]
	public FPackageIndex Content;
}


[StructFallback]
public struct FLayoutData
{
	[UPROPERTY]
	public Offset Offsets;
	[UPROPERTY]
	public Anchor Anchors;
	[UPROPERTY]
	public Alignment Alignments;


	[StructFallback]
	public struct Offset
	{
		public float Left;
		public float Top;
		public float Right;
		public float Bottom;

		public override string ToString() => $"{Left} {Top} {Right} {Bottom}";
	}

	[StructFallback]
	public struct Anchor
	{
		public FVector2D Minimum;
		public FVector2D Maximum;

		public override string ToString() => $"{Minimum.X} {Minimum.Y} {Maximum.X} {Maximum.Y}";
	}

	[StructFallback]
	public struct Alignment
	{
		public float X;
		public float Y;

		public override string ToString() => $"{X} {Y}";
	}
}