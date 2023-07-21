using Xylia.Preview.Common.Attribute;
namespace Xylia.Preview.Data.Record.CombatSequenceData.Enums;

/// <summary>
/// 结束条件
/// </summary>
public enum TerminateCond
{
	/// <summary>
	/// 最大距离
	/// </summary>
	[Signal("distance-max")]
	DistanceMax,

	/// <summary>
	/// 最小距离
	/// </summary>
	[Signal("distance-min")]
	DistanceMin,

	/// <summary>
	/// 目标获得状态效果
	/// </summary>
	[Signal("target-invoked-effect-flag")]
	TargetInvokedEffectFlag,

	/// <summary>
	/// 目标离开
	/// </summary>
	[Signal("target-leave")]
	TargetLeave,

	/// <summary>
	/// 时间结束
	/// </summary>
	[Signal("time-over")]
	TimeOver,
}