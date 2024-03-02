using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Xylia.Preview.UI.Controls.Helpers;
using Xylia.Preview.UI.Controls.Primitives;
using Xylia.Preview.UI.Converters;
using Xylia.Preview.UI.Documents;
using Xylia.Preview.UI.Extensions;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomLabelWidget : BnsCustomBaseWidget, IContentHost
{
	#region Constructorss
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

	#region Dependency Properties
	private static readonly Type Owner = typeof(BnsCustomLabelWidget);

	public static CopyMode CopyMode;

	public static readonly DependencyProperty AutoSizeProperty = Owner.Register("AutoSize", false, FrameworkPropertyMetadataOptions.AffectsMeasure);

	/// <summary>
	/// Defines size mode of the widget.
	/// </summary>
	public bool AutoSize
	{
		get => (bool)GetValue(AutoSizeProperty);
		set => SetValue(AutoSizeProperty, BooleanBoxes.Box(value));
	}



	/// <summary>
	/// DependencyProperty for <see cref="Text" /> property.
	/// </summary>
	public static readonly DependencyProperty TextProperty = Owner.Register("Text", string.Empty,
		FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnTextChanged);

	/// <summary>
	/// The Text property defines the content (text) to be displayed.
	/// </summary>
	[Localizability(LocalizationCategory.Text)]
	public string Text
	{
		get { return (string)GetValue(TextProperty); }
		set { SetValue(TextProperty, value); }
	}

	public static readonly DependencyProperty TextAlignmentProperty = Owner.Register("TextAlignment", TextAlignment.Left,
		FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits);

	public TextAlignment TextAlignment
	{
		get { return (TextAlignment)GetValue(TextAlignmentProperty); }
		set { SetValue(TextAlignmentProperty, value); }
	}

	public static readonly DependencyProperty ParamsProperty = Owner.Register("Params", new DataParams(),
		 FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits, OnParamsChanged);

	public DataParams Params
	{
		get { return (DataParams)GetValue(ParamsProperty); }
		set { SetValue(ParamsProperty, value); }
	}
	#endregion

	#region Protected Methods
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

		BaseElement.InheritDependency(this, Document);

		// Measure content size, then update if value is invalid 
		var actualSize = Document.Measure(constraint);
		if (AutoSize || double.IsInfinity(constraint.Width)) constraint.Width = actualSize.Width;
		if (AutoSize || double.IsInfinity(constraint.Height)) constraint.Height = actualSize.Height;

		// children
		base.MeasureOverride(constraint);
		return constraint;
	}

	protected override Size ArrangeOverride(Size arrangeBounds)
	{
		base.ArrangeOverride(arrangeBounds);

		// document
		var rect = new Rect(arrangeBounds);
		var p = this.String;
		if (p != null)
		{
			var size = Document.DesiredSize;
			var pos = LayoutData.ComputeOffset(RenderSize, size.Parse(), String.HorizontalAlignment, p.VerticalAlignment, p.ClippingBound);

			rect = new Rect(pos, size);
		}

		Document?.Arrange(rect);

		return arrangeBounds;
	}

	protected override void OnRender(DrawingContext ctx)
	{
		base.OnRender(ctx);

		// Render child elements
		Document.Render(ctx);
	}

	public override void UpdateString(string text) => this.Text = text;


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

	IInputElement IContentHost.InputHitTest(Point point)
	{
		if (point.X < 0 || point.Y < 0) return this;

		IInputElement ie = null;

		// retrieve IInputElement from the hit position.
		ie = Document.InputHitTest(point);

		// If nothing has been hit, assume that element itself has been hit.
		return ie ?? this;
	}

	public ReadOnlyCollection<Rect> GetRectangles(ContentElement child)
	{
		ArgumentNullException.ThrowIfNull(child);

		if (child is BaseElement e) return new ReadOnlyCollection<Rect>([e.FinalRect]);

		return null;
	}

	public IEnumerator<IInputElement> HostedElements
	{
		get
		{
			// Return enumerator created from document
			return this.Document.Children.GetEnumerator();
		}
	}

	void IContentHost.OnChildDesiredSizeChanged(UIElement child) => this.OnChildDesiredSizeChanged(child);
	#endregion

	#region Private Methods
	private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomLabelWidget)d;
		var text = (string)e.NewValue;

		widget.Document = new Paragraph(text, widget.String?.fontset);
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
	}


	private static string CutText(string text)
	{
		if (text is null) return null;

		var CopyTxt = WebUtility.HtmlDecode(text);
		CopyTxt = new Regex(@"<\s*br\s*/\s*>").Replace(CopyTxt, "\n");
		return new Regex(@"<.*?>").Replace(CopyTxt, "");
	}
	#endregion


	#region Private Fields
	private TextDocument _container;

	/// <summary>
	/// content container
	/// </summary>
	protected Paragraph Document { get; set; }

	public Dictionary<int, Timer> Timers { get; } = [];
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