using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	[DesignTimeVisible(false)]
	public partial class ProcessComparisonCell : UserControl
	{
		#region Constructor
		public ProcessComparisonCell()
		{
			InitializeComponent();
		}

		public ProcessComparisonCell(ItemExchange CrystallRule) : this()
		{
			this.LoadData(CrystallRule);
		}
		#endregion


		#region Functions
		public void LoadData(ItemExchange CrystallRule)
		{
			this.Controls.Remove(this.itemIconCell1);
			this.Controls.Remove(this.itemIconCell2);

			if (CrystallRule is null) return;


			#region RequiredItem
			var RequiredItems = new List<ItemIconCell>();
			RequiredItems.AddItem(LoadRequiredItem(CrystallRule.RequiredItem1, CrystallRule.RequiredItemStackCount1));
			RequiredItems.AddItem(LoadRequiredItem(CrystallRule.RequiredItem2, CrystallRule.RequiredItemStackCount2));
			RequiredItems.AddItem(LoadRequiredItem(CrystallRule.RequiredItem3, CrystallRule.RequiredItemStackCount3));
			RequiredItems.AddItem(LoadRequiredItem(CrystallRule.RequiredItem4, CrystallRule.RequiredItemStackCount4));
			#endregion

			#region NormalItem
			var NormalItems = new List<ItemIconCell>();
			NormalItems.AddItem(LoadNormalItem(CrystallRule.NormalItem1, CrystallRule.NormalItemStackCount1));
			NormalItems.AddItem(LoadNormalItem(CrystallRule.NormalItem2, CrystallRule.NormalItemStackCount2));
			NormalItems.AddItem(LoadNormalItem(CrystallRule.NormalItem3, CrystallRule.NormalItemStackCount3));
			NormalItems.AddItem(LoadNormalItem(CrystallRule.NormalItem4, CrystallRule.NormalItemStackCount4));
			#endregion


			#region UI
			this.SuspendLayout();

			int LocX = 0;
			int Padding = 2;

			foreach (var c in RequiredItems)
			{
				if (!this.Controls.Contains(c)) this.Controls.Add(c);

				c.Location = new Point(LocX, 0);
				LocX += c.Scale + Padding;
			}

			this.pictureBox3.Location = new Point(LocX + 20, this.pictureBox3.Top);
			LocX += 40 + this.pictureBox3.Width;

			foreach (var c in NormalItems)
			{
				if (!this.Controls.Contains(c)) this.Controls.Add(c);

				c.Location = new Point(LocX, 0);
				LocX += c.Scale + Padding;
			}

			this.ResumeLayout();
			#endregion
		}

		private static ItemIconCell LoadRequiredItem(string ItemAlias, short StackCount) => ItemAlias.CastObject().GetObjIcon(StackCount);

		private static ItemIconCell LoadNormalItem(string ItemAlias, short StackCount) => FileCache.Data.Item[ItemAlias].GetObjIcon(StackCount);
		#endregion
	}
}