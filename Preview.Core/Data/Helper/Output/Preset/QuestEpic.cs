using Xylia.Preview.Data.Record;
using Xylia.Workbook;

namespace Xylia.Preview.Data.Helper.Output.Preset
{
	public sealed class QuestEpic : OutBase
    {
        protected override string Name => "主线任务";

        protected override void CreateData()
        {
            #region Title
            ExcelInfo.SetColumn("任务序号", 10);
            ExcelInfo.SetColumn("任务别名", 15);
            ExcelInfo.SetColumn("任务名称", 30);
            ExcelInfo.SetColumn("group", 25);
            #endregion

            QuestExtension.GetEpicInfo(data =>
            {
                var row = ExcelInfo.CreateRow();
                row.AddCell(data.id);
                row.AddCell(data.alias);
                row.AddCell(data.Name2.GetText());
                row.AddCell(data.Group2.GetText());
            });
        }
    }
}