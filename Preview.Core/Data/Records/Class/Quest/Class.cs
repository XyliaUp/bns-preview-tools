using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.QuestData.Enums;

namespace Xylia.Preview.Data.Record.QuestData;

//quest
//filter
//filter-set
//reaction-set


public class AcquisitionLoss : CompletionLoss
{

}

public class Acquisition : CaseParent
{
	public Npc Npc;

	public NpcTalkMessage Msg;

	public Text Name2;



	[Signal("check-tencent-vitality")]
	public bool CheckTencentVitality;

	public int Level;

	[Signal("recommended-level")]
	public int RecommendedLevel;

	[Signal("mastery-level")]
	public short MasteryLevel;

	[Signal("faction")] 
	public Faction Faction;

	[Signal("faction-level")] 
	public byte FactionLevel;

	[Signal("faction-level-max")] 
	public byte FactionLevelMax;

	[Signal("faction-reputation")] 
	public int FactionReputation;

	[Signal("job"), Repeat(15)]
	public JobSeq[] Job;

	[DefaultValue(null)]
	[Signal("preceding-quest-check")]
	public OpCheck PrecedingQuestCheck;

	[Signal("preceding-quest"), Repeat(15)]
	public Quest[] PrecedingQuest;

	[Signal("preceding-quest-retired"), Repeat(15)]
	public Quest[] PrecedingQuestRetired;

	[Signal("preceding-quest-mission-step"), Repeat(15)]
	public byte[] PrecedingQuestMissionStep;

	[Signal("preceding-quest-count"), Repeat(15)]
	public byte[] PrecedingQuestCount;


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

	[Signal("valid-dayofweek-start-day"), Repeat(7)]
	public string[] ValidDayofweekStartDay;

	[Signal("valid-dayofweek-start-hour"), Repeat(7)]
	public string[] ValidDayofweekStartHour;

	[Signal("valid-dayofweek-end-day"), Repeat(7)]
	public string[] ValidDayofweekEndDay;

	[Signal("valid-dayofweek-end-hour"), Repeat(7)]
	public string[] ValidDayofweekEndHour;
}

public class MissionStep : BaseRecord
{
	[Signal("mission")]
	public List<Mission> Mission;

	[Signal("mission-step-success")]
	public List<MissionStepSuccess> MissionStepSuccess;

	[Signal("mission-step-fail")]
	public List<MissionStepFail> MissionStepFail;


	public byte id;

	[Signal("completion-type")]
	public OpCheck CompletionType;

	[Signal("giveup-warp-to-pcspawn")]
	public string GiveupWarpToPcSpawn;

	[Signal("giveup-zone"), Repeat(3)]
	public Zone[] GiveupZone;

	[Signal("progress-talksocial")]
	public string ProgressTalkSocial;

	[Signal("progress-talksocial-delay")]
	public float ProgressTalkSocialDelay;

	public bool Retired;

	[Signal("skip-dest-mission-step")]
	public bool SkipDestMissionStep;

	[Signal("time-limit-type")]
	public TimeLimitType TimeLimitType;

	[Signal("time-limit")]
	public short TimeLimit;

	public bool Hide;

	[Signal("mission-map-type")]
	public MissionMapType MissionMapType;

	[Side(ReleaseSide.Client)]
	public Text Desc;

	[Side(ReleaseSide.Client)]
	[Signal("guide-message-category")]
	public GuideMessageCategory GuideMessageCategory;

	[Side(ReleaseSide.Client)]
	[Signal("guide-message")]
	public Text GuideMessage;

	[Side(ReleaseSide.Client)]
	[Signal("guide-message-zone"), Repeat(2)]
	public Zone[] GuideMessageZone;

	[Side(ReleaseSide.Client)]
	[Signal("location-x")]
	public float LocationX;

	[Side(ReleaseSide.Client)]
	[Signal("location-y")]
	public float LocationY;

	[Side(ReleaseSide.Client)]
	public string Map;

	[Obsolete]
	[Side(ReleaseSide.Client)]
	[Signal("map-zoom-rate")]
	public string MapZoomRate;

	[Side(ReleaseSide.Client)]
	[Signal("enable-navigation")]
	public bool EnableNavigation;

	[Side(ReleaseSide.Client)]
	[Signal("use-auto-navigation")]
	public bool UseAutoNavigation;

	[Side(ReleaseSide.Server)]
	[Signal("quest-decision")]
	public Decision.QuestDecision QuestDecision;

	[Side(ReleaseSide.Server)]
	[Signal("zone"), Repeat(2)]
	public Zone[] Zone;

	[Side(ReleaseSide.Server)]
	[Signal("skip-quest-decision")]
	public Decision.QuestDecision SkipQuestDecision;

	[Side(ReleaseSide.Server)]
	[Signal("skip-quest-decision-zone")]
	public Decision.QuestDecision SkipQuestDecisionZone;

	[Side(ReleaseSide.Server)]
	public RollBack RollBack;
}

public partial class Case
{

}

public partial class TutorialCase
{

}

