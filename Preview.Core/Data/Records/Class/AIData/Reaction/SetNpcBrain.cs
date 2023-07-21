using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-npc-brain")]
public sealed class SetNpcBrain : NpcBase
{
	public Script_obj Npc;

	public Script_obj Target;
}