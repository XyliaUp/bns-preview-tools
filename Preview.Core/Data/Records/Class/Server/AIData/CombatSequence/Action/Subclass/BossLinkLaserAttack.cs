using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.CombatSequenceData.Enums;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 连线两个对象
	/// </summary>
	[Signal("boss-link-laser-attack")]
	public sealed class BossLinkLaserAttack : SkillBase
	{
		[Signal("is-target-skill-gather")]
		public bool IsTargetSkillGather;

		[Signal("normal-bounce")]
		public bool NormalBounce;

		[Signal("target-from")] 
		public Script_obj TargetFrom;

		[Signal("target-to")] 
		public Script_obj TargetTo;

		[Signal("start-effect-from-1")] 
		public string StartEffectFrom1;

		[Signal("start-effect-from-2")] 
		public string StartEffectFrom2;

		[Signal("start-effect-from-3")] 
		public string StartEffectFrom3;

		[Signal("start-effect-from-4")] 
		public string StartEffectFrom4;

		[Signal("start-effect-to-1")] 
		public string StartEffectTo1;

		[Signal("start-effect-to-2")] 
		public string StartEffectTo2;

		[Signal("start-effect-to-3")] 
		public string StartEffectTo3;

		[Signal("start-effect-to-4")] 
		public string StartEffectTo4;

		/// <summary>
		/// 连接结束条件 1
		/// </summary>
		[Signal("terminate-cond-1")] 
		public TerminateCond TerminateCond1;

		/// <summary>
		/// 连接结束条件 2
		/// </summary>
		[Signal("terminate-cond-2")] 
		public TerminateCond TerminateCond2;

		/// <summary>
		/// 连接结束和或状态
		/// </summary>
		[Signal("terminate-op")]
		public bool TerminateOp;

		/// <summary>
		/// 结束值 1
		/// </summary>
		[Signal("terminate-value-1")] 
		public int TerminateValue1;

		/// <summary>
		/// 结束值 2
		/// </summary>
		[Signal("terminate-value-2")] 
		public int TerminateValue2;

		/// <summary>
		/// 结束标记 1
		/// </summary>
		[Signal("terminate-flag-1")]
		public Flag TerminateFlag1;

		/// <summary>
		/// 结束标记 2
		/// </summary>
		[Signal("terminate-flag-2")] 
		public Flag TerminateFlag2;
	}
}