using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	[Signal("boss-sp-select-attack")]
	public sealed class BossSpSelectAttack : IAction
	{
		[Signal("sp-1")] public byte Sp1;
		[Signal("sp-2")] public byte Sp2;
		[Signal("sp-3")] public byte Sp3;

		[Signal("skill-1")] public _Skill Skill1;
		[Signal("skill-2")] public _Skill Skill2;
		[Signal("skill-3")] public _Skill Skill3;

		[Signal("skill3-1")] public Skill Skill3_1;
		[Signal("skill3-2")] public Skill Skill3_2;
		[Signal("skill3-3")] public Skill Skill3_3;
	}
}