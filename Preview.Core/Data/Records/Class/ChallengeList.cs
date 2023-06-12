using System;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	public sealed class ChallengeList : BaseRecord
	{
		#region Enums
		public enum TypeSeq
		{
			dayofweek,
			week,
		}

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



		public TypeSeq Type;

		[Signal("challenge-type")]
		public ChallengeTypeSeq ChallengeType;

		[Signal("required-level")]
		public byte RequiredLevel;

		[Signal("required-mastery-level")]
		public byte RequiredMasteryLevel;

		[Signal("challenge-quest-complete-count")]
		public byte ChallengeQuestCompleteCount;

		[Signal("challenge-quest-count")]
		public byte ChallengeQuestCount;

		[Signal("challenge-npc-total-count")]
		public byte ChallengeNpcTotalCount;

		[Signal("challenge-reward-total-count")]
		public byte ChallengeRewardTotalCount;



		[Signal("week-start-date-time")]
		public DateTime WeekStartDateTime;
	}
}