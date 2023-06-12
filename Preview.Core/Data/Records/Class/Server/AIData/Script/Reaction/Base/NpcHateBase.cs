using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction.Base
{
	public abstract class NpcHateBase : IReaction
	{
		public Script_obj Opponent;

		public Script_obj Target;
	}
}