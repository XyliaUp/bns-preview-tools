using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("spawn-field-item")]
public sealed class SpawnFieldItem : Reaction
{
	public ZoneArea Area;

	[Signal("field-item")]
	public FieldItem FieldItem;
}