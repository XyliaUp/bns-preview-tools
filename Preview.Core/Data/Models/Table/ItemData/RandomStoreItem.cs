namespace Xylia.Preview.Data.Models;
public sealed class RandomStoreItem : ModelElement
{
	public Ref<Item> Item { get; set; }

	public int ItemCount { get; set; }

	public int ItemPriceMoney { get; set; }

	public Ref<Item> ItemPriceItem { get; set; }

	public short ItemPriceItemAmount { get; set; }
}