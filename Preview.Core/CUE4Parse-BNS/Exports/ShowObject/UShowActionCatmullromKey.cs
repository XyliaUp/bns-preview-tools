using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Exports;
public class UShowActionCatmullromKey : ShowKeyBase
{
	public object[] aSamplePoints;
	public object[] aSamplePointRandoms;
	public float[] aSamplePointTimes;

	public FName ArriveBone; 
	public FName FollowBone;
	public string ArrivalJudgement;  //	ARRIVAL_JUDGEMENT_TARGET (EnumProperty)

	public bool bContinueBeforeEndPosition;
	public bool bEnableTerrainCheck;
	public bool bDestroyAfterAction;
	public bool bPosOnly;
	public bool bProjectileOverArrivalPos;
	public bool bProjectileReverse;
	public bool bSamplePointScaler;
	public bool bSyncActionTime;
	public bool bUseOrbitingSocketName;
	public string eArriveObjectType; // SAOT_ARRIVE_OBJECT_TARGET (EnumProperty)
	public float fActionTime;
	public float fBaseFlowHeight;
	public float fSamplePointScaler_MaxDist;
	public float fSamplePointScaler_MinDist;

	public int iMeshIndex;
	public FRotator rSpawnDataRotation;
	public string ShowKeyId; // SHOW_KEY_ACTION_LINEAR_PAWN (EnumProperty)
	public string strActionAnim;
	public FVector vArrivePos;
	public FVector vDeltaLocation;
	public FVector vStartPos;
}