using System.ComponentModel;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.CombatSequenceData.Enums;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class CombatSequence : ModelElement
{
	public string Alias { get; set; }

	public bool HideSeqStart { get; set; }

	public sbyte SeqStartSocial { get; set; }

	public bool ScriptDebugEnable { get; set; }

	[Repeat(2)]
	public TransitCond[] TransitCond { get; set; }

	[Repeat(2)]
	public TransitAction[] TransitAction { get; set; }

	[Repeat(2)]
	public sbyte[] TransitSocial { get; set; }


	public List<Sequence.NormalSequence> Normal { get; set; }

	public List<Sequence.SpecialSequence> Special { get; set; }


	#region Element
	public abstract class Action : ModelElement
	{
		public bool Combo{ get; set; }

		public Msec Duration{ get; set; }

		/// <summary>
		/// 特殊事件步骤
		/// 只有 special子节点 可以使用
		/// </summary>
		[Name("event-step")]
		public sbyte EventStep{ get; set; }

		public sbyte Repeat{ get; set; }

		[Name("immune-breaker-disable")]
		public bool ImmuneBreakerDisable{ get; set; }

		/// <summary>
		/// 仅当上级节点是 Select 时才有意义
		/// </summary>
		public sbyte Prob{ get; set; }




		#region Sub
		//public abstract class SkillBase : Action
		//{
		//	[Obsolete]
		//	public Skill Skill{ get; set; }

		//	public Ref<Skill3> Skill3{ get; set; }


		//	public Script_obj Target{ get; set; }

		//	[Name("except-condition")]
		//	public Flag ExceptCondition{ get; set; }

		//	[Name("except-link-laser")]
		//	public bool ExceptLinkLaser{ get; set; }

		//	/// <summary>
		//	/// 排除召唤兽对象
		//	/// </summary>
		//	[Name("except-summoned")]
		//	public bool ExceptSummoned{ get; set; }

		//	[Name("exclude-summoned")]
		//	public bool ExcludeSummoned{ get; set; }

		//	/// <summary>
		//	/// 排除主仇恨对象
		//	/// </summary>
		//	[Name("except-top-hate")]
		//	public bool ExceptTopHate{ get; set; }

		//	[Name("replace-top-hate")]
		//	public bool ReplaceTopHate{ get; set; }
		//}




		//public sealed class BossLinkLaserAttack : SkillBase
		//{
		//	[Name("is-target-skill-gather")]
		//	public bool IsTargetSkillGather{ get; set; }

		//	[Name("normal-bounce")]
		//	public bool NormalBounce{ get; set; }

		//	[Name("target-from")]
		//	public Script_obj TargetFrom{ get; set; }

		//	[Name("target-to")]
		//	public Script_obj TargetTo{ get; set; }

		//	[Name("start-effect-from"), Repeat(4)]
		//	public Ref<Effect>[] StartEffectFrom{ get; set; }

		//	[Name("start-effect-to"), Repeat(4)]
		//	public Ref<Effect>[] StartEffectTo{ get; set; }

		//	[Name("terminate-cond"), Repeat(2)]
		//	public TerminateCond[] TerminateCond{ get; set; }

		//	/// <summary>
		//	/// 连接结束和或状态
		//	/// </summary>
		//	[Name("terminate-op")]
		//	public bool TerminateOp{ get; set; }


		//	[Name("terminate-value"), Repeat(2)]
		//	public int[] TerminateValue{ get; set; }

		//	[Name("terminate-flag"), Repeat(2)]
		//	public Flag[] TerminateFlag{ get; set; }
		//}

		//public sealed class BossMultigroundAttack : SkillBase
		//{
		//	[Name("ground-pattern-1"), Repeat(5)]
		//	public BossGroundAttackTargetPattern GroundPattern1{ get; set; }

		//	[Name("origin-pos")]
		//	public string OriginPos{ get; set; }
		//}

		//public sealed class BossRepeaterAttack : SkillBase
		//{

		//}

		//public sealed class BossSimultaneousCasterAttack : SkillBase
		//{

		//}

		//public sealed class BossSimultaneousGroundAttack : SkillBase
		//{

		//}

		//public sealed class BossGpSelectAttack : Action
		//{
		//	public Script_obj Target{ get; set; }

		//	[Repeat(10)]
		//	public Ref<Skill3>[] Skill3{ get; set; }
		//}

		///// <summary>
		///// 前冲  需要智力为boss
		///// </summary>
		//public sealed class BossRushAttack : Action
		//{
		//	[Name("arrived-effect")]
		//	public Ref<Effect> ArrivedEffect{ get; set; }

		//	[Name("blocked-effect")]
		//	public Ref<Effect> BlockedEffect{ get; set; }

		//	[Name("pass-through")]
		//	public bool PassThrough{ get; set; }

		//	public Script_obj Target{ get; set; }

		//	[Name("target-area")]
		//	public Ref<ZoneArea> TargetArea{ get; set; }

		//	public Velocity Speed{ get; set; }

		//	public Ref<Skill> Skill{ get; set; }

		//	public Ref<Skill3> Skill3{ get; set; }
		//}

		//public sealed class BossSpSelectAttack : Action
		//{
		//	[Repeat(3)]
		//	public sbyte[] Sp{ get; set; }

		//	[Repeat(3)]
		//	public Ref<Skill>[] Skill{ get; set; }

		//	[Repeat(3)]
		//	public Ref<Skill3>[] Skill3{ get; set; }
		//}

		//public sealed class ChangeSet : Action
		//{
		//	public sbyte Stance{ get; set; }

		//	[Name("stance-effect-1")]
		//	public sbyte StanceEffect1{ get; set; }

		//	public sbyte Weapon{ get; set; }
		//}

		//public sealed class CombatMove : Action
		//{
		//	[Name("move-msec")]
		//	public Msec MoveMsec{ get; set; }

		//	[Name("move-type")]
		//	public MoveType MoveType{ get; set; }

		//	[Name("range-within")]
		//	public short RangeWithin{ get; set; }
		//}

		//public sealed class DetectCreature : Action
		//{
		//	public short Height{ get; set; }

		//	public short Radius{ get; set; }
		//}

		//public sealed class DoIndexedSocial : Action
		//{
		//	public sbyte Social{ get; set; }
		//}

		//public sealed class DoSocial : SkillBase
		//{
		//	public Ref<Social> Social{ get; set; }
		//}

		///// <summary>
		///// 采集特殊目标
		///// 选择的目标会被作为 special-target
		///// 可能的错误 
		///// 此类型只能用于特殊序列 、序列内只能有一个此类型活动
		///// 只能用于boss-npc
		///// </summary>
		//public sealed class GatherTargets : SkillBase
		//{
		//	public Flag Condition{ get; set; }

		//	[Name("gather-count")]
		//	public sbyte GatherCount{ get; set; }

		//	[Name("gather-rule")]
		//	public GatherRule GatherRule{ get; set; }
		//}

		//public sealed class Select : Action
		//{
		//	/// <summary>
		//	/// 进入概率
		//	/// </summary>
		//	[Name("enter-prob")]
		//	public sbyte EnterProb = 100{ get; set; }

		//	public List<Action> Actions { get{ get; set; } set{ get; set; } }
		//}

		//public sealed class Stay : Action
		//{
		//	public Script_obj Target{ get; set; }
		//}

		//public sealed class UseGroundSkill : SkillBase
		//{
		//	[Name("target-area")]
		//	public Ref<ZoneArea> TargetArea{ get; set; }

		//	[Name("target-area-ref")]
		//	public int TargetAreaRef{ get; set; }

		//	[Name("target-pos")]
		//	public string TargetPos{ get; set; }
		//}

		//public sealed class UseIndexedSkill : Action
		//{
		//	public int Skill{ get; set; }

		//	public int Skill3{ get; set; }
		//}

		//public sealed class UseSkill : SkillBase
		//{
		//	[Name("target-area")]
		//	public ZoneArea TargetArea{ get; set; }

		//	[Name("multi-skill3"), Repeat(15)]
		//	public Ref<Skill3>[] MultiSkill3{ get; set; }

		//	[Name("multi-target"), Repeat(15)]
		//	public Script_obj[] MultiTarget{ get; set; }
		//}

		//public sealed class UseSoulNpcSkill : Action
		//{

		//}
		#endregion
	}

	public abstract class Sequence : ModelElement
	{
		#region Fields
		public List<Action> Action { get; set; } = new();


		public short ID{ get; set; }

		[Name("attack-limit")]
		public sbyte AttackLimit{ get; set; }

		[Name("range-attack-limit")]
		public sbyte RangeAttackLimit{ get; set; }

		[Name("combat-distance")]
		public sbyte CombatDistance{ get; set; }

		[DefaultValue(1)]
		[Name("max-count")]
		public short MaxCount{ get; set; }

		[Name("script-debug-enable")]
		public bool ScriptDebugEnable{ get; set; }

		/// <summary>
		/// 普通序列过程中调用特殊序列
		/// 
		/// Special1、Special2存在默认值, 当定义 Special1 后 Special2 会默认为空
		/// //if (this.Special[0] != 0) this.Special[1] = 0{ get; set; }
		/// </summary>
		[Name("special"), Repeat(5)]
		public sbyte[] Special{ get; set; }
		#endregion

		#region Sub
		public sealed class NormalSequence : Sequence
		{
			[Name("change-min-msec")]
			public sbyte ChangeMinMsec{ get; set; }

			[Name("except-summoned")]
			public bool ExceptSummoned{ get; set; }

			[Name("hide-seq-start")]
			public bool HideSeqStart{ get; set; }

			[Name("seq-start-social")]
			public sbyte SeqStartSocial{ get; set; }

			[Name("setup-position")]
			public bool SetupPosition{ get; set; }

			[Name("setup-dir")]
			public Direction SetupDir{ get; set; }
		}

		public sealed class SpecialSequence : Sequence
		{
			[Name("condition"), Repeat(2)]
			public Flag Condition{ get; set; }

			/// <summary>
			/// 条件对象 默认是 event:npc-combat-action:target
			/// </summary>
			[Name("condition-subscription"), Repeat(2)]
			public Script_obj ConditionSubscription{ get; set; }

			[Name("start-delay")]
			public int StartDelay{ get; set; }

			[Name("min-delay")]
			public int MinDelay{ get; set; }
		}
		#endregion
	}
	#endregion
}