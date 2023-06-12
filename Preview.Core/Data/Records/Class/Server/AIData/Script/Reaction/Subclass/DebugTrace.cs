using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("debug-trace")]
	public sealed class DebugTrace : IReaction
	{
		public string Text;
	}
}