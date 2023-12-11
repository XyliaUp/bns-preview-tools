using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using CUE4Parse.BNS.Assets.Exports;

using SkiaSharp.Views.WPF;

using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.UI.Documents;
public class Image : Element
{
	#region Fields
	public string Imagesetpath;
	public string Path;

	/// <summary>
	/// Relative to line height
	/// </summary>
	public bool Enablescale;
	public float Scalerate = 1;

	public int U;
	public int V;
	public int UL;
	public int VL;
	public int Width;
	public int Height;
	#endregion

	#region UIElement 
	private BitmapSource Source { get; set; }

	protected override Size MeasureCore(Size availableSize)
	{
		// keep empty space
		var image = FileCache.Provider.LoadObject<UImageSet>(Imagesetpath)?.GetImage();
		if (image is null) return new Size(5, 5);

		Source = image.ToWriteableBitmap();
		double width = Source.Width;
		double height = Source.Height;

		if (Enablescale)
		{
			height = FontSize * Scalerate;
			width *= height / Source.Height;
		}

		return new Size(width, height);
	}

	internal override void Render(DrawingContext ctx)
	{
		if (Source != null)
			ctx.DrawImage(Source, FinalRect);
	}
	#endregion
}