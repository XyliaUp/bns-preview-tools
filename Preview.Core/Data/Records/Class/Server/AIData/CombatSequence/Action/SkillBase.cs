using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	public abstract class SkillBase : IAction
	{
		[Obsolete]
		public _Skill Skill;

		public Skill Skill3;


		public Script_obj Target;

		[Signal("except-condition")]
		public Flag ExceptCondition;

		[Signal("except-link-laser")]
		public bool ExceptLinkLaser;

		/// <summary>
		/// 排除召唤兽对象
		/// </summary>
		[Signal("except-summoned")]
		public bool ExceptSummoned;

		[Signal("exclude-summoned")]
		public bool ExcludeSummoned;

		/// <summary>
		/// 排除主仇恨对象
		/// </summary>
		[Signal("except-top-hate")]
		public bool ExceptTopHate;

		[Signal("replace-top-hate")]
		public bool ReplaceTopHate;
	}
}