public class CompletionLoss : BaseRecord
{
	[Signal("job"), Repeat(15)]
	public JobSeq[] Job;

	[Signal("item"), Repeat(4)]
	public string[] Item;

	[Signal("item-count"), Repeat(4)]
	public string[] ItemCount;


	public long Money;
}

public class GiveupLoss : BaseRecord
{
	[Signal("job"), Repeat(15)]
	public JobSeq[] Job;

	[Signal("item"), Repeat(4)]
	public string[] Item;

	[Signal("item-count"), Repeat(4)]
	public string[] ItemCount;
}

public class BasicReward : BaseRecord
{
	public int Money;

	public int Exp;

	[Signal("production-id")]
	public ProductionType ProductionId;

	[Signal("production-exp")]
	public short ProductionExp;

	[Signal("faction")]
	public Faction Faction;

	[Signal("faction-reputation")]
	public short FactionReputation;
}

public class FixedReward : BaseRecord
{
	[Signal("faction")]
	public Faction Faction;

	[Signal("job"), Repeat(15)]
	public JobSeq[] Job;

	[Signal("sex"), Repeat(4)]
	public SexSeq[] Sex;

	[Signal("race"), Repeat(4)]
	public RaceSeq[] Race;

	[Signal("slot"), Repeat(4)]
	public string[] Slot;

	[Signal("item-count"), Repeat(4)]
	public string[] ItemCount;
}

public class OptionalReward : FixedReward
{

}

public class Mission : CaseParent
{
	//max: 16
	public byte id;

	[Signal("check-tencent-vitality")]
	public bool CheckTencentVitality;


	/// <summary>
	/// min: 1
	/// </summary>
	[Signal("required-register-value")]
	public byte RequiredRegisterValue = 1;

	[Signal("reward-1")]
	public QuestReward Reward1;

	[Signal("reward-2")]
	public QuestReward Reward2;

	[Signal("reset-teleport-recycle-time")]
	public bool ResetTeleportRecycleTime;

	[Signal("required-attraction")]
	public string RequiredAttraction;

	[Signal("tendency-id")]
	public byte TendencyID;

	[Signal("simple-quest-play-section")]
	public string SimpleQuestPlaySection;


	[Signal("variation-required-condition-type")]
	public string VariationRequiredConditionType;

	[Signal("variation-required-condition-value"), Repeat(8)]
	public int[] VariationRequiredConditionValue;

	[Signal("variation-required-register-value"), Repeat(8)]
	public int[] VariationRequiredRegisterValue;

	[Signal("variation-required-field-play-point"), Repeat(8)]
	public int[] VariationRequiredFieldPlayPoint;

	[Signal("variation-reward-account-exp"), Repeat(8)]
	public int[] VariationRewardAccountExp;

	[Signal("variation-reward-faction-score"), Repeat(8)]
	public int[] VariationRewardFactionScore;

	[Signal("variation-reward-field-play-point"), Repeat(8)]
	public int[] VariationRewardFieldPlayPoint;

	[Signal("variation-reward-tendency-score"), Repeat(8)]
	public int[] VariationRewardTendencyScore;



	[Side(ReleaseSide.Client)]
	public string Name;

	[Side(ReleaseSide.Client)]
	public string Name2;

	[Side(ReleaseSide.Client)]
	[Signal("show-kill-mapunit")]
	public bool ShowKillMapunit;
}

public class MissionStepSuccess : CaseParent
{

}

public class MissionStepFail : CaseParent
{
	[Signal("rollback-step-id")]
	public byte RollbackStepID;

	[Signal("dispose-quest")]
	public bool DisposeQuest;

	public byte Step;

	[Side(ReleaseSide.Client)]
	[Signal("fail-talksocial")]
	public TalkSocial FailTalksocial;

	[Side(ReleaseSide.Client)]
	[Signal("fail-talksocial-delay")]
	public float FailTalksocialDelay;


	[Side(ReleaseSide.Server)]
	[Signal("quest-decision")]
	public Decision.QuestDecision QuestDecision;

	[Side(ReleaseSide.Server)]
	[Signal("zone"), Repeat(2)]
	public Zone[] Zone;
}

public class Completion : BaseRecord
{
	[Signal("next-quest")]
	public List<NextQuest> NextQuest;
}

public class NextQuest : BaseRecord
{
	public Quest Quest;

	public Faction Faction;

	[Signal("job"), Repeat(15)]
	public JobSeq[] Job;
}

public class Transit : BaseRecord
{
	public byte id;

	public string Zone;


	public List<Destination> Destination;

	public List<Complete> Complete;
}

public class Destination : BaseRecord
{
	[Signal("mission-step-id")]
	public byte MissionStepID;

	[DefaultValue(null)]
	[Signal("zone-index")]
	public byte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class Complete : BaseRecord
{
	[DefaultValue(null)]
	[Signal("zone-index")]
	public byte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class NotAcquire : BaseRecord
{
	[DefaultValue(null)]
	[Signal("zone-index")]
	public byte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}