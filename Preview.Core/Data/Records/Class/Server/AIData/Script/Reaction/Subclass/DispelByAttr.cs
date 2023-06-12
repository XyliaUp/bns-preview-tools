using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("dispel-by-attr")]
	public sealed class DispelByAttr : IReaction
	{
		public Script_obj Target;

		public EffectAttribute Attr;
	}
}