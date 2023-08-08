using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.UE4.Assets.Exports;
public class UWidgetTree : UObject
{
	public ResolvedObject RootWidget;   // UWidget

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
		RootWidget = GetOrDefault<ResolvedObject>(nameof(RootWidget));
	}
}