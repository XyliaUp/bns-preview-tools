using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("reset-stage")]
public sealed class ResetStage : Reaction
{
	public Script_obj Target;

	[Signal("reset-stage")]
	public NpcResetStage resetStage;
}