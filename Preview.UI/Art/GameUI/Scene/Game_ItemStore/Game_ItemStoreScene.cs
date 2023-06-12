using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Xylia.Configure;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.List;
using Xylia.Preview.GameUI.Scene.Searcher;


namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	public sealed partial class Game_ItemStoreScene : StoreScene
	{
		#region Constructor
		public Dictionary<Store2Type, TreeNode> Nodes = new();

		public Game_ItemStoreScene() : base()
		{
			this.InitializeComponent();

			#region 设置商店分组
			this.TreeView.Nodes.Clear();

			Nodes.Add(Store2Type.UnlocatedStore, this.TreeView.Nodes.Add("飞龙工商"));
			Nodes.Add(Store2Type.AccountStore, this.TreeView.Nodes.Add("侠义团商店"));
			Nodes.Add(Store2Type.SoulBoostStore, this.TreeView.Nodes.Add("成长加速礼商店"));
			Nodes.Add(Store2Type.Normal, this.TreeView.Nodes.Add("普通商店"));
			#endregion

			#region 获取职业信息
			var Source = Job.GetPcJobName();
			Source.Insert(0, "全部");
			this.JobSelector.Source = Source;

			var LastJobSelect = Ini.ReadValue("Preview", "JobFilter");
			this.JobSelector.TextValue = Source.Contains(LastJobSelect) ? LastJobSelect : this.JobSelector.Source.FirstOrDefault();
			#endregion


			_filter.Add(new(typeof(Text), "商店名"));
			_filter.Add(new(typeof(Item), "出售物品"));
			_filter.Add(new(typeof(Npc), "出售对象", false));
		}
		#endregion

		#region Functions
		protected override void LoadData()
		{
			#region UnlocatedStore
			Dictionary<Store2, UnlocatedStore> UnlocatedStores = new();
			foreach (var o in FileCache.Data.UnlocatedStore)
			{
				var store = FileCache.Data.Store2[o.Store2];
				if (store is null) continue;

				UnlocatedStores[store] = o;
			}
			#endregion


			#region Load Data
			var StoreInfoList = new List<StoreInfo>();
			foreach (var Store2 in FileCache.Data.Store2)
			{
				var Store2Alias = Store2.alias;
				string Name2 = Store2.Name2.GetText();
				string CurName = Name2 is null ? Store2Alias : $"[{ Name2 }] " + Store2Alias;

				int? Order = null;

				var StoreType = Store2Type.Normal;
				if (UnlocatedStores.TryGetValue(Store2, out var UnlocatedStore))
				{
					#region 判断商店类型
					if (UnlocatedStore.UnlocatedStoreType == UnlocatedStore.Type.AccountStore) StoreType = Store2Type.AccountStore;

					else if (
					UnlocatedStore.UnlocatedStoreType == UnlocatedStore.Type.SoulBoostStore1 ||
					UnlocatedStore.UnlocatedStoreType == UnlocatedStore.Type.SoulBoostStore2 ||
					UnlocatedStore.UnlocatedStoreType == UnlocatedStore.Type.SoulBoostStore3 ||
					UnlocatedStore.UnlocatedStoreType == UnlocatedStore.Type.SoulBoostStore4 ||
					UnlocatedStore.UnlocatedStoreType == UnlocatedStore.Type.SoulBoostStore5 ||
					UnlocatedStore.UnlocatedStoreType == UnlocatedStore.Type.SoulBoostStore6) StoreType = Store2Type.SoulBoostStore;

					else StoreType = Store2Type.UnlocatedStore;
					#endregion

					Order = UnlocatedStore.Key();
				}


				StoreInfoList.Add(new StoreInfo()
				{
					Alias = Store2Alias,
					Name = CurName,
					Order = Order ?? Store2.Key(),
					StoreType = StoreType,
				});
			}
			#endregion

			#region Insert Node
			foreach (var Info in StoreInfoList.OrderBy(a => a.Order))
			{
				if (Nodes.ContainsKey(Info.StoreType))
				{
					var CurNode = Nodes[Info.StoreType].Nodes.Add(Info.Name);
					this.TreeNodeInfo.Add(CurNode, new NodeInfo(Info.Alias, CurNode));
				}
			}

			TreeView.Nodes[0].ExpandAll();
			#endregion
		}

		protected override void Show(string StoreAlias)
		{
			this.ListPreview.Cells = null;

			var Store2 = FileCache.Data.Store2[StoreAlias];
			if (Store2 is null) return;

			//获取当前选择的职业
			JobSeq SelectedJob = Job.GetJob(this.JobSelector.TextValue);

			this.Text = $"商店 { Store2.GetName() } , 载入中...";
			this.ListPreview.Name = Store2.alias;

			var cells = new List<ListCell>();
			for (int i = 1; i <= 127; i++)
			{
				string ItemAlias = Store2.Attributes["item-" + i];
				string BuyPrice = Store2.Attributes["buy-price-" + i];

				//获取物品信息
				var ItemInfo = FileCache.Data.Item[ItemAlias];
				if (ItemInfo is null) continue;

				if (!ItemInfo.CheckEquipJob(SelectedJob)) continue;

				var ItemBuyPrice = FileCache.Data.ItemBuyPrice[BuyPrice];
				this.ListPreview.Invoke(() => cells.Add(new Store2ItemCell(ItemInfo, ItemBuyPrice)));
			}

			this.Text = $"商店 { Store2.GetName() } , 共计 { cells.Count }个兑换内容";
			this.ListPreview.Cells = cells;

			//获取是否存在出售NPC
			this.npcs = FileCache.Data.Npc.Where(npc => GetNpc(Store2, npc));
			this.Invoke(() =>
			{
				this.ucBtnExt1.Visible = npcs != null && npcs.Any();
				Clipboard.SetText(Store2.alias);
			});
		}

		protected override bool Filter(NodeInfo NodeInfo, List<BaseRecord> FilterRule)
		{
			var store = FileCache.Data.Store2[NodeInfo.RecordAlias];
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
		private void Store2Scene_Load(object sender, EventArgs e)
		{
			Store2Scene_SizeChanged(null, null);
		}

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

		private void ucBtnExt1_BtnClick(object sender, EventArgs e) => new SearcherResult(npcs).MyShowDialog();

		private static bool GetNpc(Store2 store, Npc npc) => store == npc.Store2_1 || store == npc.Store2_2 || store == npc.Store2_3 || store == npc.Store2_4 || store == npc.Store2_5 || store == npc.Store2_6;
		#endregion
	}
}