using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class RandomStore : BaseRecord
{
	[Signal("random-store-number")]
	public RandomStoreNumberSeq RandomStoreNumber;

	[Signal("charge-of-item-draw")]
	public Item ChargeOfItemDraw;

	[Signal("charge-of-money-draw")]
	public long ChargeOfMoneyDraw;

	[Signal("acquire-draw-reward-set-repeat-count")]
	public int AcquireDrawRewardSetRepeatCount;
}

public enum RandomStoreNumberSeq
{
	InvalidNumber,

	RandomStore1,

	RandomStore2,
}