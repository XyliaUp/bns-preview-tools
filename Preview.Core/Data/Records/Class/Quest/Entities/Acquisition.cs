using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.QuestData;

/// <summary>
/// 接取条件
/// 主线任务不可设置接取条件
/// </summary>
public sealed class Acquisition : CaseParent
{
	public Npc Npc;

	public NpcTalkMessage Msg;

	public Text Name2;





	[Signal("check-tencent-vitality")]
	public bool CheckTencentVitality;

	/// <summary>
	/// 要求等级
	/// </summary>
	public int Level;

	/// <summary>
	/// 推荐等级
	/// </summary>
	[Signal("recommended-level")]
	public int RecommendedLevel;

	[Signal("mastery-level")]
	public short MasteryLevel;

	[Signal("faction")] public string Faction;
	[Signal("faction-level")] public byte FactionLevel;
	[Signal("faction-level-max")] public byte FactionLevelMax;
	[Signal("faction-reputation")] public int FactionReputation;

	[Signal("job-1")] public JobSeq Job1;
	[Signal("job-2")] public JobSeq Job2;
	[Signal("job-3")] public JobSeq Job3;
	[Signal("job-4")] public JobSeq Job4;
	[Signal("job-5")] public JobSeq Job5;
	[Signal("job-6")] public JobSeq Job6;
	[Signal("job-7")] public JobSeq Job7;
	[Signal("job-8")] public JobSeq Job8;
	[Signal("job-9")] public JobSeq Job9;
	[Signal("job-10")] public JobSeq Job10;
	[Signal("job-11")] public JobSeq Job11;
	[Signal("job-12")] public JobSeq Job12;
	[Signal("job-13")] public JobSeq Job13;
	[Signal("job-14")] public JobSeq Job14;
	[Signal("job-15")] public JobSeq Job15;

	[DefaultValue(null)]
	[Signal("preceding-quest-check")] 
	public OpCheck PrecedingQuestCheck;

	[Signal("preceding-quest-1")] public string PrecedingQuest1;
	[Signal("preceding-quest-2")] public string PrecedingQuest2;
	[Signal("preceding-quest-3")] public string PrecedingQuest3;
	[Signal("preceding-quest-4")] public string PrecedingQuest4;
	[Signal("preceding-quest-5")] public string PrecedingQuest5;
	[Signal("preceding-quest-6")] public string PrecedingQuest6;
	[Signal("preceding-quest-7")] public string PrecedingQuest7;
	[Signal("preceding-quest-8")] public string PrecedingQuest8;
	[Signal("preceding-quest-9")] public string PrecedingQuest9;
	[Signal("preceding-quest-10")] public string PrecedingQuest10;
	[Signal("preceding-quest-11")] public string PrecedingQuest11;
	[Signal("preceding-quest-12")] public string PrecedingQuest12;
	[Signal("preceding-quest-13")] public string PrecedingQuest13;
	[Signal("preceding-quest-14")] public string PrecedingQuest14;
	[Signal("preceding-quest-15")] public string PrecedingQuest15;


	[Signal("preceding-quest-retired-1")] public string PrecedingQuestRetired1;
	[Signal("preceding-quest-retired-2")] public string PrecedingQuestRetired2;
	[Signal("preceding-quest-retired-3")] public string PrecedingQuestRetired3;
	[Signal("preceding-quest-retired-4")] public string PrecedingQuestRetired4;
	[Signal("preceding-quest-retired-5")] public string PrecedingQuestRetired5;
	[Signal("preceding-quest-retired-6")] public string PrecedingQuestRetired6;
	[Signal("preceding-quest-retired-7")] public string PrecedingQuestRetired7;
	[Signal("preceding-quest-retired-8")] public string PrecedingQuestRetired8;
	[Signal("preceding-quest-retired-9")] public string PrecedingQuestRetired9;
	[Signal("preceding-quest-retired-10")] public string PrecedingQuestRetired10;
	[Signal("preceding-quest-retired-11")] public string PrecedingQuestRetired11;
	[Signal("preceding-quest-retired-12")] public string PrecedingQuestRetired12;
	[Signal("preceding-quest-retired-13")] public string PrecedingQuestRetired13;
	[Signal("preceding-quest-retired-14")] public string PrecedingQuestRetired14;
	[Signal("preceding-quest-retired-15")] public string PrecedingQuestRetired15;

	[Signal("preceding-quest-mission-step-1")] public byte PrecedingQuestMissionStep1;
	[Signal("preceding-quest-mission-step-2")] public byte PrecedingQuestMissionStep2;
	[Signal("preceding-quest-mission-step-3")] public byte PrecedingQuestMissionStep3;
	[Signal("preceding-quest-mission-step-4")] public byte PrecedingQuestMissionStep4;
	[Signal("preceding-quest-mission-step-5")] public byte PrecedingQuestMissionStep5;
	[Signal("preceding-quest-mission-step-6")] public byte PrecedingQuestMissionStep6;
	[Signal("preceding-quest-mission-step-7")] public byte PrecedingQuestMissionStep7;
	[Signal("preceding-quest-mission-step-8")] public byte PrecedingQuestMissionStep8;
	[Signal("preceding-quest-mission-step-9")] public byte PrecedingQuestMissionStep9;
	[Signal("preceding-quest-mission-step-10")] public byte PrecedingQuestMissionStep10;
	[Signal("preceding-quest-mission-step-11")] public byte PrecedingQuestMissionStep11;
	[Signal("preceding-quest-mission-step-12")] public byte PrecedingQuestMissionStep12;
	[Signal("preceding-quest-mission-step-13")] public byte PrecedingQuestMissionStep13;
	[Signal("preceding-quest-mission-step-14")] public byte PrecedingQuestMissionStep14;
	[Signal("preceding-quest-mission-step-14")] public byte PrecedingQuestMissionStep15;

