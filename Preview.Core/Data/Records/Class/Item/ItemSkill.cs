using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class ItemSkill : BaseRecord
	{
		[Signal("skill-id")]
		public int SkillId;

		[Signal("skill-variation-id-1")]
		public byte SkillVariationId1;

		[Signal("skill-variation-id-2")]
		public byte SkillVariationId2;

		[Signal("skill-variation-id-3")]
		public byte SkillVariationId3;

		[Signal("skill-variation-id-4")]
		public byte SkillVariationId4;

		[Signal("skill-variation-id-5")]
		public byte SkillVariationId5;

		[Signal("skill-variation-id-6")]
		public byte SkillVariationId6;

		[Signal("skill-variation-id-7")]
		public byte SkillVariationId7;

		[Signal("skill-variation-id-8")]
		public byte SkillVariationId8;

		[Signal("include-inheritance-skill")]
		public bool IncludeInheritanceSkill;

		[Signal("item-sim-skill")]
		public string ItemSimSkill;

		public Text Name2;

		public Text Describe2;

		[Signal("item-skill-tooltip")]
		public Text ItemSkillTooltip;
	}
}