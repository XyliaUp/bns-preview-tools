using System.Windows.Controls;

using Microsoft.Win32;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Art.GameUI.Scene.Game_Auction;
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
	}


	#region List
	private void Brower_GameFolder_Click(object sender, RoutedEventArgs e)
	{
		new SettingsView().ShowDialog();
	}

	private void Brower_ItemList_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFileDialog() { Filter = @"|*.chv|All files|*.*" };
		if (dialog.ShowDialog() == true) ItemListPath.Text = dialog.FileName;
	}
	#endregion

	#region Preview	 
	private void SearchItem_Click(object sender, EventArgs e)
	{
		var scene = new Game_AuctionScene();
		scene.NameFilter = SearchItem_Rule.Text;
		scene.Show();
	}

	private void ClearCacheData_Click(object sender, RoutedEventArgs e)
	{
		FileCache.Clear();
		ProcessEx.ClearMemory();
	}
	#endregion

	#region Preview
	private void DatabaseGui_Click(object sender, RoutedEventArgs e) => new DatabaseStudio().Show();

	private void BuyPrice_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemBuyPriceOut>();
	private void ItemTransform_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemTransformRecipeOut>();
	private void ItemCloset_Main_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemCloset>();
	#endregion
}