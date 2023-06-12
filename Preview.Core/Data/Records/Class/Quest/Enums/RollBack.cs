
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	/// <summary>
	/// 回滚类型
	/// </summary>
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
}