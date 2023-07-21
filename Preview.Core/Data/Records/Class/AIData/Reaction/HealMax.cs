using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("heal-max")]
public sealed class HealMax : Reaction
{
	[Obsolete]
	public Script_obj Target;

	public Script_obj Target2;
}