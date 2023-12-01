using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowTrailMeshKey : ShowKeyBase
{
	[UPROPERTY]
	public bool bDestroyAfterAction;

	[UPROPERTY]
	public bool bTrackSpawnObject;

	[UPROPERTY]
	public float fDestroyDelayTime;

	[UPROPERTY]
	public string strAttachBone;

	[UPROPERTY]
	public FPackageIndex TrailBoneMesh;

	[UPROPERTY]
	public object[] TrailInfos;

	[UPROPERTY]
	public FVector vAttachPos;

	[UPROPERTY]
	public FVector vMeshScale;
}