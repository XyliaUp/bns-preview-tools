namespace Xylia.Preview.Data.Models;
public sealed class SkillTrainByItem : ModelElement
{
	#region Attributes
	public Ref<Skill3>[] OriginSkill { get; set; }

	public Ref<Skill3>[] ChangeSkill { get; set; }

	public string Icon { get; set; }

	public Ref<Text> Description { get; set; }
	#endregion


	#region Methods
	public override string ToString() => Description.GetText();
	#endregion
}