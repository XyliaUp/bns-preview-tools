using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Xylia.Preview.UI.Extensions;
public static class ControlHelpers
{
	public static byte[] Snapshot(this FrameworkElement visual)
	{
		var bmp = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
		bmp.Render(visual);

		using var ms = new MemoryStream();
		BitmapEncoder encoder = new PngBitmapEncoder();
		encoder.Frames.Add(BitmapFrame.Create(bmp));
		encoder.Save(ms);

		return ms.ToArray();
	}
}