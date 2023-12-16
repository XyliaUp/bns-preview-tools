using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStoreDrawReward : ModelElement
{
	[Name("random-store-number")]
	public RandomStoreNumberSeq RandomStoreNumber { get; set; }

	[Name("required-draw-count")]
	public int RequiredDrawCount { get; set; }
}