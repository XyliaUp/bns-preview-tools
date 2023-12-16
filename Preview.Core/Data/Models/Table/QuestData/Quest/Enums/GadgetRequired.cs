using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.QuestData.Enums;
public enum GadgetRequired
{
	[Name("dont-care")]
	DontCare,

	[Name("carry-and-remove")]
	CarryAndRemove,

	Carry,

	Empty,

	[Name("not-A")]
	NotA,
}