using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowDynamicParamKey : ShowKeyBase
{
	public string[] ApplyDependencyTrailNames;
	public FStructFallback ApplyMeshes;
	public int[] ApplyMeshIndices;
	public float fPlayTime;
	public bool IsAddMaterial;
	public FName MaterialName;
	public FName ParameterName;
	public string PlayType;  // DYNAMICPARAMPLAY_QuickInit (EnumProperty)
	public int ProbabilityPercentage;
	public string ShowKeyId;  // SHOW_KEY_DAMAGE (EnumProperty)
	public FVector vecEndValue;
	public FVector vecInitValue;
	public FVector vecStartValue;
}