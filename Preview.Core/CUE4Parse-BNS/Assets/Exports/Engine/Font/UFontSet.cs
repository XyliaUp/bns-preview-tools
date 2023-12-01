using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UFontSet : USerializeObject
{
	[UPROPERTY]
	public FPackageIndex FontAttribute;

	[UPROPERTY]
	public FPackageIndex FontColors;

	[UPROPERTY]
	public FPackageIndex FontFace;
}