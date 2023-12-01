namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowAnimKey : ShowKeyBase
{
	public string AnimSetName;
	public bool bAutoFacialAnimSet;
	public bool bRacePostfix; 
	public bool bUpperOnly;
	public bool bUpperOnlyRideVehicle;
	public bool bUseAnimSet;

	public float fEndTime;
	public float fFadeInTime;
	public float fFadeOutTime;
	public float fMovingBlendFadeTime;
	public float fMovingBlendTime;
	public float fStartTime;
	public float fStopFadeOutTime;

	public bool Loop;
	public string strAnim;
}