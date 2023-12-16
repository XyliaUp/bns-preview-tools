using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.QuestData.Enums;
public enum RollBack
{
	None,

	[Name("leave-world")]
	LeaveWorld,

	[Name("leave-zone")]
	LeaveZone,

	[Name("remove-fielditem")]
	RemoveFieldItem,
}