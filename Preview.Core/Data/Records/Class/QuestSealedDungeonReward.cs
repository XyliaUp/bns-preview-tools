
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class QuestSealedDungeonReward : BaseRecord
{
	public int Id;

	public byte Level;

	[Signal("reward-item"), Repeat(4)]
	public Item[] RewardItem;

	[Signal("reward-item-count"), Repeat(4)]
	public short[] RewardItemCount;
}