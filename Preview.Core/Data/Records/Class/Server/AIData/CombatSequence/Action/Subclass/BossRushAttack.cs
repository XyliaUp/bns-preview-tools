using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 前冲  需要智力为boss
	/// </summary>
	[Signal("boss-rush-attack")]
	public sealed class BossRushAttack : IAction
	{
		/// <summary>
		/// 到达后效果
		/// </summary>
		[Signal("arrived-effect")]
		public string ArrivedEffect;

		/// <summary>
		/// 被打断后效果
		/// </summary>
		[Signal("blocked-effect")]
		public string BlockedEffect;

		[Signal("pass-through")]
		public bool PassThrough;

		/// <summary>
		/// 冲向目标
		/// </summary>
		public Script_obj Target;
	
		/// <summary>
		/// 冲向区域
		/// </summary>
		[Signal("target-area")]
		public string TargetArea;


		public short Speed;

		[Obsolete]
		public string Skill;

		public string Skill3;
	}
}