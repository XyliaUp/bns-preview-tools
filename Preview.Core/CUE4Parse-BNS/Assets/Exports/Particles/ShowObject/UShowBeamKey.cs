using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowBeamKey : ShowKeyBase
{
	public float PartialRenderingDuration;
	public FPackageIndex ParticleSys;

	public string StartBoneName;
	public int StartMeshIndex;
	public FVector StartOffset;
	public string EndBoneName;
	public int EndMeshIndex;
	public FVector EndOffset;
}