using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record.ScriptData.Reaction.Base;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置NPC智力
	/// </summary>
	[Signal("set-npc-brain")]
	public sealed class SetNpcBrain : NpcBase
	{
		public Script_obj Npc;

		public Script_obj Target;
	}
}