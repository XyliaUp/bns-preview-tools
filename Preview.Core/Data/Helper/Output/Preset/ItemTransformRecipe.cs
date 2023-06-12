using Xylia.Extension;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

namespace Xylia.Preview.Data.Helper.Output.Preset
{
	public sealed class ItemTransformRecipeOut : OutBase
    {
        protected override string Name => "物品成长配方";

        protected override void CreateData()
        {
            #region Title
            ExcelInfo.SetColumn("alias", 40);
            ExcelInfo.SetColumn("目标道具", 25);
            ExcelInfo.SetColumn("装备类型", 20);
            ExcelInfo.SetColumn("物品等级", 15);
            ExcelInfo.SetColumn("结果道具", 25);
            ExcelInfo.SetColumn("概率方式", 15);
            ExcelInfo.SetColumn("配方目录", 20);
            #endregion

            FileCache.Data.ItemTransformRecipe.ForEach(Info =>
            {
                var row = ExcelInfo.CreateRow();
                row.AddCell(Info.alias);

                #region 获取主祭品
                var MainIngredient = Info.MainIngredient?.CastObject();
                if (MainIngredient is Item Item)
                {
                    row.AddCell(Item.Name2);
                    row.AddCell(Item.EquipType.GetName());
                    row.AddCell(Item.ItemGrade);
                }
                else if (MainIngredient is ItemBrand)
                {
                    var key = MainIngredient.Key();
                    var ItemBrandTooltip = FileCache.Data.ItemBrandTooltip[key, (byte)Info.MainIngredientConditionType];

                    row.AddCell("(组) " + (ItemBrandTooltip?.Name2.GetText() ?? key.ToString()));
                    row.AddCell(Info.MainIngredientConditionType.GetName());
                    row.AddCell(ItemBrandTooltip?.ItemGrade);
                }
                #endregion

                row.AddCell(FileCache.Data.Item[Info.TitleItem]?.Name2);
                row.AddCell(Info.UseRandom ? "随机" : "必成");
                row.AddCell(Info.Category.GetSignal());
            });
        }
    }
}