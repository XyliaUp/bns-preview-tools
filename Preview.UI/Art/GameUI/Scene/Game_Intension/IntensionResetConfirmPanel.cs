using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_Intension;
public partial class IntensionResetConfirmPanel : EquipmentGuidePage
{
	public IntensionResetConfirmPanel() => InitializeComponent();



	List<ItemImproveOptionList> OptionLists;

	public override void SetData()
	{
		var ImproveId = MyWeapon.ImproveId;

		OptionLists = new();
		CurrentOptionList.Clear();
		foreach (var o in FileCache.Data.ItemImprove.Where(o => o.Id == ImproveId && o.SuccessOptionListId != 0))
		{
			var OptionList = FileCache.Data.ItemImproveOptionList[o.SuccessOptionListId];
			if (OptionList is null) continue;

			CurrentOptionList.Add($"强化至 {o.Level + 1} 阶段时追加强化效果");
			OptionLists.Add(OptionList);
		}

		CurrentOptionList.RefreshList();
		CurrentOptionList.Controls[0]?.CallEvent("OnClick");
	}

	private void CurrentOptionList_SelectedItemChanged(object sender, EventArgs e)
	{
		var ImproveLevel = MyWeapon.ImproveLevel;

		var OptionList = OptionLists[CurrentOptionList.SelectedIndex];
		this.SubIngredientPreview.SetData(OptionList);

		#region option
		AcquirableOptionList.Clear();
		foreach (var option in OptionList.Option)
		{
			var _option = option;
			if (option is null) continue;
			if (option.Level < ImproveLevel)
				_option = FileCache.Data.ItemImproveOption[option.Id, ImproveLevel] ?? option;

			AcquirableOptionList.Add(_option.ToString());
		}

		AcquirableOptionList.RefreshList();
		#endregion
	}

	protected override void SubIngredientPreview_RecipeChanged(RecipeChangedEventArgs e)
	{
		this.FixedIngredientPreview.SetData(e.ItemImproveOptionList, "draw-cost-sub-item-" + e.Index, "draw-cost-sub-item-count-" + e.Index, 6);
		this.MoneyCostPreview.MoneyCost = e.ItemImproveOptionList.Attributes[$"draw-cost-money", e.Index].ToInt32();
	}
}