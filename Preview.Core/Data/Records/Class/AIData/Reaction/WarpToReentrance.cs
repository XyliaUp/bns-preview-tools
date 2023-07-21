using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("warp-to-reentrance")]
public sealed class WarpToReentrance : Reaction
{
	public Script_obj Target;
}