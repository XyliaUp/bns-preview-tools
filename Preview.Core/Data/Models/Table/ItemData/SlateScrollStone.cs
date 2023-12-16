namespace Xylia.Preview.Data.Models;
public sealed class SlateScrollStone : ModelElement
{
	public Ref<SlateScroll> scroll { get; set; }

	public Ref<SlateStone> stone { get; set; }

	public bool tooltip { get; set; }
}