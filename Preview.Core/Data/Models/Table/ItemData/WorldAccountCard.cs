﻿using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class WorldAccountCard : ModelElement, IHaveName
{
	public Ref<Item> Item { get; set; }

	public string CardImage { get; set; }


	#region Interface
	public string Text => Item.Instance?.Name2.GetText();
	#endregion
}