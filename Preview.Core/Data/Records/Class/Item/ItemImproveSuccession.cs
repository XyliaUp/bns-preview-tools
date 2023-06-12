using System.Linq;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class ItemImproveSuccession : BaseRecord
	{
		[Signal("feed-item-improve-id")]
		public int FeedItemImproveId;

		[Signal("feed-item-improve-level")]
		public byte FeedItemImproveLevel;

		[Signal("result-item-improve-id")]
		public int ResultItemImproveId;

		[Signal("result-item-improve-level")]
		public byte ResultItemImproveLevel;

		[Signal("seed-item-improve-id")]
		public int SeedItemImproveId;

		[Signal("seed-item-improve-level")]
		public byte SeedItemImproveLevel;


		#region Functions
		public static ItemImproveSuccession QuerySeedItem(Item ItemInfo) => FileCache.Data.ItemImproveSuccession.FirstOrDefault(o => o.SeedItemImproveId == ItemInfo.ImproveId && o.SeedItemImproveLevel == ItemInfo.ImproveLevel);

		public Item GetFeedItem(Item ItemInfo) => FileCache.Data.Item.FirstOrDefault(o => o.ImproveId == this.FeedItemImproveId && o.ImproveLevel == this.FeedItemImproveLevel && o.Brand == ItemInfo.Brand);
		public Item GetResultItem(Item ItemInfo) => FileCache.Data.Item.FirstOrDefault(o => o.ImproveId == this.ResultItemImproveId && o.ImproveLevel == this.ResultItemImproveLevel && o.Brand == ItemInfo.Brand);
		#endregion
	}
}