using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveSuccession : Record
{
	public string Alias;


	[Name("feed-item-improve-id")]
	public int FeedItemImproveId;

	[Name("feed-item-improve-level")]
	public sbyte FeedItemImproveLevel;

	[Name("result-item-improve-id")]
	public int ResultItemImproveId;

	[Name("result-item-improve-level")]
	public sbyte ResultItemImproveLevel;

	[Name("seed-item-improve-id")]
	public int SeedItemImproveId;

	[Name("seed-item-improve-level")]
	public sbyte SeedItemImproveLevel;


	#region Functions
	public static ItemImproveSuccession QuerySeedItem(Item ItemInfo) => FileCache.Data.ItemImproveSuccession.FirstOrDefault(o => o.SeedItemImproveId == ItemInfo.ImproveId && o.SeedItemImproveLevel == ItemInfo.ImproveLevel);


	public Item GetFeedItem(Item ItemInfo) => FileCache.Data.Item.FirstOrDefault(o => 
		o.ImproveId == this.FeedItemImproveId && o.ImproveLevel == this.FeedItemImproveLevel &&
		o.Brand.Instance == ItemInfo.Brand.Instance);
	
	public Item GetResultItem(Item ItemInfo) => FileCache.Data.Item.FirstOrDefault(o =>
		o.ImproveId == this.ResultItemImproveId && o.ImproveLevel == this.ResultItemImproveLevel &&
		o.Brand.Instance == ItemInfo.Brand.Instance);
	#endregion
}