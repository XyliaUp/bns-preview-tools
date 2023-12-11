using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class NpcBrainParameters : ModelElement
{
	public string Alias;

	public Ref<Script> Script;

	public Ref<Social> HateShareSocial;
	public Ref<Social> HideOffSocial;
	public Ref<Social> HideOnSocial;

	[Name("hide-on-init")]
	public bool HideOnInit;

	[Name("hide-on-return-start")]
	public bool HideOnReturnStart;

	[Name("hide-type")]
	public HideType HideType;

	[Name("despawn-social")]
	public Ref<Social> DespawnSocial;

	[Repeat(value: 2)]
	public Direction[] SetupPosition;

	[Repeat(value: 2)]
	public Direction[] SetupDir;

	[Name("spawn-social")]
	public Ref<Social> SpawnSocial;

	[Name("spawn-skill3")]
	public string SpawnSkill3;

	[Name("return-start-social")]
	public Ref<Social> ReturnStartSocial;

	[Name("return-end-social")]
	public Ref<Social> ReturnEndSocial;

	[Name("monster-talkable")]
	public bool MonsterTalkable;

	[Name("period-bind-cur-target-sec")]
	public int PeriodBindCurTargetSec;

	[Repeat(3)]
	public sbyte[] Bleeding1;

	[Repeat(3)]
	public int[] Reach;


	[Repeat(10)]
	public Ref<CombatSequence>[] CombatSequence;

	/// <summary>
	/// 可触发 invoke-effect Event的效果类型
	/// </summary>
	[Name("event-invoke-effect-attribute") , Repeat(4)]
	public EffectAttributeSeq[] EventInvokeEffectAttribute;

	[Name("event-invoke-effect-attribute-value")]
	public long EventInvokeEffectAttributeValue;

	[Name("event-invoke-effect-attribute-value-2")]
	public long EventInvokeEffectAttributeValue2;

	[Name("event-invoke-effect-attribute-value-3")]
	public long EventInvokeEffectAttributeValue3;



	/// <summary>
	/// 可触发 invoked-effect Event的效果类型
	/// </summary>
	[Name("event-invoked-effect-attribute"), Repeat(4)]
	public EffectAttributeSeq[] EventInvokedEffectAttribute;

	[Name("event-invoked-effect-attribute-value")]
	public long EventInvokedEffectAttributeValue;

	[Name("event-invoked-effect-attribute-value-2")]
	public long EventInvokedEffectAttributeValue2;

	[Name("event-invoked-effect-attribute-value-3")]
	public long EventInvokedEffectAttributeValue3;



	[Repeat(7)]
	public short[] MaxAttackDistance;

	[Repeat(7)]
	public short[] MaxAttackHeight;

	[Repeat(7)]
	public short[] MinAttackDistance;


	[Repeat(7)]
	public short[] MaxCloseInDistance;

	[Repeat(7)]
	public short[] MinCloseInDistance;




	[Repeat(20), Obsolete]
	public Ref<Skill>[] Skill;

	[Repeat(20)]
	public Ref<Skill3>[] Skill3;

	[Repeat(12)]
	public Ref<Social>[] Social;

	[Repeat(4)]
	public SoulNpcSkillCategory[] SoulNpcSkillCategory;


	[Repeat(3)]
	public StanceSeq[] Stance;

	[Repeat(3)]
	public Ref<Effect>[] StanceEffect;

	public TargetingType TargetingType;

	public TransitType TransitType;


	[Repeat(8)]
	public short[] Timer;

	[Name("drop-weapon-cs")]
	public string DropWeaponCs;

	[Repeat(3)]
	public Ref<Item>[] Weapon;
}



/// <summary>
/// 隐藏类型
/// </summary>
public enum HideType
{
	None,

	/// <summary>
	/// 地下, 无法攻击
	/// </summary>
	Burrow,

	/// <summary>
	/// 只是无法锁定, 还是可以攻击到
	/// </summary>
	Hide,
}

public enum TargetingType
{
	None,

	[Name("hate-top")]
	HateTop,

	[Name("hate-tour")]
	HateTour,
}

/// <summary>
/// 战斗序列转移类型
/// </summary>
public enum TransitType
{
	None,

	/// <summary>
	/// Event转移
	/// </summary>
	[Name("by-event")]
	ByEvent,

	/// <summary>
	/// 定时器转移
	/// </summary>
	[Name("by-timer")]
	ByTimer,
}