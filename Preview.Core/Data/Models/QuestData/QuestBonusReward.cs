using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class QuestBonusReward : Record
{
	public string Alias;



	public sbyte NormalBonusRewardTotalCount;

	[Repeat(4)]
	public Ref<Item>[] FixedItem;

	[Repeat(4)]
	public short[] FixedItemCount;

	public sbyte FixedItemTotalCount;

	public sbyte RandomItemSelectedCount;

	public sbyte RandomItemTotalInputCount;

	public Ref<Text> RandomItemTooltipText;

	public sbyte PaidBonusRewardTotalCount;

	public int PaidItemCost;


	[Repeat(4)]
	public Ref<Item>[] PaidFixedItem;

	[Repeat(4)]
	public short[] PaidFixedItemCount;

	public sbyte PaidFixedItemTotalCount;


	public sbyte PaidRandomItemSelectedCount;

	public sbyte PaidRandomItemTotalInputCount;

	public Ref<Text> PaidRandomItemTooltipText;
}