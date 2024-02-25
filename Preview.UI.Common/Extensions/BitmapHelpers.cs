using System.IO;
using System.Windows.Media.Imaging;

namespace Xylia.Preview.UI.Extensions;
public static class BitmapHelper
{
	public static byte[] AsData(this BitmapSource source)
	{
		// TODO: othor encoders
		using var ms = new MemoryStream();
		BitmapEncoder encoder = new PngBitmapEncoder();
		encoder.Frames.Add(BitmapFrame.Create(source));
		encoder.Save(ms);

		return ms.ToArray();
	}
}