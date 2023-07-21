using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public class SkillTooltipAttribute : BaseRecord
{
	[Signal("arg-type") , Repeat(4)]
	public ArgType[] Arg_Type;
	public enum ArgType
	{
		None,

		[Signal("damage-percent-min-max")]
		DamagePercentMinMax,

		[Signal("damage-percent")]
		DamagePercent,

		Time,

		[Signal("stack-count")]
		StackCount,

		Effect,

		[Signal("heal-percent")]
		HealPercent,

		[Signal("drain-percent")]
		DrainPercent,

		Skill,

		[Signal("consume-percent")]
		ConsumePercent,

		[Signal("probability-percent")]
		ProbabilityPercent,

		[Signal("stance-type")]
		StanceType,

		Percent,

		Counter,

		Distance,

		[Signal("key-command")]
		KeyCommand,

		Number,

		[Signal("text-alias")]
		TextAlias,

		[Signal("r-hypermove")]
		rHypermove,

		[Signal("r-heal-percent")]
		rHealPercent,

		[Signal("r-heal-diff")]
		rHealDiff,

		[Signal("r-shield-percent")]
		rShieldPercent,

		[Signal("r-shield-diff")]
		rShieldDiff,

		[Signal("r-support-percent")]
		rSupportPercent,

		[Signal("r-support-diff")]
		rSupportDiff,

	}

	public Text Text;

	public string Icon;

	[Signal("skill-modify-type")]
	public ModifyType SkillModifyType;
	public enum ModifyType
	{
		None,

		[Signal("recycle-duration")]
		RecycleDuration,

		[Signal("sp-consume")]
		SpConsume,

		[Signal("damage")]
		Damage,

		[Signal("hp-drain")]
		HpDrain,

		[Signal("heal-percent")]
		HealPercent,
	}
}