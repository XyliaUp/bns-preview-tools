using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
public sealed class ItemImproveOptionList : BaseRecord
{
	public int Id;
	public JobSeq Job;

	[Repeat(50)]
	public ItemImproveOption[] Options;

	[Repeat(50)]
	public short[] OptionProbWeight;



	[Signal("draw-cost-money") , Repeat(4)]
	public int[] DrawCostMoney;

	[Signal("draw-cost-main-item"), Repeat(4)]
	public Item[] DrawCostMainItem;

	[Signal("draw-cost-main-item-count"), Repeat(4)]
	public short[] DrawCostMainItemCount;
}