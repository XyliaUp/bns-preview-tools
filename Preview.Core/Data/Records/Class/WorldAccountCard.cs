using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class WorldAccountCard : BaseRecord, IName
{
	public string Item;

	public bool Disabled;

	[Signal("sort-no")]
	public short SortNo;

	[Signal("card-image")]
	public string CardImage;


	#region Interface
	public string GetName() => Item.CastObject<Item>()?.Name2;
	#endregion
}