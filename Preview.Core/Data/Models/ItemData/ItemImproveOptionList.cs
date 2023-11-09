using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveOptionList : Record
{
	public int Id;
	public JobSeq Job;

	[Repeat(50)]
	public Ref<ItemImproveOption>[] Option;

	[Repeat(50)]
	public short[] OptionProbWeight;



	[Name("draw-cost-money") , Repeat(4)]
	public int[] DrawCostMoney;

	[Name("draw-cost-main-item"), Repeat(4)]
	public Ref<Item>[] DrawCostMainItem;

	[Name("draw-cost-main-item-count"), Repeat(4)]
	public short[] DrawCostMainItemCount;
}