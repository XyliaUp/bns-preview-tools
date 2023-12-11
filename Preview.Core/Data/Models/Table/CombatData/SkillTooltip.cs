using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public class SkillTooltip : ModelElement
{
	public Ref<Skill3> Skill;

	[Name("tooltip-group")]
	public TooltipGroup tooltipGroup;

	[Name("ect-order")]
	public ECTOrder EctOrder;

	[Name("ect-order-english")]
	public ECTOrder EctOrderEnglish;

	[Name("ect-order-french")]
	public ECTOrder EctOrderFrench;

	[Name("ect-order-german")]
	public ECTOrder EctOrderGerman;

	[Name("ect-order-russian")]
	public ECTOrder EctOrderRussian;

	[Name("ect-order-bportuguese")]
	public ECTOrder EctOrderBportuguese;

	[Name("effect-attribute")] 
	public Ref<SkillTooltipAttribute> EffectAttribute;

	[Name("effect-arg") , Repeat(4)] 
	public string[] EffectArg;

	[Name("condition-attribute") ]
	public Ref<SkillTooltipAttribute> ConditionAttribute;

	[Name("condition-arg"), Repeat(2)]
	public string[] ConditionArg;

	[Name("target-attribute")]
	public Ref<SkillTooltipAttribute> TargetAttribute;

	[Name("before-stance-attribute")]
	public Ref<SkillTooltipAttribute> BeforeStanceAttribute;

	[Name("after-stance-attribute")]
	public Ref<SkillTooltipAttribute> AfterStanceAttribute;

	[Name("default-text")]
	public Ref<Text> DefaultText;

	[Name("attribute-color-text")]
	public Ref<Text> AttributeColorText;

	[Name("skill-modify-diff-repeat-count")]
	public sbyte SkillModifyDiffRepeatCount;

	[Name("skill-attack-attribute-coefficient-percent")]
	public short SkillAttackAttributeCoefficientPercent;

	[Name("item-default-text")]
	public Ref<Text> ItemDefaultText;

	[Name("item-replace-text")]
	public Ref<Text> ItemReplaceText;

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