using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowPlayerCameraAnimKey : ShowKeyBase
{
	public bool bAffectUserCamDist;
	public float BlendInTimePercentage;
	public float BlendOutTimePercentage;
	public bool bOverridePreviousAnim;
	public bool bRetainLastState;
	public bool bTrackTargetPawnLocation;
	public bool bUseBoneLocationBasis;
	public FName LocationBasisBone;
	public ResolvedObject PlayerCameraAnimTemplate;
}