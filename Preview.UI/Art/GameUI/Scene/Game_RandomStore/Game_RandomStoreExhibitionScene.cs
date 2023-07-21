using Xylia.Preview.Data.Helper;
using static Xylia.Preview.Data.Record.RandomStoreItemDisplay;

namespace Xylia.Preview.GameUI.Scene.Game_RandomStore;
public partial class Game_RandomStoreExhibitionScene : Form
{
	public Game_RandomStoreExhibitionScene()
	{
		InitializeComponent();
		this.TabControl.SelectedIndex = 0;
	}

	private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (this.TabControl.SelectedTab == this.tabPage1)
		{
			if (RandomStoreItemDisplayList_1.Items.Any())
				return;

			RandomStoreItemDisplayList_1.MaxItemNum = 0;
			RandomStoreItemDisplayList_1.Items.AddRange(GetCells(RandomStoreTypeSeq.Paid));
			RandomStoreItemDisplayList_1.RefreshList();
		}
		else if (this.TabControl.SelectedTab == this.tabPage2)
		{
			if (RandomStoreItemDisplayList_2.Items.Any()) 
				return;

			RandomStoreItemDisplayList_2.MaxItemNum = 0;
			RandomStoreItemDisplayList_2.Items.AddRange(GetCells(RandomStoreTypeSeq.Free));
			RandomStoreItemDisplayList_2.RefreshList();
		}
		else if (this.TabControl.SelectedTab == this.tabPage3)
		{

		}
	}


	public static ItemDisplayListCell[] GetCells(RandomStoreTypeSeq RandomStoreType)
	{
		var StoreItems = FileCache.Data.RandomStoreItemDisplay
			.Where(o => o.RandomStoreType == RandomStoreType)
			.Select(o => new ItemDisplayListCell(o)).ToList();

		StoreItems.Sort();
		return StoreItems.ToArray();
	}
}