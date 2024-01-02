using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

using HtmlAgilityPack;
using Xylia.Preview.UI.Controls;

namespace Xylia.Preview.UI.Documents;

[ContentProperty("Children")]
public abstract class Element : ContentElement
{
	#region Public Properties
	/// <summary>
	/// element children collection
	/// </summary>
	public List<Element> Children { get; protected set; }
	#endregion

	#region Dependency Properties
	/// <summary>
	/// DependencyProperty for <see cref="FontFamily" /> property.
	/// </summary>
	public static readonly DependencyProperty FontFamilyProperty =BnsCustomBaseWidget.FontFamilyProperty.AddOwner(typeof(Element),
		   new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily,
			   FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

	/// <summary>
	/// The FontFamily property specifies the name of font family.
	/// </summary>
	[Localizability(LocalizationCategory.Font, Modifiability = Modifiability.Unmodifiable)]
	public FontFamily FontFamily
	{
		get { return (FontFamily)GetValue(FontFamilyProperty); }
		set { SetValue(FontFamilyProperty, value); }
	}

	/// <summary>
	/// DependencyProperty for <see cref="FontStyle" /> property.
	/// </summary>
	public static readonly DependencyProperty FontStyleProperty = BnsCustomBaseWidget.FontStyleProperty.AddOwner(typeof(Element),
		new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle,
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

	/// <summary>
	/// The FontStyle property requests normal, italic, and oblique faces within a font family.
	/// </summary>
	public FontStyle FontStyle
	{
		get { return (FontStyle)GetValue(FontStyleProperty); }
		set { SetValue(FontStyleProperty, value); }
	}

	/// <summary>
	/// DependencyProperty for <see cref="FontWeight" /> property.
	/// </summary>
	public static readonly DependencyProperty FontWeightProperty = BnsCustomBaseWidget.FontWeightProperty.AddOwner(typeof(Element),
		new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight,
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

	/// <summary>
	/// The FontWeight property specifies the weight of the font.
	/// </summary>
	public FontWeight FontWeight
	{
		get { return (FontWeight)GetValue(FontWeightProperty); }
		set { SetValue(FontWeightProperty, value); }
	}

	/// <summary>
	/// DependencyProperty for <see cref="FontStretch" /> property.
	/// </summary>
	public static readonly DependencyProperty FontStretchProperty = BnsCustomBaseWidget.FontStretchProperty.AddOwner(typeof(Element),
		new FrameworkPropertyMetadata(FontStretches.Normal,
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

	/// <summary>
	/// The FontStretch property selects a normal, condensed, or extended face from a font family.
	/// </summary>
	public FontStretch FontStretch
	{
		get { return (FontStretch)GetValue(FontStretchProperty); }
		set { SetValue(FontStretchProperty, value); }
	}

	/// <summary>
	/// DependencyProperty for <see cref="FontSize" /> property.
	/// </summary>
	public static readonly DependencyProperty FontSizeProperty = BnsCustomBaseWidget.FontSizeProperty.AddOwner(typeof(Element),
		new FrameworkPropertyMetadata(SystemFonts.MessageFontSize,
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

	/// <summary>
	/// The FontSize property specifies the size of the font.
	/// </summary>
	[TypeConverter(typeof(FontSizeConverter))]
	[Localizability(LocalizationCategory.None)]
	public double FontSize
	{
		get { return (double)GetValue(FontSizeProperty); }
		set { SetValue(FontSizeProperty, value); }
	}

	/// <summary>
	/// DependencyProperty for <see cref="Foreground" /> property.
	/// </summary>
	public static readonly DependencyProperty ForegroundProperty = BnsCustomBaseWidget.ForegroundProperty.AddOwner(typeof(Element),
		new FrameworkPropertyMetadata(SystemColors.ControlTextBrush,
			FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender | FrameworkPropertyMetadataOptions.Inherits));

	/// <summary>
	/// The Foreground property specifies the foreground brush of an element's text content.
	/// </summary>
	public Brush Foreground
	{
		get { return (Brush)GetValue(ForegroundProperty); }
		set { SetValue(ForegroundProperty, value); }
	}

	public static readonly DependencyProperty ParamsProperty = BnsCustomLabelWidget.ParamsProperty.AddOwner(typeof(Element),
		new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

	public DataParams Params
	{
		get { return (DataParams)GetValue(ParamsProperty); }
		set { SetValue(ParamsProperty, value); }
	}


	/// <summary>
	/// DependencyProperty for <see cref="TextAlignment" /> property.
	/// </summary>
	public static readonly DependencyProperty TextAlignmentProperty = BnsCustomLabelWidget.TextAlignmentProperty.AddOwner(typeof(Element),
		new FrameworkPropertyMetadata(TextAlignment.Left,
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

	public TextAlignment TextAlignment
	{
		get { return (TextAlignment)GetValue(TextAlignmentProperty); }
		set { SetValue(TextAlignmentProperty, value); }
	}
	#endregion


	#region Protected Methods
	/// <summary>
	/// Notification that a specified property has been invalidated
	/// </summary>
	/// <param name="e">EventArgs that contains the property, metadata, old value, and new value for this change</param>
	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		// Always call base.OnPropertyChanged, otherwise Property Engine will not work.
		base.OnPropertyChanged(e);


		bool IsValueChange = e.NewValue != e.OldValue;

		// If the modified property affects layout we have some additional
		// bookkeeping to take care of.
		var fmetadata = e.Property.GetMetadata(e.Property.OwnerType) as FrameworkPropertyMetadata;
		if (fmetadata != null)
		{
			bool affectsMeasureOrArrange = fmetadata.AffectsMeasure || fmetadata.AffectsArrange || fmetadata.AffectsParentMeasure || fmetadata.AffectsParentArrange;
			bool affectsRender = (fmetadata.AffectsRender && (IsValueChange || !fmetadata.SubPropertiesDoNotAffectRender));
			if (affectsMeasureOrArrange || affectsRender)
			{
				var textContainer = EnsureTextContainer();
				if (textContainer != null)
				{
					textContainer.BeginChange();
					textContainer.EndChange();
				}
			}
		}
	}


	public Size DesiredSize { get; set; }
	public Rect FinalRect { get; set; }

	public void Measure(Size availableSize)
	{
		DesiredSize = MeasureCore(availableSize);
	}

	public void Arrange(Rect finalRect)
	{
		FinalRect = ArrangeCore(finalRect);
	}

	/// <summary>
	/// Measurement override. Implement your size-to-content logic here.
	/// </summary>
	/// <param name="availableSize"></param>
	/// <returns></returns>
	protected virtual Size MeasureCore(Size availableSize)
	{
		Size size = new Size();
		Size currentLineSize = new Size();

		if (this is Paragraph p)
		{
			// internal padding
			availableSize = new Size(
				availableSize.Width - p.LeftMargin - p.RightMargin,
				availableSize.Height - p.TopMargin - p.BottomMargin);
		}

		foreach (var element in this.Children)
		{
			InheritDependency(this, element);
			element.Measure(availableSize);
			Size desiredSize = element.DesiredSize;

			// warp layout
			if (element is Paragraph)
			{
				size.Width = Math.Max(currentLineSize.Width, size.Width);
				size.Height += currentLineSize.Height;
				currentLineSize = desiredSize;
			}
			else if (element is BR || currentLineSize.Width + desiredSize.Width > availableSize.Width)
			{
				size.Width = Math.Max(currentLineSize.Width, size.Width);
				size.Height += currentLineSize.Height;
				currentLineSize = desiredSize;

				if (desiredSize.Width > availableSize.Width)
				{
					size.Width = Math.Max(desiredSize.Width, size.Width);
					size.Height += desiredSize.Height;
					currentLineSize = new Size();
				}
			}
			else
			{
				currentLineSize.Width += desiredSize.Width;
				currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
			}
		}

		// full panel size
		size.Width = Math.Max(currentLineSize.Width, size.Width);
		size.Height += currentLineSize.Height;
		return size;
	}

	/// <summary>
	/// ArrangeCore allows for the customization of the final sizing and positioning of children.
	/// </summary>
	/// <param name="finalRect"></param>
	/// <returns></returns>
	protected virtual Rect ArrangeCore(Rect finalRect)
	{
		#region multiple lines
		if (this.Children is null)
			return finalRect;

		// start element in current line 
		int firstInLine = 0;
		Size currentLineSize = new Size();

		List<Element[]> lines = new();
		for (int i = 0; i < this.Children.Count; i++)
		{
			var element = this.Children[i];
			Size desiredSize = element.DesiredSize;

			if (element is Paragraph || i + 1 == this.Children.Count)
			{
				lines.Add([.. this.Children.GetRange(firstInLine, i - firstInLine + 1)]);
				currentLineSize = desiredSize;
				firstInLine = i + 1;
			}
			else if (element is BR || currentLineSize.Width + desiredSize.Width > finalRect.Width)
			{
				lines.Add([.. this.Children.GetRange(firstInLine, i - firstInLine)]);
				currentLineSize = desiredSize;

				// single line if control width gather than line width
				if (desiredSize.Width > finalRect.Width)
				{
					lines.Add(this.Children.GetRange(i, 1).ToArray());
					currentLineSize = new Size();
				}

				firstInLine = i;
			}
			else
			{
				currentLineSize.Width += desiredSize.Width;
				currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
			}
		}
		#endregion

		#region arrange every line
		double y = finalRect.Y;
		if (this is Paragraph p2) y += p2.TopMargin;

		foreach (var line in lines)
		{
			if (line.Length == 0) continue;

			double widget = line.Sum(x => x.DesiredSize.Width);
			double height = line.Max(x => x.DesiredSize.Height);

			double x = finalRect.X;
			if (this is Paragraph p)
			{
				var vect = p.ComputeAlignmentOffset(finalRect.Size, new Size(widget, height));
				x += vect.X + p.LeftMargin;
			}

			foreach (var element in line)
			{
				element.Arrange(new Rect(x, y, element.DesiredSize.Width, height));
				x += element.DesiredSize.Width;
			}

			y += height;
		}
		#endregion

		return finalRect;
	}


	/// <summary>
	/// Initialize element from html
	/// </summary>
	/// <param name="node"></param>
	protected internal abstract void Load(HtmlNode node);

	internal virtual void Render(DrawingContext ctx)
	{
		Children?.ForEach(x => x.Render(ctx));
	}

	internal virtual IInputElement InputHitTest(Point point)
	{
		IInputElement element = null;

		if (Children is null) return element;

		// only Link need respond
		foreach (var child in Children)
		{
			if (child.FinalRect.Contains(point))
			{
				if (child is Link) return child;
			}

			element = child.InputHitTest(point);
		}

		return element;
	}
	#endregion

	#region Private Methods
	/// <summary>
	/// Inherits implement at FrameworkElement
	/// https://source.dot.net/#PresentationFramework/System/Windows/FrameworkElement.cs,1913
	/// </summary>
	public static void InheritDependency(DependencyObject parent, DependencyObject current)
	{
		current.SetValue(FontFamilyProperty, parent.GetValue(FontFamilyProperty));
		current.SetValue(FontStyleProperty, parent.GetValue(FontStyleProperty));
		current.SetValue(FontWeightProperty, parent.GetValue(FontWeightProperty));
		current.SetValue(FontStretchProperty, parent.GetValue(FontStretchProperty));
		current.SetValue(FontSizeProperty, parent.GetValue(FontSizeProperty));
		current.SetValue(ForegroundProperty, parent.GetValue(ForegroundProperty));
		current.SetValue(ParamsProperty, parent.GetValue(ParamsProperty));
		current.SetValue(TextAlignmentProperty, parent.GetValue(TextAlignmentProperty));
	}

	private TextDocument EnsureTextContainer() => null;
	#endregion
}