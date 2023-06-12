using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("in-out-detect-stop")]
	public sealed class InOutDetectStop : IReaction
	{
		public byte Index;
	}
}