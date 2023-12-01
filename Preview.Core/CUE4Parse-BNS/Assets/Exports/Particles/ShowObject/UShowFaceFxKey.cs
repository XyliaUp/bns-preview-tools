using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowFaceFxKey : ShowKeyBase
{
	public string AnimName;
	public bool bForcedPlaySound;
	public string FaceFXAnimSetName;    
	public string GroupName;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
		AnimName = GetOrDefault<string>(nameof(AnimName));
		bForcedPlaySound = GetOrDefault<bool>(nameof(bForcedPlaySound));
		FaceFXAnimSetName = GetOrDefault<string>(nameof(FaceFXAnimSetName));
		GroupName = GetOrDefault<string>(nameof(GroupName));
	}
}