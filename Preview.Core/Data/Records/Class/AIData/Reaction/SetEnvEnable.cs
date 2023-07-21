using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;


[Signal("set-env-enable")]
public sealed class SetEnvEnable : Reaction
{
	[Obsolete]
	public Script_obj Target;

	[Obsolete]
	public Script_obj Target1;


	public Script_obj Target2;


	public bool Enable;

	public bool State;
}