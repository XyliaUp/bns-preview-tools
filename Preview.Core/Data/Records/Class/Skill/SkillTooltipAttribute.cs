using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class SkillTooltipAttribute : BaseRecord
	{
		[Signal("arg-type-1")] 
		public ArgType ArgType1;

		[Signal("arg-type-2")]
		public ArgType ArgType2;

		[Signal("arg-type-3")] 
		public ArgType ArgType3;

		[Signal("arg-type-4")] 
		public ArgType ArgType4;

		public string Text;

		public string Icon;

		/// <summary>
		/// 技能变更类型
		/// </summary>
		[Signal("skill-modify-type")]
		public ModifyType SkillModifyType;


		#region Enums
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
		#endregion
	}
}