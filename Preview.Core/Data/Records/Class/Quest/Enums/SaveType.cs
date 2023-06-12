using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	/// <summary>
	/// 存储类型
	/// </summary>
	public enum SaveType
	{
		All,

		/// <summary>
		/// 数据范围 25000~25500
		/// </summary>
		Nothing,

		/// <summary>
		/// 数据范围 20000~23000
		/// </summary>
		[Signal("except-completion")]
		ExceptCompletion,

		/// <summary>
		/// 数据范围 28000~
		/// </summary>
		[Signal("except-completion-and-logout-save")]
		ExceptCompletionAndLogoutSave,
	}
}