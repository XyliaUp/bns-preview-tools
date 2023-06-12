using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 增加挑战分数
	/// 允许负值
	/// </summary>
	[Signal("add-zone-score")]
	public sealed class AddZoneScore : IReaction
	{
		/// <summary>
		/// 分数
		/// </summary>
		public int Score;
	}
}