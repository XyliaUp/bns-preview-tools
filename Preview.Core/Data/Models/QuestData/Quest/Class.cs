using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Models.QuestData.Enums;

namespace Xylia.Preview.Data.Models.QuestData;
public class AcquisitionLoss : Record
{
	public Ref<Faction> Faction;

	[Repeat(15)]
	public JobSeq[] Job;

	[Repeat(4)]
	public SexSeq[] Sex;

	[Repeat(4)]
	public RaceSeq[] Race;

	public int Money;

	[Repeat(4)]
	public Ref<Item>[] Item;

	[Repeat(4)]
	public sbyte[] ItemCount;
}

public class Acquisition : Record
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }

	public List<BasicReward> BasicReward { get; set; }

	public List<FixedReward> FixedReward { get; set; }

	public List<OptionalReward> OptionalReward { get; set; }

	public List<AcquisitionLoss> AcquisitionLoss { get; set; }




	public Ref<Npc> Npc;

	public Ref<NpcTalkMessage> Msg;

	public Ref<Text> Name2;

	public sbyte Level;

	public sbyte MasteryLevel;

	public MasteryLevelOpenStateSeq MasteryLevelOpenState;
	public enum MasteryLevelOpenStateSeq
	{
		DontCare,
		Close,
		Open,
	}

	public sbyte RecommendedLevel;

	[Repeat(15)]
	public Ref<Quest>[] PrecedingQuest;

	[Repeat(15)]
	public Ref<Quest>[] PrecedingQuestRetired;

	[Repeat(15)]
	public sbyte[] PrecedingQuestMissionStep;

	[Repeat(15)]
	public sbyte[] PrecedingQuestCount;

	public OpCheck PrecedingQuestCheck;

	public Ref<Faction> Faction;

	[Repeat(15)]
	public JobSeq[] Job;

	public short SortNo;

	public ProductionType ProductionId;

	public short ProductionExp;

	public short FactionLevel;

	public short FactionLevelMax;

	public enum DayOfWeekSeq
	{
		None,
		Mon,
		Tue,
		Wed,
		Thu,
		Fri,
		Sat,
		Sun,
	}


	[Repeat(7)]
	public DayOfWeekSeq[] ValidDayofweekStartDay;

	[Repeat(7)]
	public sbyte[] ValidDayofweekStartHour;

	[Repeat(7)]
	public DayOfWeekSeq[] ValidDayofweekEndDay;

	[Repeat(7)]
	public sbyte[] ValidDayofweekEndHour;

	[Repeat(2)]
	public Ref<QuestReward>[] Reward;

	public bool CheckTencentVitality;

	public Ref<GameMessage> TalkToSelfGuideMsg;
}

public class MissionStep : Record
{
	[Name("mission")]
	public List<Mission> Mission { get; set; }

	[Name("mission-step-success")]
	public List<MissionStepSuccess> MissionStepSuccess { get; set; }

	[Name("mission-step-fail")]
	public List<MissionStepFail> MissionStepFail { get; set; }



	public sbyte id;

	public OpCheck CompletionType;

	public short MissionMask;

	public sbyte MissionList;

	public MissionMapTypeSeq MissionMapType;
	public enum MissionMapTypeSeq
	{
		Location,

		MapUnit,
	}

	public Ref<MapInfo> Map;


	[Side(ReleaseSide.Client)]
	public float LocationX;

	[Side(ReleaseSide.Client)]
	public float LocationY;

	[Side(ReleaseSide.Client)]
	public float LocationZ;

	[Side(ReleaseSide.Client)]
	public bool UseAutoNavigation;

	[Side(ReleaseSide.Client)]
	public bool EnableNavigation;

	[Side(ReleaseSide.Client), Obsolete]
	public MapZoomRateSeq MapZoomRate;
	public enum MapZoomRateSeq
	{
		None,
		Min,
		Max,
	}

	public Ref<Text> Desc;

	public Ref<Text> GuideMessage;

	[Repeat(2), Side(ReleaseSide.Client)]
	public Ref<Zone>[] GuideMessageZone;

	[Side(ReleaseSide.Client)]
	public GuideMessageCategorySeq GuideMessageCategory;
	public enum GuideMessageCategorySeq
	{
		Guide,

		Situation,
	}


	public Ref<Effect> Effect;

	public TimeLimitType TimeLimitType;

	public int TimeLimit;

	public bool Hide;

	public Ref<TalkSocial> ProgressTalkSocial;

	public float ProgressTalkSocialDelay;

	public bool Retired;

	public sbyte SkipDestMissionStep;

	[Repeat(3)]
	public Ref<Zone>[] GiveupZone;

	public Ref<ZonePcSpawn> GiveupWarpToPcSpawn;




	[Side(ReleaseSide.Server)]
	public Ref<Decision.QuestDecision> QuestDecision;

