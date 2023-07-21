using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-npc-hate-on")]
public sealed class SetNpcHateOn : Reaction
{
	public Script_obj Target;

	public bool Flag;
}