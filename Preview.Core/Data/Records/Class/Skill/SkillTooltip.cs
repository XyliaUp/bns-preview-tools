using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	public sealed class SkillTooltip : BaseRecord
	{
		#region Fields
		public string Skill;

		[Signal("tooltip-group")]
		public TooltipGroup tooltipGroup;

		[Signal("ect-order")]
		public ECTOrder EctOrder;
		public ECTOrder EctOrderEnglish;
		public ECTOrder EctOrderFrench;
		public ECTOrder EctOrderGerman;
		public ECTOrder EctOrderRussian;
		public ECTOrder EctOrderBportuguese;

		[Signal("effect-attribute")] public string EffectAttribute;
		[Signal("effect-arg-1")] public string EffectArg1;
		[Signal("effect-arg-2")] public string EffectArg2;
		[Signal("effect-arg-3")] public string EffectArg3;
		[Signal("effect-arg-4")] public string EffectArg4;

		[Signal("condition-attribute")] public string ConditionAttribute;
		[Signal("condition-arg-1")] public string ConditionArg1;
		[Signal("condition-arg-2")] public string ConditionArg2;


		[Signal("target-attribute")]
		public string TargetAttribute;

		[Signal("before-stance-attribute")]
		public string BeforeStanceAttribute;

		[Signal("after-stance-attribute")]
		public string AfterStanceAttribute;

		[Signal("default-text")]
		public string DefaultText;

		[Signal("attribute-color-text")]
		public string AttributeColorText;

		[Signal("skill-modify-diff-repeat-count")]
		public byte SkillModifyDiffRepeatCount;

		[Signal("skill-attack-attribute-coefficient-percent")]
		public short SkillAttackAttributeCoefficientPercent;

		[Signal("item-default-text")]
		public string ItemDefaultText;

		[Signal("item-replace-text")]
		public string ItemReplaceText;
		#endregion


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
}