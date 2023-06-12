using Xylia.Preview.Common.Attribute;

using Xylia.Preview.Data.Record.ScriptData.Reaction.Base;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("diff-npc-hate")]
	public sealed class DiffNpcHate : NpcHateBase
	{
		public int Amount;
	}
}