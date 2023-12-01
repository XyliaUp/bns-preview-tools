using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UBNSFontFace : USerializeObject
{
	[UPROPERTY]
	public FPackageIndex FontFace;

	[UPROPERTY]
	public float Height;

	[UPROPERTY]
	public float SpaceBetweenLines;
}