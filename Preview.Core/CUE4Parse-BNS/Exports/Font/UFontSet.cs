using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.BNS.Exports;
public class UFontSet : UObject
{
	public ResolvedObject FontAttribute;
	public ResolvedObject FontColors;
	public ResolvedObject FontFace;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		FontAttribute = GetOrDefault<ResolvedObject>(nameof(FontAttribute));
		FontColors = GetOrDefault<ResolvedObject>(nameof(FontColors));
		FontFace = GetOrDefault<ResolvedObject>(nameof(FontFace));
	}
}