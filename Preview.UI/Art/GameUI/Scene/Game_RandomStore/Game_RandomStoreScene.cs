using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ItemStore;

namespace Xylia.Preview.GameUI.Scene.Game_RandomStore;
public sealed partial class Game_RandomStoreScene : StoreScene
{
	#region Fields
	readonly Dictionary<GroupType, TreeNode> Nodes = new();
	readonly Dictionary<string, List<RandomStoreItem>> RandomStoreItemGroup = new();

	public Game_RandomStoreScene() : base()
	{
		this.InitializeComponent();
		this.TreeView.Nodes.Clear();
		this.Nodes.Clear();

		foreach (var seq in Enum.GetValues<GroupType>())
		{
			var TreeNode = new TreeNode(seq.GetDescription());
			this.TreeView.Nodes.Add(TreeNode);

			Nodes.Add(seq, TreeNode);
		}

		_filter.Add(new(typeof(Item), "物品"));
	}
	#endregion


	#region Functions
	protected override void LoadData()
	{
		foreach (var record in FileCache.Data.RandomStoreItem)
		{
			string group = record.alias.RegexReplace("_[0-9]*$");
			if (!RandomStoreItemGroup.ContainsKey(group))
			{
				RandomStoreItemGroup.Add(group, new());


				GroupType CurGroupType;
				if (group.MyContains("gold"))
				{
					if (group.MyContains("rare")) CurGroupType = GroupType.GoldRare;
					else CurGroupType = GroupType.GoldNormal;
				}
				else
				{
					if (group.MyContains("awesome")) CurGroupType = GroupType.Awesome;
					else if (group.MyContains("rare")) CurGroupType = GroupType.Rare;
					else CurGroupType = GroupType.Normal;
				}

				AddNode(Nodes[CurGroupType], group, group);
			}

			RandomStoreItemGroup[group].Add(record);
		}
	}

	protected override void Show(string GroupAlias)
	{
		this.ListPreview.Items.Clear();
		if (!RandomStoreItemGroup.TryGetValue(GroupAlias, out var items)) return;

		foreach (var item in items)
		{
			var buy = new ItemBuyPrice()
			{
				Money = item.ItemPriceMoney,
				RequiredItem = new Item[] { item.ItemPriceItem },
				RequiredItemCount = new short[] { item.ItemPriceItemAmount },
			};

			this.Invoke(() => this.ListPreview.Items.Add(new Store2ItemCell(item.Item, item.ItemCount, buy)));
		}

		this.ListPreview.RefreshList();
	}

	protected override bool Filter(string alias, List<BaseRecord> FilterRule)
	{
		var items = this.RandomStoreItemGroup.GetValueOrDefault(alias, new());
		foreach (var rule in FilterRule)
		{
			if (rule is Item item)
			{
				foreach (var ItemGroup in items)
				{
					var ItemInfo = FileCache.Data.Item[ItemGroup.Item];
					if (ItemInfo != null && ItemInfo == item) return true;
				}
			}
		}

		return false;
	}
	#endregion
}



public enum GroupType
{
	[Description("聚灵阁 稀有")]
	Rare,

	[Description("聚灵阁 一般")]
	Normal,

	[Description("聚灵阁 鸿运")]
	Awesome,

	[Description("金币聚灵阁 稀有")]
	GoldRare,

	[Description("金币聚灵阁 一般")]
	GoldNormal,
}