using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Xylia.Preview.UI.Controls;

[ContentProperty("Items")]
public class BnsCustomUISceneWidget : FrameworkElement, IBnsCustomBaseWidget
{
	#region Constructors
	/// <summary>
	///     Default BnsCustomUISceneWidget constructor
	/// </summary>
	/// <remarks>
	///     Automatic determination of current Dispatcher. Use alternative constructor
	///     that accepts a Dispatcher for best performance.
	/// </remarks>
	public BnsCustomUISceneWidget()
	{
		Items = new ChildCollection(this);
	}
	#endregion

	#region Properies
	public IList Items { get; }

	protected override int VisualChildrenCount => Items.Count;

	protected override Visual GetVisualChild(int index) => Items[index] as Visual;
	#endregion


	#region Visual Helper
	private FrameworkElement _activate;

	public static readonly DependencyProperty Activateroperty = DependencyProperty.Register("Activate",
		typeof(string), typeof(BnsCustomUISceneWidget), new FrameworkPropertyMetadata(null, OnActivateChanged));

	public string Activate
	{
		get { return (string)GetValue(Activateroperty); }
		set { SetValue(Activateroperty, value); }
	}

	private static void OnActivateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomUISceneWidget)d;
		var value = (string)e.NewValue;

		if (widget.Items.Count == 0) return;

		widget._activate = widget.Items.OfType<FrameworkElement>().First(x => x.Name == value);
		widget.InvalidateVisual();
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		if (_activate is null) return new Size();

		Size childConstraint = new Size(Double.PositiveInfinity, Double.PositiveInfinity);
		_activate.Measure(childConstraint);
		
		return _activate.DesiredSize;
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		if (_activate is null) return finalSize;

		_activate.Arrange(new Rect(new Point(), finalSize));
		return finalSize;
	}
	#endregion
}