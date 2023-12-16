using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveOptionList : ModelElement
{
	public int Id { get; set; }
	public JobSeq Job { get; set; }

	[Repeat(50)]
	public Ref<ItemImproveOption>[] Option { get; set; }

	[Repeat(50)]
	public short[] OptionProbWeight { get; set; }



	[Name("draw-cost-money"), Repeat(4)]
	public int[] DrawCostMoney { get; set; }

	[Name("draw-cost-main-item"), Repeat(4)]
	public Ref<Item>[] DrawCostMainItem { get; set; }

	[Name("draw-cost-main-item-count"), Repeat(4)]
	public short[] DrawCostMainItemCount { get; set; }
}