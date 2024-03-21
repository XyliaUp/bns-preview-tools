using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class ChallengeList : ModelElement
{
	#region Attributes
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

	public ChallengeTypeSeq ChallengeType { get; set; }

	public sbyte RequiredLevel { get; set; }

	public sbyte RequiredMasteryLevel { get; set; }


	public Ref<Quest>[] ChallengeQuestBasic { get; set; }

	public Ref<Quest>[] ChallengeQuestExpansion { get; set; }

	public Grade[] ChallengeQuestGrade { get; set; }

	public sbyte ChallengeQuestCompleteCount { get; set; }

	public Ref<ModelElement>[] ChallengeQuestAttraction { get; set; }

	public sbyte ChallengeQuestCount { get; set; }

	public DifficultyTypeSeq[] ChallengeNpcDifficulty { get; set; }

	public Ref<Npc>[] ChallengeNpcKill { get; set; }

	public Ref<ModelElement>[] ChallengeNpcAttraction { get; set; }


	public enum Grade : byte
	{
		None,
		Grade1,
		Grade2,
		Grade3,
	}

	public Grade[] ChallengeNpcGrade { get; set; }

	public Ref<Quest>[] ChallengeNpcQuest { get; set; }

	public sbyte ChallengeNpcTotalCount { get; set; }

	public sbyte ChallengeRewardTotalCount { get; set; }

	public sbyte[] ChallengeCountForReward { get; set; }

	public Ref<ChallengeListReward>[] Reward { get; set; }
	#endregion


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