using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_Intension
{
	public partial class SuccessionPanel : ItemGrowth2Page
	{
		#region	Constructor
		public ItemImproveSuccession ItemImproveSuccession;

		public SuccessionPanel() => InitializeComponent();  
		#endregion

		public override void SetData()
		{
			var FeedItem = ItemImproveSuccession.GetFeedItem(MyWeapon);
			var ResultItem = ItemImproveSuccession.GetResultItem(MyWeapon);

			this.ResultWeaponPreview.SetData(ResultItem.alias);
			this.SubIngredientPreview.SetData(FeedItem);
			this.FixedIngredientPreview.SetData(ItemImproveSuccession, "cost-sub-item", "cost-sub-item-count");
			this.MoneyCostPreview.MoneyCost = ItemImproveSuccession.Attributes[$"cost-money"].ToInt();


			// UI.ItemGrowth.Warning.Improvement.CanNotSuccession
			// UI.ItemGrowth.Warning.ItemSpirit.CanNotSuccession 
			this.WarningPreview.Text = $"UI.ItemGrowth2.ImproveSuccession.Warning.SeedItem".GetText();
		}
	}
}