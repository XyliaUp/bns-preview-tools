namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowMaterialKey : ShowKeyBase
{
	public string ApplyPriority; // MaterialPriority_High (EnumProperty)
	public string ApplyType;     // MaterialChange (EnumProperty)
	public string[] arrDependencyTrailName;
	public int[] arrMeshIndex;
	public string[] arrMeshIndexEnum;  // EnumProperty
	public bool bHideFaceMeshBeard;
	public bool bHideFaceMeshEye;
	public float fValue;
	public bool Loop;
	public string strName;
}