using OfficeOpenXml;

using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers.Output.Quests;
public sealed class QuestEpic : OutSet
{
	protected override void CreateData(ExcelWorksheet sheet)
	{
		#region Title
		sheet.SetColumn(Column++, "任务序号", 10);
		sheet.SetColumn(Column++, "任务别名", 15);
		sheet.SetColumn(Column++, "任务名称", 30);
		sheet.SetColumn(Column++, "group", 25);
		#endregion

		GetEpic(data =>
		{
			Row++;
			int column = 1;

			sheet.Cells[Row, column++].SetValue(data.id);
			sheet.Cells[Row, column++].SetValue(data.Alias);
			sheet.Cells[Row, column++].SetValue(data.Name2.GetText());
			sheet.Cells[Row, column++].SetValue(data.Group2.GetText());
		});
	}


	public static void GetEpic(Action<Quest> act, JobSeq TargetJob = JobSeq.소환사) => GetEpic("q_epic_221", act, TargetJob);

	public static void GetEpic(string alias, Action<Quest> act, JobSeq TargetJob = JobSeq.소환사)
	{
		var quest = FileCache.Data.Quest[alias];
		if (quest is null) return;

		// act
		act(quest);

		// get next
		var Completion = quest.Completion?.FirstOrDefault();
		if (Completion is null) return;
		foreach (var NextQuest in Completion.NextQuest)
		{
			var jobs = NextQuest.Job;
			if (jobs is null || jobs[0] == JobSeq.JobNone || jobs.FirstOrDefault(job => job == TargetJob) != JobSeq.JobNone)
				GetEpic(NextQuest.Quest.Instance?.Alias, act, TargetJob);
		}
	}
}