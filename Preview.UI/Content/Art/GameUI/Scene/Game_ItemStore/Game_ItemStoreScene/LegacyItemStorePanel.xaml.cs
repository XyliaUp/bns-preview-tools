using System.Windows;
using System.Windows.Controls;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.Helpers.Output.Items;
using Xylia.Preview.UI.ViewModels;
using static Xylia.Preview.Data.Models.UnlocatedStore;

namespace Xylia.Preview.UI.GameUI.Scene.Game_ItemStore;
public partial class LegacyItemStorePanel
{
	#region Methods 
	protected override void OnLoading()
	{
		InitializeComponent();

		#region type
		var UnlocatedStore = FileCache.Data.Provider.GetTable<UnlocatedStore>();
		var UnlocatedStoreUi = FileCache.Data.Provider.GetTable<UnlocatedStoreUi>();

		var group = new Dictionary<UnlocatedStoreTypeSeq, TreeViewItem>();
		foreach (var record in UnlocatedStoreUi.Append(new UnlocatedStoreUi()
		{
			UnlocatedStoreType = UnlocatedStoreTypeSeq.UnlocatedNone,
			TitleText = new("UI.ItemStore.Title"),
		}))
		{
			if (record.UnlocatedStoreType > UnlocatedStoreTypeSeq.SoulBoostStore1 &&
				record.UnlocatedStoreType <= UnlocatedStoreTypeSeq.SoulBoostStore6) continue;

			this.TreeView.Items.Add(group[record.UnlocatedStoreType] = new TreeViewImageItem()
			{
				Header = record.TitleText.GetText(),
				//Image = FileCache.Provider.LoadObject(record.TitleIcon)?.GetImage()?.ToImageSource(),
			});
		}
		#endregion

		#region Store
		foreach (var store2 in FileCache.Data.Provider.GetTable<Store2>())
		{
			var type = UnlocatedStore.FirstOrDefault(x => store2.Equals(x.Store2))?.UnlocatedStoreType ?? UnlocatedStoreTypeSeq.UnlocatedNone;
			if (type > UnlocatedStoreTypeSeq.SoulBoostStore1 && type <= UnlocatedStoreTypeSeq.SoulBoostStore6)
				type = UnlocatedStoreTypeSeq.SoulBoostStore1;

			group[type].Items.Add(new TreeViewItem()
			{
				Tag = store2,
				Header = $"[{store2.Name2.GetText()}] {store2}"
			});
		}
		#endregion
	}

	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;
		if (e.NewValue is not Control element || element.Tag is not Store2 record) return;

		ItemStore_ItemList.DataContext = record;
	}

	private void ExtractPrice_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemBuyPriceOut>();

	private void ExtractCloset_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<ItemCloset>();
	#endregion
}