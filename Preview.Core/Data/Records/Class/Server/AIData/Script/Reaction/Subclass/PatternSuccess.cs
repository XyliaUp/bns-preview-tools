using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 挑战模式阶段成功
	/// </summary>
	[Signal("pattern-success")]
	public sealed class PatternSuccess : IReaction
	{
		public byte Index;
	}
}