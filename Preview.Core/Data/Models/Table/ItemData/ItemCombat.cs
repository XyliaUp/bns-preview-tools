using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ItemCombat : ModelElement
{
	#region Attributes
	public JobStyleSeq JobStyle { get; set; }

	public Ref<ItemSkill>[] ItemSkill { get; set; }

	public Ref<ItemSkill>[] ItemSkillSecond { get; set; }

	public Ref<ItemSkill>[] ItemSkillThird { get; set; }

	public Ref<SkillModifyInfoGroup> SkillModifyInfoGroup { get; set; }
	#endregion

	#region Methods
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
	#endregion
}