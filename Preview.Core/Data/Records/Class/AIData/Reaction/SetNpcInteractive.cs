using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-npc-interactive")]
public sealed class SetNpcInteractive : Reaction
{
	public Script_obj Target;

	public bool Flag;
}