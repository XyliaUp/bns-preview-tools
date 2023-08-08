using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowPostProcKey : ShowKeyBase
{
	public FColor AmbientCharacter;
	public bool bAcceptStopEvent;
	public bool bAppliedPartyMember;
	public bool bMotionBlurSkinning;
	public bool bUseCustomWeatherTable;
	public bool bUseOverlay;
	public bool bUsePPColorCtrl;
	public float CharacterBright;
	public float ColorRatio;
	public float FadeInTime;
	public float FadeOutTime;
	public FColor LightCharacter;
	public bool Loop;
	public float MotionBlurSkinning_Duration;
	public float MotionBlurSkinning_VelocityScale;
	public ResolvedObject OverlayMaterial;
	public string PostprocAppliedObject;  // POSTPROC_CASTER_TARGET(EnumProperty)
	public ResolvedObject PostProcessParameter;
	public int Priority;
	public int ProbabilityPercentage;
	public float StaticBright;
	public float TerrainBright;
}