using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowPawnOutlineBloomKey : ShowKeyBase
{
	[UPROPERTY]
	public float EndScale;

	[UPROPERTY]
	public FColor OutLineColor;

	[UPROPERTY]
	public float StartAlpha;
}