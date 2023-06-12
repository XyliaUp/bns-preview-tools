using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.QuestData
{
	[Signal("next-quest")]
	public sealed class NextQuest : BaseRecord
	{
		/// <summary>
		/// 任务别名
		/// </summary>
		public string Quest;

		/// <summary>
		/// 所属势力
		/// </summary>
		public string Faction;

		 /// <summary>
		 /// 职业
		 /// </summary>
		[Signal("job-1")] public JobSeq Job1;

		/// <summary>
		/// 职业
		/// </summary>
		[Signal("job-2")] public JobSeq Job2;

		/// <summary>
		/// 职业
		/// </summary>
		[Signal("job-3")] public JobSeq Job3;

		/// <summary>
		/// 职业
		/// </summary>
		[Signal("job-4")] public JobSeq Job4;

		/// <summary>
		/// 职业
		/// </summary>
		[Signal("job-5")] public JobSeq Job5;
	}
}