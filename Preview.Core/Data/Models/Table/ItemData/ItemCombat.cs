using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ItemCombat : ModelElement
{
	[Name("job-style")]
	public JobStyleSeq JobStyle { get; set; }

	[Name("item-skill")]
	public Ref<ItemSkill>[] ItemSkill { get; set; }

	[Name("item-skill-second")]
	public Ref<ItemSkill>[] ItemSkillSecond { get; set; }

	[Name("item-skill-third")]
	public Ref<ItemSkill>[] ItemSkillThird { get; set; }

	[Name("skill-modify-info-group")]
	public Ref<SkillModifyInfoGroup> SkillModifyInfoGroup { get; set; }



	public override string ToString()
	{
		var ItemSkills = new List<Ref<ItemSkill>>();
		ItemSkills.AddRange(ItemSkill);
		ItemSkills.AddRange(ItemSkillSecond);
		ItemSkills.AddRange(ItemSkillThird);

		return ItemSkills.Select(@ref => @ref.Instance)
			.Where(record => record?.Description2 != null)
			.Aggregate(SkillModifyInfoGroup.Instance?.ToString(), (sum, now) => sum + "<br/>" + now.Description2.GetText());
	}
}