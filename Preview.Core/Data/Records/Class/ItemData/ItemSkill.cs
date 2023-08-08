using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class ItemSkill : BaseRecord
{
	[Signal("skill-id")]
	public int SkillId;

	[Signal("skill-variation-id") , Repeat(8)]
	public sbyte[] SkillVariationId1;

	[Signal("include-inheritance-skill")]
	public bool IncludeInheritanceSkill;

	[Signal("item-sim-skill")]
	public Skill3 ItemSimSkill;

	public Text Name2;

	public Text Description2;

	[Signal("item-skill-tooltip")]
	public Text ItemSkillTooltip;
}