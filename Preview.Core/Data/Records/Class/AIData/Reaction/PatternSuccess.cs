using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 挑战模式阶段成功
/// </summary>
[Signal("pattern-success")]
public sealed class PatternSuccess : Reaction
{
	public byte Index;
}