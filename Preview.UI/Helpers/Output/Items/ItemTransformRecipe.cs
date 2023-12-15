using OfficeOpenXml;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers.Output.Items;
public sealed class ItemTransformRecipeOut : OutSet
{
    protected override void CreateData(ExcelWorksheet sheet)
    {
        #region Title
        sheet.SetColumn(Column++, "alias", 40);
        sheet.SetColumn(Column++, "目标道具", 25);
        sheet.SetColumn(Column++, "装备类型", 20);
        sheet.SetColumn(Column++, "物品等级", 15);
        sheet.SetColumn(Column++, "结果道具", 25);
        sheet.SetColumn(Column++, "概率方式", 15);
        sheet.SetColumn(Column++, "配方目录", 20);
        #endregion

        FileCache.Data.Get<ItemTransformRecipe>().ForEach(Info =>
        {
            Row++;
            int column = 1;


            sheet.Cells[Row, column++].SetValue(Info);

            #region MainIngredient
            var MainIngredient = Info.MainIngredient.Instance;
            if (MainIngredient is Item Item)
            {
                sheet.Cells[Row, column++].SetValue(Item.Name2);
				sheet.Cells[Row, column++].SetValue(Item.EquipType.GetText());
				sheet.Cells[Row, column++].SetValue(Item.ItemGrade);
			}
            else if (MainIngredient is ItemBrand)
            {
                var key = MainIngredient.Source.Ref.Id;
                var ItemBrandTooltip = FileCache.Data.Get<ItemBrandTooltip>()[key, (byte)Info.MainIngredientConditionType];

                sheet.Cells[Row, column++].SetValue("(组) " + (ItemBrandTooltip?.Name2.GetText() ?? key.ToString()));
                sheet.Cells[Row, column++].SetValue(Info.MainIngredientConditionType.GetText());
                sheet.Cells[Row, column++].SetValue(ItemBrandTooltip?.ItemGrade);
            }
            #endregion

            sheet.Cells[Row, column++].SetValue(Info.TitleItem.Instance?.Name2);
            sheet.Cells[Row, column++].SetValue(Info.UseRandom ? "随机" : "必成");
            sheet.Cells[Row, column++].SetValue(Info.Category.GetText());
        });
    }
}