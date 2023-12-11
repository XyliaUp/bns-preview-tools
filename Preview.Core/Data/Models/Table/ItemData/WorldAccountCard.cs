using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class WorldAccountCard : ModelElement, IHaveName
{
	public string Alias;

	public Ref<Item> Item;

	public bool Disabled;

	[Name("sort-no")]
	public short SortNo;

	[Name("card-image")]
	public string CardImage;


	#region Interface
	public string Text => Item.Instance?.Name2.GetText();
	#endregion
}