using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("remove-field-item")]
	public sealed class RemoveFieldItem : IReaction
	{
		public Script_obj Target;

		public string Spawn1;
		public string Spawn2;
	}
}