using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	[Signal("use-indexed-skill")]
	public sealed class UseIndexedSkill : IAction
	{
		public int Skill;

		public int Skill3;
	}
}