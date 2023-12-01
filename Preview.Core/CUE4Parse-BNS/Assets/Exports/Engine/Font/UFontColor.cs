using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UFontColor : USerializeObject
{
	[UPROPERTY]
	public FLinearColor FontColor;

	[UPROPERTY]
	public FLinearColor FontShadowColor;
}