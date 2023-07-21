using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper.Output;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class WorldAccountMuseum : BaseRecord, IOut
{
	#region Fields
	[Signal("start-time")]
	public DateTime StartTime;

	[Signal("end-time")]
	public DateTime EndTime;

	[Signal("can-not-used")]
	public bool CanNotUsed;

	[Signal("ability"), Repeat(3)]
	public AttachAbility[] Ability;

	[Signal("ability-value"), Repeat(3)]
	public int[] AbilityValue;

	[Signal("collection-name")]
	public Text CollectionName;

	[Signal("collection-category")]
	public CollectionCategorySeq CollectionCategory;

	public enum CollectionCategorySeq
	{
		[Signal("level-1")]
		Level1,

		[Signal("level-2")]
		Level2,

		[Signal("level-3")]
		Level3,

		[Signal("level-4")]
		Level4,

		Event,

		Favorite,
	}
	#endregion


	OutSetTable IOut.OutTable()
	{
		OutSetTable table = new();
		table.type = typeof(WorldAccountMuseum).Name;
		table.attribute.Add(new("alias"));
		table.attribute.Add(new("collection-name"));
		table.attribute.Add(new("collection-category"));
		table.attribute.Add(new("ability")
		{
			repeat = 3,
			extra = "ability-value"
		});
		table.attribute.Add(new("collection-card")
		{
			repeat = 8,
			extra = "collection-card-count"
		});

		return table;
	}
}