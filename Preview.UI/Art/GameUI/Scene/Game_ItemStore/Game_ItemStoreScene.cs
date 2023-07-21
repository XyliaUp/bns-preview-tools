using System.ComponentModel;

using Xylia.Configure;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Searcher;

using static Xylia.Preview.Data.Record.UnlocatedStore;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore;
public sealed partial class Game_ItemStoreScene : StoreScene
{
	#region Constructor
	readonly ComponentResourceManager resources = new(typeof(Game_ItemStoreScene));
	readonly Dictionary<UnlocatedStoreTypeSeq, TreeNode> Nodes = new();

	public Game_ItemStoreScene() : base()
	{
		this.InitializeComponent();

		#region store type
		this.TreeView.Nodes.Clear();
		foreach (var record in FileCache.Data.UnlocatedStoreUi)
		{
			var text = record.TitleText.GetText();
			var type = record.UnlocatedStoreType;
			if (type >= UnlocatedStoreTypeSeq.SoulBoostStore2 && type <= UnlocatedStoreTypeSeq.SoulBoostStore6) continue;

			Nodes[type] = this.TreeView.Nodes.Add(text);
		}

		Nodes[UnlocatedStoreTypeSeq.UnlocatedNone] = this.TreeView.Nodes.Add("normal");
		#endregion

		#region job filter
		var Source = Job.GetPcJobName();
		Source.Insert(0, resources.GetString("Filter_All"));
		this.JobSelector.Source = Source;

		var LastJobSelect = Ini.ReadValue("Preview", "JobFilter");
		this.JobSelector.TextValue = Source.Contains(LastJobSelect) ? LastJobSelect : this.JobSelector.Source.FirstOrDefault();
		#endregion


		_filter.Add(new(typeof(Text), resources.GetString("Filter_Text")));
		_filter.Add(new(typeof(Item), resources.GetString("Filter_Item")));
		_filter.Add(new(typeof(Npc), resources.GetString("Filter_Npc")));
	}
	#endregion

	#region Functions
	protected override void LoadData()
	{
		var UnlocatedStores = new Dictionary<Store2, UnlocatedStore>();
		foreach (var record in FileCache.Data.UnlocatedStore) UnlocatedStores[record.Store2] = record;

		foreach (var store2 in FileCache.Data.Store2)
		{
			var text = $"[{store2.Name2.GetText()}] {store2.alias}";
			var type = UnlocatedStores.GetValueOrDefault(store2)?.UnlocatedStoreType ?? UnlocatedStoreTypeSeq.UnlocatedNone;
			if (type >= UnlocatedStoreTypeSeq.SoulBoostStore2 && type <= UnlocatedStoreTypeSeq.SoulBoostStore6)
				type = UnlocatedStoreTypeSeq.SoulBoostStore1;

			AddNode(Nodes[type], store2.alias, text);
		}
	}

	protected override void Show(string StoreAlias)
	{
		this.ListPreview.Items.Clear();

		var store2 = FileCache.Data.Store2[StoreAlias];
		if (store2 is null) return;

		var SelectedJob = Job.GetJob(this.JobSelector.TextValue);
		this.Text = string.Format(resources.GetString("Title1"), store2.GetName());
		for (int i = 0; i < store2.Item.Length; i++)
		{
			var item = store2.Item[i];
			if (item is null || !item.CheckEquipJob(SelectedJob)) continue;

			this.Invoke(() => this.ListPreview.Items.Add(new Store2ItemCell(item, 1, store2.BuyPrice[i])));
		}


		this.Text = string.Format(resources.GetString("Title2"), store2.GetName(), this.ListPreview.Items.Count);
		this.npcs = FileCache.Data.Npc.Where(npc => GetNpc(store2, npc));
		this.ListPreview.RefreshList();

		this.Invoke(() => this.ucBtnExt1.Visible = npcs != null && npcs.Any());
	}

	protected override bool Filter(string alias, List<BaseRecord> FilterRule)
	{
		var store = FileCache.Data.Store2[alias];
		if (store is null) return false;

		foreach (var rule in FilterRule)
		{
			if (rule is Item item)
			{
				foreach (var a in store.Attributes)
				{
					if (!a.Key.StartsWith("item-")) continue;

					var ItemInfo = FileCache.Data.Item[a.Value];
					if (ItemInfo != null && ItemInfo == item) return true;
				}
			}
			else if (rule is Npc npc)
			{
				var flag = GetNpc(store, npc);
				if (flag) return true;
			}
		}

		return false;
	}
	#endregion

	#region Functions (UI)
	private void Store2Scene_SizeChanged(object sender, EventArgs e)
	{
		this.ListPreview.Height = this.Height - this.ControlPanel.Bottom - 55;
	}

	private void JobSelector_SelectedChangedEvent(object sender, EventArgs e)
	{
		Ini.WriteValue("Preview", "JobFilter", this.JobSelector.TextValue);
		this.TreeView_AfterSelect(null, null);
	}


	IEnumerable<BaseRecord> npcs;

	private void ucBtnExt1_BtnClick(object sender, EventArgs e) => Task.Run(() => new SearcherResult(npcs).ShowDialog());

	private static bool GetNpc(Store2 store, Npc npc) => npc.Store2.FirstOrDefault(o => store == o) != null;
	#endregion
}