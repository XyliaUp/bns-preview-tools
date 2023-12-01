using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowSocketControllerKey : ShowKeyBase
{
	public float ActionTime;
	public int iAttachCasterMeshIndex;
	public string MovementType; // SOCKET_MONEMENT_TYPE_ATTACHCASTER(EnumProperty)
	public int OrbitUseModCount;
	public FPackageIndex OrbitUseParticleSys;
	public FVector RelativeLocation;
	public FRotator RelativeRotation;
	public string SocketName;
	public bool SocketPosOnly;
	public string strAttachCasterBone;
	public string strAttachTagetBone;
	public string strWorldPosBone;
	public FVector vWorldDeltaPos;
}