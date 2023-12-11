using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public class SkillTooltipAttribute : ModelElement
{
	[Repeat(4)]
	public ArgTypeSeq[] ArgType;
	public enum ArgTypeSeq
	{
		None,

		[Name("damage-percent-min-max")]
		DamagePercentMinMax,

		[Name("damage-percent")]
		DamagePercent,

		Time,

		[Name("stack-count")]
		StackCount,

		Effect,

		[Name("heal-percent")]
		HealPercent,

		[Name("drain-percent")]
		DrainPercent,

		Skill,

		[Name("consume-percent")]
		ConsumePercent,

		[Name("probability-percent")]
		ProbabilityPercent,

		[Name("stance-type")]
		StanceType,

		Percent,

		Counter,

		Distance,

		[Name("key-command")]
		KeyCommand,

		Number,

		[Name("text-alias")]
		TextAlias,

		[Name("r-hypermove")]
		rHypermove,

		[Name("r-heal-percent")]
		rHealPercent,

		[Name("r-heal-diff")]
		rHealDiff,

		[Name("r-shield-percent")]
		rShieldPercent,

		[Name("r-shield-diff")]
		rShieldDiff,

		[Name("r-support-percent")]
		rSupportPercent,

		[Name("r-support-diff")]
		rSupportDiff,

	}

	public Ref<Text> Text;

	public string Icon;

	public ModifyType SkillModifyType;
	public enum ModifyType
	{
		None,

		[Name("recycle-duration")]
		RecycleDuration,

		[Name("sp-consume")]
		SpConsume,

		[Name("damage")]
		Damage,

		[Name("hp-drain")]
		HpDrain,

		[Name("heal-percent")]
		HealPercent,
	}
}