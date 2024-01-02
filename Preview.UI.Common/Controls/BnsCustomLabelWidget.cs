using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using Xylia.Preview.UI.Documents;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomLabelWidget : BnsCustomBaseWidget, IContentHost
{
	#region Ctor
	static BnsCustomLabelWidget()
	{
		// some tag is special in real html
		// those are non-closed, so must to re-set
		HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("link");
	}

	public BnsCustomLabelWidget()
	{
		// Create TextContainer associated with it
		TextDocument container = new TextDocument(this, true);
		//container.CollectTextChanges = true;
		_container = container;
		_container.Changed += new EventHandler(OnContainerChanged);

		Document = new();
	}
	#endregion


	#region Public Properties
	private TextDocument _container;

	/// <summary>
	/// content container
	/// </summary>
	protected Paragraph Document { get; set; }

	public Dictionary<int, Timer> Timers { get; } = [];
	#endregion

	#region Dependency Properties
	public static CopyMode CopyMode;

	/// <summary>
	/// DependencyProperty for <see cref="Text" /> property.
	/// </summary>
	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
		typeof(string), typeof(BnsCustomLabelWidget),
		  new FrameworkPropertyMetadata(string.Empty,
			  FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnTextChanged));

	/// <summary>
	/// The Text property defines the content (text) to be displayed.
	/// </summary>
	[Localizability(LocalizationCategory.Text)]
	public string Text
	{
		get { return (string)GetValue(TextProperty); }
		set { SetValue(TextProperty, value); }
	}

	public static readonly DependencyProperty ParamsProperty = DependencyProperty.Register("Params",
		typeof(DataParams), typeof(BnsCustomLabelWidget),
		  new FrameworkPropertyMetadata(new DataParams(),
			  FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits, OnParamsChanged));

	public DataParams Params
	{
		get { return (DataParams)GetValue(ParamsProperty); }
		set { SetValue(ParamsProperty, value); }
	}

	public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment",
		typeof(TextAlignment), typeof(BnsCustomLabelWidget),
		  new FrameworkPropertyMetadata(TextAlignment.Left,
			  FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

	public TextAlignment TextAlignment
	{
		get { return (TextAlignment)GetValue(TextAlignmentProperty); }
		set { SetValue(TextAlignmentProperty, value); }
	}
	#endregion


	#region Protected Methods
	protected override void SetText(string text) => this.Text = text;


	protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
	{
		base.OnMouseDoubleClick(e);

		var text = CopyMode switch
		{
			CopyMode.None => null,
			CopyMode.Trimmed => CutText(Params.Handle(Text)),
			CopyMode.Regular => Params.Handle(Text),
			CopyMode.Original => Text,

			_ => throw new NotSupportedException(),
		};

		if (string.IsNullOrWhiteSpace(text)) return;
		Clipboard.SetText(text);
	}

	protected override Size MeasureOverride(Size constraint)
	{
		if (Document is null) return new Size();

		Element.InheritDependency(this, Document);
		Document.Measure(constraint);
		return Document.DesiredSize;
	}

	protected override Size ArrangeOverride(Size arrangeBounds)
	{
		base.ArrangeOverride(arrangeBounds);

		Document?.Arrange(new Rect(arrangeBounds));
		return arrangeBounds;
	}

	protected override void OnRender(DrawingContext ctx)
	{
		ArgumentNullException.ThrowIfNull(ctx);

		// Draw background in rectangle.
		Brush background = Background;
		if (background != null) ctx.DrawRectangle(background, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));

		// Render child elements
		Document.Render(ctx);
	}


	//-------------------------------------------------------------------
	//
	//  IContentHost
	//
	//-------------------------------------------------------------------
	protected sealed override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
	{
		ArgumentNullException.ThrowIfNull(hitTestParameters);

		Rect r = new Rect(new Point(), RenderSize);

		if (r.Contains(hitTestParameters.HitPoint))
		{
			return new PointHitTestResult(this, hitTestParameters.HitPoint);
		}
		return null;
	}

	/// <summary>
	/// Hit tests to the correct ContentElement within the ContentHost
	/// that the mouse is over.
	/// </summary>
	/// <param name="point">Mouse coordinates relative to the ContentHost.</param>
	IInputElement IContentHost.InputHitTest(Point point)
	{
		if (point.X < 0 || point.Y < 0) return this;

		IInputElement ie = null;

		// retrieve IInputElement from the hit position.
		ie = Document.InputHitTest(point);

		// If nothing has been hit, assume that element itself has been hit.
		return ie ?? this;
	}

	/// <summary>
	/// Returns an ICollection of bounding rectangles for the given ContentElement
	/// </summary>
	/// <param name="child">
	/// Content element for which rectangles are required
	/// </param>
	/// <remarks>
	/// Looks at the ContentElement e line by line and gets rectangle bounds for each line
	/// </remarks>
	public ReadOnlyCollection<Rect> GetRectangles(ContentElement child)
	{
		return null;


		//ArgumentNullException.ThrowIfNull(child);

		//// If layout data is not updated we assume that we will not be able to find the element we need and throw excception
		//if (!IsLayoutDataValid)
		//{
		//	// return empty collection
		//	return new ReadOnlyCollection<Rect>(new List<Rect>(0));
		//}

		//// Line props may be invalid, even if Measure/Arrange is valid - rendering only props are changing.
		//LineProperties lineProperties = GetLineProperties();

		//// Check for complex content
		//if (_complexContent == null || !(_complexContent.TextContainer is TextContainer))
		//{
		//	// return empty collection
		//	return new ReadOnlyCollection<Rect>(new List<Rect>(0));
		//}

		//// First find the element start and end position
		//TextPointer start = FindElementPosition((IInputElement)child);
		//if (start == null)
		//{
		//	return new ReadOnlyCollection<Rect>(new List<Rect>(0));
		//}

		//TextPointer end = null;
		//if (child is TextElement)
		//{
		//	end = new TextPointer(((TextElement)child).ElementEnd);
		//}
		//else if (child is FrameworkContentElement)
		//{
		//	end = new TextPointer(start);
		//	end.MoveByOffset(+1);
		//}

		//if (end == null)
		//{
		//	return new ReadOnlyCollection<Rect>(new List<Rect>(0));
		//}

		//int startOffset = _complexContent.TextContainer.Start.GetOffsetToPosition(start);
		//int endOffset = _complexContent.TextContainer.Start.GetOffsetToPosition(end);

		//int lineIndex = 0;
		//int lineOffset = 0;
		//double lineHeightOffset = 0;
		//int lineCount = LineCount;
		//while (startOffset >= (lineOffset + GetLine(lineIndex).Length) && lineIndex < lineCount)
		//{
		//	Debug.Assert(lineCount == LineCount);
		//	lineOffset += GetLine(lineIndex).Length;
		//	lineIndex++;
		//	lineHeightOffset += GetLine(lineIndex).Height;
		//}
		//Debug.Assert(lineIndex < lineCount);

		//int lineStart = lineOffset;
		//List<Rect> rectangles = new List<Rect>();
		//double wrappingWidth = CalcWrappingWidth(RenderSize.Width);

		//TextRunCache textRunCache = new TextRunCache();

		//Vector contentOffset = CalcContentOffset(RenderSize, wrappingWidth);
		//do
		//{
		//	Debug.Assert(lineCount == LineCount);
		//	// Check that line index never exceeds line count
		//	Debug.Assert(lineIndex < lineCount);

		//	// Create lines as long as they are spanned by the element
		//	LineMetrics lineMetrics = GetLine(lineIndex);

		//	Line line = CreateLine(lineProperties);

		//	using (line)
		//	{
		//		// Check if paragraph ellipsis are rendered
		//		bool ellipsis = ParagraphEllipsisShownOnLine(lineIndex, lineOffset);
		//		Format(line, lineMetrics.Length, lineStart, wrappingWidth, GetLineProperties(lineIndex == 0, lineProperties), lineMetrics.TextLineBreak, textRunCache, ellipsis);

		//		// Verify consistency of line formatting
		//		// Workaround for (Crash when mouse over a Button with TextBlock). Re-enable this assert when MIL Text issue is fixed.
		//		if (lineMetrics.Length == line.Length)
		//		{
		//			//MS.Internal.Invariant.Assert(lineMetrics.Length == line.Length, "Line length is out of sync");
		//			//Debug.Assert(DoubleUtil.AreClose(CalcLineAdvance(line.Height, lineProperties), lineMetrics.Height), "Line height is out of sync.");

		//			int boundStart = (startOffset >= lineStart) ? startOffset : lineStart;
		//			int boundEnd = (endOffset < lineStart + lineMetrics.Length) ? endOffset : lineStart + lineMetrics.Length;

		//			double xOffset = contentOffset.X;
		//			double yOffset = contentOffset.Y + lineHeightOffset;
		//			List<Rect> lineBounds = line.GetRangeBounds(boundStart, boundEnd - boundStart, xOffset, yOffset);
		//			Debug.Assert(lineBounds.Count > 0);
		//			rectangles.AddRange(lineBounds);
		//		}
		//	}

		//	lineStart += lineMetrics.Length;
		//	lineHeightOffset += lineMetrics.Height;
		//	lineIndex++;
		//}
		//while (endOffset > lineStart);

		//// Rectangles collection must be non-null
		//Invariant.Assert(rectangles != null);
		//return new ReadOnlyCollection<Rect>(rectangles);
	}

	/// <summary>
	/// Returns elements hosted by the content host as an enumerator class
	/// </summary>
	public IEnumerator<IInputElement> HostedElements
	{
		get
		{
			return this.Document.Children.GetEnumerator();

			//if (CheckFlags(Flags.ContentChangeInProgress))
			//{
			//	throw new InvalidOperationException(SR.TextContainerChangingReentrancyInvalid);
			//}

			//if (_complexContent == null || !(_complexContent.TextContainer is TextContainer))
			//{
			//	// Return empty collection
			//	return new HostedElements(new ReadOnlyCollection<TextSegment>(new List<TextSegment>(0)));
			//}

			//// Create a TextSegment from TextContainer, use it to return enumerator
			//System.Collections.Generic.List<TextSegment> textSegmentsList = new System.Collections.Generic.List<TextSegment>(1);
			//TextSegment textSegment = new TextSegment(_complexContent.TextContainer.Start, _complexContent.TextContainer.End);
			//textSegmentsList.Insert(0, textSegment);
			//ReadOnlyCollection<TextSegment> textSegments = new ReadOnlyCollection<TextSegment>(textSegmentsList);

			//// Return enumerator created from textSegments
			//return new HostedElements(textSegments);
		}
	}

	void IContentHost.OnChildDesiredSizeChanged(UIElement child) => this.OnChildDesiredSizeChanged(child);
	#endregion


	#region Private Methods
	private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomLabelWidget)d;
		var text = (string)e.NewValue;

		widget.Document = text is null ? new Paragraph() :
			new Paragraph(text.Replace("\n", "<br/>"));
	}

	private static void OnParamsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomLabelWidget)d;
		var Params = (DataParams)e.NewValue;

		// itself is a change of params
		var handler = new EventHandler(widget.OnContainerChanged);
		handler.Invoke(widget, EventArgs.Empty);
		Params.Changed += handler;
	}

	private void OnContainerChanged(object? sender, EventArgs e)
	{
		InvalidateMeasure();
		InvalidateVisual();
	}

	private static string CutText(string text)
	{
		if (text is null) return null;

		var CopyTxt = WebUtility.HtmlDecode(text);
		CopyTxt = new Regex(@"<\s*br\s*/\s*>").Replace(CopyTxt, "\n");
		return new Regex(@"<.*?>").Replace(CopyTxt, "");
	}
	#endregion
}

public enum CopyMode
{
	None,

	/// <summary>
	/// Text after replacing parameters and remove other XML parameters 
	/// </summary>
	Trimmed,

	/// <summary>
	/// Text after replacing parameters
	/// </summary>
	Regular,

	/// <summary>
	/// original text
	/// </summary>
	Original,
}