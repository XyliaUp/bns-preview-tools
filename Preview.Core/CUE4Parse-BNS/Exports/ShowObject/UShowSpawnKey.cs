using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowSpawnKey : ShowKeyBase
{
	public bool bBeam;
	public bool bBeamInfinite;
	public bool bBeamProjectile;
	public float BeamFadeTime;
	public bool bForceKillByTotalTime;
	public float EffectLifeScaler;
	public float EffectLifeScaler_Extra;
	public float EffectSpawnScaler;
	public float EffectSpawnScaler_Extra;
	public float EffectTimeScaler;
	public float EffectTimeScaler_Extra;
	public ResolvedObject ExtraParticle;
	public ResolvedObject SpawnObject;
	public string SpawnObjectType; // SPAWN_GADGET(EnumProperty)
	public string SpawnSpotType;   // SPAWN_SPOT_TARGET(EnumProperty)

	public float TotalTime;
	public FVector vSpawnPos;
	public FVector vSpawnScale;
}