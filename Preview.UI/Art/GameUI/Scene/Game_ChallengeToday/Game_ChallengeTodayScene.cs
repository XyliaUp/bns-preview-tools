using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.Forms;

using BNSTag = Xylia.Preview.Common.Tag;

namespace Xylia.Preview.GameUI.Scene.Game_ChallengeToday
{
	public partial class Game_ChallengeTodayScene : PreviewFrm
	{
		#region Fields
		public static Dictionary<ChallengeList.ChallengeTypeSeq, string> TodayChallengeType = new()
		{
			{ ChallengeList.ChallengeTypeSeq.Mon, "UI.DayOfWeek.Monday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Tue, "UI.DayOfWeek.Tuesday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Wed, "UI.DayOfWeek.Wednesday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Thu, "UI.DayOfWeek.Thursday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Fri, "UI.DayOfWeek.Friday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Sat, "UI.DayOfWeek.Saturday".GetText() },
			{ ChallengeList.ChallengeTypeSeq.Sun, "UI.DayOfWeek.Sunday".GetText() },
		};


		public static DayOfWeek WeeklyResetDayOfWeek = DayOfWeek.Friday;

		public static byte DailyResetTime = 6;

		public static byte WeeklyResetTime = 6;
		#endregion


		#region Constructor
		public Game_ChallengeTodayScene()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;


			this.Text = "UI.ChallengeToday.Title".GetText();

			//UI.ChallengeToday.Condition.MasteryLevel
			//UI.ChallengeToday.Condition.Level


			this.ChallengeToday_ChallengeRewardGuide.Text = "UI.ChallengeToday.ChallengeRewardGuide".GetText();
			this.ChallengeToday_CompleteCount.Params[2] = 0; 
			this.ChallengeToday_CompleteCount.Text = "UI.ChallengeToday.CompleteCount".GetText();
			this.ChallengeToday_TodayQuest.Text = "UI.ChallengeToday.TodayQuest".GetText();
			this.ChallengeToday_TodayReward.Text = "UI.ChallengeToday.TodayReward.Label".GetText();   //UI.ChallengeToday.DayofWeekReward.Label
			this.ChallengeToday_State.Text = "UI.ChallengeToday.State".GetText();


			this.DaySelect.Source = new();
			foreach (var item in TodayChallengeType) this.DaySelect.Source.Add(item.Value);
			this.DaySelect.Source.Add("UI.QuestQuickSlot.ChallengeThisWeek.QuestGuide".GetText());
			this.DaySelect.SelectedIndex = 0;
		}
		#endregion

		#region Functions (UI)
		private void OutputList_Click(object sender, EventArgs e) => Execute.StartOutput<Data.Helper.Output.Preset.ChallengeList>();
		#endregion


		#region Task
		private void DaySelect_SelectedChangedEvent(object sender, EventArgs e)
		{
			if (DaySelect.TextValue == "UI.QuestQuickSlot.ChallengeThisWeek.QuestGuide".GetText())
			{
				var timer = this.RequiredTime.Timers[1] = new BNSTag.Timer(WeeklyResetDayOfWeek, WeeklyResetTime);
				this.RequiredTime.Text = timer.Span.TotalMinutes > 1 ? "UI.ChallengeThisWeek.LeftTime".GetText() : "UI.ChallengeThisWeek.LeftTime.OneMinute".GetText();


				var Week1 = FileCache.Data.ChallengeList.FirstOrDefault(o => o.ChallengeType == ChallengeList.ChallengeTypeSeq.Week1);
				if (Week1 is null) return;

				//Week1.WeekStartDateTime
				this.LoadData(ChallengeList.ChallengeTypeSeq.Week1);
			}
			else
			{
				var ChallengeType = TodayChallengeType.First(o => DaySelect.TextValue == o.Value).Key;
				var ChallengeDayOfWeek = (System.DayOfWeek)(ChallengeType - 1);

				
				//获取剩余时间
				var TodayOfWeek = DateTime.Now.DayOfWeek;
				var TimerTarget = ChallengeDayOfWeek == TodayOfWeek ? ChallengeDayOfWeek + 1 : ChallengeDayOfWeek;
				var timer = this.RequiredTime.Timers[1] = new BNSTag.Timer(TimerTarget, DailyResetTime);

				this.RequiredTime.Text = (ChallengeDayOfWeek == TodayOfWeek + 1 ?
					timer.Span.TotalMinutes > 1 ? "UI.ChallengeToday.TomorrowLeftTime" : "UI.ChallengeToday.TomorrowLeftTime.OneMinute" :
					timer.Span.TotalMinutes > 1 ? "UI.ChallengeToday.LeftTime" : "UI.ChallengeToday.LeftTime.OneMinute").GetText();

				if (ChallengeDayOfWeek != TodayOfWeek) this.RequiredTime.Text = this.RequiredTime.Text.Replace("UI.ChallengeToday.Today".GetText(), DaySelect.TextValue);


				this.LoadData(ChallengeType);
			}
		}

