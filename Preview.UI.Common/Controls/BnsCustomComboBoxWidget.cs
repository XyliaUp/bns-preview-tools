using System;
using System.Collections;
using System.Windows;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomComboBoxWidget : BnsCustomSelectBaseWidget
{
	#region Protected Methods
	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		Button = GetChild<BnsCustomToggleButtonWidget>("Button");
		Label = GetChild<BnsCustomLabelWidget>("Label");
		List = GetChild<BnsCustomListCtrlWidget>("List");

		// set handler
		Button.Checked += Button_Checked;
		Button.Unchecked += Button_Unchecked;

		Label.IsHitTestVisible = false;
		List.Visibility = Visibility.Collapsed;
	}

	protected override Size MeasureOverride(Size constraint)
	{
		// Mesure child size
		return base.MeasureOverride(constraint);

		// TODO: Mesure 
	}
	#endregion

	#region Private Helpers
	private void Button_Checked(object sender, RoutedEventArgs e)
	{
		List.Visibility = Visibility.Visible;
	}

	private void Button_Unchecked(object sender, RoutedEventArgs e)
	{
		List.Visibility = Visibility.Collapsed;
	}
	#endregion

	#region Private Fields
	private BnsCustomToggleButtonWidget Button;
	private BnsCustomLabelWidget Label;
	private BnsCustomListCtrlWidget List;

	protected override IList Items => List.Children;
	#endregion
}