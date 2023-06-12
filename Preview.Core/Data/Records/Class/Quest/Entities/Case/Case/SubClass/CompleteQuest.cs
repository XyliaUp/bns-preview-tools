using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	/// <summary>
	/// 完成任务
	/// </summary>
	public sealed class CompleteQuest : CaseBase
	{
		[Signal("complete-quest")]
		public Quest Complete_Quest;
	}
}