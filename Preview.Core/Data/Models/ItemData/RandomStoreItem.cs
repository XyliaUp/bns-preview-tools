﻿using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStoreItem : Record
{
	public string Alias;


	[Name("item")]
	public Ref<Item> Item;

	[Name("item-count")]
	public int ItemCount;

	[Name("item-price-money")]
	public int ItemPriceMoney;

	[Name("item-price-item")]
	public Ref<Item> ItemPriceItem;

	[Name("item-price-item-amount")]
	public short ItemPriceItemAmount;
}