using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using static Xylia.Preview.Data.Record.RandomStoreItemDisplay;


namespace Xylia.Preview.GameUI.Scene.Game_RandomStore
{
	public partial class Game_RandomStoreExhibitionScene : Form
	{
		public Game_RandomStoreExhibitionScene()
		{
			InitializeComponent();
			this.TabControl.SelectedIndex = 0;
		}

		private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.DesignMode) return;

			else if (this.TabControl.SelectedTab == this.tabPage1)
			{
				if (Loaded1) return;

				Loaded1 = true;
				RandomStoreItemDisplayList_1.MaxCellNum = 0;
				RandomStoreItemDisplayList_1.Cells = GetCells(RandomStoreTypeSeq.Paid);
			}
			else if (this.TabControl.SelectedTab == this.tabPage2)
			{
				if (Loaded2) return;

				Loaded2 = true;
				RandomStoreItemDisplayList_2.MaxCellNum = 0;
				RandomStoreItemDisplayList_2.Cells = GetCells(RandomStoreTypeSeq.Free);
			}
			else if (this.TabControl.SelectedTab == this.tabPage3)
			{

			}
		}


		bool Loaded1 = false;

		bool Loaded2 = false;

		public static List<ItemDisplayListCell> GetCells(RandomStoreTypeSeq RandomStoreType)
		{
			var StoreItems = FileCache.Data.RandomStoreItemDisplay
				.Where(o => o.RandomStoreType == RandomStoreType)
				.Select(o => new ItemDisplayListCell(o)).ToList();

			StoreItems.Sort();
			return StoreItems;
		}
	}
}