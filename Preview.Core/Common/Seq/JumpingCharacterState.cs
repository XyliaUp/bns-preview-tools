using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	public enum JumpingCharacterState
	{
		[Signal("dont-care")]
		DontCare,

		None,

		Trial,

		Jumping,
	}
}