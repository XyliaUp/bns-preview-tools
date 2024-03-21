using CUE4Parse.UE4;
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
public struct FLayoutData : IUStruct
{
	[UPROPERTY]
	public Offset Offsets;
	[UPROPERTY]
	public Anchor Anchors;
	[UPROPERTY]
	public FVector2D Alignments;


	[StructFallback]
	public struct Offset : IUStruct
	{
		public float Left;
		public float Top;
		public float Right;
		public float Bottom;

		public override string ToString() => $"{Left} {Top} {Right} {Bottom}";
	}

	[StructFallback]
	public struct Anchor : IUStruct
	{
		public FVector2D Minimum;
		public FVector2D Maximum;

		public override string ToString() => $"{Minimum.X} {Minimum.Y} {Maximum.X} {Maximum.Y}";
	}
}