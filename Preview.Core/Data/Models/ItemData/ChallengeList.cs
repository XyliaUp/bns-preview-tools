using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class ChallengeList : Record
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
	public ChallengeTypeSeq ChallengeType;

	[Name("required-level")]
	public sbyte RequiredLevel;

	[Name("required-mastery-level")]
	public sbyte RequiredMasteryLevel;



	[Name("challenge-quest-basic"), Repeat(20)]
	public Ref<Quest>[] ChallengeQuestBasic;

	[Name("challenge-quest-expansion"), Repeat(20)]
	public Ref<Quest>[] ChallengeQuestExpansion;

	[Name("challenge-quest-grade"), Repeat(20)]
	public Grade[] ChallengeQuestGrade;

	[Name("challenge-quest-complete-count")]
	public sbyte ChallengeQuestCompleteCount;

	[Name("challenge-quest-attraction"), Repeat(20)]
	public Ref<Record>[] ChallengeQuestAttraction;

	[Name("challenge-quest-count"), Side(ReleaseSide.Client)]
	public sbyte ChallengeQuestCount;

	[Name("challenge-npc-difficulty"), Repeat(20)]
	public DifficultyTypeSeq[] ChallengeNpcDifficulty;

	[Name("challenge-npc-kill"), Repeat(20)]
	public Ref<Npc>[] ChallengeNpcKill;

	[Name("challenge-npc-attraction"), Repeat(20)]
	public Ref<Record>[] ChallengeNpcAttraction;

	[Name("challenge-npc-grade"), Repeat(20)]
	public Grade[] ChallengeNpcGrade;

	[Name("challenge-npc-quest"), Repeat(20)]
	public Ref<Quest>[] ChallengeNpcQuest;

	[Name("challenge-npc-total-count"), Side(ReleaseSide.Client)]
	public sbyte ChallengeNpcTotalCount;

	[Name("challenge-reward-total-count"), Side(ReleaseSide.Client)]
	public sbyte ChallengeRewardTotalCount;

	[Name("challenge-count-for-reward"), Repeat(20)]
	public sbyte[] ChallengeCountForReward;

	[Repeat(20)]
	public Ref<ChallengeListReward>[] Reward;



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