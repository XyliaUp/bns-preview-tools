namespace Xylia.Preview.Data.Models;
public sealed class SkillTrainByItemList : ModelElement
{
	#region Attributes
	public Ref<SkillTrainByItem>[] ChangeSet { get; set; }
	#endregion
}