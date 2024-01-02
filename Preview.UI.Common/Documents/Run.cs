using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

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
	protected internal override void Load(HtmlNode node)
	{
		Text = WebUtility.HtmlDecode(node.OuterHtml);
	}

	private Typeface typeface => new(FontFamily, FontStyle, FontWeight, FontStretch);

	protected override Size MeasureCore(Size availableSize)
	{
		var format = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, FontSize, Foreground, 96);
		format.MaxTextWidth = double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width;
		format.TextAlignment = TextAlignment.Center;

		return new Size(format.WidthIncludingTrailingWhitespace, format.Height);
	}

	internal override void Render(DrawingContext ctx)
	{
		var format = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, FontSize, Foreground, 96);
		format.MaxTextWidth = DesiredSize.Width;
		format.TextAlignment = this.TextAlignment;

		// Draw the formatted text string to the DrawingContext of the control.
		ctx.DrawText(format, new Point(this.FinalRect.X, this.FinalRect.Y));
	}
	#endregion
}