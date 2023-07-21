using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 设置NPC可被攻击
/// </summary>
[Signal("set-npc-attackable")]
public sealed class SetNpcAttackable : Reaction
{
	public Script_obj Target;

	public bool Flag;
}