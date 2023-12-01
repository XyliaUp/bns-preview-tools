using CUE4Parse.UE4.Assets.Objects;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowPointLightKey : ShowKeyBase
{
	public string AffectType; // Affect_Dynamic(EnumProperty)
	public FStructFallback Brightness;
	public float DurationTime;
	public FStructFallback LightColor;
	public FStructFallback Offset;
	public FStructFallback Radius;
}