using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;

using Xylia.Extension;

namespace CUE4Parse.BNS.Exports;
public class UImageSet : UObject
{
	public ResolvedObject Image;
	public float U;
	public float V;
	public float UL;
	public float VL;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		Image = GetOrDefault<ResolvedObject>(nameof(Image));
		U = GetOrDefault<float>(nameof(U));
		V = GetOrDefault<float>(nameof(V));
		UL = GetOrDefault<float>(nameof(UL));
		VL = GetOrDefault<float>(nameof(VL));
	}

	public Bitmap GetImage() => Image?.Load()?.GetImage()?.Clone((int)U, (int)V, (int)UL, (int)VL);
}