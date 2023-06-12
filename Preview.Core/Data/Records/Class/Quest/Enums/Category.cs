using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	public enum Category
	{
		/// <summary>
		/// 主线任务
		/// </summary>
		Epic,

		/// <summary>
		/// 普通任务
		/// </summary>
		Normal,

		/// <summary>
		/// 职业任务
		/// </summary>
		Job,


		/// <summary>
		/// 副本进度任务
		/// </summary>
		Dungeon,


		/// <summary>
		/// 斩首任务
		/// </summary>
		Attraction,


		[Signal("tendency-simple")]
		TendencySimple,

		[Signal("tendency-tendency")]
		TendencyTendency,


		/// <summary>
		/// 师徒任务
		/// </summary>
		Mentoring,


		Hunting,
	}
}
