using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	public enum RecycleGroup
	{
		None,

		[Signal("class")]
		Class,

		[Signal("item-1")]
		Item1,

		[Signal("item-2")]
		Item2,

		[Signal("class-2")]
		Class2,

		[Signal("db")]
		DB,

		[Signal("gadget")]
		Gadget,
	}
}