using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomUISceneWidget : Selector
{
	#region Constructors
	/// <summary>
	///     Default BnsCustomUISceneWidget constructor
	/// </summary>
	/// <remarks>
	///     Automatic determination of current Dispatcher. Use alternative constructor
	///     that accepts a Dispatcher for best performance.
	/// </remarks>
	public BnsCustomUISceneWidget() : base()
	{

	}
	#endregion

	#region Properties

	#endregion


	#region Overrided Methods
	protected override void OnSelectionChanged(SelectionChangedEventArgs e)
	{
		UpdateSelectedContent();
		if (IsKeyboardFocusWithin)
		{
			// If keyboard focus is within the control, make sure it is going to the correct place
			var item = GetSelectedTabItem();
			//item?.SetFocus();
		}
		base.OnSelectionChanged(e);
	}
	#endregion

	#region private helpers
	private FrameworkElement GetSelectedTabItem()
	{
		object selectedItem = SelectedItem;
		if (selectedItem != null)
		{
			// Check if the selected item is a TabItem
			FrameworkElement tabItem = selectedItem as FrameworkElement;
			//if (tabItem == null)
			//{
			//	// It is a data item, get its TabItem container
			//	tabItem = ItemContainerGenerator.ContainerFromIndex(SelectedIndex) as TabItem;

			//	// Due to event leapfrogging, we may have the wrong container.
			//	// If so, re-fetch the right container using a more expensive method.
			//	// (BTW, the previous line will cause a debug assert in this case) 
			//	if (tabItem == null ||
			//		!ItemsControl.EqualsEx(selectedItem, ItemContainerGenerator.ItemFromContainer(tabItem)))
			//	{
			//		tabItem = ItemContainerGenerator.ContainerFromItem(selectedItem) as TabItem;
			//	}
			//}

			return tabItem;
		}

		return null;
	}

	private void UpdateSelectedContent()
	{
		if (SelectedIndex < 0)
		{
			//SelectedContent = null;
			//SelectedContentTemplate = null;
			//SelectedContentTemplateSelector = null;
			//SelectedContentStringFormat = null;
			return;
		}

		var tabItem = GetSelectedTabItem();
		if (tabItem != null)
		{
			FrameworkElement visualParent = VisualTreeHelper.GetParent(tabItem) as FrameworkElement;

			//if (visualParent != null)
			//{
			//	KeyboardNavigation.SetTabOnceActiveElement(visualParent, tabItem);
			//	KeyboardNavigation.SetTabOnceActiveElement(this, visualParent);
			//}

			//SelectedContent = tabItem.Content;
			//ContentPresenter scp = SelectedContentPresenter;
			//if (scp != null)
			//{
			//	scp.HorizontalAlignment = tabItem.HorizontalContentAlignment;
			//	scp.VerticalAlignment = tabItem.VerticalContentAlignment;
			//}

			//// Use tabItem's template or selector if specified, otherwise use TabControl's
			//if (tabItem.ContentTemplate != null || tabItem.ContentTemplateSelector != null || tabItem.ContentStringFormat != null)
			//{
			//	SelectedContentTemplate = tabItem.ContentTemplate;
			//	SelectedContentTemplateSelector = tabItem.ContentTemplateSelector;
			//	SelectedContentStringFormat = tabItem.ContentStringFormat;
			//}
			//else
			//{
			//	SelectedContentTemplate = ContentTemplate;
			//	SelectedContentTemplateSelector = ContentTemplateSelector;
			//	SelectedContentStringFormat = ContentStringFormat;
			//}
		}
	}
	#endregion
}