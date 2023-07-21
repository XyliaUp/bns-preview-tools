using NPOI.SS.UserModel;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

namespace Xylia.Preview.Helper.Output;
public sealed class ChallengeListOut : OutBase
{
	protected override string Name => "今日挑战";

	protected override void CreateData()
	{
		var TodayChallengeType = new Dictionary<ChallengeList.ChallengeTypeSeq, string>()
		{
			{ ChallengeList.ChallengeTypeSeq.Mon, "UI.DayOfWeek.Monday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Tue, "UI.DayOfWeek.Tuesday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Wed, "UI.DayOfWeek.Wednesday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Thu, "UI.DayOfWeek.Thursday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Fri, "UI.DayOfWeek.Friday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Sat, "UI.DayOfWeek.Saturday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Sun, "UI.DayOfWeek.Sunday".GetText() },
		};



		#region Title
		foreach (var ChallengeType in TodayChallengeType)
			ExcelInfo.SetColumn(ChallengeType.Value, 30);

		var rows = new List<IRow>();
		for (int idx = 0; idx <= 50; idx++) rows.Add(ExcelInfo.CreateRow());
		#endregion

		int cellIdx = -1;
		foreach (var ChallengeType in TodayChallengeType)
		{
			var challengeList = FileCache.Data.ChallengeList.FirstOrDefault(o => o.ChallengeType == ChallengeType.Key);
			if (challengeList is null) continue;

			cellIdx++;
			int rowIdx = 0;

			#region Quest Task
			Linq.For(20, (idx) =>
			{
				var quest = challengeList.ChallengeQuestBasic[idx];
				if (quest is null) return;

				var Info = quest.Name2.GetText();
				rows[rowIdx++].CreateCell(cellIdx).SetCellValue(Info);
			});
			#endregion

			#region Npc Task
			Linq.For(20, (idx) =>
			{
				var npc = challengeList.ChallengeNpcKill[idx];
				if (npc is null) return;


				var Difficulty = challengeList.ChallengeNpcDifficulty[idx].GetText();
				var Attraction = challengeList.ChallengeNpcAttraction[idx].GetName();

				var Info = $"[{Difficulty}] {Attraction} - {npc.Name2.GetText()}";

				rows[rowIdx++].CreateCell(cellIdx).SetCellValue(Info);
			});
			#endregion
		}
	}
}