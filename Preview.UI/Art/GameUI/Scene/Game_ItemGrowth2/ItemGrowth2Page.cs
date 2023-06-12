using System.Collections.Generic;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	public partial class ItemGrowth2Page : EquipmentGuidePage
	{
		#region	Constructor
		public IEnumerable<ItemTransformRecipe> Recipes;

		public ItemGrowth2Page() => InitializeComponent();
		#endregion

		
		public override void SetData() => this.ResultWeaponPreview.SetData(Recipes);

		protected virtual void ResultWeaponPreview_ResultItemChanged(ResultItemChangedEventArgs e)
		{
			if (e.Recipes is not null)
				this.SubIngredientPreview.SetData(e.Recipes);
		}

		protected override void SubIngredientPreview_RecipeChanged(RecipeChangedEventArgs e)
		{
			this.FixedIngredientPreview.SetData(e.ItemTransformRecipe , "fixed-ingredient" , "fixed-ingredient-stack-count");
			this.MoneyCostPreview.MoneyCost = e.ItemTransformRecipe.MoneyCost;

			//获取特殊说明	
			var warning = e.ItemTransformRecipe.Warning;
			this.WarningPreview.Text = warning switch
			{
				ItemTransformRecipe.WarningSeq.None => null,
				ItemTransformRecipe.WarningSeq.DeleteParticle => "UI.Sewing.Warning.DeleteParticle".GetText(),
				ItemTransformRecipe.WarningSeq.DeleteDesign => "UI.Sewing.Warning.DeleteDesign".GetText(),

				_ => $"Transform.Warning.{ warning.GetSignal() }".GetText(),
			};
		}
	}
}