using CUE4Parse.UE4.Assets;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowTrailPawnKey : ShowKeyBase
{
	public int AttachMeshIndex;
	public bool bAttachToSocket;

	public bool Loop;
	public string strEndBone;
	public string strStartBone;
	public ResolvedObject TrailSys;
}