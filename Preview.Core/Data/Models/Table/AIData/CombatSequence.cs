using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class CombatSequence : ModelElement
{
	
}

#region Sequence
public enum GatherRule
{
	Far,

	Near,

	Random,

	Rotate,

	MinHate,
}

public enum GroundPattern
{
	Pattern2,
	Pattern3,
	Pattern4,
	Pattern5,
}

public enum MoveType
{
	Retreat,

	Flee,
}

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

public enum TransitAction
{
	None,

	Burrow,

	Unburrow,

	Social,
}

public enum TransitCond
{
	Manual,


	[Name("alarm-1")]
	Alarm1,

	[Name("alarm-2")]
	Alarm2,

	[Name("alarm-3")]
	Alarm3,

	[Name("alarm-4")]
	Alarm4,

	[Name("alarm-5")]
	Alarm5,

	[Name("alarm-6")]
	Alarm6,

	[Name("alarm-7")]
	Alarm7,

	[Name("alarm-8")]
	Alarm8,


	[Name("bleeding-5")]
	Bleeding5,

	[Name("bleeding-10")]
	Bleeding10,

	[Name("bleeding-15")]
	Bleeding15,

	[Name("bleeding-20")]
	Bleeding20,

	[Name("bleeding-25")]
	Bleeding25,

	[Name("bleeding-30")]
	Bleeding30,

	[Name("bleeding-35")]
	Bleeding35,

	[Name("bleeding-40")]
	Bleeding40,

	[Name("bleeding-45")]
	Bleeding45,

	[Name("bleeding-50")]
	Bleeding50,

	[Name("bleeding-55")]
	Bleeding55,

	[Name("bleeding-60")]
	Bleeding60,

	[Name("bleeding-65")]
	Bleeding65,

	[Name("bleeding-70")]
	Bleeding70,

	[Name("bleeding-75")]
	Bleeding75,

	[Name("bleeding-80")]
	Bleeding80,

	[Name("bleeding-85")]
	Bleeding85,

	[Name("bleeding-90")]
	Bleeding90,

	[Name("bleeding-95")]
	Bleeding95,

	[Name("bleeding-100")]
	Bleeding100,
}
#endregion