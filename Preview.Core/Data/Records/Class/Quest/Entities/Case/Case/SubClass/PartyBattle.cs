using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class PartyBattle : CaseBase
	{
		[Signal("party-battle-type")]
		public PartyBattleTypeSeq PartyBattleType;
		public enum PartyBattleTypeSeq
		{
			None,

			[Signal("occupation-war")]
			OccupationWar,

			[Signal("capture-the-flag")]
			CaptureTheFlag,

			[Signal("lead-the-ball")]
			LeadTheBall,
		}



		[Signal("party-battle-result")]
		public ResultSeq PartyBattleResult;
		
		[DefaultValue(All)]
		public enum ResultSeq
		{
			None,

			All,

			Win,

			Lose,
		}
	}
}