using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("reset-stage")]
	public sealed class ResetStage : IReaction
	{
		public Script_obj Target;

		[Signal("reset-stage")]
		public NpcResetStage resetStage;
	}
}