using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowTrailMeshKey : ShowKeyBase
{
	public bool bDestroyAfterAction;
	public bool bTrackSpawnObject;
	public float fDestroyDelayTime;
	public string strAttachBone;
	public ResolvedObject TrailBoneMesh;
	public object[] TrailInfos;
	public FVector vAttachPos;
	public FVector vMeshScale;
}