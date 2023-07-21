using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Readers;

using Animation = CUE4Parse.UE4.Assets.Exports.Animation;

namespace CUE4Parse.BNS.Exports;
public class UAnimSequence : Animation.UAnimSequence
{
	public ResolvedObject Animset;
	public string SequenceName;
	public bool bServiced;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

	}
}