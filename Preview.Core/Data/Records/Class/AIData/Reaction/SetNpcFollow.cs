using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-npc-follow")]
public sealed class SetNpcFollow : Reaction
{
	public Script_obj Master;

	public Script_obj Npc;

	public Detect Detect;
}