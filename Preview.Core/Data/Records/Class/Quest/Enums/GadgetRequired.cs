using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	public enum GadgetRequired
	{
		[Signal("dont-care")]
		DontCare,

		[Signal("carry-and-remove")]
		CarryAndRemove,

		Carry,

		Empty,

		[Signal("not-A")]
		NotA,
	}
}