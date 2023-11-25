using System.Windows;
using System.Windows.Controls;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Controls;

using static Xylia.Preview.Data.Models.UnlocatedStore;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemStore;
public partial class Game_ItemStoreScene : GameScene
{
	public Game_ItemStoreScene()
	{
		InitializeComponent();
	}


	#region Methods 
	protected override void OnLoaded(EventArgs e)
	{
		#region type
		var group = new Dictionary<UnlocatedStoreTypeSeq, TreeViewItem>();
		foreach (var record in FileCache.Data.UnlocatedStoreUi.Append(new UnlocatedStoreUi()
		{
			UnlocatedStoreType = UnlocatedStoreTypeSeq.UnlocatedNone,
			TitleText = new("UI.ItemStore.Title"),
		}))
		{
			if (record.UnlocatedStoreType > UnlocatedStoreTypeSeq.SoulBoostStore1 &&
				record.UnlocatedStoreType <= UnlocatedStoreTypeSeq.SoulBoostStore6) continue;

			var item = new TreeViewImageItem()
			{
				Header = record.TitleText.GetText(),
				//Image = FileCache.Provider.LoadObject(record.TitleIcon)?.GetImage()?.ToImageSource(),
			};
			this.TreeView.Items.Add(group[record.UnlocatedStoreType] = item);
		}
		#endregion


		foreach (var store2 in FileCache.Data.Store2)
		{
			var text = $"[{store2.Name2.GetText()}] {store2.Alias}";
			var type = FileCache.Data.UnlocatedStore.FirstOrDefault(x => x.Store2 == store2)?.UnlocatedStoreType ?? UnlocatedStoreTypeSeq.UnlocatedNone;
			if (type > UnlocatedStoreTypeSeq.SoulBoostStore1 && type <= UnlocatedStoreTypeSeq.SoulBoostStore6)
				type = UnlocatedStoreTypeSeq.SoulBoostStore1;

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