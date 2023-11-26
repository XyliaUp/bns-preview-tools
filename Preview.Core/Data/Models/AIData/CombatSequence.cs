using System.ComponentModel;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Models.CombatSequenceData.Enums;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class CombatSequence : Record
{
	public string Alias;

	public bool HideSeqStart;

	public sbyte SeqStartSocial;

	public bool ScriptDebugEnable;

	[Repeat(2)]
	public TransitCond[] TransitCond;

	[Repeat(2)]
	public TransitAction[] TransitAction;

	[Repeat(2)]
	public sbyte[] TransitSocial;


	public List<Sequence.NormalSequence> Normal { get; set; }

	public List<Sequence.SpecialSequence> Special { get; set; }


	#region Element
	public abstract class Action : Record
	{
		public bool Combo;

		public Msec Duration;

		/// <summary>
		/// 特殊事件步骤
		/// 只有 special子节点 可以使用
		/// </summary>
		[Name("event-step")]
		public sbyte EventStep;

		public sbyte Repeat;

		[Name("immune-breaker-disable")]
		public bool ImmuneBreakerDisable;

		/// <summary>
		/// 仅当上级节点是 Select 时才有意义
		/// </summary>
		public sbyte Prob;




		#region Sub
		public abstract class SkillBase : Action
		{
			[Obsolete]
			public Skill Skill;

			public Ref<Skill3> Skill3;


			public Script_obj Target;

			[Name("except-condition")]
			public Flag ExceptCondition;

			[Name("except-link-laser")]
			public bool ExceptLinkLaser;

			/// <summary>
			/// 排除召唤兽对象
			/// </summary>
			[Name("except-summoned")]
			public bool ExceptSummoned;

			[Name("exclude-summoned")]
			public bool ExcludeSummoned;

			/// <summary>
			/// 排除主仇恨对象
			/// </summary>
			[Name("except-top-hate")]
			public bool ExceptTopHate;

			[Name("replace-top-hate")]
			public bool ReplaceTopHate;
		}




		public sealed class BossLinkLaserAttack : SkillBase
		{
			[Name("is-target-skill-gather")]
			public bool IsTargetSkillGather;

			[Name("normal-bounce")]
			public bool NormalBounce;

			[Name("target-from")]
			public Script_obj TargetFrom;

			[Name("target-to")]
			public Script_obj TargetTo;

			[Name("start-effect-from"), Repeat(4)]
			public Ref<Effect>[] StartEffectFrom;

			[Name("start-effect-to"), Repeat(4)]
			public Ref<Effect>[] StartEffectTo;

			[Name("terminate-cond"), Repeat(2)]
			public TerminateCond[] TerminateCond;

			/// <summary>
			/// 连接结束和或状态
			/// </summary>
			[Name("terminate-op")]
			public bool TerminateOp;


			[Name("terminate-value"), Repeat(2)]
			public int[] TerminateValue;

			[Name("terminate-flag"), Repeat(2)]
			public Flag[] TerminateFlag;
		}

		public sealed class BossMultigroundAttack : SkillBase
		{
			[Name("ground-pattern-1"), Repeat(5)]
			public BossGroundAttackTargetPattern GroundPattern1;

			[Name("origin-pos")]
			public string OriginPos;
		}

		public sealed class BossRepeaterAttack : SkillBase
		{

		}

		public sealed class BossSimultaneousCasterAttack : SkillBase
		{

		}

		public sealed class BossSimultaneousGroundAttack : SkillBase
		{

		}

		public sealed class BossGpSelectAttack : Action
		{
			public Script_obj Target;

			[Repeat(10)]
			public Ref<Skill3>[] Skill3;
		}

		/// <summary>
		/// 前冲  需要智力为boss
		/// </summary>
		public sealed class BossRushAttack : Action
		{
			[Name("arrived-effect")]
			public Ref<Effect> ArrivedEffect;

			[Name("blocked-effect")]
			public Ref<Effect> BlockedEffect;

			[Name("pass-through")]
			public bool PassThrough;

			public Script_obj Target;

			[Name("target-area")]
			public Ref<ZoneArea> TargetArea;

			public Velocity Speed;

			public Ref<Skill> Skill;

			public Ref<Skill3> Skill3;
		}

		public sealed class BossSpSelectAttack : Action
		{
			[Repeat(3)]
			public sbyte[] Sp;

			[Repeat(3)]
			public Ref<Skill>[] Skill;

			[Repeat(3)]
			public Ref<Skill3>[] Skill3;
		}

		public sealed class ChangeSet : Action
		{
			public sbyte Stance;

			[Name("stance-effect-1")]
			public sbyte StanceEffect1;

			public sbyte Weapon;
		}

		public sealed class CombatMove : Action
		{
			[Name("move-msec")]
			public Msec MoveMsec;

			[Name("move-type")]
			public MoveType MoveType;

			[Name("range-within")]
			public short RangeWithin;
		}

		public sealed class DetectCreature : Action
		{
			public short Height;

			public short Radius;
		}

		public sealed class DoIndexedSocial : Action
		{
			public sbyte Social;
		}

		public sealed class DoSocial : SkillBase
		{
			public Ref<Social> Social;
		}

		/// <summary>
		/// 采集特殊目标
		/// 选择的目标会被作为 special-target
		/// 可能的错误 
		/// 此类型只能用于特殊序列 、序列内只能有一个此类型活动
		/// 只能用于boss-npc
		/// </summary>
		public sealed class GatherTargets : SkillBase
		{
			public Flag Condition;

			[Name("gather-count")]
			public sbyte GatherCount;

			[Name("gather-rule")]
			public GatherRule GatherRule;
		}

		public sealed class Select : Action
		{
			/// <summary>
			/// 进入概率
			/// </summary>
			[Name("enter-prob")]
			public sbyte EnterProb = 100;

			public List<Action> Actions { get; set; }
		}

		public sealed class Stay : Action
		{
			public Script_obj Target;
		}

		public sealed class UseGroundSkill : SkillBase
		{
			[Name("target-area")]
			public Ref<ZoneArea> TargetArea;

			[Name("target-area-ref")]
			public int TargetAreaRef;

			[Name("target-pos")]
			public string TargetPos;
		}

		public sealed class UseIndexedSkill : Action
		{
			public int Skill;

			public int Skill3;
		}

		public sealed class UseSkill : SkillBase
		{
			[Name("target-area")]
			public ZoneArea TargetArea;

			[Name("multi-skill3"), Repeat(15)]
			public Ref<Skill3>[] MultiSkill3;

			[Name("multi-target"), Repeat(15)]
			public Script_obj[] MultiTarget;
		}

		public sealed class UseSoulNpcSkill : Action
		{

		}
		#endregion
	}

	public abstract class Sequence : Record
	{
		#region Fields
		public List<Action> Action { get; set; } = new();


		public short ID;

		[Name("attack-limit")]
		public sbyte AttackLimit;

		[Name("range-attack-limit")]
		public sbyte RangeAttackLimit;

		[Name("combat-distance")]
		public sbyte CombatDistance;

		[DefaultValue(1)]
		[Name("max-count")]
		public short MaxCount;

		[Name("script-debug-enable")]
		public bool ScriptDebugEnable;

		/// <summary>
		/// 普通序列过程中调用特殊序列
		/// 
		/// Special1、Special2存在默认值, 当定义 Special1 后 Special2 会默认为空
		/// //if (this.Special[0] != 0) this.Special[1] = 0;
		/// </summary>
		[Name("special"), Repeat(5)]
		public sbyte[] Special;
		#endregion

		#region Sub
		public sealed class NormalSequence : Sequence
		{
			[Name("change-min-msec")]
			public sbyte ChangeMinMsec;

			[Name("except-summoned")]
			public bool ExceptSummoned;

			[Name("hide-seq-start")]
			public bool HideSeqStart;

			[Name("seq-start-social")]
			public sbyte SeqStartSocial;

			[Name("setup-position")]
			public bool SetupPosition;

			[Name("setup-dir")]
			public Direction SetupDir;
		}

		public sealed class SpecialSequence : Sequence
		{
			[Name("condition"), Repeat(2)]
			public Flag Condition;

			/// <summary>
			/// 条件对象 默认是 event:npc-combat-action:target
			/// </summary>
			[Name("condition-subscription"), Repeat(2)]
			public Script_obj ConditionSubscription;

			[Name("start-delay")]
			public int StartDelay;

			[Name("min-delay")]
			public int MinDelay;
		}
		#endregion
	}
	#endregion
}