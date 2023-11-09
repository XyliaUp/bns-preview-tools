using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ItemSkill : Record
{
	public string Alias;


	[Name("skill-id")]
	public int SkillId;

	[Name("skill-variation-id") , Repeat(8)]
	public sbyte[] SkillVariationId1;

	[Name("include-inheritance-skill")]
	public bool IncludeInheritanceSkill;

	[Name("item-sim-skill")]
	public Ref<Skill3> ItemSimSkill;

	public Ref<Text> Name2;

	public Ref<Text> Description2;

	[Name("item-skill-tooltip")]
	public Ref<Text> ItemSkillTooltip;
}