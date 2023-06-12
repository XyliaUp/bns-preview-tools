using Xylia.Preview.Common.Attribute;

using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class DuelFinish : CaseBase
	{
		[Signal("duel-grade")]
		public byte DuelGrade;

		[Signal("duel-result")]
		public ResultSeq DuelResult;
		public enum ResultSeq
		{
			All,

			Win,

			Lose,
		}




		[Signal("duel-type")]
		public DuelTypeSeq DuelType;
		public enum DuelTypeSeq
		{
			None,

			[Signal("death-match-1vs1")]
			DeathMatch1VS1,

			[Signal("tag-match-3vs3")]
			TagMatch3VS3,

			[Signal("sudden-death-3vs3")]
			SuddenDeath3VS3,
		}




		[Signal("arena-matching-rule-detail")]
		public ArenaMatchingRule ArenaMatchingRuleDetail;
		public enum ArenaMatchingRule
		{
			None,
			Normal,

			[Signal("death-match-1vs1")]
			N2,

			[Signal("death-match-1vs1")]
			N3,

			[Signal("death-match-1vs1")]
			N4,

		}


		[Signal("duel-straight-win")]
		public int DuelStraightWin;
	}
}