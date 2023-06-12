using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("warp-to-reentrance")]
	public sealed class WarpToReentrance : IReaction
	{
		public Script_obj Target;
	}
}