using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowCameraKey : ShowKeyBase
{
	public bool bCasterCenter;
	public bool bFOVRandom;
	public bool bLocRandom;
	public bool bRotRandom;
	public bool bUseRadius;
	public bool bWorldTargetPos;
	public string CamAppliedObject;  // CAM_CASTER_TARGET (EnumProperty)
	public string CamModifierType;  // CAM_MODIFIER_MOVE (EnumProperty)
	public bool Loop;
	public FVector Position;
	public int ProbabilityPercentage;
	public float ShakeRadius;


	public FStructFallback FovAdjust;
	public float FOVAmplitude;
	public float FOVFrequency;
	public FStructFallback LocAdjust;
	public float LocAmplitude;
	public float LocFrequency;
	public FStructFallback RotAdjust;
	public float RotAmplitude;
	public float RotFrequency;
}