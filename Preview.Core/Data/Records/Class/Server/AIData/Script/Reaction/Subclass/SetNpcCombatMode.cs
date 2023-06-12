using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record.ScriptData.Reaction.Base;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
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
}