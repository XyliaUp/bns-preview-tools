using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImprove : ModelElement
{
	public int Id { get; set; }

	public sbyte Level { get; set; }

	public int SuccessOptionListId { get; set; }

	public sbyte SuccessOptionSlotNumber { get; set; }


	internal IEnumerable<ItemRecipeHelper> CreateRecipe()
	{
		var recipes = new List<ItemRecipeHelper>();

		for (sbyte i = 1; i <= 5; i++)
		{
			var CostMainItem = Attributes.Get<Record>("cost-main-item-" + i)?.As<Item>();
			if (CostMainItem is null) continue;

			var CostMainItemCount = Attributes.Get<short>("cost-main-item-count-" + i);
			var UseSuccessProbability = Attributes.Get<BnsBoolean>("use-success-probability-" + i);

			recipes.Add(new ItemRecipeHelper()
			{
				MainItem = CostMainItem,
				MainItemCount = CostMainItemCount,
				SuccessProbability = (short)(UseSuccessProbability ? 100 : 1000),
			}); 
		}

		return recipes;
	}
}