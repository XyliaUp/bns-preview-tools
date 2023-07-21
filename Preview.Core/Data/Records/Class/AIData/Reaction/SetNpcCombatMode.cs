using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 设置NPC战斗模式
/// </summary>
[Signal("set-npc-combat-mode")]
public sealed class SetNpcCombatMode : NpcBase
{
	public Script_obj Target;

	[Signal("combat-mode")]
	public bool CombatMode;

	[Signal("attack-target")]
	public Script_obj AttackTarget;
}