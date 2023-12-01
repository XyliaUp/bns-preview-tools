using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.UE4.Assets.Exports;
public class UWidgetTree : USerializeObject
{
	[UPROPERTY]
	public FPackageIndex RootWidget;   //UWidget
}