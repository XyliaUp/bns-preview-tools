using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowSoundKey : ShowKeyBase
{
	public bool bAcceptStopEvent;
	public bool bAppliedEnemy;
	public bool bAppliedPartyMember;
	public bool bFollowActor;
	public bool bNoStopOnShowStop;
	public bool bPlayerRelationOnly;
	public bool bUseTrack;
	public bool bUseWorldTargetMode;
	public float fVolumeLevel;
	public float PlayerRelationSoundMultiplier;
	public ResolvedObject SoundCue;
	public ResolvedObject SoundCueMyself;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
		SoundCue = GetOrDefault<ResolvedObject>(nameof(SoundCue));
	}
}