	[Side(ReleaseSide.Server), Repeat(2)]
	public Ref<Zone>[] Zone;

	[Side(ReleaseSide.Server)]
	public Ref<Decision.QuestDecision> SkipQuestDecision;

	[Side(ReleaseSide.Server)]
	public Ref<Decision.QuestDecision> SkipQuestDecisionZone;

	[Side(ReleaseSide.Server)]
	public RollBack RollBack;


	public override string GetText => Desc.GetText();
}

public partial class Case
{
	

}

public partial class TutorialCase
{

}

public class CompletionLoss : AcquisitionLoss
{

}

public class GiveupLoss : Record
{
	public Ref<Faction> Faction;

	[Repeat(15)]
	public JobSeq[] Job;

	[Repeat(4)]
	public SexSeq[] Sex;

	[Repeat(4)]
	public RaceSeq[] Race;

	[Repeat(4)]
	public Ref<Item>[] Item;

	[Repeat(4)]
	public sbyte[] ItemCount;
}

public class BasicReward : Record
{
	public int Money;

	public int Exp;

	public ProductionType ProductionId;

	public short ProductionExp;

	public Ref<Faction> Faction;

	public short FactionReputation;
}

public class FixedReward : Record
{
	public Ref<Faction> Faction;

	[Repeat(15)]
	public JobSeq[] Job;

	[Repeat(4)]
	public SexSeq[] Sex;

	[Repeat(4)]
	public RaceSeq[] Race;

	[Repeat(4)]
	public Ref<Record>[] Slot;

	[Repeat(4)]
	public sbyte[] ItemCount;
}

public class OptionalReward : FixedReward
{

}

public class Mission : Record
{
	#region Fields
	public sbyte id;

	//[Side(ReleaseSide.Client)]
	//public string Name;

	[Side(ReleaseSide.Client)]
	public Ref<Text> Name2;

	public short RequiredRegisterValue = 1;

	public sbyte Step;

	public sbyte RegisterStartBit;

	public long RegisterMask;

	[Side(ReleaseSide.Client)]
	public bool ShowKillMapunit;

	[Repeat(3)]
	public Ref<QuestReward>[] Reward;

	public bool CheckTencentVitality;

	public bool ResetTeleportRecycleTime;

	public Ref<Record> RequiredAttraction;

	public sbyte TendencyId;

	public SimpleQuestPlaySectionSeq SimpleQuestPlaySection;
	public enum SimpleQuestPlaySectionSeq
	{
		None,
		Our,
		Enemy,
	}


	public sbyte VariationCount;

	public VariationRequiredConditionTypeSeq VariationRequiredConditionType;
	public enum VariationRequiredConditionTypeSeq
	{
		None,
		FieldPlayPoint,
		FactionScore,
	}

	[Repeat(8)]
	public int[] VariationRequiredConditionValue;

	[Repeat(8)]
	public int[] VariationRequiredFieldPlayPoint;

	[Repeat(8)]
	public short[] VariationRequiredRegisterValue;

	[Repeat(8)]
	public int[] VariationRewardFieldPlayPoint;

	[Repeat(8)]
	public short[] VariationRewardFactionScore;

	[Repeat(8)]
	public int[] VariationRewardAccountExp;

	[Repeat(8)]
	public int[] VariationRewardTendencyScore;
	#endregion

	#region Elements
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }

	public List<BasicReward> BasicReward { get; set; }

	public List<FixedReward> FixedReward { get; set; }

	public List<OptionalReward> OptionalReward { get; set; }

	public List<CompletionLoss> CompletionLoss { get; set; }
	#endregion


	#region Properities
	public string Text => Name2.GetText();
	#endregion
}

public class MissionStepSuccess : Record
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }
}

public class MissionStepFail : Record
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }



	public sbyte RollbackStepId;

	public bool DisposeQuest;

	public sbyte Step;

	[Side(ReleaseSide.Client)]
	public Ref<TalkSocial> FailTalksocial;

	[Side(ReleaseSide.Client)]
	public float FailTalksocialDelay;



	[Side(ReleaseSide.Server)]
	public Ref<Decision.QuestDecision> QuestDecision;

	[Side(ReleaseSide.Server), Repeat(2)]
	public Ref<Zone>[] Zone;
}

public class Completion : Record
{
	public List<NextQuest> NextQuest { get; set; }
}

public class NextQuest : Record
{
	public Ref<Faction> Faction;

	[Repeat(15)]
	public JobSeq[] Job;

	public Ref<Quest> Quest;
}

public class Transit : Record
{
	public List<Destination> Destination { get; set; }
	public List<Complete> Complete { get; set; }
	public List<NotAcquire> NotAcquire { get; set; }


	public sbyte id;

	public Ref<Zone> Zone;
}

public class Destination : Record
{
	public sbyte MissionStepId;

	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class Complete : Record
{
	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class NotAcquire : Record
{
	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}