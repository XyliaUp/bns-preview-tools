using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 增加挑战分数
/// </summary>
[Signal("add-zone-score")]
public sealed class AddZoneScore : Reaction
{
	/// <summary>
	/// 允许负值
	/// </summary>
	public int Score;
}