using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls.Forms;
using Xylia.Preview.GameUI.Scene.Game_Intension;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.ItemGrowth.Scene;
public partial class EquipmentGuideScene : PreviewFrm
{
	#region Constructor
	readonly Item ItemInfo;

	public EquipmentGuideScene(Item MyWeapon)
	{
		InitializeComponent();

		this.ItemInfo = MyWeapon;
		this.Pages = new();
		this.TabControl.TabPages.Clear();

		var recipes = ItemTransformRecipe.QueryRecipe(ItemInfo);
		if (recipes != null && recipes.Any()) SetPage(new ItemGrowth2Page(), "UI.ItemGrowth2.Tab.Product".GetText()).Recipes = recipes;

		var itemspirit =   FileCache.Data.ItemSpirit.FirstOrDefault(o => o.MainIngredient == MyWeapon);
		if (itemspirit != null) SetPage(new ItemSpiritPage(), "UI.ItemSpirit.Panel.Title".GetText()).ItemSpirit = itemspirit;

		if (ItemInfo.ImproveId != 0)
		{
			var improve = FileCache.Data.ItemImprove[ItemInfo.ImproveId, ItemInfo.ImproveLevel];
			if (improve != null) SetPage(new IntensionPanel(), "UI.ItemGrowth.GrowthState.ImproveLabel".GetText()).ItemImprove = improve;

			var improvesuccession = ItemImproveSuccession.QuerySeedItem(ItemInfo);
			if (improvesuccession != null) SetPage(new SuccessionPanel(), "UI.ItemGrowth2.Tab.ImproveSuccession".GetText()).ItemImproveSuccession = improvesuccession;

			SetPage(new IntensionResetConfirmPanel(), "UI.ItemGrowth2.Tab.DrawImprove".GetText());
		}
	}
	#endregion


	#region 页面控制
	public readonly List<EquipmentGuidePage> Pages;

	public T SetPage<T>(T page, string text) where T : EquipmentGuidePage
	{
		page.MyWeapon = ItemInfo;
		Pages.Add(page);

		TabPage tabPage = new(text);
		tabPage.Controls.Add(page);
		this.TabControl.TabPages.Add(tabPage);
		return page;
	}
	#endregion


	private void EquipmentGuideScene_Shown(object sender, EventArgs e)
	{
		foreach (var page in Pages)
		{
			page.BackColor = Color.Black;
			page.SetData();
		}
	}
}