using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	[Signal("use-ground-skill")]
	public sealed class UseGroundSkill : SkillBase
	{
		[Signal("target-area")]
		public ZoneArea TargetArea;

		[Signal("target-area-ref")]
		public int TargetAreaRef;

		[Signal("target-pos")]
		public string TargetPos;
	}
}