using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("deactivate-teleport")]
	public sealed class DeactivateTeleport : IReaction
	{
		public Script_obj Target;

		public Teleport Teleport;
	}
}