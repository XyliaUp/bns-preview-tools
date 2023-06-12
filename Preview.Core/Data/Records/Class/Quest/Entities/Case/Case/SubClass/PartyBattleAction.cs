using Xylia.Preview.Common.Attribute;

using static Xylia.Preview.Data.Record.QuestData.Case.PartyBattle;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class PartyBattleAction : CaseBase
	{
		[Signal("party-battle-type")]
		public PartyBattleTypeSeq PartyBattleType;


		[Signal("party-battle-action-type")]
		public PartyBattleActionTypeSeq PartyBattleActionType;
		public enum PartyBattleActionTypeSeq
		{
			None,

			Occupy,
		}
	}
}