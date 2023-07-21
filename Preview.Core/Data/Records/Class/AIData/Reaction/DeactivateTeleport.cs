using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("deactivate-teleport")]
public sealed class DeactivateTeleport : Reaction
{
	public Script_obj Target;

	public Teleport Teleport;
}