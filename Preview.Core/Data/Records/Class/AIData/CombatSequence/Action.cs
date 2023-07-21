using System.Xml;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Struct;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.CombatSequenceData.Enums;

namespace Xylia.Preview.Data.Records.Class.AIData.CombatSequence;
public abstract class Action : BaseRecord
{
    public bool Combo;

    public Msec Duration;

    /// <summary>
    /// 特殊事件步骤
    /// 只有 special子节点 可以使用
    /// </summary>
    [Signal("event-step")]
    public byte EventStep;

    public byte Repeat;

    [Signal("immune-breaker-disable")]
    public bool ImmuneBreakerDisable;

    /// <summary>
    /// 仅当上级节点是 Select 时才有意义
    /// </summary>
    public byte Prob;




    #region Sub
    public abstract class SkillBase : Action
    {
        [Obsolete]
        public Skill Skill;

        public Skill3 Skill3;


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

        [Signal("start-effect-from"), Repeat(4)]
        public Effect[] StartEffectFrom;

        [Signal("start-effect-to"), Repeat(4)]
        public Effect[] StartEffectTo;

        [Signal("terminate-cond"), Repeat(2)]
        public TerminateCond[] TerminateCond;

        /// <summary>
        /// 连接结束和或状态
        /// </summary>
        [Signal("terminate-op")]
        public bool TerminateOp;


        [Signal("terminate-value"), Repeat(2)]
        public int[] TerminateValue;

        [Signal("terminate-flag"), Repeat(2)]
        public Flag[] TerminateFlag;
    }

    public sealed class BossMultigroundAttack : SkillBase
    {
        [Signal("ground-pattern-1"), Repeat(5)]
        public BossGroundAttackTargetPattern GroundPattern1;

        [Signal("origin-pos")]
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
        public Skill3[] Skill3;
    }

    /// <summary>
    /// 前冲  需要智力为boss
    /// </summary>
    public sealed class BossRushAttack : Action
    {
        [Signal("arrived-effect")]
        public Effect ArrivedEffect;

        [Signal("blocked-effect")]
        public Effect BlockedEffect;

        [Signal("pass-through")]
        public bool PassThrough;

        public Script_obj Target;

        [Signal("target-area")]
        public ZoneArea TargetArea;

        public short Speed;

        public Skill Skill;

        public Skill3 Skill3;
    }

    public sealed class BossSpSelectAttack : Action
    {
        [Repeat(3)]
        public byte[] Sp;

        [Repeat(3)]
        public Skill[] Skill;

        [Repeat(3)]
        public Skill3[] Skill3;
    }

    public sealed class ChangeSet : Action
    {
        public byte Stance;

        [Signal("stance-effect-1")]
        public byte StanceEffect1;

        public byte Weapon;
    }

    public sealed class CombatMove : Action
    {
        [Signal("move-msec")]
        public Msec MoveMsec;

        [Signal("move-type")]
        public MoveType MoveType;

        [Signal("range-within")]
        public short RangeWithin;
    }

    public sealed class DetectCreature : Action
    {
        public short Height;

        public short Radius;
    }

    public sealed class DoIndexedSocial : Action
    {
        public byte Social;
    }

    public sealed class DoSocial : SkillBase
    {
        public Social Social;
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

        [Signal("gather-count")]
        public byte GatherCount;

        [Signal("gather-rule")]
        public GatherRule GatherRule;
    }

    public sealed class Select : Action
    {
        /// <summary>
        /// 进入概率
        /// </summary>
        [Signal("enter-prob")]
        public byte EnterProb = 100;

        public List<Action> Actions;
    }

    public sealed class Stay : Action
    {
        public Script_obj Target;
    }

    public sealed class UseGroundSkill : SkillBase
    {
        [Signal("target-area")]
        public ZoneArea TargetArea;

        [Signal("target-area-ref")]
        public int TargetAreaRef;

        [Signal("target-pos")]
        public string TargetPos;
    }

    public sealed class UseIndexedSkill : Action
    {
        public int Skill;

        public int Skill3;
    }

    public sealed class UseSkill : SkillBase
    {
        [Signal("target-area")]
        public ZoneArea TargetArea;

        [Signal("multi-skill3"), Repeat(15)]
        public Skill3[] MultiSkill3;

        [Signal("multi-target"), Repeat(15)]
        public Script_obj[] MultiTarget;
    }

    public sealed class UseSoulNpcSkill : Action
    {

    }
    #endregion
}