using Xylia.Preview.Data.Common.Attribute;
namespace Xylia.Preview.Data.Models.CombatSequenceData.Enums;

/// <summary>
/// 结束条件
/// </summary>
public enum TerminateCond
{
	/// <summary>
	/// 最大距离
	/// </summary>
	[Name("distance-max")]
	DistanceMax,

	/// <summary>
	/// 最小距离
	/// </summary>
	[Name("distance-min")]
	DistanceMin,

	/// <summary>
	/// 目标获得状态效果
	/// </summary>
	[Name("target-invoked-effect-flag")]
	TargetInvokedEffectFlag,

	/// <summary>
	/// 目标离开
	/// </summary>
	[Name("target-leave")]
	TargetLeave,

	/// <summary>
	/// 时间结束
	/// </summary>
	[Name("time-over")]
	TimeOver,
}