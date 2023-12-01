using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStore : Record
{
	public string Alias;


	[Name("random-store-number")]
	public RandomStoreNumberSeq RandomStoreNumber;

	[Name("charge-of-item-draw")]
	public Ref<Item> ChargeOfItemDraw;

	[Name("charge-of-money-draw")]
	public long ChargeOfMoneyDraw;

	[Name("acquire-draw-reward-set-repeat-count")]
	public int AcquireDrawRewardSetRepeatCount;
}

public enum RandomStoreNumberSeq
{
	InvalidNumber,

	RandomStore1,

	RandomStore2,
}