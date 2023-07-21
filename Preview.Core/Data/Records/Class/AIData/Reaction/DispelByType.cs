using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("dispel-by-type")]
public sealed class DispelByType : Reaction
{
	public Script_obj Target;

	public Script_obj From;


	[Signal("dispel-force")]
	public bool DispelForce;

	[Signal("effect-type")]
	public string EffectType;
}