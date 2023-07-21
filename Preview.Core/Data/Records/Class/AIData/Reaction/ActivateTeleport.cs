using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("activate-teleport")]
public sealed class ActivateTeleport : Reaction
{
	public Script_obj Target;

	public Teleport Teleport;
}