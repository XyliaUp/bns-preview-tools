using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-env-state")]
public sealed class SetEnvState : Reaction
{
	[Obsolete]
	public Script_obj Target;

	public Script_obj Target2;

	[Obsolete]
	public EnvState State;

	public EnvState State2;
}