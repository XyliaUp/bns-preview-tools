using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	[DesignTimeVisible(false)]
	public partial class FixedIngredientPreview : Panel
	{
		#region Functions
		private void CreateNew(Item Item, int StackCount, ref int LocX)
		{
			var ItemIcon = new ItemIconCell()
			{
				ObjectRef = Item,
				Image = Item.Icon(),
				ShowStackCount = true,
				StackCount = StackCount,

				Location = new Point(LocX, 0),
			};

			this.Controls.Add(ItemIcon);
			LocX = ItemIcon.Right + 3;
		}

		private void HandleSize(int LocX)
		{
			this.Width = LocX;
			this.Height = 50;

			this.DataLoaded?.Invoke();
		}
		#endregion


		#region SetData
		public event EmptyHandler DataLoaded;

		public void SetData(BaseRecord record, string itemAttr, string countAttr, byte num = 8)
		{
			if (record is null)
				throw new ArgumentNullException(nameof(record));


			this.Controls.Remove<ItemIconCell>();

			int LocX = 0;
			for (byte i = 1; i <= num; i++)
			{
				var Item = FileCache.Data.Item[record.Attributes[itemAttr + "-" + i]];
				if (Item is null) continue;

				var ItemCount = record.Attributes[countAttr + "-" + i].ToShort();
				CreateNew(Item, ItemCount, ref LocX);
			}

			this.HandleSize(LocX);
		}
		#endregion
	}
}