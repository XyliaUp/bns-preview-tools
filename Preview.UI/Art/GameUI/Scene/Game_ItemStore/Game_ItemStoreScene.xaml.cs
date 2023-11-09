using System.Windows.Controls;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Common.Controls;

using static Xylia.Preview.Data.Models.UnlocatedStore;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemStore;
public partial class Game_ItemStoreScene : Window
{
	public Game_ItemStoreScene()
	{
		InitializeComponent();
		LoadData();
	}


	#region Methods 
	private void LoadData()
	{
		#region type
		var group = new Dictionary<UnlocatedStoreTypeSeq, TreeViewItem>();
		foreach (var record in FileCache.Data.UnlocatedStoreUi.Append(new UnlocatedStoreUi()
		{
			UnlocatedStoreType = UnlocatedStoreTypeSeq.UnlocatedNone,
			TitleText = new("UI.ItemStore.Title"),
		}))
		{
			var item = new TreeViewImageItem() { HeaderText = record.TitleText.GetText() };
			this.TreeView.Items.Add(group[record.UnlocatedStoreType] = item);
		}
		#endregion


		foreach (var store2 in FileCache.Data.Store2)
		{
			var text = $"[{store2.Name2.GetText()}] {store2.Alias}";
			var type = FileCache.Data.UnlocatedStore.FirstOrDefault(x => x.Store2 == store2)?.UnlocatedStoreType ?? UnlocatedStoreTypeSeq.UnlocatedNone;

			var item = new TreeViewItem() { Header = text, Tag = store2 };
			group[type].Items.Add(item);
		}
	}

	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;
		if (e.NewValue is not Control element || element.Tag is not Store2 record) return;

		ItemStorePanel.DataContext = record;
	}
	#endregion
}