using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("dispel-by-attr")]
public sealed class DispelByAttr : Reaction
{
	public Script_obj Target;

	public EffectAttributeSeq Attr;
}