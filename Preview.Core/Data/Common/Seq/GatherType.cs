using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Common.Seq;
public enum GatherType
{
	Target,

	[Name("target-360")]
	Target360,

	[Name("target-front-180")]
	TargetFront180,

	[Name("target-back-180")]
	TargetBack180,

	[Name("target-front-90")]
	TargetFront90,

	[Name("target-back-90")]
	TargetBack90,

	[Name("target-front-15")]
	TargetFront15,

	[Name("target-front-30")]
	TargetFront30,

	[Name("target-front-45")]
	TargetFront45,

	[Name("target-front-60")]
	TargetFront60,

	[Name("target-front-120")]
	TargetFront120,

	[Name("target-front-270")]
	TargetFront270,

	[Name("laser")]
	Laser,

	[Name("target-and-link-target")]
	TargetAndLinkTarget,

	[Name("shifting-laser")]
	ShiftingLaser,
}