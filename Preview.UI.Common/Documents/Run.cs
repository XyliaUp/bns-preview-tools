using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using HtmlAgilityPack;

namespace Xylia.Preview.UI.Documents;

/// <summary>
/// A terminal element in text flow hierarchy - contains a uniformatted run of unicode characters
/// </summary>
[ContentProperty("Text")]
public class Run : BaseElement
{
	#region Constructors
	/// <summary>
	/// Initializes an instance of Run class.
	/// </summary>
	public Run()
	{

	}

	/// <summary>
	/// Initializes an instance of Run class specifying its text content.
	/// </summary>
	/// <param name="text">
	/// Text content assigned to the Run.
	/// </param>
	public Run(string text)
	{
		this.Text = text;
	}
	#endregion


	#region Properties
	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
		typeof(string), typeof(Run), new FrameworkPropertyMetadata(string.Empty,
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

	protected override Size MeasureCore(Size availableSize)
	{
		_format = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
			new(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Foreground, 96);

		//_format.SetTextDecorations();
		_format.MaxTextWidth = double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width;
		_format.TextAlignment = this.TextAlignment;  //EnsureTextContainer().StringProperty.HorizontalAlignment

		return new Size(_format.WidthIncludingTrailingWhitespace, _format.Height);
	}

	protected internal override void OnRender(DrawingContext ctx)
	{
		// Draw the formatted text string to the DrawingContext of the control.
		if (_format != null)
		{
			ctx.DrawText(_format, this.FinalRect.TopLeft);
		}
	}
	#endregion

	#region Private Fields
	private FormattedText? _format;
	#endregion
}