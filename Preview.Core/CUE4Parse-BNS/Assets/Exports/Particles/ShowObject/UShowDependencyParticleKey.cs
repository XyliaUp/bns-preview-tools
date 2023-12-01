using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowDependencyParticleKey : ShowKeyBase
{
	public FPackageIndex Add_ParticleSys;
	public bool Add_PosOnly;
	public FVector Add_RelativeLocation;
	public FVector Add_RelativeScale;
	public string Add_SocketName;
	public bool bActiveDependencyParticles;
	public bool bAddDependencyParticles;
	public bool bHideDependencyParticles;
	public string DependencyParticleName;

	public bool Loop;
}