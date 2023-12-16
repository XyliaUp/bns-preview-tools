using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveSuccession : ModelElement
{
	public int FeedItemImproveId { get; set; }

	public sbyte FeedItemImproveLevel { get; set; }

	public int ResultItemImproveId { get; set; }

	public sbyte ResultItemImproveLevel { get; set; }

	public int SeedItemImproveId { get; set; }

	public sbyte SeedItemImproveLevel { get; set; }


	#region Functions
	public static ItemImproveSuccession QuerySeedItem(Item ItemInfo) => FileCache.Data.Get<ItemImproveSuccession>().FirstOrDefault(o => o.SeedItemImproveId == ItemInfo.ImproveId && o.SeedItemImproveLevel == ItemInfo.ImproveLevel);


	public Item GetFeedItem(Item ItemInfo) => FileCache.Data.Item.FirstOrDefault(o => 
		o.ImproveId == this.FeedItemImproveId && o.ImproveLevel == this.FeedItemImproveLevel &&
		o.Brand.Instance == ItemInfo.Brand.Instance);
	
	public Item GetResultItem(Item ItemInfo) => FileCache.Data.Item.FirstOrDefault(o =>
		o.ImproveId == this.ResultItemImproveId && o.ImproveLevel == this.ResultItemImproveLevel &&
		o.Brand.Instance == ItemInfo.Brand.Instance);
	#endregion
}