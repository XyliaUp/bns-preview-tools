using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStore : ModelElement
{
	public RandomStoreNumberSeq RandomStoreNumber { get; set; }

	public Ref<Item> ChargeOfItemDraw { get; set; }

	public long ChargeOfMoneyDraw { get; set; }

	public int AcquireDrawRewardSetRepeatCount { get; set; }
}

public enum RandomStoreNumberSeq
{
	InvalidNumber,

	RandomStore1,

	RandomStore2,
}