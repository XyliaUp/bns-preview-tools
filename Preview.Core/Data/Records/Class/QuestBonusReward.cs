using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class QuestBonusReward : BaseRecord
	{
		[Signal("normal-bonus-reward-total-count")]
		public byte NormalBonusRewardTotalCount;

		[Signal("fixed-item-1")]
		public string FixedItem1;

		[Signal("fixed-item-2")]
		public string FixedItem2;

		[Signal("fixed-item-3")]
		public string FixedItem3;

		[Signal("fixed-item-4")]
		public string FixedItem4;

		[Signal("fixed-item-count-1")]
		public short FixedItemCount1;

		[Signal("fixed-item-count-2")]
		public short FixedItemCount2;

		[Signal("fixed-item-count-3")]
		public short FixedItemCount3;

		[Signal("fixed-item-count-4")]
		public short FixedItemCount4;

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

		[Signal("paid-fixed-item-1")]
		public string PaidFixedItem1;

		[Signal("paid-fixed-item-2")]
		public string PaidFixedItem2;

		[Signal("paid-fixed-item-3")]
		public string PaidFixedItem3;

		[Signal("paid-fixed-item-4")]
		public string PaidFixedItem4;

		[Signal("paid-fixed-item-count-1")]
		public short PaidFixedItemCount1;

		[Signal("paid-fixed-item-count-2")]
		public short PaidFixedItemCount2;

		[Signal("paid-fixed-item-count-3")]
		public short PaidFixedItemCount3;

		[Signal("paid-fixed-item-count-4")]
		public short PaidFixedItemCount4;

		[Signal("paid-fixed-item-total-count")]
		public byte PaidFixedItemTotalCount;



		[Signal("paid-random-item-selected-count")]
		public byte PaidRandomItemSelectedCount;

		[Signal("paid-random-item-total-input-count")]
		public byte PaidRandomItemTotalInputCount;

		[Signal("paid-random-item-tooltip-text")]
		public string PaidRandomItemTooltipText;
	}
}