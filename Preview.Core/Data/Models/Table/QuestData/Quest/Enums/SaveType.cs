using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.QuestData.Enums;
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
	[Name("except-completion")]
	ExceptCompletion,

	/// <summary>
	/// 数据范围 28000~
	/// </summary>
	[Name("except-completion-and-logout-save")]
	ExceptCompletionAndLogoutSave,
}