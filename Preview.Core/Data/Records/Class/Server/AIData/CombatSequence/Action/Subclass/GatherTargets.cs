using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.CombatSequenceData.Enums;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 采集特殊目标
	/// 选择的目标会被作为 special-target
	/// 可能的错误 
	/// 此类型只能用于特殊序列 、序列内只能有一个此类型活动
	/// 只能用于boss-npc
	/// </summary>
	[Signal("gather-targets")]
	public sealed class GatherTargets : SkillBase
	{
		public Flag Condition;

		[Signal("gather-count")]
		public byte GatherCount;

		[Signal("gather-rule")]
		public GatherRule GatherRule;
	}
}