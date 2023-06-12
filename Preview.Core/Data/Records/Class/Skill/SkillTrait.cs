
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class SkillTrait : BaseRecord
	{
		public JobSeq Job;

		[Signal("job-style")]
		public JobStyleSeq JobStyle;

		public byte Tier;

		[Signal("tier-variation")]
		public byte TierVariation;

		public bool Enable = true;



		public Text Name2;

		public string Icon;

		[Signal("trait-symbol")]
		public string TraitSymbol;

		[Signal("tooltip-train-name")]
		public Text TooltipTrainName;

		[Signal("tooltip-train-description")]
		public Text TooltipTrainDescription;

		[Signal("tooltip-effect-description")]
		public Text TooltipEffectDescription;

		[Signal("tooltip-skill-systematization-group-1")]
		public string TooltipSkillSystematizationGroup1;

		[Signal("tooltip-skill-systematization-group-2")]
		public string TooltipSkillSystematizationGroup2;

		[Signal("tooltip-skill-systematization-group-3")]
		public string TooltipSkillSystematizationGroup3;

		[Signal("tooltip-skill-systematization-group-4")]
		public string TooltipSkillSystematizationGroup4;

		[Signal("tooltip-skill-systematization-group-5")]
		public string TooltipSkillSystematizationGroup5;

		[Signal("tooltip-skill-systematization-group-6")]
		public string TooltipSkillSystematizationGroup6;

		[Signal("tooltip-acquire-skill-list-skill3-id-1")]
		public int TooltipAcquireSkillListSkill3Id1;

		[Signal("tooltip-acquire-skill-list-skill3-id-2")]
		public int TooltipAcquireSkillListSkill3Id2;

		[Signal("tooltip-acquire-skill-list-skill3-id-3")]
		public int TooltipAcquireSkillListSkill3Id3;

		[Signal("tooltip-acquire-skill-list-skill3-id-4")]
		public int TooltipAcquireSkillListSkill3Id4;

		[Signal("tooltip-acquire-skill-list-skill3-id-5")]
		public int TooltipAcquireSkillListSkill3Id5;

		[Signal("tooltip-acquire-skill-list-skill3-id-6")]
		public int TooltipAcquireSkillListSkill3Id6;

		[Signal("tooltip-acquire-skill-list-skill3-description-1")]
		public Text TooltipAcquireSkillListSkill3Description1;

		[Signal("tooltip-acquire-skill-list-skill3-description-2")]
		public Text TooltipAcquireSkillListSkill3Description2;

		[Signal("tooltip-acquire-skill-list-skill3-description-3")]
		public Text TooltipAcquireSkillListSkill3Description3;

		[Signal("tooltip-acquire-skill-list-skill3-description-4")]
		public Text TooltipAcquireSkillListSkill3Description4;

		[Signal("tooltip-acquire-skill-list-skill3-description-5")]
		public Text TooltipAcquireSkillListSkill3Description5;

		[Signal("tooltip-acquire-skill-list-skill3-description-6")]
		public Text TooltipAcquireSkillListSkill3Description6;

		[Signal("tooltip-variable-skill-list-skill3-id-1")]
		public int TooltipVariableSkillListSkill3Id1;

		[Signal("tooltip-variable-skill-list-skill3-id-2")]
		public int TooltipVariableSkillListSkill3Id2;

		[Signal("tooltip-variable-skill-list-skill3-id-3")]
		public int TooltipVariableSkillListSkill3Id3;

		[Signal("tooltip-variable-skill-list-skill3-id-4")]
		public int TooltipVariableSkillListSkill3Id4;

		[Signal("tooltip-variable-skill-list-skill3-id-5")]
		public int TooltipVariableSkillListSkill3Id5;

		[Signal("tooltip-variable-skill-list-skill3-id-6")]
		public int TooltipVariableSkillListSkill3Id6;

		[Signal("variable-tooltip-skill3-variation-id-1")]
		public byte VariableTooltipSkill3VariationId1;

		[Signal("variable-tooltip-skill3-variation-id-2")]
		public byte VariableTooltipSkill3VariationId2;

		[Signal("variable-tooltip-skill3-variation-id-3")]
		public byte VariableTooltipSkill3VariationId3;

		[Signal("variable-tooltip-skill3-variation-id-4")]
		public byte VariableTooltipSkill3VariationId4;

		[Signal("variable-tooltip-skill3-variation-id-5")]
		public byte VariableTooltipSkill3VariationId5;

		[Signal("variable-tooltip-skill3-variation-id-6")]
		public byte VariableTooltipSkill3VariationId6;

		[Signal("tooltip-variable-skill-list-skill3-description-1")]
		public Text TooltipVariableSkillListSkill3Description1;

		[Signal("tooltip-variable-skill-list-skill3-description-2")]
		public Text TooltipVariableSkillListSkill3Description2;

		[Signal("tooltip-variable-skill-list-skill3-description-3")]
		public Text TooltipVariableSkillListSkill3Description3;

		[Signal("tooltip-variable-skill-list-skill3-description-4")]
		public Text TooltipVariableSkillListSkill3Description4;

		[Signal("tooltip-variable-skill-list-skill3-description-5")]
		public Text TooltipVariableSkillListSkill3Description5;

		[Signal("tooltip-variable-skill-list-skill3-description-6")]
		public Text TooltipVariableSkillListSkill3Description6;
	}
}