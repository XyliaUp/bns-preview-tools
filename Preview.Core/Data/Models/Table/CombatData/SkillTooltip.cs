using System.Text;

namespace Xylia.Preview.Data.Models;
public class SkillTooltip : ModelElement
{
	#region Attributes
	public Ref<Skill3> Skill { get; set; }

	public enum TooltipGroupSeq
	{
		M1,
		M2,
		SUB,
		STANCE,
		CONDITION,
	}

	public TooltipGroupSeq TooltipGroup { get; set; }

	public enum EctOrderSeq
	{
		CTE,
		CET,
		TCE,
		TEC,
		ECT,
		ETC
	}

	public EctOrderSeq EctOrder { get; set; }

	public EctOrderSeq EctOrderEnglish { get; set; }

	public EctOrderSeq EctOrderFrench { get; set; }

	public EctOrderSeq EctOrderGerman { get; set; }

	public EctOrderSeq EctOrderRussian { get; set; }

	public EctOrderSeq EctOrderBportuguese { get; set; }

	public Ref<SkillTooltipAttribute> EffectAttribute { get; set; }

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
	#endregion


	#region Methods
	public override string ToString()
	{
		StringBuilder builder = new();

		var order = this.EctOrder;
		switch (order)
		{
			case EctOrderSeq.CTE:
				builder.Append(ConditionAttribute.Instance);
				builder.Append(TargetAttribute.Instance);
				builder.Append(EffectAttribute.Instance);
				break;
			case EctOrderSeq.CET:
				builder.Append(ConditionAttribute.Instance);
				builder.Append(EffectAttribute.Instance);
				builder.Append(TargetAttribute.Instance);
				break;
			case EctOrderSeq.TCE:
				builder.Append(TargetAttribute.Instance);
				builder.Append(ConditionAttribute.Instance);
				builder.Append(EffectAttribute.Instance);
				break;
			case EctOrderSeq.TEC:
				builder.Append(TargetAttribute.Instance);
				builder.Append(EffectAttribute.Instance);
				builder.Append(ConditionAttribute.Instance);
				break;
			case EctOrderSeq.ECT:
				builder.Append(EffectAttribute.Instance);
				builder.Append(ConditionAttribute.Instance);
				builder.Append(TargetAttribute.Instance);
				break;
			case EctOrderSeq.ETC:
				builder.Append(EffectAttribute.Instance);
				builder.Append(TargetAttribute.Instance);
				builder.Append(ConditionAttribute.Instance);
				break;
		}

		return builder.ToString();
	}
	#endregion
}