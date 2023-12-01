using CUE4Parse.UE4.Objects.Core.Math;

using SkiaSharp;

namespace CUE4Parse.BNS.Conversion;
public static class Textures
{
	public static SKBitmap Clone(this SKBitmap source, float u, float v, float ul, float vl)
	{
		if (ul == 0) ul = source.Width - u;
		if (vl == 0) vl = source.Height - v;

		var output = new SKBitmap((int)Math.Ceiling(ul), (int)Math.Ceiling(vl));
		for (int i = 0; i < ul; i++)
		{
			if (source.Width < u + i) continue;

			for (int j = 0; j < vl; j++)
			{
				if (source.Height < v + j) continue;

				output.SetPixel(i, j, source.GetPixel((int)(u + i), (int)(v + j)));
			}
		}

		return output;
	}

	public static SKBitmap Clone(this SKBitmap source, FVector2D UV, FVector2D UVSize) => source.Clone(UV.X, UV.Y, UVSize.X, UVSize.Y);


	public static SKBitmap Compose(this SKBitmap imgBack, SKBitmap img)
	{
		if (imgBack is null) return null;
		if (img is null) return imgBack;

		var bitmap = new SKBitmap(imgBack.Width, imgBack.Height);

		using var bitmapCanvas = new SKCanvas(bitmap);
		bitmapCanvas.DrawBitmap(imgBack, 0, 0);
		bitmapCanvas.DrawBitmap(img, new SKRect(0, 0, img.Width, img.Height), new SKRect(0, 0, bitmap.Width, bitmap.Height));

		return bitmap;
	}


	public static void Save(this SKBitmap source, string path, SKEncodedImageFormat format = SKEncodedImageFormat.Png)
	{
		File.WriteAllBytes(path, source.Encode(format, 100).ToArray());
	}
}