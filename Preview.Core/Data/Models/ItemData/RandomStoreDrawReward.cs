using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStoreDrawReward : Record
{
	public string Alias;


	[Name("random-store-number")]
	public RandomStoreNumberSeq RandomStoreNumber;

	[Name("required-draw-count")]
	public int RequiredDrawCount;
}