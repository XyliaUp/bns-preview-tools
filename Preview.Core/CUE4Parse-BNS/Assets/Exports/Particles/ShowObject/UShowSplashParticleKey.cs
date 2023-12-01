using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowSplashParticleKey : ShowKeyBase
{
	[UPROPERTY]
	public string AppliedObject;  // SP_CASTER(EnumProperty)

	[UPROPERTY]
	public FPackageIndex ParticleSys;
}