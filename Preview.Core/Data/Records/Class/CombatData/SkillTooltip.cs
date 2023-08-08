using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
public class SkillTooltip : BaseRecord
{
	public string Skill;

	[Signal("tooltip-group")]
	public TooltipGroup tooltipGroup;

	[Signal("ect-order")]
	public ECTOrder EctOrder;

	[Signal("ect-order-english")]
	public ECTOrder EctOrderEnglish;

	[Signal("ect-order-french")]
	public ECTOrder EctOrderFrench;

	[Signal("ect-order-german")]
	public ECTOrder EctOrderGerman;

	[Signal("ect-order-russian")]
	public ECTOrder EctOrderRussian;

	[Signal("ect-order-bportuguese")]
	public ECTOrder EctOrderBportuguese;

	[Signal("effect-attribute")] 
	public SkillTooltipAttribute EffectAttribute;

	[Signal("effect-arg") , Repeat(4)] 
	public string[] EffectArg;

	[Signal("condition-attribute") ]
	public SkillTooltipAttribute ConditionAttribute;

	[Signal("condition-arg"), Repeat(2)]
	public string[] ConditionArg;

	[Signal("target-attribute")]
	public SkillTooltipAttribute TargetAttribute;

	[Signal("before-stance-attribute")]
	public SkillTooltipAttribute BeforeStanceAttribute;

	[Signal("after-stance-attribute")]
	public SkillTooltipAttribute AfterStanceAttribute;

	[Signal("default-text")]
	public Text DefaultText;

	[Signal("attribute-color-text")]
	public Text AttributeColorText;

	[Signal("skill-modify-diff-repeat-count")]
	public sbyte SkillModifyDiffRepeatCount;

	[Signal("skill-attack-attribute-coefficient-percent")]
	public short SkillAttackAttributeCoefficientPercent;

	[Signal("item-default-text")]
	public Text ItemDefaultText;

	[Signal("item-replace-text")]
	public Text ItemReplaceText;

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