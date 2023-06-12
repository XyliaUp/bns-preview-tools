namespace Xylia.Preview.Data.Record.ScriptData.Decision
{
	public sealed class Decision : IDecision
	{
		/// <summary>
		/// 事件类型
		/// </summary>
		public EventType Event;

		/// <summary>
		/// 继承母对象的当前事件
		/// </summary>
		public bool Forward;
	}
}