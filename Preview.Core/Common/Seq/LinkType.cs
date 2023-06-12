using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	public enum LinkType
	{
		None,
		Mount,
		Inhalation,
		Slugfest,
		Catch,

		[Signal("catch-none-human")]
		CatchNoneHuman,

		[Signal("catch-friend")]
		CatchFriend,

		[Signal("inhalation-catch")]
		InhalationCatch,

		[Signal("range-catch")]
		RangeCatch,
	}
}