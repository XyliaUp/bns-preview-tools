using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowParticleMaterialKey : ShowKeyBase
{
	public bool bAttacked;
	public bool bPosOnly;
	public bool bWorldPos;
	public FVector DeltaPos;
	public float EffectLifeScaler;
	public float EffectSpawnScaler;
	public float EffectTimeScaler;
	public FVector HitForce;
	public FVector Scale;
	public string strAttachBone;
	public string strCasterAttackBone;
	public string strTargetAttectedBone;
	public string strWorldPosBone;
	public string WeaponSize; // PSWS_Medium(EnumProperty)
}