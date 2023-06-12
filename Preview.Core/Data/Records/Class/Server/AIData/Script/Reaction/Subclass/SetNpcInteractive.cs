using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("set-npc-interactive")]
	public sealed class SetNpcInteractive : IReaction
	{
		public Script_obj Target;

		public bool Flag;
	}
}