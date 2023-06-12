using System.Collections.Generic;
using System.Linq;

using NPOI.SS.UserModel;

using Xylia.Extension;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Data.Helper.Output.Preset
{
	public sealed class ChallengeList : OutBase
	{
		protected override string Name => "今日挑战";


		protected override void CreateData()
		{
			var TodayChallengeType = new Dictionary<Record.ChallengeList.ChallengeTypeSeq, string>()
			{
				{ Record.ChallengeList.ChallengeTypeSeq.Mon, "UI.DayOfWeek.Monday".GetText() },
				{ Record.ChallengeList.ChallengeTypeSeq.Tue, "UI.DayOfWeek.Tuesday".GetText() },
				{ Record.ChallengeList.ChallengeTypeSeq.Wed, "UI.DayOfWeek.Wednesday".GetText() },
				{ Record.ChallengeList.ChallengeTypeSeq.Thu, "UI.DayOfWeek.Thursday".GetText() },
				{ Record.ChallengeList.ChallengeTypeSeq.Fri, "UI.DayOfWeek.Friday".GetText() },
				{ Record.ChallengeList.ChallengeTypeSeq.Sat, "UI.DayOfWeek.Saturday".GetText() },
				{ Record.ChallengeList.ChallengeTypeSeq.Sun, "UI.DayOfWeek.Sunday".GetText() },
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
				for (int idx = 1; idx <= 20; idx++)
				{
					var ChallengeQuestBasic = challengeList.Attributes["challenge-quest-basic-" + idx];
					var ChallengeQuestExpansion = challengeList.Attributes["challenge-quest-expansion-" + idx];

					if (ChallengeQuestBasic is null) break;

					var Info = FileCache.Data.Quest[ChallengeQuestBasic].Name2.GetText();
					rows[rowIdx++].CreateCell(cellIdx).SetCellValue(Info);
				}
				#endregion

				#region Npc Task
				for (int idx = 1; idx <= 20; idx++)
				{
					var ChallengeNpcDifficulty = challengeList.Attributes["challenge-npc-difficulty-" + idx].ToEnum<DifficultyType>();
					var ChallengeNpcKill = challengeList.Attributes["challenge-npc-kill-" + idx];
					var ChallengeNpcAttraction = challengeList.Attributes["challenge-npc-attraction-" + idx];

					var KillNpc = FileCache.Data.Npc[ChallengeNpcKill];
					if (KillNpc is null) break;

					var AttractionInfo = ChallengeNpcAttraction.CastObject().GetName();
					var Info = $"[{ChallengeNpcDifficulty.GetName()}] {AttractionInfo} - {KillNpc.Name2.GetText()}";
					rows[rowIdx++].CreateCell(cellIdx).SetCellValue(Info);
				}
				#endregion
			}
		}
	}
}