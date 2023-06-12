using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.List;
using Xylia.Preview.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_RandomStore
{
	public partial class ItemDisplayListCell : ListCell, IComparable<ItemDisplayListCell>
	{
		public readonly RandomStoreItemDisplay data;
		
		public readonly Item DisplayItem;

		public ItemDisplayListCell(RandomStoreItemDisplay Record) 
		{
			InitializeComponent();

			this.data = Record;
			this.DisplayItem = FileCache.Data.Item[Record.DisplayItem];


			var ItemIcon = DisplayItem.IconExtra();
			if (data.NewArrival) ItemIcon = ItemIcon.Combine(Resource_BNSR.SlotItem_New, DrawLocation.TopLeft);

			this.ItemShow.LoadData(DisplayItem, ItemIcon);
		}


		public int CompareTo(ItemDisplayListCell other)
		{
			//判断是否是新物品
			if (!this.data.NewArrival && other.data.NewArrival) return 1;
			else if (this.data.NewArrival && !other.data.NewArrival) return -1;

			//判断物品品质（大的在前）
			if (this.DisplayItem.ItemGrade != other.DisplayItem.ItemGrade) 
				return other.DisplayItem.ItemGrade - this.DisplayItem.ItemGrade;

			//判断物品种类（小的在前）
			if (this.DisplayItem.GameCategory3 != other.DisplayItem.GameCategory3) 
				return this.DisplayItem.GameCategory3 - other.DisplayItem.GameCategory3;

			//最后判断顺序（小的在前）
			return (int)(this.data.TableIndex - other.data.TableIndex);
		}
	}
}