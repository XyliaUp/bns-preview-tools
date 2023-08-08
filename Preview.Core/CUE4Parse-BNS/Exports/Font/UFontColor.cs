using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Exports;
public class UFontColor : UObject
{
	public FLinearColor FontColor;
	public FLinearColor FontShadowColor;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		FontColor = GetOrDefault<FLinearColor>(nameof(FontColor));
		FontShadowColor = GetOrDefault<FLinearColor>(nameof(FontShadowColor));
	}
}