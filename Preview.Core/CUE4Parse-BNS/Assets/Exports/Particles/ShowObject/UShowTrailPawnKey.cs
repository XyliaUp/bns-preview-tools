using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowTrailPawnKey : ShowKeyBase
{
	[UPROPERTY]
	public int AttachMeshIndex;

	[UPROPERTY]
	public bool bAttachToSocket;

	[UPROPERTY]
	public bool Loop;

	[UPROPERTY]
	public string strEndBone;

	[UPROPERTY]
	public string strStartBone;

	[UPROPERTY]
	public FPackageIndex TrailSys;
}