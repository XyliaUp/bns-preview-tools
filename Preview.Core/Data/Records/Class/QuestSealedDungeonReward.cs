
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class QuestSealedDungeonReward : BaseRecord
	{
		public byte Level;

		[Signal("reward-item-1")]
		public Item RewardItem1;

		[Signal("reward-item-2")]
		public Item RewardItem2;

		[Signal("reward-item-3")]
		public Item RewardItem3;

		[Signal("reward-item-4")]
		public Item RewardItem4;

		[Signal("reward-item-count-1")]
		public short RewardItemCount1;

		[Signal("reward-item-count-2")]
		public short RewardItemCount2;

		[Signal("reward-item-count-3")]
		public short RewardItemCount3;

		[Signal("reward-item-count-4")]
		public short RewardItemCount4;
	}
}