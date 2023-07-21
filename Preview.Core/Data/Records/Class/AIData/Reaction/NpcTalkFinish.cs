using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("npc-talk-finish")]
public sealed class NpcTalkFinish : Reaction
{
	public Script_obj Npc;
}