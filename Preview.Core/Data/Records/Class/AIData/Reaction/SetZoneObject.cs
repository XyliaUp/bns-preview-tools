using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-zone-object")]
public sealed class SetZoneObject : Reaction
{
	public Script_obj Object;

	public sbyte Zreg;
}