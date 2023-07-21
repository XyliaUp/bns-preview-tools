using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;
[Signal("dispel-buff")]
public sealed class DispelBuff : Reaction
{
	public Script_obj Target;
}