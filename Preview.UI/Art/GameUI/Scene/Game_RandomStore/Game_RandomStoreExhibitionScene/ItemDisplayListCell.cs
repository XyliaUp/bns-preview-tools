using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Extension;
using Xylia.Preview.UI.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_RandomStore;
public partial class ItemDisplayListCell : UserControl, IComparable<ItemDisplayListCell>
{
	public readonly RandomStoreItemDisplay record;

	public ItemDisplayListCell(RandomStoreItemDisplay Record)
	{
		InitializeComponent();

		this.record = Record;
		if (Record?.DisplayItem is null) return;


		var ItemIcon = record.DisplayItem.IconExtra();
		if (record.NewArrival) ItemIcon = ItemIcon.Combine(Resource_BNSR.SlotItem_New, DrawLocation.TopLeft);

		this.ItemShow.LoadData(record.DisplayItem, ItemIcon);
	}

	public int CompareTo(ItemDisplayListCell other)
	{
		//判断是否是新物品
		if (!this.record.NewArrival && other.record.NewArrival) return 1;
		else if (this.record.NewArrival && !other.record.NewArrival) return -1;

		//判断物品品质（大的在前）
		if (this.record.DisplayItem.ItemGrade != other.record.DisplayItem.ItemGrade)
			return other.record.DisplayItem.ItemGrade - this.record.DisplayItem.ItemGrade;

		//判断物品种类（小的在前）
		if (this.record.DisplayItem.GameCategory3 != other.record.DisplayItem.GameCategory3)
			return this.record.DisplayItem.GameCategory3 - other.record.DisplayItem.GameCategory3;

		//最后判断顺序（小的在前）
		return this.record.Ref.Id - other.record.Ref.Id;
	}
}