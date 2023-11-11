using System.Data;
using System.Linq;
using System.Windows;

using HtmlAgilityPack;

namespace Xylia.Preview.UI.Documents;
/// <summary>
/// Paragraph element 
/// </summary>
public class Paragraph : Element
{
	#region Constructor
	public Paragraph()
	{
		this.Children = new();
	}

	public Paragraph(string InnerText) : this()
	{
		if (InnerText is null) return;

		var doc = new HtmlDocument();
		doc.LoadHtml(InnerText.Replace("\n", "<br/>"));

		this.Children = doc.DocumentNode.ChildNodes.Select(TextDocument.ToElement)
			.Where(x => x is not null)
			.ToList();
	}
	#endregion

	#region Dependency Properties
	/// <summary>
	/// HorizontalContentAlignment Dependency Property.
	///     Flags:              Can be used in style rules
	///     Default Value:      HorizontalAlignment.Left
	/// </summary>
	public static readonly DependencyProperty HorizontalAlignmentProperty =
		DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(Paragraph),
			new FrameworkPropertyMetadata(HorizontalAlignment.Left));

	/// <summary>
	///     The horizontal alignment of the control.
	///     This will only affect controls whose template uses the property
	///     as a parameter. On other controls, the property will do nothing.
	/// </summary>
	public HorizontalAlignment HorizontalAlignment
	{
		get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
		set { SetValue(HorizontalAlignmentProperty, value); }
	}

	/// <summary>
	/// VerticalAlignment Dependency Property.
	/// </summary>
	public static readonly DependencyProperty VerticalAlignmentProperty =
		DependencyProperty.Register("VerticalAlignment", typeof(VerticalAlignment), typeof(Paragraph),
			  new FrameworkPropertyMetadata(VerticalAlignment.Top));

	/// <summary>
	///     The vertical alignment of the control.
	///     This will only affect controls whose template uses the property
	///     as a parameter. On other controls, the property will do nothing.
	/// </summary>
	public VerticalAlignment VerticalAlignment
	{
		get { return (VerticalAlignment)GetValue(VerticalAlignmentProperty); }
		set { SetValue(VerticalAlignmentProperty, value); }
	}
	#endregion

	#region Public Properties
	public float TopMargin;
	public float Leftmargin;
	public float RightMargin;
	public float BottomMargin;

	public bool Justification;
	public JustificationTypeSeq JustificationType;
	public enum JustificationTypeSeq
	{
		LineFeedByWidgetArea,
		LineFeedByLineArea,
	}


	public string Bullets;
	public string BulletsFontset;
	#endregion

	#region Protected Methods
	protected override void Load(HtmlNode node)
	{
		base.Load(node);

		if (Bullets != null) Children.Insert(0, new Font(BulletsFontset, new Run(Bullets)));
	}

	protected override Size MeasureCore(Size availableSize)
	{
		var size = base.MeasureCore(availableSize);

		if (TopMargin != 0) size.Height += TopMargin;
		if (BottomMargin != 0) size.Height += BottomMargin;
		if (Leftmargin != 0) size.Width += Leftmargin;
		if (RightMargin != 0) size.Width += RightMargin;

		return size;
	}

	internal Vector ComputeAlignmentOffset(Size clientSize, Size inkSize)
	{
		Vector offset = new Vector();

		HorizontalAlignment ha = HorizontalAlignment;
		VerticalAlignment va = VerticalAlignment;

		//this is to degenerate Stretch to Top-Left in case when clipping is about to occur
		//if we need it to be Center instead, simply remove these 2 ifs
		if (ha == HorizontalAlignment.Stretch && inkSize.Width > clientSize.Width)
		{
			ha = HorizontalAlignment.Left;
		}

		if (va == VerticalAlignment.Stretch && inkSize.Height > clientSize.Height)
		{
			va = VerticalAlignment.Top;
		}
		//end of degeneration of Stretch to Top-Left

		if (ha == HorizontalAlignment.Center || ha == HorizontalAlignment.Stretch)
		{
			offset.X = (clientSize.Width - inkSize.Width) * 0.5;
		}
		else if (ha == HorizontalAlignment.Right)
		{
			offset.X = clientSize.Width - inkSize.Width;
		}
		else
		{
			offset.X = 0;
		}

		if (va == VerticalAlignment.Center || va == VerticalAlignment.Stretch)
		{
			offset.Y = (clientSize.Height - inkSize.Height) * 0.5;
		}
		else if (va == VerticalAlignment.Bottom)
		{
			offset.Y = clientSize.Height - inkSize.Height;
		}
		else
		{
			offset.Y = 0;
		}

		return offset;
	}
	#endregion
}