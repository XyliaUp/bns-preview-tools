using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
public sealed class ReactionSet : ModelElement
{
	[Side(ReleaseSide.Server)]
	public List<Reaction> Reaction { get; set; }

	/// <summary>
	/// 概率 0~100, decision 下的所有 ReactionSet 概率和不能超过最大值
	/// </summary>
	public sbyte Probability;

	/// <summary>
	/// 概率 0~10000, decision 下的所有 ReactionSet 概率和不能超过最大值 
	/// </summary>
	public short Probability10000;
}