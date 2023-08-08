using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowBeamKey : ShowKeyBase
{
	public float PartialRenderingDuration;
	public ResolvedObject ParticleSys;

	public string StartBoneName;
	public int StartMeshIndex;
	public FVector StartOffset;
	public string EndBoneName;
	public int EndMeshIndex;
	public FVector EndOffset;
}