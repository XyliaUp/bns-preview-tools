using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowDependencyTrailKey : ShowKeyBase
{
    public string Add_FirstSocketName;
	public string Add_SecondSamplerType;  // TPST_Bone (EnumProperty)
	public string Add_SecondSocketName;
	public FPackageIndex Add_TrailSystem;
	public bool Add_UsingDependencyRope;
	public bool bAddDependencyTrails;
	public bool bHideDependencyTrails;
	public bool bTermDependencyTrails;
	public string DependencyTrailName;
	public bool EnableDynamicRopeWidth;
	public bool EnableDynamicWrappingScaler;
	public bool EnableTrailPushForce;
	public string HoldingTrailSampleBoneName;
	public int HoldingTrailSampleIndex;
	public int HoldingTrailSampleMeshIndex;
	public FStructFallback HorizontalRopeParticles_Dist;
	public bool Loop;
	public FStructFallback RopeWidth_Dist;
	public float TrailPushForceBound;
	public FVector TrailPushForceDirection_Max;
	public FVector TrailPushForceDirection_Min;
	public float TrailPushForceTerm_Max;
	public float TrailPushForceTerm_Min;
	public float TrailPushForceTime_Max;
	public float TrailPushForceTime_Min;
	public float TrailPushForceValue_Max;
	public float TrailPushForceValue_Min;
	public FStructFallback WrappingAddition_Dist;
	public FStructFallback WrappingScaler_Dist;
}