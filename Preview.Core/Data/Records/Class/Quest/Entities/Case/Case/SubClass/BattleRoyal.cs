using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class BattleRoyal : CaseBase
	{
		[Signal("battle-royal-field")]
		public BattleRoyalField BattleRoyalField;
	}
}