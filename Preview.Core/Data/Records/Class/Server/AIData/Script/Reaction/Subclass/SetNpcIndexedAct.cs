using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("set-npc-indexed-act")]
	public sealed class SetNpcIndexedAct : IReaction
	{
		[Signal("seq-idx")]
		public byte SeqIdx;

		public Script_obj Target;
	}
}