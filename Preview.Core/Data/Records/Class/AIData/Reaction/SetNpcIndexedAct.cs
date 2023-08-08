using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-npc-indexed-act")]
public sealed class SetNpcIndexedAct : Reaction
{
	[Signal("seq-idx")]
	public sbyte SeqIdx;

	public Script_obj Target;
}