using CUE4Parse.UE4.Assets.Utils;

namespace CUE4Parse.BNS.Assets.Exports;
[StructFallback]
public struct ResizeLink
{
	public bool bEnable;
	public string Type;

	public float Offset1;
	public string LinkWidgetName1;
}