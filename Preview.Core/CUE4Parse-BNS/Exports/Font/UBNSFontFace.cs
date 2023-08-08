using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.BNS.Exports;
public sealed class UBNSFontFace : UObject
{
	public ResolvedObject FontFace;
	public float Height;
	public float SpaceBetweenLines;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		FontFace = GetOrDefault<ResolvedObject>(nameof(FontFace));
		Height = GetOrDefault<float>(nameof(Height));
		SpaceBetweenLines = GetOrDefault<float>(nameof(SpaceBetweenLines));
	}
}