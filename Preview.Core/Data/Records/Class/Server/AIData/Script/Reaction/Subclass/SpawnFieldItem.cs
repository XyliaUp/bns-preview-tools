using Xylia.Preview.Common.Attribute;

using  Xylia.Preview.Data.Record;


namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("spawn-field-item")]
	public sealed class SpawnFieldItem : IReaction
	{
		public ZoneArea Area;

		[Signal("field-item")]
		public FieldItem FieldItem;
	}
}