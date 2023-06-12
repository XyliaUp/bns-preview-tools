using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	public enum Detect
	{
		None,

		[Signal("both-360")]
		Both360,

		[Signal("pc-180")]
		Pc180,

		[Signal("npc-180")]
		Npc180,

		[Signal("pc-360")]
		Pc360,

		[Signal("npc-360")]
		Npc360,
	}
}