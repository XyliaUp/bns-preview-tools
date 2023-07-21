using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class QuestBonusReward : BaseRecord
{
	[Signal("normal-bonus-reward-total-count")]
	public byte NormalBonusRewardTotalCount;

	[Signal("fixed-item"), Repeat(4)]
	public Item[] FixedItem;

	[Signal("fixed-item-count"), Repeat(4)]
	public short[] FixedItemCount;

	[Signal("fixed-item-total-count")]
	public byte FixedItemTotalCount;



	[Signal("random-item-selected-count")]
	public byte RandomItemSelectedCount;

	[Signal("random-item-total-input-count")]
	public byte RandomItemTotalInputCount;

	[Signal("random-item-tooltip-text")]
	public string RandomItemTooltipText;

	[Signal("paid-bonus-reward-total-count")]
	public byte PaidBonusRewardTotalCount;

	[Signal("paid-item-cost")]
	public int PaidItemCost;


	[Signal("paid-fixed-item"), Repeat(4)]
	public Item[] PaidFixedItem;

	[Signal("paid-fixed-item-count"), Repeat(4)]
	public short[] PaidFixedItemCount;

	[Signal("paid-fixed-item-total-count")]
	public byte PaidFixedItemTotalCount;



	[Signal("paid-random-item-selected-count")]
	public byte PaidRandomItemSelectedCount;

	[Signal("paid-random-item-total-input-count")]
	public byte PaidRandomItemTotalInputCount;

	[Signal("paid-random-item-tooltip-text")]
	public string PaidRandomItemTooltipText;
}