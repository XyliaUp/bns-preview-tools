using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("acquire-field-item")]
public sealed class AcquireFieldItem : Reaction
{
	public Script_obj Target;

	[Signal("field-item")]
	public FieldItem FieldItem;
}