		private void LoadData(ChallengeList.ChallengeTypeSeq ChallengeType)
		{
			TaskPanel.Controls.Remove<ChallengeCell>();

			var ChallengeList = FileCache.Data.ChallengeList.FirstOrDefault(o => o.ChallengeType == ChallengeType);
			if (ChallengeList is null) return;

			#region Task
			int LocY = 0;

			List<ChallengeCell> ChallengeCells = new();
			for (byte idx = 1; idx <= 20; idx++)
			{
				var ChallengeQuestBasic = ChallengeList.Attributes["challenge-quest-basic-" + idx];
				var ChallengeQuestExpansion = ChallengeList.Attributes["challenge-quest-expansion-" + idx];
				var ChallengeQuestGrade = ChallengeList.Attributes["challenge-quest-grade-" + idx].ToEnum<ChallengeList.Grade>();
				var ChallengeQuestAttraction = ChallengeList.Attributes["challenge-quest-attraction-" + idx];

				if (ChallengeQuestBasic is null) break;

				ChallengeCell ChallengeCell = new();
				ChallengeCells.Add(ChallengeCell);
				ChallengeCell.LoadData(ChallengeQuestBasic, ChallengeQuestExpansion, ChallengeQuestGrade, ChallengeQuestAttraction);
			}

			for (byte idx = 1; idx <= 20; idx++)
			{
				var ChallengeNpcDifficulty = ChallengeList.Attributes["challenge-npc-difficulty-" + idx].ToEnum<DifficultyType>();
				var ChallengeNpcKill = ChallengeList.Attributes["challenge-npc-kill-" + idx];
				var ChallengeNpcAttraction = ChallengeList.Attributes["challenge-npc-attraction-" + idx];
				var ChallengeNpcGrade = ChallengeList.Attributes["challenge-npc-grade-" + idx].ToEnum<ChallengeList.Grade>();
				var ChallengeNpcQuest = ChallengeList.Attributes["challenge-npc-quest-" + idx];

				if (ChallengeNpcKill is null) break;

				ChallengeCell ChallengeCell = new();
				ChallengeCells.Add(ChallengeCell);
				ChallengeCell.LoadData(ChallengeNpcDifficulty, ChallengeNpcKill, ChallengeNpcGrade, ChallengeNpcAttraction, ChallengeNpcQuest);
			}

			contentPanel1.Text = $"0/{ChallengeCells.Count}";
			ChallengeCells.ForEach(cell =>
			{
				TaskPanel.Controls.Add(cell);
				cell.Location = new Point(10, LocY);
				LocY = cell.Bottom;
			});
			#endregion

			#region Reward
			this.ChallengeListReward = new();
			this.ChallengeCountForReward = new();
			for (byte idx = 1; idx <= 20; idx++)
			{
				var reward = FileCache.Data.ChallengeListReward[ChallengeList.Attributes["reward-" + idx]];
				if (reward is null) break;

				this.ChallengeListReward.Add(reward);
				this.ChallengeCountForReward.Add(ChallengeList.Attributes["challenge-count-for-reward-" + idx].ToByte());
			}

			this.SelectReward(0);
			#endregion
		}
		#endregion

		#region Reward
		private List<ChallengeListReward> ChallengeListReward;

		private List<byte> ChallengeCountForReward;

		private byte SeletedIndex;

		private void RewardPreview_PrevSeleted(object sender, EventArgs e) => SelectReward(this.SeletedIndex - 1);

		private void RewardPreview_NextSeleted(object sender, EventArgs e) => SelectReward(this.SeletedIndex + 1);

		private void SelectReward(int Index)
		{
			if (Index < 0 || ChallengeListReward is null || Index >= ChallengeListReward.Count) return;

			var reward = ChallengeListReward[Index];
			var count = ChallengeCountForReward[Index];


			this.ChallengeToday_CompleteCount.Params[2] = Index + 1;
			this.ChallengeToday_CompleteCount.Refresh();

			this.SeletedIndex = (byte)Index;
			this.RewardPreview.LoadData(reward);
			this.RewardPreview.Location = new Point((this.Width - this.RewardPreview.Width) / 2, RewardPreview.Location.Y);
		}
		#endregion
	}
}