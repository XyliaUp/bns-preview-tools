using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Exports;
public class UParticleEmitter : UObject
{
	public FName EmitterName;

	public int SubUVDataOffset;



	public bool bUseLegacySpawningBehavior;

	//////////////////////////////////////////////////////////////////////////
	// Below is information udpated by calling CacheEmitterModuleInfo

	public bool bRequiresLoopNotification;
	public bool bAxisLockEnabled;
	public bool bMeshRotationActive;

	public bool ConvertedModules;

	public bool bIsSoloing;

	public bool bCookedOut;

	public bool bDisabledLODsKeepEmitterAlive;

	public bool bDisableWhenInsignficant;

	public bool bRemoveHMDRollInVR;




	public UParticleLODLevel LODLevels;

	public int PeakActiveParticles;

	public int InitialAllocationCount;

	public float QualityLevelSpawnRateScale;

	public uint DetailModeBitmask;


	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		//LODLevels = GetOrDefault<ResolvedObject[]>(nameof(LODLevels));
		//DetailModeBitmask = GetOrDefault<uint>(nameof(DetailModeBitmask));
	}
}

public class UBnsParticleSpriteEmitter : UParticleEmitter
{
	
}