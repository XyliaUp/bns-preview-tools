using System.Windows;
using CUE4Parse.BNS.Assets.Exports;
using HtmlAgilityPack;
using SkiaSharp.Views.WPF;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Documents;
public class Arg : BaseElement
{
	#region Fields
	public string? P { get; set; }
	public string? Id { get; set; }
	public string? Seq { get; set; }

	TextArguments.Argument Argument;
	#endregion

	#region Methods
	protected internal override void Load(HtmlNode node)
	{
		this.P = node.Attributes["p"]?.Value;
		this.Id = node.Attributes["id"]?.Value;
		this.Seq = node.Attributes["seq"]?.Value;
		this.Argument = new TextArguments.Argument(P, Id, Seq);
	}

	protected override Size MeasureCore(Size availableSize)
	{
		this.Children = [];

		var result = Argument.GetObject(this.Arguments);
		if (result is null) return new Size();
		else if (result is ImageProperty bitmap) Children.Add(new Image() { Source = bitmap.Image?.ToWriteableBitmap() });
		else if (result is int @int) Children.Add(new Run() { Text = @int.ToString("N0") });
		else if (result is not null) Children.Add(new Paragraph(result.ToString()));

		return base.MeasureCore(availableSize);
	}
	#endregion
}