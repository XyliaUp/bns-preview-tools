using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowParticleKey : ShowKeyBase
{
	[UPROPERTY]
	public int AttachMeshIndex;

	[UPROPERTY]
	public string AutoScale;  // AUTOSCALE_TYPE_ALL (EnumProperty)
	public bool bAcceptStopEvent;
	public bool bAttachToSocket;
	public bool bAttacked;
	public bool bPosOnly;
	public bool bTerrainCheck;
	public bool bUseAdjustLocationSocketScale;
	public bool bUseBeforeRuleMoveLocation;
	public bool bUseDistortion;
	public bool bUseExternalColor;
	public bool bUseGeoParticle;
	public bool bUseSocketRelativeValue;
	public bool bWorldPos;
	public FVector DeltaPos;
	public string DeltaPosToBone;
	public float EffectLifeScaler;
	public float EffectSpawnScaler;
	public float EffectTimeScaler;
	public float fBaseAddHeight;
	public float fDecalradiusMax;
	public float fDecalradiusMin;
	public float fEndTime;
	public FVector HitForce;

	[UPROPERTY]
	public string ParticleActivePos;  // CENTER_CENTER (EnumProperty)

	[UPROPERTY]
	public FPackageIndex ParticleSys;
	public string PP_Distortion;      // POSTPROC_TARGET(EnumProperty)
	public int ProbabilityPercentage;
	public FRotator Rotation;
	public FVector Scale;
	public string strAttachBone;
	public string strCasterAttackBone;
	public string strTargetAttectedBone;
	public string strWorldPosBone;
	public int UseGeoParticleSlot;
	public string WorldTargetType;   // WORLD_TARGET_TYPE_PARTICLE(EnumProperty)
}