using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	public enum GatherType
	{
		Target,

		[Signal("target-360")]
		Target360,

		[Signal("target-front-180")]
		TargetFront180,

		[Signal("target-back-180")]
		TargetBack180,

		[Signal("target-front-90")]
		TargetFront90,

		[Signal("target-back-90")]
		TargetBack90,

		[Signal("target-front-15")]
		TargetFront15,

		[Signal("target-front-30")]
		TargetFront30,

		[Signal("target-front-45")]
		TargetFront45,

		[Signal("target-front-60")]
		TargetFront60,

		[Signal("target-front-120")]
		TargetFront120,

		[Signal("target-front-270")]
		TargetFront270,

		[Signal("laser")]
		Laser,

		[Signal("target-and-link-target")]
		TargetAndLinkTarget,

		[Signal("shifting-laser")]
		ShiftingLaser,
	}
}