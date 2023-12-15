using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xylia.Preview.UI.Converters;

namespace Xylia.Preview.UI;
/// <summary>
///  Defines an area that consists of anchor.
/// </summary>
public class AnchorPanel : Panel
{
	#region Public Methods
	/// <summary>
	/// Reads the attached property Left from the given element.
	/// </summary>
	/// <param name="element">The element from which to read the Left attached property.</param>
	/// <returns>The property's value.</returns>
	/// <seealso cref="Canvas.LeftProperty" />
	public static Rect GetOffset(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Rect)element.GetValue(OffsetProperty);
	}

	/// <summary>
	/// Writes the attached property Left to the given element.
	/// </summary>
	/// <param name="element">The element to which to write the Left attached property.</param>
	/// <param name="length">The length to set</param>
	/// <seealso cref="Canvas.LeftProperty" />
	public static void SetOffset(UIElement element, Rect value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(OffsetProperty, value);
	}

	public static Anchor GetAnchor(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Anchor)element.GetValue(AnchorProperty);
	}

	public static void SetAnchor(UIElement element, Anchor value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(AnchorProperty, value);
	}
	#endregion

	#region Public Properties
	//having this invalidate callback allows to host UIElements in Canvas and still
	//receive invalidations when Left/Top/Bottom/Right properties change - 
	//registering the attached properties with AffectsParentArrange flag would be a mistake
	//because those flags only work for FrameworkElements
	private static void OnPositioningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		UIElement uie = d as UIElement;
		if (uie != null)
		{
			var p = VisualTreeHelper.GetParent(uie) as AnchorPanel;
			p?.InvalidateArrange();
		}
	}

	/// <summary>
	/// This is the dependency property registered for the Canvas' Left attached property.
	/// 
	/// The Left property is read by a Canvas on its children to determine where to position them.
	/// The child's offset from this property does not have an effect on the Canvas' own size.
	/// Conflict between the Left and Right properties is resolved in favor of Left.
	/// </summary>
	public static readonly DependencyProperty OffsetProperty
		= DependencyProperty.RegisterAttached("Offset", typeof(Rect), typeof(AnchorPanel),
				new FrameworkPropertyMetadata((Rect)default, new PropertyChangedCallback(OnPositioningChanged)));

	public static readonly DependencyProperty AnchorProperty
		= DependencyProperty.RegisterAttached("Anchor", typeof(Anchor), typeof(AnchorPanel),
			new FrameworkPropertyMetadata((Anchor)default, new PropertyChangedCallback(OnPositioningChanged)));
	#endregion


	#region Protected Methods
	/// <summary>
	/// Updates DesiredSize of the Canvas.  Called by parent UIElement.  This is the first pass of layout.
	/// </summary>
	/// <param name="constraint">Constraint size is an "upper limit" that Canvas should not exceed.</param>
	/// <returns>Canvas' desired size.</returns>
	protected override Size MeasureOverride(Size availableSize)
	{
		Size childConstraint = new Size(Double.PositiveInfinity, Double.PositiveInfinity);

		foreach (UIElement child in InternalChildren)
		{
			if (child == null) continue;
			child.Measure(childConstraint);
		}

		return new Size();
	}

	/// <summary>
	/// Canvas computes a position for each of its children taking into account their margin and
	/// attached Canvas properties: Top, Left.  
	/// 
	/// Canvas will also arrange each of its children.
	/// </summary>
	/// <param name="arrangeSize">Size that Canvas will assume to position children.</param>
	protected override Size ArrangeOverride(Size arrangeSize)
	{
		foreach (UIElement child in InternalChildren)
		{
			if (child == null) continue;

			double x = 0;
			double y = 0;

			//Compute offset of the child:
			var anchor = GetAnchor(child);
			var offset = GetOffset(child);

			x = anchor.Minimum.X * (arrangeSize.Width - child.DesiredSize.Width) + offset.X;
			y = anchor.Minimum.Y * (arrangeSize.Height - child.DesiredSize.Height) + offset.Y;

			child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
		}
		return arrangeSize;
	}
	#endregion
}