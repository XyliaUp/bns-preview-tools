using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.QuestData;
using Xylia.Preview.Data.Record.QuestData.Enums;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class Quest : BaseRecord
{
	#region Fields
	public Lazy<List<Acquisition>> Acquisition;

	public Lazy<List<MissionStep>> MissionStep;

	public Lazy<List<Completion>> Completion;

	public Lazy<List<Transit>> Transit;

	public Lazy<List<Complete>> Complete;

	public Lazy<List<GiveupLoss>> GiveupLoss;

	public override int Key() => this.id;



	public int id;

	[Signal("max-repeat")]
	public int MaxRepeat;

	[Side(ReleaseSide.Client)]
	public string Name;

	[Side(ReleaseSide.Client)]
	public Text Name2;

	[Signal("district-1")]
	public District District1;

	[Signal("district-2")]
	public District District2;

	[Signal("map-group-1-1")]
	public MapGroup1 Map_Group_1_1;

	[Signal("map-group-1-2")]
	public MapGroup1 Map_Group_1_2;

	[Side(ReleaseSide.Client)]
	public string Group;

	[Side(ReleaseSide.Client)]
	public Text Group2;

	[Side(ReleaseSide.Client)]
	public Text Desc;

	[Side(ReleaseSide.Client)]
	[Signal("completed-desc")]
	public Text CompletedDesc;

	public Category Category;

	[Signal("completed-list")]
	public bool CompletedList;

	[Side(ReleaseSide.Client)]
	public /*Grade*/ byte Grade;

	public bool Tutorial;

	[Side(ReleaseSide.Client)]
	[Signal("show-tutorial-tag")]
	public bool ShowTutorialTag;

	[Signal("last-mission-step")]
	public byte LastMissionStep;

















	[Signal("effect-exist")]
	public bool EffectExist;

	[Signal("day-of-week")]
	public QuestDayOfWeekSeq DayOfWeek;

	public enum QuestDayOfWeekSeq
	{
		None,

		Daily,

		Weekly,

		Monthly,

		Mon,

		Tue,

		Wed,

		The,

		Fri,

		Sat,

		Sun,

		[Signal("sat-sun")]
		SatSun,

		[Signal("fri-sat-sun")]
		FriSatSun,
	}

	[Signal("reset-type")]
	public ResetType ResetType;

	[Signal("reset-by-acquire-time")]
	public ResetType2 ResetByAcquireTime;

	[Signal("reset-day-of-week")]
	public BDayOfWeek ResetDayOfWeek;

	[Signal("reset-day-of-month")]
	public byte ResetDayOfMonth;

	[Signal("activated-faction")]
	public Faction ActivatedFaction;

	[Signal("main-faction")]
	public Faction MainFaction;

	public ProductionType Production;

	[Signal("save-type")]
	public SaveType SaveType;

	[Side(ReleaseSide.Client)]
	[Signal("invoke-fx-msg")]
	public bool InvokeFxMsg;

	public Dungeon Dungeon;

	[Signal("dungeon-type")]
	public DungeonTypeSeq DungeonType;

	public enum DungeonTypeSeq
	{
		unbind,

		bind,
	}


	[Signal("craft-type")]
	public CraftType CraftType;

	[Signal("content-type")]
	public ContentType ContentType;

	/// <summary>
	/// 不再使用
	/// </summary>
	public bool Retired;

	[DefaultValue(true)]
	[Signal("progress-difficulty-type-1")]
	public bool ProgressDifficultyType1 = true;

	[DefaultValue(true)]
	[Signal("progress-difficulty-type-2")]
	public bool ProgressDifficultyType2 = true;

	[DefaultValue(true)]
	[Signal("progress-difficulty-type-3")]
	public bool ProgressDifficultyType3 = true;

	[DefaultValue(true)]
	[Signal("progress-difficulty-type-always")]
	public bool ProgressDifficultyTypeAlways = true;

	[Signal("attraction-1")]
	public string Attraction1;

	[Signal("attraction-2")]
	public string Attraction2;

	[Signal("attraction-3")]
	public string Attraction3;

	[Signal("attraction-4")]
	public string Attraction4;

	[Signal("attraction-info")]
	public string AttractionInfo;

	[Obsolete]
	[Signal("reset-enable")]
	public bool ResetEnable;

	[Obsolete]
	[Signal("reset-money")]
	public bool ResetMoney;

	[Obsolete]
	[Signal("reset-item-1")]
	public string ResetItem1;

	[Obsolete]
	[Signal("reset-item-2")]
	public string ResetItem2;

	[Obsolete]
	[Signal("reset-item-3")]
	public string ResetItem3;

	[Obsolete]
	[Signal("reset-item-4")]
	public string ResetItem4;

	[Obsolete]
	[Signal("reset-item-count-1")]
	public int ResetItemCount1;

	[Obsolete]
	[Signal("reset-item-count-2")]
	public int ResetItemCount2;

	[Obsolete]
	[Signal("reset-item-count-3")]
	public int ResetItemCount3;

	[Obsolete]
	[Signal("reset-item-count-4")]
	public int ResetItemCount4;


	[Side(ReleaseSide.Client)]
	[Signal("acquire-talksocial")]
	public TalkSocial AcquireTalksocial;

	[Side(ReleaseSide.Client)]
	[Signal("acquire-talksocial-delay")]
	public float AcquireTalksocialDelay;

	[Side(ReleaseSide.Client)]
	[Signal("complete-talksocial")]
	public TalkSocial CompleteTalksocial;

	[Side(ReleaseSide.Client)]
	[Signal("complete-talksocial-delay")]
	public float CompleteTalksocialDelay;

	[Signal("check-vitality")]
	public bool CheckVitality;

	[Signal("valid-date-start-year")]
	public short ValidDateStartYear;

	[Signal("valid-date-start-month")]
	public byte ValidDateStartMonth;

	[Signal("valid-date-start-day")]
	public byte ValidDateStartDay;

	[Signal("valid-date-end-year")]
	public short ValidDateEndYear;

	[Signal("valid-date-end-month")]
	public byte ValidDateEndMonth;

	[Signal("valid-date-end-day")]
	public byte ValidDateEndDay;

	[Signal("valid-time-start-hour")]
	public byte ValidTimeStartHour;

	[Signal("valid-time-end-hour")]
	public byte ValidTimeEndHour;

	[Signal("valid-dayofweek-sun")]
	public bool ValidDayofweekSun;

	[Signal("valid-dayofweek-mon")]
	public bool ValidDayofweekMon;

	[Signal("valid-dayofweek-tue")]
	public bool ValidDayofweekTue;

	[Signal("valid-dayofweek-wed")]
	public bool ValidDayofweekWed;

	[Signal("valid-dayofweek-thu")]
	public bool ValidDayofweekThu;

	[Signal("valid-dayofweek-fri")]
	public bool ValidDayofweekFri;

	[Signal("valid-dayofweek-sat")]
	public bool ValidDayofweekSat;

	[Signal("replay-epic-original")]
	public string ReplayEpicOriginal;

	[Signal("replay-epic-start-point")]
	public bool ReplayEpicStartPoint;

	[Signal("replay-epic-pcspawn")]
	public ZonePcSpawn ReplayEpicPcspawn;

	public Dungeon Dungeon2;

	[Signal("duel-mission-steps")]
	public byte DuelMissionSteps;

	[Signal("duel-missions")]
	public byte DuelMissions;

	[Signal("duel-cases")]
	public byte DuelCases;

	[Signal("duel-case-subtypes")]
	public short DuelCaseSubtypes;

	[Signal("exceed-level-next-level")]
	public byte ExceedLevelNextLevel;

	[Signal("contents-reset")]
	public ContentsReset ContentsReset;







	[DefaultValue(BroadcastCategory.None)]
	[Signal("broadcast-category")]
	public BroadcastCategory BroadcastCategory;

	[Side(ReleaseSide.Server)]
	[Signal("extra-quest-complete-achievement-1")]
	public string ExtraQuestCompleteAchievement1;

	[Side(ReleaseSide.Server)]
	[Signal("extra-quest-complete-achievement-2")]
	public string ExtraQuestCompleteAchievement2;

	[Side(ReleaseSide.Server)]
	[Signal("extra-quest-complete-achievement-3")]
	public string ExtraQuestCompleteAchievement3;

	[Side(ReleaseSide.Server)]
	[Signal("replay-epic-zone-leave-cinematic")]
	public string ReplayEpicZoneLeaveCinematic;

	[Signal("cinema-check")]
	public bool CinemaCheck;

	[Signal("replay-check")]
	public bool ReplayCheck;
	#endregion


	#region Functions
	public override void LoadData(XmlElement data)
	{
		base.LoadData(data);

		this.Acquisition = new(() => BaseRecord.LoadChildren<Acquisition>(data, "acquisition"));
		this.MissionStep = new(() => BaseRecord.LoadChildren<MissionStep>(data, "mission-step"));
		this.Transit = new(() => BaseRecord.LoadChildren<Transit>(data, "transit"));
		this.Completion = new(() => BaseRecord.LoadChildren<Completion>(data, "completion"));
		this.GiveupLoss = new(() => BaseRecord.LoadChildren<GiveupLoss>(data, "giveup-loss"));
		this.Complete = new(() => BaseRecord.LoadChildren<Complete>(data, "complete"));
	}






	//protected bool SerializeChild(ReleaseSide ReleaseSide) => !this.Retired || ReleaseSide == ReleaseSide.Server;
	public void Save(string OutFolder, ReleaseSide side = default)
	{
		Directory.CreateDirectory(OutFolder);
		this.XmlInfo(side).Save(OutFolder + $"\\questdata.{this.id}.xml");
	}
	#endregion

	#region	Property
	public Color ForeColor
	{
		get
		{
			if (this.Retired) return Color.Red;

			var RecommendedLevel = this.Acquisition.Value?.FirstOrDefault()?.RecommendedLevel ?? 0;
			if (RecommendedLevel < 60 - 10) return Color.Gray;

			return Color.LightGreen;
		}
	}
	#endregion
}


public static partial class QuestExtension
{
	public static void GetEpicInfo(Action<Quest> act, JobSeq TargetJob = JobSeq.소환사) => GetEpicInfo("q_epic_221", act, TargetJob);

	public static void GetEpicInfo(this string QuestAlias, Action<Quest> act, JobSeq TargetJob = JobSeq.소환사)
	{
		var QuestData = FileCache.Data.Quest[QuestAlias];
		if (QuestData is null) return;

		//执行活动
		act(QuestData);

		//获取下一任务信息
		var Completion = QuestData.Completion.Value?.FirstOrDefault();
		if (Completion is null) return;
		foreach (var NextQuest in Completion.NextQuest)
		{
			if (NextQuest.Job1 == JobSeq.JobNone || NextQuest.Job1 == TargetJob)
				GetEpicInfo(NextQuest.Quest, act, TargetJob);
		}
	}
}