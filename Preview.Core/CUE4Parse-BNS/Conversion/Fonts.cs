using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;

using Xylia.Preview.Common.Struct;

namespace CUE4Parse.BNS.Conversion;
public static class Fonts
{
	public static FontSet GetFont(this UObject UFontSet)
	{
		var result = new FontSet();

		#region FontFace
		var FontFace = UFontSet.GetOrDefault<ResolvedObject>("FontFace")?.Load();
		if (FontFace != null)
		{
			result.Height = FontFace.GetOrDefault<float>("Height");
		}
		#endregion

		#region FontAttribute
		var FontAttribute = UFontSet.GetOrDefault<ResolvedObject>("FontAttribute")?.Load();
		if (FontAttribute != null)
		{
			FontAttribute.TryGetValue(out FStructFallback FontAttributes, "FontAttributes");
			result.Italic = FontAttributes.GetOrDefault<bool>("Italic");
			result.Shadow = FontAttributes.GetOrDefault<bool>("Shadow");
			result.Strokeout = FontAttributes.GetOrDefault<bool>("Strokeout");
		}
		#endregion

		#region FontColors
		var FontColors = UFontSet.GetOrDefault<ResolvedObject>("FontColors")?.Load();
		if (FontColors != null)
		{
			var FontColor = FontColors.GetOrDefault<FLinearColor>("FontColor");
			result.Color = Color.FromArgb((byte)Math.Round(FontColor.A * 255),
				(byte)Math.Round(FontColor.R * 255),
				(byte)Math.Round(FontColor.G * 255),
				(byte)Math.Round(FontColor.B * 255));
		}
		#endregion

		return result;
	}
}