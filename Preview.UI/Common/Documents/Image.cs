using System.Windows.Media;
using System.Windows.Media.Imaging;

using CUE4Parse.BNS.Exports;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Extension;

namespace Xylia.Preview.UI.Common.Documents;
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
	private BitmapSource _source { get; set; }

	protected override Size MeasureCore(Size availableSize)
	{
		var image = FileCache.Provider.LoadObject<UImageSet>(Imagesetpath)?.GetImage();
		if (image is null) return new Size();


		_source = image.ToImageSource();
		double width = _source.Width;
		double height = _source.Height;

		if (Enablescale)
		{
			height = FontSize * Scalerate;
			width *= height / _source.Height;
		}

		return new Size(width, height);
	}

	internal override void Render(DrawingContext ctx)
	{
		if (_source != null)
			ctx.DrawImage(_source, FinalRect);
	}
	#endregion
}