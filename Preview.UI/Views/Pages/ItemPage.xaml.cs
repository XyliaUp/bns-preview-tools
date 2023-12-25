#define DEV
using System.Windows;
using System.Windows.Controls;
using Xylia.Preview.Common;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Helpers.Output.Items;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views.Editor;

namespace Xylia.Preview.UI.Views.Pages;
public partial class ItemPage : Page
{
	public ItemPage()
	{
		DataContext = new ItemPageViewModel();
		InitializeComponent();

#if DEV
		DEBUG.Visibility = Visibility.Visible;
		DEBUG.IsSelected = true;
#endif
	}

	#region Preview
	private void DatabaseGui_Click(object sender, RoutedEventArgs e) => new DatabaseStudio().Show();
	private void ClearCacheData_Click(object sender, RoutedEventArgs e)
	{
		FileCache.Clear();
		ProcessHelper.ClearMemory();
	}

	private void BuyPrice_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemBuyPriceOut>();
	private void ItemTransform_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemTransformRecipeOut>();
	private void ItemCloset_Main_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemCloset>();
	#endregion
}