using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowParticleAnimKey : ShowKeyBase
{
	[UPROPERTY]
	public FName AnimSeqName;

	[UPROPERTY]
	public FPackageIndex AnimSets;

	[UPROPERTY]
	public float AnimSpeed;

	[UPROPERTY]
	public bool bAutoScale;

	[UPROPERTY]
	public bool bPosOnly;

	[UPROPERTY]
	public float EffectLifeScaler;

	[UPROPERTY]
	public float EffectSpawnScaler;

	[UPROPERTY]
	public float EffectTimeScaler;

	[UPROPERTY]
	public FPackageIndex Mesh;

	[UPROPERTY]
	public object[] ParticleInfos;

	[UPROPERTY]
	public FVector RelativePosition;

	[UPROPERTY]
	public FRotator RelativeRotation;

	[UPROPERTY]
	public FVector RelativeScale;

	[UPROPERTY]
	public string strAttachBoneName;
}