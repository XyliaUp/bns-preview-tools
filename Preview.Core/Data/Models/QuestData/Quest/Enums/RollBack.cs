
using Xylia.Preview.Data.Common.Attribute;

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