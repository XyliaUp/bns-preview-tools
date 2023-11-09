using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class Store2 : Record
{
	public string Alias;

	public Ref<Text> Name2;

	public string Icon;

	public string NoneSelectedIcon;

	public Ref<Faction> Faction;

	[Name("item"), Repeat(127)]
	public Ref<Item>[] Item;

	[Name("buy-price"), Repeat(127)]
	public Ref<ItemBuyPrice>[] BuyPrice;


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