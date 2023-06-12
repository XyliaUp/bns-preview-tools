using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	public sealed class RandomStoreItem : BaseRecord
	{
		public Item Item;

		[Signal("item-count")]
		public int ItemCount;

		[Signal("item-price-money")]
		public int ItemPriceMoney;

		[Signal("item-price-item")]
		public Item ItemPriceItem;

		[Signal("item-price-item-amount")]
		public short ItemPriceItemAmount;
	}
}