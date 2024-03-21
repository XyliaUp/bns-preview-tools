using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse_Conversion.Textures;
using HtmlAgilityPack;
using SkiaSharp.Views.WPF;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.UI.Documents;
public class Image : BaseElement
{
	#region Fields
	public string? Imagesetpath { get; set; }
	public string? Path { get; set; }

	/// <summary>
	/// Relative to line height
	/// </summary>
	public bool Enablescale { get; set; }
	public float Scalerate { get; set; }

	public int U { get; set; }
	public int V { get; set; }
	public int UL { get; set; }
	public int VL { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }

	internal BitmapSource? Source;
	#endregion

	#region UIElement 
	protected internal override void Load(HtmlNode node)
	{
		Path = node.Attributes["path"]?.Value;
		Imagesetpath = node.Attributes["imagesetpath"]?.Value;
		Enablescale = node.GetAttributeValue("enablescale", false);
		Scalerate = node.GetAttributeValue("scalerate", 1f);

		U = node.GetAttributeValue("u", 0);
		V = node.GetAttributeValue("v", 0);
		UL = node.GetAttributeValue("ul", 0);
		VL = node.GetAttributeValue("vl", 0);
		Width = node.GetAttributeValue("width", 0);
		Height = node.GetAttributeValue("height", 0);
	}

	protected override Size MeasureCore(Size availableSize)
	{
		var image = FileCache.Provider.LoadObject<UImageSet>(Imagesetpath)?.GetImage() ?? 
			FileCache.Provider.LoadObject<UTexture2D>(Path)?.Decode()?.Clone(U, V, UL, VL);

		Source = image?.ToWriteableBitmap();
		if (Source is null) return new Size(); 

		double width = Source.Width;
		double height = Source.Height;

		if (Enablescale)
		{
			height = FontSize * Scalerate;
			width *= height / Source.Height;
		}

		return new Size(width, height);
	}

	protected internal override void OnRender(DrawingContext ctx)
	{
		if (Source != null)
			ctx.DrawImage(Source, FinalRect);
	}
	#endregion
}