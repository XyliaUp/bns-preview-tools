using System.Diagnostics;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveSuccession : ModelElement
{
	#region Attributes
	public int SeedImproveId { get; set; }
	public sbyte SeedImproveLevel { get; set; }
	public int ResultImproveId { get; set; }
	public sbyte ResultImproveLevel { get; set; }
	public int FeedMainImproveId { get; set; }
	public sbyte FeedMainImproveLevel { get; set; }


	public bool KeepMainIngredientSpirit { get; set; }
	#endregion


	#region Methods
	internal static ItemImproveSuccession FindBySeed(Item SeedItem)
	{
		return FileCache.Data.Provider.GetTable<ItemImproveSuccession>().FirstOrDefault(record =>
			SeedItem.ImproveId == record.SeedImproveId && SeedItem.ImproveLevel == record.SeedImproveLevel);
	}

	internal static ItemImproveSuccession FindByFeed(Item FeedItem, Item SeedItem = null)
	{
		return FileCache.Data.Provider.GetTable<ItemImproveSuccession>().FirstOrDefault(record =>
			FeedItem.ImproveId == record.FeedMainImproveId && FeedItem.ImproveLevel == record.FeedMainImproveLevel &&
		   (SeedItem is null || SeedItem.ImproveId == record.ResultImproveId));
	}


	internal IEnumerable<ItemRecipeHelper> CreateRecipe(Item SeedItem, out Item ResultItem)
	{
		// This method is missing the seed, the result is inaccurate
		if (SeedItem is null)
		{
			ResultItem = FileCache.Data.Provider.GetTable<Item>().FirstOrDefault(item => item.ImproveId == ResultImproveId && item.ImproveLevel == ResultImproveLevel);
			SeedItem = FileCache.Data.Provider.GetTable<Item>().FirstOrDefault(item => item.ImproveId == SeedImproveId && item.ImproveLevel == SeedImproveLevel);
		}
		else
		{
			Debug.Assert(SeedItem.ImproveId == SeedImproveId);
			Debug.Assert(SeedItem.ImproveLevel == SeedImproveLevel);
			ResultItem = ItemImprove.GetResultItem(SeedItem, ResultImproveLevel);
		}

		// NOTE:
		// If pass in is the feed-item , use the seed-item as the MainItem
		// Otherwise , use the feed-item as the MainItem 
		var FeedMainIngredient = Attributes.Get<Record>("feed-main-ingredient")?.As<Item>();
		var FeedMainIngredientCount = Attributes.Get<short>("feed-main-ingredient-count");
		var FeedSubIngredient = LinqExtensions.For(8, (id) => Attributes.Get<Record>($"feed-sub-ingredient-{id}")?.As<Item>());
		var FeedSubIngredientCount = LinqExtensions.For(8, (id) => Attributes.Get<short>($"feed-sub-ingredient-count-{id}"));
		var CostMoney = Attributes.Get<int>("cost-money");


		var recipe = new ItemRecipeHelper
		{
			MainItem = SeedItem,	  
			MainItemCount = 1,
			SubItem = [FeedMainIngredient, .. FeedSubIngredient],
			SubItemCount = [FeedMainIngredientCount, .. FeedSubIngredientCount],
			Money = CostMoney,
			SuccessProbability = 1000,

			Guide = "UI.ItemGrowth2.ImproveSuccession.Warning.SeedItem".GetText(),
		};

		return [recipe];
	}
	#endregion
}