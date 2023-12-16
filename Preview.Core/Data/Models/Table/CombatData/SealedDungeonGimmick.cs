namespace Xylia.Preview.Data.Models;
public sealed class SealedDungeonGimmick : ModelElement
{
	public Ref<Text> Name { get; set; }

	public Ref<Text> IconName { get; set; }

	public Ref<Text> IconTooltip { get; set; }

	public string Icon { get; set; }
}