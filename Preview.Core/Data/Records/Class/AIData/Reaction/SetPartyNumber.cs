using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-party-number")]
public sealed class SetPartyNumber : Reaction
{
	public Script_obj Target;

	public sbyte Reg;

	public int Amount;
}