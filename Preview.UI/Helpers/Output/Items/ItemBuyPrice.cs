using OfficeOpenXml;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers.Output.Items;
public sealed class ItemBuyPriceOut : OutSet
{
    protected override void CreateData(ExcelWorksheet sheet)
    {
		var ItemBuyPriceTable = FileCache.Data.Get<ItemBuyPrice>();
		var ItemBrandTooltiptTable = FileCache.Data.Get<ItemBrandTooltip>();

		#region Title
		sheet.SetColumn(Column++, "alias", 70);
        sheet.SetColumn(Column++, "钱币", 15);
        sheet.SetColumn(Column++, "物品组", 20);
        sheet.SetColumn(Column++, "物品1", 25);
        sheet.SetColumn(Column++, "物品2", 25);
        sheet.SetColumn(Column++, "物品3", 25);
        sheet.SetColumn(Column++, "物品4", 25);
        sheet.SetColumn(Column++, "灵气");
        sheet.SetColumn(Column++, "仙豆");
        sheet.SetColumn(Column++, "龙果");
        sheet.SetColumn(Column++, "仙桃");
        sheet.SetColumn(Column++, "珍珠");
        sheet.SetColumn(Column++, "满足成就点数");
        sheet.SetColumn(Column++, "满足完成成就");
        sheet.SetColumn(Column++, "满足势力等级");
        sheet.SetColumn(Column++, "满足个人战比武等级");
        sheet.SetColumn(Column++, "满足车轮战比武等级");
        sheet.SetColumn(Column++, "满足升龙谷等级");
        sheet.SetColumn(Column++, "满足白鲸湖等级");
        sheet.SetColumn(Column++, "满足银河遗迹等级");
        sheet.SetColumn(Column++, "限购设置");
        #endregion

		foreach (var record in ItemBuyPriceTable)
        {
            Row++;
            int column = 1;


            sheet.Cells[Row, column++].SetValue(record.Alias);
            sheet.Cells[Row, column++].SetValue(record.money);

			#region brand & item
			ItemBrandTooltip ItemBrandTooltip = null;
			var ItemBrand = record.RequiredItembrand.Instance;
			if (ItemBrand != null) ItemBrandTooltip = ItemBrandTooltiptTable.FirstOrDefault(x => x.BrandId == ItemBrand.Id && x.ItemConditionType == record.RequiredItembrandConditionType);
            sheet.Cells[Row, column++].SetValue(ItemBrandTooltip?.Name2.GetText() ?? ItemBrand?.ToString());

            for (int i = 0; i < 4; i++)
            {
                var item = record.RequiredItem[i].Instance;
                var count = record.RequiredItemCount[i];

                if (item is null) sheet.Cells[Row, column++].SetValue("");
                else sheet.Cells[Row, column++].SetValue((item.ItemNameOnly ?? item.ToString()) + " " + count);
            }
            #endregion


            sheet.Cells[Row, column++].SetValue(record.RequiredFactionScore);
            sheet.Cells[Row, column++].SetValue(record.RequiredDuelPoint);
			sheet.Cells[Row, column++].SetValue(record.RequiredPartyBattlePoint);
			sheet.Cells[Row, column++].SetValue(record.RequiredFieldPlayPoint);
			sheet.Cells[Row, column++].SetValue(record.RequiredLifeContentsPoint);
			sheet.Cells[Row, column++].SetValue(record.RequiredAchievementScore);

			#region achievemen
			string AchievementName = record.RequiredAchievementId == 0 ? null :
                FileCache.Data.Get<Achievement>().FirstOrDefault(o => o.Id == record.RequiredAchievementId && o.Step == record.RequiredAchievementStepMin)?.Text;
            sheet.Cells[Row, column++].SetValue(AchievementName);
            #endregion

            sheet.Cells[Row, column++].SetValue(record.FactionLevel);
            sheet.Cells[Row, column++].SetValue(record.CheckSoloDuelGrade);
			sheet.Cells[Row, column++].SetValue(record.CheckTeamDuelGrade);
			sheet.Cells[Row, column++].SetValue(record.CheckBattleFieldGradeOccupationWar);
			sheet.Cells[Row, column++].SetValue(record.CheckBattleFieldGradeCaptureTheFlag);
			sheet.Cells[Row, column++].SetValue(record.CheckBattleFieldGradeLeadTheBall);
			sheet.Cells[Row, column++].SetValue(record.CheckContentQuota.Instance);
		}
    }
}