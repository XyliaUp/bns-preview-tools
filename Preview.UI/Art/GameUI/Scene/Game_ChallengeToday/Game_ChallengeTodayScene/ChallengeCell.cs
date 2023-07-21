using System.ComponentModel;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;

using static Xylia.Preview.Data.Record.ChallengeList;


namespace Xylia.Preview.GameUI.Scene.Game_ChallengeToday;

[DesignTimeVisible(false)]
public partial class ChallengeCell : UserControl
{
	public ChallengeCell() => InitializeComponent();

	public bool Completed = false;

	public bool CannotAcquire = false;


	#region Functions
	public void LoadData(Quest ChallengeQuestBasic, Quest ChallengeQuestExpansion, Grade Grade, BaseRecord Attraction)
	{
		if (ChallengeQuestBasic is null) return;

		this.LoadAttraction(Attraction, out var grade);
		this.AttractionInfo.Top += 10;

		this.ChallengeName.Params[1] = ChallengeQuestBasic?.Name2.GetText();
		this.ChallengeName.Params[2] = null;
		this.ChallengeName.Params[3] = ChallengeQuestBasic;
		this.ChallengeName.Params[4] = GetGradeInfo(Grade);

		string Part1 = CannotAcquire ? "UI.ChallengeToday.NpcQuest.Cannot.Acquire".GetText() : "<arg p='3:quest.front-icon.scale.100'/>";
		string Part2 = "<p bottommargin='10' topmargin='10'><arg p='4:string'/> <arg p='1:string'/></p>";
		this.ChallengeName.Text = Part1 + Part2;
		this.ChallengeName.ForeColor = ChallengeQuestBasic?.ForeColor ?? default;


		this.QuestList_RewardQuestName.Visible = false;
	}

	public void LoadData(Npc ChallengeNpcKill, Quest ChallengeNpcQuest, Grade Grade, BaseRecord Attraction, DifficultyType ChallengeNpcDifficulty)
	{
		this.LoadAttraction(Attraction, out var grade);
		this.AttractionInfo.Top += 25;

		this.ChallengeName.Params[1] = $"<font name='00008130.Program.Fontset_ItemGrade_{grade}'>{Attraction.GetName()}</font>";
		this.ChallengeName.Params[2] = GetDifficultyInfo(ChallengeNpcDifficulty);
		this.ChallengeName.Params[3] = ChallengeNpcKill;
		this.ChallengeName.Params[4] = GetGradeInfo(Grade);

		string Part1 = CannotAcquire ? "UI.ChallengeToday.NpcQuest.Cannot.Acquire" :
			Completed ? "UI.ChallengeToday.NpcQuest.Completed" : "UI.ChallengeToday.NpcQuest.Progress";
		string Part2 = ChallengeNpcDifficulty == DifficultyType.None ?
			 Completed ? "UI.ChallengeToday.NpcQuestName.Completed" : "UI.ChallengeToday.NpcQuestName" :
			 Completed ? "UI.ChallengeToday.Difficulty.NpcQuestName.Completed" : "UI.ChallengeToday.Difficulty.NpcQuestName";
		this.ChallengeName.Text = Part1.GetText() + Part2.GetText();



		this.QuestList_RewardQuestName.Visible = ChallengeNpcQuest != null;
		this.QuestList_RewardQuestName.Params[2] = ChallengeNpcQuest;
		this.QuestList_RewardQuestName.Text = "UI.ChallengeToday.QuestList.RewardQuestName".GetText();
	}




	private static string GetGradeInfo(Grade ChallengeGrade) => ChallengeGrade switch
	{
		Grade.Grade1 => "UI.ChallengeToday.Difficulty.Grade1".GetText(),
		Grade.Grade2 => "UI.ChallengeToday.Difficulty.Grade2".GetText(),
		Grade.Grade3 => "UI.ChallengeToday.Difficulty.Grade3".GetText(),
		_ => null,
	};

	private static string GetDifficultyInfo(DifficultyType DifficultyType) => DifficultyType switch
	{
		DifficultyType.Easy => "UI.AttractionShortcutPanel.1.btn1.name".GetText(),
		DifficultyType.Normal => "UI.AttractionShortcutPanel.1.btn2.name".GetText(),
		DifficultyType.Hard => "UI.AttractionShortcutPanel.1.btn3.name".GetText(),
		_ => null,
	};


	private void LoadAttraction(BaseRecord Attraction, out byte grade)
	{
		grade = 1;
		string text;

		if (Attraction is Cave2 cave2)
		{
			grade = cave2.UiTextGrade;
			if (grade == 0) grade = 4;

			text = "UI.ChallengeToday.QuestGroup.Cave2";
		}
		else if (Attraction is Dungeon dungeon)
		{
			grade = dungeon.UiTextGrade;
			if (grade == 0) grade = 5;

			text = "UI.ChallengeToday.QuestGroup.Dungeon";
		}
		else if (Attraction is RaidDungeon raidDungeon)
		{
			grade = raidDungeon.UiTextGrade;
			if (grade == 0) grade = 7;

			text = "UI.ChallengeToday.QuestGroup.Dungeon";
		}
		else text = "UI.ChallengeToday.QuestGroup.Quest";

		this.AttractionInfo.Params[2] = $"<link id=''><font name='00008130.UI.Hypertext_Item_Grade{grade}'>{Attraction.GetName()}</font></link>";
		this.AttractionInfo.Text = text.GetText();
	}
	#endregion
}