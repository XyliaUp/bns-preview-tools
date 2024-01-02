using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Xylia.Preview.UI;
/// <summary>
///  Defines an area that consists of anchor.
/// </summary>
public class Anchor : Panel
{
	#region Public Properties
	//having this invalidate callback allows to host UIElements in AnchorPanel and still
	//receive invalidations when Offset/Top/Bottom/Right properties change - 
	//registering the attached properties with AffectsParentArrange flag would be a mistake
	//because those flags only work for FrameworkElements
	private static void OnPositioningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is UIElement uie)
		{
			var p = VisualTreeHelper.GetParent(uie) as Anchor;
			p?.InvalidateArrange();
		}
	}

	/// <summary>
	/// This is the dependency property registered for the AnchorPanel' Offset attached property.
	/// 
	/// The Offset property is read by a AnchorPanel on its children to determine where to position them.
	/// The child's offset from this property does not have an effect on the AnchorPanel' own size.
	/// </summary>
	public static readonly DependencyProperty OffsetsProperty
		= DependencyProperty.RegisterAttached("Offsets", typeof(Rect), typeof(Anchor),
				new FrameworkPropertyMetadata((Rect)default, new PropertyChangedCallback(OnPositioningChanged)));

	public static readonly DependencyProperty AnchorsProperty
		= DependencyProperty.RegisterAttached("Anchors", typeof(Converters.Anchor), typeof(Anchor),
			new FrameworkPropertyMetadata((Converters.Anchor)default, new PropertyChangedCallback(OnPositioningChanged)));

	public static readonly DependencyProperty AlignmentsProperty
	= DependencyProperty.RegisterAttached("Alignments", typeof(Vector), typeof(Anchor),
		new FrameworkPropertyMetadata((Vector)default, new PropertyChangedCallback(OnPositioningChanged)));
	#endregion

	#region Public Methods
	/// <summary>
	/// Reads the attached property Offset from the given element.
	/// </summary>
	/// <param name="element">The element from which to read the Offset attached property.</param>
	/// <returns>The property's value.</returns>
	/// <seealso cref="Anchor.OffsetProperty" />
	public static Rect GetOffsets(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Rect)element.GetValue(OffsetsProperty);
	}

	/// <summary>
	/// Writes the attached property Offset to the given element.
	/// </summary>
	/// <param name="element">The element to which to write the Offset attached property.</param>
	/// <param name="value">The offset to set</param>
	/// <seealso cref="Anchor.OffsetProperty" />
	public static void SetOffsets(UIElement element, Rect value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(OffsetsProperty, value);
	}

	/// <summary>
	/// Reads the attached property Anchor from the given element.
	/// </summary>
	/// <param name="element">The element from which to read the Anchor attached property.</param>
	/// <returns>The property's value.</returns>
	/// <seealso cref="Anchor.AnchorProperty" />
	public static Converters.Anchor GetAnchors(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Converters.Anchor)element.GetValue(AnchorsProperty);
	}

	/// <summary>
	/// Writes the attached property Anchor to the given element.
	/// </summary>
	/// <param name="element">The element to which to write the Anchor attached property.</param>
	/// <param name="value">The Anchor to set</param>
	/// <seealso cref="Anchor.AnchorProperty" />
	public static void SetAnchors(UIElement element, Converters.Anchor value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(AnchorsProperty, value);
	}

	/// <summary>
	/// Reads the attached property Alignment from the given element.
	/// </summary>
	/// <param name="element">The element from which to read the Alignment attached property.</param>
	/// <returns>The property's value.</returns>
	/// <seealso cref="Anchor.AlignmentsProperty" />
	public static Vector GetAlignments(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Vector)element.GetValue(AlignmentsProperty);
	}

	/// <summary>
	/// Writes the attached property Alignment to the given element.
	/// </summary>
	/// <param name="element">The element to which to write the Alignment attached property.</param>
	/// <param name="value">The Alignment to set</param>
	/// <seealso cref="Anchor.AlignmentsProperty" />
	public static void SetAlignments(UIElement element, Vector value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(AlignmentsProperty, value);
	}
	#endregion


	#region Protected Methods
	/// <summary>
	/// Updates DesiredSize of the AnchorPanel.  Called by parent UIElement.  This is the first pass of layout.
	/// </summary>
	/// <param name="constraint">Constraint size is an "upper limit" that AnchorPanel should not exceed.</param>
	/// <returns>AnchorPanel' desired size.</returns>
	protected override Size MeasureOverride(Size availableSize)
	{
		// invalid size
		if (double.IsInfinity(availableSize.Width) || double.IsInfinity(availableSize.Height))
			return Size.Empty;

		foreach (UIElement child in InternalChildren)
		{
			if (child == null) continue;

			var size = availableSize;
			var offset = GetOffsets(child);
			if (offset.Width > 0) size.Width = offset.Width;
			if (offset.Height > 0) size.Height = offset.Height;

			child.Measure(size);
		}

		return availableSize;
	}

	/// <summary>
	/// AnchorPanel computes a position for each of its children taking into account their margin and
	/// attached AnchorPanel properties: Top, Offset.  
	/// 
	/// AnchorPanel will also arrange each of its children.
	/// </summary>
	/// <param name="arrangeSize">Size that AnchorPanel will assume to position children.</param>
	protected override Size ArrangeOverride(Size arrangeSize)
	{
		foreach (UIElement child in InternalChildren)
		{
			if (child == null) continue;

			//Compute offset of the child
			var anchor = GetAnchors(child);
			var offset = GetOffsets(child);

			double x = anchor.Minimum.X * (arrangeSize.Width - child.DesiredSize.Width) + offset.X;
			double y = anchor.Minimum.Y * (arrangeSize.Height - child.DesiredSize.Height) + offset.Y;
			double w = anchor.Maximum.X <= anchor.Minimum.X ? child.DesiredSize.Width : arrangeSize.Width * (anchor.Maximum.X - anchor.Minimum.X);
			double h = anchor.Maximum.Y <= anchor.Minimum.Y ? child.DesiredSize.Height : arrangeSize.Height * (anchor.Maximum.Y - anchor.Minimum.Y);

			child.Arrange(new Rect(x, y, w, h));
		}

		return arrangeSize;
	}
	#endregion
}