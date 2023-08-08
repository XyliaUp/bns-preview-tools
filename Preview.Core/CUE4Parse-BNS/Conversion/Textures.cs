using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Texture;

using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;

using Xylia.Extension;

namespace CUE4Parse.BNS.Conversion;
public static class Textures
{
	public static Bitmap GetImage(this UObject o)
	{
		if (o is null) return null;
		else if (o is UTexture texture)
		{
			var bitmap = texture.Decode();
			if (bitmap is not null) return new Bitmap(bitmap.Encode(SkiaSharp.SKEncodedImageFormat.Png, 100).AsStream());
		}
		else if (o is UImageSet ImageSet)
		{
			return ImageSet.GetImage();
		}

		return null;
	}

	public static Bitmap Clone(this Bitmap source, int u, int v, int ul, int vl)
	{
		if (ul == 0) ul = source.Width - u;
		if (vl == 0) vl = source.Height - v;

		var output = new Bitmap(ul, vl);
		Graphics.FromImage(output).DrawImage(source, 0, 0, new Rectangle(u, v, ul, vl), GraphicsUnit.Pixel);
		return output;
	}
}