using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class ItemCombat : BaseRecord
{
	[Signal("job-style")]
	public JobStyleSeq JobStyle;


	[Signal("item-skill"), Repeat(16)]
	public ItemSkill[] ItemSkill;

	[Signal("item-skill-second"), Repeat(16)]
	public ItemSkill[] ItemSkillSecond;

	[Signal("item-skill-third"), Repeat(16)]
	public ItemSkill[] ItemSkillThird;

	[Signal("skill-build-up-parent-skill3-id"), Repeat(3)]
	public int[] SkillBuildUpParentSkill3Id;

	[Signal("skill-build-up-level"), Repeat(3)]
	public sbyte[] SkillBuildUpLevel;

	[Signal("skill-modify-info-group")]
	public SkillModifyInfoGroup SkillModifyInfoGroup;



	public override string ToString()
	{
		var ItemSkills = new List<ItemSkill>();
		ItemSkills.AddRange(ItemSkill);
		ItemSkills.AddRange(ItemSkillSecond);
		ItemSkills.AddRange(ItemSkillThird);


		return ItemSkills.Where(record => record?.Description2 != null)
			.Aggregate(SkillModifyInfoGroup?.ToString(), (sum, now) => sum + "<br/>" + now.Description2.GetText());
	}
}