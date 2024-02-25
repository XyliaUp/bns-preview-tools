using CUE4Parse.UE4.Objects.Core.Math;
using SkiaSharp;

namespace CUE4Parse.UE4.Objects.Core.Serialization;
public static class FLinearColorEx
{
	public static SKColor ToSKColor(this FLinearColor color)
	{
		var f = color.ToFColor(true);
		return new SKColor(f.R, f.G, f.B, f.A);
	}
}