using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Common.Seq;
public enum Detect
{
	None,

	[Name("both-360")]
	Both360,

	[Name("pc-180")]
	Pc180,

	[Name("npc-180")]
	Npc180,

	[Name("pc-360")]
	Pc360,

	[Name("npc-360")]
	Npc360,
}