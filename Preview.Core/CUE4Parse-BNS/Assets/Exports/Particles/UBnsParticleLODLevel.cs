using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Particles;

namespace CUE4Parse.BNS.Assets.Exports;
public class UParticleLODLevel : USerializeObject
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
}

public class UBnsParticleLODLevel : UParticleLODLevel
{
	
}