using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStoreDrawReward : ModelElement
{
	public string Alias;


	[Name("random-store-number")]
	public RandomStoreNumberSeq RandomStoreNumber;

	[Name("required-draw-count")]
	public int RequiredDrawCount;
}