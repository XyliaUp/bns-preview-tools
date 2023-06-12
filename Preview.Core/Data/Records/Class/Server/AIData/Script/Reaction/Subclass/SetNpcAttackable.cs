using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置NPC可被攻击
	/// </summary>
	[Signal("set-npc-attackable")]
	public sealed class SetNpcAttackable : IReaction
	{
		public Script_obj Target;

		public bool Flag;
	}
}