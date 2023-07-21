using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class RandomStoreDrawReward : BaseRecord
{
	[Signal("random-store-number")]
	public RandomStoreNumberSeq RandomStoreNumber;

	[Signal("required-draw-count")]
	public int RequiredDrawCount;
}