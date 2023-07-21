using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 设置NPC活动
/// </summary>
[Signal("set-npc-act")]
public sealed class SetNpcAct : Reaction
{
	public Script_obj Target;

	public ActSequence.ActSequence Seq;
}