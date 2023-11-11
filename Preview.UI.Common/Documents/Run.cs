﻿using System.Globalization;
using System.Web;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

using HtmlAgilityPack;

namespace Xylia.Preview.UI.Documents;

[ContentProperty("Text")]
public class Run : Element
{
	#region Constructor
	public Run()
	{

	}

	public Run(string text)
	{
		this.Text = text;
	}
	#endregion

	#region Properties
	public static readonly DependencyProperty TextProperty =
		DependencyProperty.Register("Text", typeof(string), typeof(Run),
			 new FrameworkPropertyMetadata(string.Empty,
				 FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

	public string Text
	{
		get { return (string)GetValue(TextProperty); }
		set { SetValue(TextProperty, value); }
	}
	#endregion

	#region Methods
	protected override void Load(HtmlNode node)
	{
		Text = HttpUtility.HtmlDecode(XmlConvert.DecodeName(node.InnerText));
	}


	private Typeface typeface => new(FontFamily, FontStyle, FontWeight, FontStretch);

	protected override Size MeasureCore(Size availableSize)
	{
		var format = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, FontSize, Foreground, 96);
		format.MaxTextWidth = double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width;

		return new Size(format.Width, format.Height);
	}

	internal override void Render(DrawingContext ctx)
	{
		var format = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, FontSize, Foreground, 96);
		format.MaxTextWidth = DesiredSize.Width;

		// Draw the formatted text string to the DrawingContext of the control.
		ctx.DrawText(format, new Point(this.FinalRect.X, this.FinalRect.Y));
	}
	#endregion
}