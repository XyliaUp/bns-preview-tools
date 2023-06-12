using System;

using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_Intension
{
	public partial class IntensionPanel : ItemGrowth2Page
	{
		#region	Constructor
		public ItemImprove ItemImprove;

		public IntensionPanel() => InitializeComponent();
		#endregion


		public override void SetData()
		{
			this.ResultWeaponPreview.SetData(this.MyWeapon.ImproveNextItem);
			this.SubIngredientPreview.SetData(ItemImprove);

			if (ItemImprove.SuccessOptionListId != 0)
			{
				var Optionlist = FileCache.Data.ItemImproveOptionList[ItemImprove.SuccessOptionListId];
				System.Diagnostics.Debug.WriteLine($"{ItemImprove.Level} 强化成功时追加强化效果");
			}
		}

		protected override void SubIngredientPreview_RecipeChanged(RecipeChangedEventArgs e)
		{
			this.FixedIngredientPreview.SetData(e.ItemImprove, "cost-sub-item-" + e.Index, "cost-sub-item-count-" + e.Index);
			this.MoneyCostPreview.MoneyCost = e.ItemImprove.Attributes[$"cost-money-{e.Index}"].ToInt();

			//获取特殊说明
			var UseSuccessProbability = e.ItemImprove.Attributes[$"use-success-probability-{e.Index}"].ToBool();
			var FailDiff = e.ItemImprove.Attributes[$"fail-level-diff-{e.Index}"].ToByte();

			if (UseSuccessProbability)
			{
				if (FailDiff != 0)
				{
					this.WarningPreview.Params[1] = FailDiff;
					this.WarningPreview.Text = $"UI.ItemGrowth.Warning.Improvement.FailProbabilityWithoutLevelDown".GetText();
				}
				else
				{
					this.WarningPreview.Text = $"UI.ItemGrowth.Warning.Improvement.FailProbabilityWithoutLevelDown".GetText();
				}
			}
		}
	}
}