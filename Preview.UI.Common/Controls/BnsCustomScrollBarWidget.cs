using System;
using System.Windows;
using CUE4Parse.BNS.Assets.Exports;
using Xylia.Preview.UI.Controls.Primitives;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomScrollBarWidget : BnsCustomBaseWidget
{
	#region Constructors
	static BnsCustomScrollBarWidget()
	{
		// Register Event Handler
		EventManager.RegisterClassHandler(typeof(BnsCustomScrollBarWidget), BnsCustomSliderBarWidget.ValueChangedEvent, new RoutedPropertyChangedEventHandler<double>(OnValueChanged));
	}
	#endregion

	#region Override Methods
	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		Parent = base.Parent as UserWidget;
		SliderBar = this.GetChild<BnsCustomSliderBarWidget>("SliderBar");
		DecrementButton = GetChild<BnsCustomLabelButtonWidget>("DecrementButton");
		IncrementButton = this.GetChild<BnsCustomLabelButtonWidget>("IncrementButton");

		if (SliderBar != null)
		{
			// ensure the content does not beyond bound
			Parent.ClipToBounds = true;

			if (DecrementButton != null) DecrementButton.Click += (_, _) => BnsCustomSliderBarWidget.DecreaseSmall.Execute(null, SliderBar);
			if (IncrementButton != null) IncrementButton.Click += (_, _) => BnsCustomSliderBarWidget.IncreaseSmall.Execute(null, SliderBar);
		}
	}

	private static void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	{
		var widget = (BnsCustomScrollBarWidget)sender;
		if (widget.SliderBar is null) return;

		var oldValue = e.OldValue;
		var newValue = e.NewValue;

		// When event is not registered, for handle widget scroll
		// This method may have issues
		var IsDefaultEvent = widget.SliderBar.GetBindingExpression(BnsCustomRangeBaseWidget.ValueProperty) is null;
		if (IsDefaultEvent)
		{
			var target = widget.Parent;
			var offset = (newValue - oldValue) * 100;

			// update
			if (widget.SliderBar.SliderOrientation == Orientation.Orient_Horizontal) target.HorizontalOffset += offset;
			else target.VerticalOffset += offset;

			target.InvalidateArrange();
		}
	}
	#endregion


	#region Private Fields
	private new UserWidget Parent;
	private BnsCustomSliderBarWidget SliderBar;
	private BnsCustomLabelButtonWidget DecrementButton;
	private BnsCustomLabelButtonWidget IncrementButton;
	#endregion
}