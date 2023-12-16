using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class ChallengeList : ModelElement
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


	[Name("challenge-type")]
	public ChallengeTypeSeq ChallengeType { get; set; }

	[Name("required-level")]
	public sbyte RequiredLevel { get; set; }

	[Name("required-mastery-level")]
	public sbyte RequiredMasteryLevel { get; set; }



	[Name("challenge-quest-basic"), Repeat(20)]
	public Ref<Quest>[] ChallengeQuestBasic { get; set; }

	[Name("challenge-quest-expansion"), Repeat(20)]
	public Ref<Quest>[] ChallengeQuestExpansion { get; set; }

	[Name("challenge-quest-grade"), Repeat(20)]
	public Grade[] ChallengeQuestGrade { get; set; }

	[Name("challenge-quest-complete-count")]
	public sbyte ChallengeQuestCompleteCount { get; set; }

	[Name("challenge-quest-attraction"), Repeat(20)]
	public Ref<ModelElement>[] ChallengeQuestAttraction { get; set; }

	[Name("challenge-quest-count"), Side(ReleaseSide.Client)]
	public sbyte ChallengeQuestCount { get; set; }

	[Name("challenge-npc-difficulty"), Repeat(20)]
	public DifficultyTypeSeq[] ChallengeNpcDifficulty { get; set; }

	[Name("challenge-npc-kill"), Repeat(20)]
	public Ref<Npc>[] ChallengeNpcKill { get; set; }

	[Name("challenge-npc-attraction"), Repeat(20)]
	public Ref<ModelElement>[] ChallengeNpcAttraction { get; set; }

	[Name("challenge-npc-grade"), Repeat(20)]
	public Grade[] ChallengeNpcGrade { get; set; }

	[Name("challenge-npc-quest"), Repeat(20)]
	public Ref<Quest>[] ChallengeNpcQuest { get; set; }

	[Name("challenge-npc-total-count"), Side(ReleaseSide.Client)]
	public sbyte ChallengeNpcTotalCount { get; set; }

	[Name("challenge-reward-total-count"), Side(ReleaseSide.Client)]
	public sbyte ChallengeRewardTotalCount { get; set; }

	[Name("challenge-count-for-reward"), Repeat(20)]
	public sbyte[] ChallengeCountForReward { get; set; }

	[Repeat(20)]
	public Ref<ChallengeListReward>[] Reward { get; set; }



	#region Sub
	public sealed class DayOfWeek : ChallengeList
	{

	}

	public sealed class Week : ChallengeList
	{
		[Name("week-start-date-time")]
		public DateTime WeekStartDateTime;
	}
	#endregion
}