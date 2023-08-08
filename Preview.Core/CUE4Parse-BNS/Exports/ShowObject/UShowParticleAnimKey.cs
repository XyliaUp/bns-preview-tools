using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowParticleAnimKey : ShowKeyBase
{
	public FName AnimSeqName;
	public ResolvedObject AnimSets;
	public float AnimSpeed;
	public bool bAutoScale;
	public bool bPosOnly;
	public float EffectLifeScaler;
	public float EffectSpawnScaler;
	public float EffectTimeScaler;
	public ResolvedObject Mesh;
	public object[] ParticleInfos;
	public FVector RelativePosition;
	public FRotator RelativeRotation;
	public FVector RelativeScale;
	public string strAttachBoneName;
}