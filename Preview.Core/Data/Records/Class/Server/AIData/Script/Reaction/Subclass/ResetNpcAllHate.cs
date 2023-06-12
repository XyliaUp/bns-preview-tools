using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 重置全部仇恨
	/// </summary>
	[Signal("reset-npc-all-hate")]
	public sealed class ResetNpcAllHate : IReaction
	{
		public Script_obj Target;

		[Signal("group-1")]
		public Script_obj Group1;
	}
}