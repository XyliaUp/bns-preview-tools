using OfficeOpenXml;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers.Output.Quests;
public sealed class QuestOut : OutSet
{
    protected override void CreateData(ExcelWorksheet sheet)
    {
        #region Title
        sheet.SetColumn(Column++, "任务序号", 10);
        sheet.SetColumn(Column++, "任务别名", 15);
        sheet.SetColumn(Column++, "任务名称", 30);
        sheet.SetColumn(Column++, "group", 25);
        sheet.SetColumn(Column++, "category", 10);
        sheet.SetColumn(Column++, "content-type", 10);
        sheet.SetColumn(Column++, "reset-type", 10);
        sheet.SetColumn(Column++, "retired", 10);
        sheet.SetColumn(Column++, "tutorial", 10);
        #endregion


        foreach (var Quest in FileCache.Data.Provider.GetTable<Quest>().OrderBy(o => o.Source.PrimaryKey))
        {
            Row++;
            int column = 1;

            sheet.Cells[Row, column++].SetValue(Quest.Source.PrimaryKey);
            sheet.Cells[Row, column++].SetValue(Quest.Attributes["alias"]);
            sheet.Cells[Row, column++].SetValue(Quest.Text);
            sheet.Cells[Row, column++].SetValue(Quest.Title);
            sheet.Cells[Row, column++].SetValue(Quest.Attributes["category"]);
            sheet.Cells[Row, column++].SetValue(Quest.Attributes["content-type"]);
			sheet.Cells[Row, column++].SetValue(Quest.Attributes["reset-type"]);
			sheet.Cells[Row, column++].SetValue(Quest.Attributes["retired"]);
			sheet.Cells[Row, column++].SetValue(Quest.Attributes["tutorial"]);
		}
    }
}