using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowSoundKey : ShowKeyBase
{
	[UPROPERTY] public bool bAcceptStopEvent;
	[UPROPERTY] public bool bAppliedEnemy;
	[UPROPERTY] public bool bAppliedPartyMember;
	[UPROPERTY] public bool bFollowActor;
	[UPROPERTY] public bool bNoStopOnShowStop;
	[UPROPERTY] public bool bPlayerRelationOnly;
	[UPROPERTY] public bool bUseTrack;
	[UPROPERTY] public bool bUseWorldTargetMode;
	[UPROPERTY] public float fVolumeLevel;
	[UPROPERTY] public float PlayerRelationSoundMultiplier;

	[UPROPERTY]
	public FPackageIndex SoundCue;

	[UPROPERTY]
	public FPackageIndex SoundCueMyself;
}