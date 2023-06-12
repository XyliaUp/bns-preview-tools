using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Extension;
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
			#region Ability
			string AbilityText = "无变更";
			if (ItemSpirit.AttachAbility1 != MainAbility.None) AbilityText = $"{ItemSpirit.AttachAbility1.GetName()} {ItemSpirit.AbilityMin1}~{ItemSpirit.AbilityMax1}";
			if (ItemSpirit.AttachAbility2 != MainAbility.None) AbilityText += $"\n{ItemSpirit.AttachAbility2.GetName()} {ItemSpirit.AbilityMin2}~{ItemSpirit.AbilityMax2}";
			this.AbilityInfo.Text = AbilityText;
			#endregion

			#region	ApplicablePart
			string ApplicablePartText = null;
			if (ItemSpirit.ApplicablePart1 != EquipType.None) ApplicablePartText = ItemSpirit.ApplicablePart1.GetName();
			if (ItemSpirit.ApplicablePart2 != EquipType.None) ApplicablePartText += "、" + ItemSpirit.ApplicablePart2.GetName();
			if (ItemSpirit.ApplicablePart3 != EquipType.None) ApplicablePartText += "、" + ItemSpirit.ApplicablePart3.GetName();
			if (ItemSpirit.ApplicablePart4 != EquipType.None) ApplicablePartText += "、" + ItemSpirit.ApplicablePart4.GetName();

			this.ApplicablePartInfo.Text = ApplicablePartText;
			#endregion


			this.FixedIngredientPreview.SetData(ItemSpirit, "fixed-ingredient", "fixed-ingredient-stack-count");
			this.MoneyCostPreview.MoneyCost = ItemSpirit.MoneyCost;

			//获取特殊说明
			if (ItemSpirit.Warning == ItemSpirit.WarningSeq.Fail) this.WarningPreview.Text = $"有一定概率失败。";
		}
	}
}