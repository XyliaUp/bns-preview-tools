using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("debug-print")]
public sealed class DebugPrint : Reaction
{
	public string text;

	[Repeat(8)]
	public Script_obj[] Param;
}