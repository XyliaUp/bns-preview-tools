using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UAnimSequence : CUE4Parse.UE4.Assets.Exports.Animation.UAnimSequence
{
	[UPROPERTY]
	public FPackageIndex Animset;

	[UPROPERTY]
	public string SequenceName;

	[UPROPERTY]
	public bool bServiced;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
	}
}