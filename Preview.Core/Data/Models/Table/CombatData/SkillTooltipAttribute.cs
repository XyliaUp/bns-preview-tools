using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public class SkillTooltipAttribute : ModelElement
{
	[Repeat(4)]
	public ArgTypeSeq[] ArgType { get; set; }
	public enum ArgTypeSeq
	{
		None,

			DamagePercentMinMax,

			DamagePercent,

		Time,

			StackCount,

		Effect,

			HealPercent,

			DrainPercent,

		Skill,

			ConsumePercent,

			ProbabilityPercent,

			StanceType,

		Percent,

		Counter,

		Distance,

			KeyCommand,

		Number,

			TextAlias,

			rHypermove,

			rHealPercent,

			rHealDiff,

			rShieldPercent,

			rShieldDiff,

			rSupportPercent,

			rSupportDiff,

	}

	public Ref<Text> Text { get; set; }

	public string Icon { get; set; }

	public ModifyType SkillModifyType { get; set; }
	public enum ModifyType
	{
		None,

			RecycleDuration,

			SpConsume,

			Damage,

			HpDrain,

			HealPercent,
	}
}