	[Signal("preceding-quest-count-1")] public byte PrecedingQuestCount1;
	[Signal("preceding-quest-count-2")] public byte PrecedingQuestCount2;
	[Signal("preceding-quest-count-3")] public byte PrecedingQuestCount3;
	[Signal("preceding-quest-count-4")] public byte PrecedingQuestCount4;
	[Signal("preceding-quest-count-5")] public byte PrecedingQuestCount5;
	[Signal("preceding-quest-count-6")] public byte PrecedingQuestCount6;
	[Signal("preceding-quest-count-7")] public byte PrecedingQuestCount7;
	[Signal("preceding-quest-count-8")] public byte PrecedingQuestCount8;
	[Signal("preceding-quest-count-9")] public byte PrecedingQuestCount9;
	[Signal("preceding-quest-count-10")] public byte PrecedingQuestCount10;
	[Signal("preceding-quest-count-11")] public byte PrecedingQuestCount11;
	[Signal("preceding-quest-count-12")] public byte PrecedingQuestCount12;
	[Signal("preceding-quest-count-13")] public byte PrecedingQuestCount13;
	[Signal("preceding-quest-count-14")] public byte PrecedingQuestCount14;
	[Signal("preceding-quest-count-15")] public byte PrecedingQuestCount15;


	[Signal("production-id")] 
	public ProductionType ProductionId;

	[Signal("production-exp")] 
	public int ProductionExp;

	[Signal("reward-1")]
	public QuestReward Reward1;

	[Signal("reward-2")]
	public QuestReward Reward2;

	[Signal("sort-no")]
	public short SortNo;

	[Signal("talk-to-self-guide-msg")]
	public string TalkToSelfGuideMsg;



	[Signal("valid-dayofweek-start-day-1")] public string ValidDayofweekStartDay1;
	[Signal("valid-dayofweek-start-day-2")] public string ValidDayofweekStartDay2;
	[Signal("valid-dayofweek-start-day-3")] public string ValidDayofweekStartDay3;
	[Signal("valid-dayofweek-start-day-4")] public string ValidDayofweekStartDay4;
	[Signal("valid-dayofweek-start-day-5")] public string ValidDayofweekStartDay5;
	[Signal("valid-dayofweek-start-day-6")] public string ValidDayofweekStartDay6;
	[Signal("valid-dayofweek-start-day-7")] public string ValidDayofweekStartDay7;
	[Signal("valid-dayofweek-start-hour-1")] public string ValidDayofweekStartHour1;
	[Signal("valid-dayofweek-start-hour-2")] public string ValidDayofweekStartHour2;
	[Signal("valid-dayofweek-start-hour-3")] public string ValidDayofweekStartHour3;
	[Signal("valid-dayofweek-start-hour-4")] public string ValidDayofweekStartHour4;
	[Signal("valid-dayofweek-start-hour-5")] public string ValidDayofweekStartHour5;
	[Signal("valid-dayofweek-start-hour-6")] public string ValidDayofweekStartHour6;
	[Signal("valid-dayofweek-start-hour-7")] public string ValidDayofweekStartHour7;

	[Signal("valid-dayofweek-end-day-1")] public string ValidDayofweekEndDay1;
	[Signal("valid-dayofweek-end-day-2")] public string ValidDayofweekEndDay2;
	[Signal("valid-dayofweek-end-day-3")] public string ValidDayofweekEndDay3;
	[Signal("valid-dayofweek-end-day-4")] public string ValidDayofweekEndDay4;
	[Signal("valid-dayofweek-end-day-5")] public string ValidDayofweekEndDay5;
	[Signal("valid-dayofweek-end-day-6")] public string ValidDayofweekEndDay6;
	[Signal("valid-dayofweek-end-day-7")] public string ValidDayofweekEndDay7;
	[Signal("valid-dayofweek-end-hour-1")] public string ValidDayofweekEndHour1;
	[Signal("valid-dayofweek-end-hour-2")] public string ValidDayofweekEndHour2;
	[Signal("valid-dayofweek-end-hour-3")] public string ValidDayofweekEndHour3;
	[Signal("valid-dayofweek-end-hour-4")] public string ValidDayofweekEndHour4;
	[Signal("valid-dayofweek-end-hour-5")] public string ValidDayofweekEndHour5;
	[Signal("valid-dayofweek-end-hour-6")] public string ValidDayofweekEndHour6;
	[Signal("valid-dayofweek-end-hour-7")] public string ValidDayofweekEndHour7;
}