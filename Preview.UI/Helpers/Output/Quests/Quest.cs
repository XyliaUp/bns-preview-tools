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
        sheet.SetColumn(Column++, "相关副本", 35);
        #endregion


        foreach (var Quest in FileCache.Data.Quest.OrderBy(o => o.id))
        {
            Row++;
            int column = 1;

            sheet.Cells[Row, column++].SetValue(Quest.id);
            sheet.Cells[Row, column++].SetValue(Quest.Alias);
			sheet.Cells[Row, column++].SetValue(Quest.Name2.GetText());
			sheet.Cells[Row, column++].SetValue(Quest.Group2.GetText());
			sheet.Cells[Row, column++].SetValue(Quest.Category);
			sheet.Cells[Row, column++].SetValue(Quest.ContentType);
			sheet.Cells[Row, column++].SetValue(Quest.ResetType);
			sheet.Cells[Row, column++].SetValue(Quest.Retired);
			sheet.Cells[Row, column++].SetValue(Quest.Tutorial);
			sheet.Cells[Row, column++].SetValue(Quest.AttractionInfo.Instance?.GetText ?? Quest.AttractionInfo.ToString());
		}
    }
}