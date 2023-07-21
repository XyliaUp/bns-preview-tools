using Xylia.Preview.Common.Attribute;
namespace Xylia.Preview.Data.Record.CombatSequenceData.Enums;

/// <summary>
/// 转移条件
/// </summary>
public enum TransitCond
{
	/// <summary>
	/// 手动转移
	/// </summary>
	Manual,


	[Signal("alarm-1")]
	Alarm1,

	[Signal("alarm-2")]
	Alarm2,

	[Signal("alarm-3")]
	Alarm3,

	[Signal("alarm-4")]
	Alarm4,

	[Signal("alarm-5")]
	Alarm5,

	[Signal("alarm-6")]
	Alarm6,

	[Signal("alarm-7")]
	Alarm7,

	[Signal("alarm-8")]
	Alarm8,


	[Signal("bleeding-5")]
	Bleeding5,

	[Signal("bleeding-10")]
	Bleeding10,

	[Signal("bleeding-15")]
	Bleeding15,

	[Signal("bleeding-20")]
	Bleeding20,

	[Signal("bleeding-25")]
	Bleeding25,

	[Signal("bleeding-30")]
	Bleeding30,

	[Signal("bleeding-35")]
	Bleeding35,

	[Signal("bleeding-40")]
	Bleeding40,

	[Signal("bleeding-45")]
	Bleeding45,

	[Signal("bleeding-50")]
	Bleeding50,

	[Signal("bleeding-55")]
	Bleeding55,

	[Signal("bleeding-60")]
	Bleeding60,

	[Signal("bleeding-65")]
	Bleeding65,

	[Signal("bleeding-70")]
	Bleeding70,

	[Signal("bleeding-75")]
	Bleeding75,

	[Signal("bleeding-80")]
	Bleeding80,

	[Signal("bleeding-85")]
	Bleeding85,

	[Signal("bleeding-90")]
	Bleeding90,

	[Signal("bleeding-95")]
	Bleeding95,

	[Signal("bleeding-100")]
	Bleeding100,
}