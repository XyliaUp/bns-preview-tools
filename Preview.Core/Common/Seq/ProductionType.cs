using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq;
public enum ProductionType
{
	None,

	[Signal("production-type-1")]
	ProductionType1,

	[Signal("production-type-2")] 
	ProductionType2,

	[Signal("production-type-3")] 
	ProductionType3,

	[Signal("production-type-4")]
	ProductionType4,

	[Signal("production-type-5")]
	ProductionType5,

	[Signal("production-type-6")]
	ProductionType6,

	[Signal("production-type-7")]
	ProductionType7,

	[Signal("gathering-type-1")] 
	GatheringType1,

	[Signal("gathering-type-2")]
	GatheringType2,

	[Signal("gathering-type-3")]
	GatheringType3,

	[Signal("gathering-type-4")]
	GatheringType4,

	[Signal("gathering-type-5")]
	GatheringType5,

	[Signal("gathering-type-6")] 
	GatheringType6,

	[Signal("gathering-type-7")] 
	GatheringType7,
}