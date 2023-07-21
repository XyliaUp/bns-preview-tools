using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	public partial class ItemSpiritPage : EquipmentGuidePage
	{
		#region	Constructor
		public ItemSpirit ItemSpirit;

		public ItemSpiritPage() => InitializeComponent();
		#endregion

		public override void SetData()
		{
			this.AbilityInfo.Text = 
				GetText(ItemSpirit.AttachAbility[0], ItemSpirit.AbilityMin[0], ItemSpirit.AbilityMin[0]) + "\n" + 
				GetText(ItemSpirit.AttachAbility[1], ItemSpirit.AbilityMin[1], ItemSpirit.AbilityMin[1]);

			this.ApplicablePartInfo.Text = ItemSpirit.ApplicablePart.Select(o => o.GetName()).Aggregate("、");
			this.FixedIngredientPreview.SetData(ItemSpirit, "fixed-ingredient", "fixed-ingredient-stack-count");
			this.MoneyCostPreview.MoneyCost = ItemSpirit.MoneyCost;

			if (ItemSpirit.Warning == ItemSpirit.WarningSeq.Fail) this.WarningPreview.Text = $"有一定概率失败。";
		}


		private string GetText(MainAbility ability , int min , int max)
		{
			if (ability == MainAbility.None) return null;
			return $"{ability.GetName()} {min}~{max}"; 
		}
	}
}