using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("reset-npc-all-hate")]
public sealed class ResetNpcAllHate : Reaction
{
	public Script_obj Target;

	[Signal("group-1")]
	public Script_obj Group1;
}