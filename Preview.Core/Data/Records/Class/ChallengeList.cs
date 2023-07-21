using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
public class ChallengeList : BaseRecord
{
	#region Enums
	public enum ChallengeTypeSeq
	{
		None,
		Sun,
		Mon,
		Tue,
		Wed,
		Thu,
		Fri,
		Sat,

		Week1,
		Week2,
		Week3,
		Week4,
		Week5,
		Week6,
		Week7,
		Week8,
		Week9,
		Week10,
	}

	public enum Grade : byte
	{
		None,
		Grade1,
		Grade2,
		Grade3,
	}
	#endregion


	[Signal("challenge-type")]
	public ChallengeTypeSeq ChallengeType;

	[Signal("required-level")]
	public byte RequiredLevel;

	[Signal("required-mastery-level")]
	public byte RequiredMasteryLevel;



	[Signal("challenge-quest-basic"), Repeat(20)]
	public Quest[] ChallengeQuestBasic;

	[Signal("challenge-quest-expansion"), Repeat(20)]
	public Quest[] ChallengeQuestExpansion;

	[Signal("challenge-quest-grade"), Repeat(20)]
	public Grade[] ChallengeQuestGrade;

	[Signal("challenge-quest-complete-count")]
	public byte ChallengeQuestCompleteCount;

	[Signal("challenge-quest-attraction"), Repeat(20)]
	public BaseRecord[] ChallengeQuestAttraction;

	[Signal("challenge-quest-count"), Side(ReleaseSide.Client)]
	public byte ChallengeQuestCount;

	[Signal("challenge-npc-difficulty"), Repeat(20)]
	public DifficultyType[] ChallengeNpcDifficulty;

	[Signal("challenge-npc-kill"), Repeat(20)]
	public Npc[] ChallengeNpcKill;

	[Signal("challenge-npc-attraction"), Repeat(20)]
	public BaseRecord[] ChallengeNpcAttraction;

	[Signal("challenge-npc-grade"), Repeat(20)]
	public Grade[] ChallengeNpcGrade;

	[Signal("challenge-npc-quest"), Repeat(20)]
	public Quest[] ChallengeNpcQuest;

	[Signal("challenge-npc-total-count"), Side(ReleaseSide.Client)]
	public byte ChallengeNpcTotalCount;

	[Signal("challenge-reward-total-count"), Side(ReleaseSide.Client)]
	public byte ChallengeRewardTotalCount;

	[Signal("challenge-count-for-reward"), Repeat(20)]
	public byte[] ChallengeCountForReward;

	[Signal("reward"), Repeat(20)]
	public ChallengeListReward[] Reward;



	#region Sub
	public sealed class DayOfWeek : ChallengeList
	{

	}

	public sealed class Week : ChallengeList
	{
		[Signal("week-start-date-time")]
		public DateTime WeekStartDateTime;
	}
	#endregion
}