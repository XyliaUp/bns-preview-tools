
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums;
public enum RollBack
{
	None,

	[Signal("leave-world")]
	LeaveWorld,

	[Signal("leave-zone")]
	LeaveZone,

	[Signal("remove-fielditem")]
	RemoveFieldItem,
}