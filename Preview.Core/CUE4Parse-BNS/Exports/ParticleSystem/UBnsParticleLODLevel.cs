using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.Particles;

namespace CUE4Parse.BNS.Exports;
public class UParticleLODLevel : UObject
{
	public int Level;

	public bool bEnabled;

	public UParticleModuleRequired RequiredModule;

	//public UParticleModule[] Modules;

	//public UParticleModuleTypeDataBase TypeDataModule;

	//public UParticleModuleSpawn SpawnModule;

	//public UParticleModuleEventGenerator EventGenerator;

	//public UParticleModuleSpawnBase[] SpawningModules;

	//public UParticleModule[] SpawnModules;

	//public UParticleModule[] UpdateModules;

	//public UParticleModuleOrbit[] OrbitModules;

	//public UParticleModuleEventReceiverBase EventReceiverModules;

	public uint ConvertedModules;

	public int PeakActiveParticles;


	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
	}
}

public class UBnsParticleLODLevel : UParticleLODLevel
{
	
}