namespace Xylia.Preview.Data.Models;
public class SkillTooltipAttribute : ModelElement
{
	#region Attributes
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
	#endregion

	#region Methods
	public string ToString(string[] arguments) => Text.GetText([.. arguments]);

	public override string ToString() => Text.GetText();
	#endregion
}