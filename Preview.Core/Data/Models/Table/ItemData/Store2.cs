using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class Store2 : ModelElement
{
	public Ref<Text> Name2 { get; set; }

	public string Icon { get; set; }

	public string NoneSelectedIcon { get; set; }

	public Ref<Faction> Faction { get; set; }

	[Name("item")]
	public Ref<Item>[] Item { get; set; }

	[Name("buy-price")]
	public Ref<ItemBuyPrice>[] BuyPrice { get; set; }


	#region Properities
	public object ItemList
	{
		get
		{
			var source = new KeyValuePair<Item, ItemBuyPrice>[127];
			for (int x = 0; x < source.Length; x++)
			{
				source[x] = new(
					Item[x].Instance,
					BuyPrice[x].Instance);
			}

			return source.Where(x => x.Key != null && x.Value != null);
		}
	}
	#endregion
}