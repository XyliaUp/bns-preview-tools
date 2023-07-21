using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class Store2 : BaseRecord
{
	public Text Name2;

	[Signal("icon")]
	public string Icon;

	[Signal("none-selected-icon")]
	public string NoneSelectedIcon;

	public Faction Faction;


	[Signal("item"), Repeat(127)]
	public Item[] Item;

	[Signal("buy-price"), Repeat(127)]
	public ItemBuyPrice[] BuyPrice;
}