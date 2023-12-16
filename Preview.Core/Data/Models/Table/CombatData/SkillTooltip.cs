using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public class SkillTooltip : ModelElement
{
	public Ref<Skill3> Skill { get; set; }

	public TooltipGroup tooltipGroup { get; set; }

	public ECTOrder EctOrder { get; set; }

	public ECTOrder EctOrderEnglish { get; set; }

	public ECTOrder EctOrderFrench { get; set; }

	public ECTOrder EctOrderGerman { get; set; }

	public ECTOrder EctOrderRussian { get; set; }

	public ECTOrder EctOrderBportuguese { get; set; }

	public Ref<SkillTooltipAttribute> EffectAttribute { get; set; }

	[Repeat(4)] 
	public string[] EffectArg { get; set; }

	public Ref<SkillTooltipAttribute> ConditionAttribute { get; set; }

	public string[] ConditionArg { get; set; }

	public Ref<SkillTooltipAttribute> TargetAttribute { get; set; }

	public Ref<SkillTooltipAttribute> BeforeStanceAttribute { get; set; }

	public Ref<SkillTooltipAttribute> AfterStanceAttribute { get; set; }

	public Ref<Text> DefaultText { get; set; }

	public Ref<Text> AttributeColorText { get; set; }

	public sbyte SkillModifyDiffRepeatCount { get; set; }

	public short SkillAttackAttributeCoefficientPercent { get; set; }

	public Ref<Text> ItemDefaultText { get; set; }

	public Ref<Text> ItemReplaceText { get; set; }

	#region Enums
	public enum TooltipGroup
	{
		M1,
		M2,
		SUB,
		STANCE,
		CONDITION
	}

	public enum ECTOrder
	{
		CTE,
		CET,
		TCE,
		TEC,
		ECT,
		ETC,
	}
	#endregion
}