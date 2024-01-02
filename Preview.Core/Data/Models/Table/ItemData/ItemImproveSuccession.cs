using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveSuccession : ModelElement
{
	public int SeedImproveId { get; set; }
	public sbyte SeedImproveLevel { get; set; }
	public int ResultImproveId { get; set; }
	public sbyte ResultImproveLevel { get; set; }
	public int FeedMainImproveId { get; set; }
	public sbyte FeedMainImproveLevel { get; set; }


	internal IEnumerable<ItemRecipeHelper> CreateRecipe(out Item ResultItem)
	{
		var recipe = new ItemRecipeHelper();
		ResultItem = FileCache.Data.Get<Item>().FirstOrDefault(item =>
			item.ImproveId == ResultImproveId && item.ImproveLevel == ResultImproveLevel);

		recipe.SuccessProbability = 1000;

		return [recipe];
	}
}