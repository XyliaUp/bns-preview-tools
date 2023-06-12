using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("npc-talk-finish")]
	public sealed class NpcTalkFinish : IReaction
	{
		public Script_obj Npc;
	}
}