using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("reset-npc-hate")]
public sealed class ResetNpcHate : Reaction
{
	public Script_obj Opponent;

	public Script_obj Target;
}