using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("debug-trace")]
public sealed class DebugTrace : Reaction
{
	public string Text;
}