using Xylia.Preview.Data.Helper;
using Xylia.Preview.UI.Custom.Controls;

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
		if (this.TabControl.SelectedTab == this.tabPage1) GetCells(RandomStoreItemDisplayList_1, RandomStoreTypeSeq.Paid);
		else if (this.TabControl.SelectedTab == this.tabPage2) GetCells(RandomStoreItemDisplayList_2, RandomStoreTypeSeq.Free);
		else if (this.TabControl.SelectedTab == this.tabPage3)
		{

		}
	}



	public static void GetCells(ListPreview preview, RandomStoreTypeSeq RandomStoreType)
	{
		if (preview.Items.Any()) return;

		var items = FileCache.Data.RandomStoreItemDisplay
			.Where(o => o.RandomStoreType == RandomStoreType)
			.Select(o => new ItemDisplayListCell(o)).ToList();

		items.Sort();


		preview.Items = new(items);
		preview.RefreshList();
	}
}