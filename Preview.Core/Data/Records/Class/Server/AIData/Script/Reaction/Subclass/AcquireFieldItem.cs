using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("acquire-field-item")]
	public sealed class AcquireFieldItem : IReaction
	{
		public Script_obj Target;

		[Signal("field-item")]
		public FieldItem FieldItem;
	}
}