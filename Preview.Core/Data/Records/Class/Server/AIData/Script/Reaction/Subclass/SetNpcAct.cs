using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置NPC活动
	/// </summary>
	[Signal("set-npc-act")]
	public sealed class SetNpcAct : IReaction
	{
		public Script_obj Target;

		public ActSequence.ActSequence Seq;
	}
}