using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Workbook;

namespace Xylia.Preview.Helper.Output;
public sealed class SetItemOut : OutBase
{
	protected override void CreateData()
	{
		#region Title
		//ExcelInfo.SetColumn("任务序号", 10);
		#endregion

		foreach (var record in FileCache.Data.SetItem.Skip(578))
		{
			var row = ExcelInfo.CreateRow();

			for (int i = 1; i <= 10; i++)
			{
				Console.WriteLine(FileCache.Data.SkillModifyInfoGroup[record.Attributes["count-3-skill-modify-info-group", i]]);
			}
		}
	}
}