using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("remove-field-item")]
public sealed class RemoveFieldItem : Reaction
{
	public Script_obj Target;

	public string Spawn1;
	public string Spawn2;
}