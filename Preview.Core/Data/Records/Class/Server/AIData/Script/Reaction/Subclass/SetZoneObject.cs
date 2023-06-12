using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("set-zone-object")]
	public sealed class SetZoneObject : IReaction
	{
		public Script_obj Object;

		public byte Zreg;
	}
}