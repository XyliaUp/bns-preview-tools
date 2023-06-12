using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置NPC跟随
	/// </summary>
	[Signal("set-npc-follow")]
	public sealed class SetNpcFollow : IReaction
	{
		public Script_obj Master;

		public Script_obj Npc;

		public Detect Detect;
	}
}