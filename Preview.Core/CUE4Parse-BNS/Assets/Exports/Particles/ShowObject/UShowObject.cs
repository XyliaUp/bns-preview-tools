using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UShowObject : USerializeObject
{
	[UPROPERTY]
	public FPackageIndex[] EventKeys;
}