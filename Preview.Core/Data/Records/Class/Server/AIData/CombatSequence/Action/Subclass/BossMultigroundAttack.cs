using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	[Signal("boss-multiground-attack")]
	public sealed class BossMultigroundAttack : SkillBase
	{
		[Signal("ground-pattern-1")] 
		public BossGroundAttackTargetPattern GroundPattern1;

		[Signal("ground-pattern-2")] 
		public BossGroundAttackTargetPattern GroundPattern2;

		[Signal("ground-pattern-3")] 
		public BossGroundAttackTargetPattern GroundPattern3;

		[Signal("ground-pattern-4")] 
		public BossGroundAttackTargetPattern GroundPattern4;

		[Signal("ground-pattern-5")] 
		public BossGroundAttackTargetPattern GroundPattern5;

		[Signal("origin-pos")] 
		public string OriginPos;
	}
}