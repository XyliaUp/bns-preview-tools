using System;
using System.Drawing;

using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;

using Xylia.Extension;
using Xylia.Preview.Common.Struct;

namespace CUE4Parse.BNS;

public static class Convert
{
	public static UObject GetUObject(this string Path) => FileCache.PakData.GetObject(Path);

	public static UObject GetUObject(this FSoftObjectPath Path) => FileCache.PakData.GetObject(Path.AssetPathName.Text);

	public static UObject GetUObject(this UObject obj) => FileCache.PakData.GetObject(obj);





	public static Bitmap GetImage(this UObject o)
	{
		if (o is null) return null;
		else if (o is UTexture2D texture)
		{
			var bitmap = texture.Decode();
			if (bitmap is not null) return new Bitmap(bitmap.Encode(SkiaSharp.SKEncodedImageFormat.Png, 100).AsStream());
		}
		else if (o.ExportType == "ImageSet")
		{
			if (!o.TryGetValue(out UObject Image, "Image")) return null;

			var u = (int)o.GetOrDefault<float>("U");
			var v = (int)o.GetOrDefault<float>("V");
			var ul = (int)o.GetOrDefault<float>("UL");
			var vl = (int)o.GetOrDefault<float>("VL");

			return Image.GetUObject().GetImage()
				.GetSubImage(u, v, ul, vl);
		}

		return null;
	}

	public static Bitmap GetSubImage(this Bitmap source, int u, int v, int ul, int vl)
	{
		if (ul == 0) ul = source.Width - u;
		if (vl == 0) vl = source.Height - v;

		var output = new Bitmap(ul, vl);
		Graphics.FromImage(output).DrawImage(source, 0, 0, new Rectangle(u, v, ul, vl), GraphicsUnit.Pixel);
		return output;
	}



	public static FontSet GetFont(this UObject UFontSet)
	{
		var result = new FontSet();

		#region FontFace
		UFontSet.TryGetValue(out UObject FontFace, "FontFace");
		FontFace = FontFace.GetUObject();
		if (FontFace != null)
		{
			result.Height = FontFace.GetOrDefault<float>("Height");
		}
		#endregion

		#region FontAttribute
		UFontSet.TryGetValue(out UObject FontAttribute, "FontAttribute");
		FontAttribute = FontAttribute.GetUObject();
		if (FontAttribute != null)
		{
			FontAttribute.TryGetValue(out FStructFallback FontAttributes, "FontAttributes");
			result.Italic = FontAttributes.GetOrDefault<bool>("Italic");
			result.Shadow = FontAttributes.GetOrDefault<bool>("Shadow");
			result.Strokeout = FontAttributes.GetOrDefault<bool>("Strokeout");
		}
		#endregion

		#region FontColors
		UFontSet.TryGetValue(out UObject FontColors, "FontColors");
		FontColors = FontColors.GetUObject();
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

	public static byte[] GetWave(this UObject Object, int ReferenceIdx = 0)
	{
		//System.Diagnostics.Debug.WriteLine(Object.GetPathName_Bns());


		if (Object is null) return null;
		else if (Object is USoundWave soundWave)
		{
			Object.Decode(true, out var audioFormat, out var data);
			return data;
		}
		else if (Object.ExportType == "SoundCue")
		{
			var FirstNode = Object.GetOrDefault<UObject>("FirstNode").GetUObject();
			var ChildNodes = FirstNode.GetOrDefault<UObject[]>("ChildNodes");
			foreach (var ChildNode in ChildNodes)
			{
				var obj = ChildNode.GetUObject();
				return obj.GetOrDefault<FSoftObjectPath>("SoundWaveAssetPtr").GetUObject().GetWave();
			}
		}
		else if (Object.ExportType == "ShowSoundKey") return Object.GetOrDefault<UObject>("SoundCue").GetUObject().GetWave();

		else if (Object.ExportType == "ShowFaceFxKey") return Object.GetOrDefault<string>("FaceFXAnimSetName").GetUObject().GetWave(ReferenceIdx);
		else if (Object.ExportType == "LegacyFaceFXAnimSet") return Object.GetOrDefault<UObject[]>("ReferencedSoundCues")[ReferenceIdx].GetUObject().GetWave();

		else if (Object.ExportType == "ShowFaceFxUE4Key") return Object.GetOrDefault<FSoftObjectPath>("FaceFXAnimObj").GetUObject().GetWave();
		else if (Object.ExportType == "FaceFXAnim") return Object.GetOrDefault<FSoftObjectPath>("SoundCue").GetUObject().GetWave();
		else if (Object.ExportType == "ShowObject")
		{
			var EventKeys = Object.GetOrDefault<UObject[]>("EventKeys");
			if (EventKeys is null) return null;

			foreach (var _eventKey in EventKeys)
			{
				if (_eventKey.ExportType == "ShowSoundKey") return _eventKey.GetUObject().GetWave();
				else if (_eventKey.ExportType == "ShowFaceFxUE4Key") return _eventKey.GetUObject().GetWave();
				else if (_eventKey.ExportType == "ShowFaceFxKey") return _eventKey.GetUObject().GetWave(ReferenceIdx);
			}
		}

		return null;
	}
}