using System.Drawing;
using System.Windows.Media.Imaging;

using SkiaSharp;

using Vanara.PInvoke;

namespace Xylia.Preview.UI.Extension;
public static partial class Drawing
{
	public static Bitmap ChangeColor(this Bitmap bitmap, Color Target)
	{
		bitmap = new Bitmap(bitmap);

		for (int i = 0; i < bitmap.Width; i++)
			for (int j = 0; j < bitmap.Height; j++)
				if (bitmap.GetPixel(i, j).A != 0) bitmap.SetPixel(i, j, Target);

		return bitmap;
	}


	public static Bitmap ToBitmap(this SKBitmap source)
	{
		if (source is null)
			return null;

		return new Bitmap(source.Encode(SKEncodedImageFormat.Png, 100).AsStream());
	}

	public static BitmapSource ToImageSource(this SKBitmap source)=> source.ToBitmap()?.ToImageSource();

	public static BitmapSource ToImageSource(this Bitmap bitmap)
	{
		IntPtr hBitmap = bitmap.GetHbitmap();
		var wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
			hBitmap,
			IntPtr.Zero,
			Int32Rect.Empty,
			BitmapSizeOptions.FromEmptyOptions());

		if (!Gdi32.DeleteObject(hBitmap))
			throw new System.ComponentModel.Win32Exception();

		return wpfBitmap;
	}
}