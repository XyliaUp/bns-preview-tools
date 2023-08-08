using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class QuestBonusReward : BaseRecord
{
	[Signal("normal-bonus-reward-total-count")]
	public sbyte NormalBonusRewardTotalCount;

	[Signal("fixed-item"), Repeat(4)]
	public Item[] FixedItem;

	[Signal("fixed-item-count"), Repeat(4)]
	public short[] FixedItemCount;

	[Signal("fixed-item-total-count")]
	public sbyte FixedItemTotalCount;



	[Signal("random-item-selected-count")]
	public sbyte RandomItemSelectedCount;

	[Signal("random-item-total-input-count")]
	public sbyte RandomItemTotalInputCount;

	[Signal("random-item-tooltip-text")]
	public string RandomItemTooltipText;

	[Signal("paid-bonus-reward-total-count")]
	public sbyte PaidBonusRewardTotalCount;

	[Signal("paid-item-cost")]
	public int PaidItemCost;


	[Signal("paid-fixed-item"), Repeat(4)]
	public Item[] PaidFixedItem;

	[Signal("paid-fixed-item-count"), Repeat(4)]
	public short[] PaidFixedItemCount;

	[Signal("paid-fixed-item-total-count")]
	public sbyte PaidFixedItemTotalCount;



	[Signal("paid-random-item-selected-count")]
	public sbyte PaidRandomItemSelectedCount;

	[Signal("paid-random-item-total-input-count")]
	public sbyte PaidRandomItemTotalInputCount;

	[Signal("paid-random-item-tooltip-text")]
	public string PaidRandomItemTooltipText;
}