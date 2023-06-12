using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	public sealed class NpcBrainParameters : BaseRecord
	{
		public string Alias;

		public string Script;

		[Signal("hate-share-social")]
		public Social HateShareSocial;

		[Signal("hide-off-social")]
		public Social HideOffSocial;

		[Signal("hide-on-social")]
		public Social HideOnSocial;

		[Signal("hide-on-init")]
		public bool HideOnInit;

		[Signal("hide-on-return-start")]
		public bool HideOnReturnStart;

		[Signal("hide-type")]
		public HideType HideType;

		/// <summary>
		/// 消除时社交
		/// </summary>
		[Signal("despawn-social")]
		public Social DespawnSocial;

		[Signal("setup-position-1")]
		public Direction SetupPosition1;

		[Signal("setup-position-2")]
		public Direction SetupPosition2;

		[Signal("setup-dir-1")]
		public Direction SetupDir1;

		[Signal("setup-dir-2")]
		public Direction SetupDir2;

		/// <summary>
		/// 刷新时社交
		/// </summary>
		[Signal("spawn-social")]
		public Social SpawnSocial;

		[Signal("spawn-skill3")]
		public string SpawnSkill3;

		[Signal("return-start-social")]
		public Social ReturnStartSocial;

		[Signal("return-end-social")]
		public Social ReturnEndSocial;

		[Signal("monster-talkable")]
		public bool MonsterTalkable;

		[Signal("period-bind-cur-target-sec")]
		public int PeriodBindCurTargetSec;

		[Signal("bleeding-1")]
		public byte Bleeding1;

		[Signal("bleeding-2")]
		public byte Bleeding2;

		[Signal("bleeding-3")]
		public byte Bleeding3;

		[Signal("reach-1")]
		public int Reach1;

		[Signal("reach-2")]
		public int Reach2;

		[Signal("reach-3")]
		public int Reach3;


		[Signal("combat-sequence-1")]
		public string CombatSequence1;

		[Signal("combat-sequence-2")]
		public string CombatSequence2;

		[Signal("combat-sequence-3")]
		public string CombatSequence3;

		[Signal("combat-sequence-4")]
		public string CombatSequence4;

		[Signal("combat-sequence-5")]
		public string CombatSequence5;

		[Signal("combat-sequence-6")]
		public string CombatSequence6;

		[Signal("combat-sequence-7")]
		public string CombatSequence7;

		[Signal("combat-sequence-8")]
		public string CombatSequence8;

		[Signal("combat-sequence-9")]
		public string CombatSequence9;

		[Signal("combat-sequence-10")]
		public string CombatSequence10;


		/// <summary>
		/// 可触发 invoke-effect Event的效果类型
		/// </summary>
		[Signal("event-invoke-effect-attribute-1")]
		public EffectAttribute EventInvokeEffectAttribute1;

		[Signal("event-invoke-effect-attribute-2")]
		public EffectAttribute EventInvokeEffectAttribute2;

		[Signal("event-invoke-effect-attribute-3")]
		public EffectAttribute EventInvokeEffectAttribute3;

		[Signal("event-invoke-effect-attribute-4")]
		public EffectAttribute EventInvokeEffectAttribute4;

		[Signal("event-invoke-effect-attribute-value")]
		public long EventInvokeEffectAttributeValue;

		[Signal("event-invoke-effect-attribute-value-2")]
		public long EventInvokeEffectAttributeValue2;

		[Signal("event-invoke-effect-attribute-value-3")]
		public long EventInvokeEffectAttributeValue3;



		/// <summary>
		/// 可触发 invoked-effect Event的效果类型
		/// </summary>
		[Signal("event-invoked-effect-attribute-1")]
		public EffectAttribute EventInvokedEffectAttribute1;

		[Signal("event-invoked-effect-attribute-2")]
		public EffectAttribute EventInvokedEffectAttribute2;

		[Signal("event-invoked-effect-attribute-3")]
		public EffectAttribute EventInvokedEffectAttribute3;

		[Signal("event-invoked-effect-attribute-4")]
		public EffectAttribute EventInvokedEffectAttribute4;

		[Signal("event-invoked-effect-attribute-value")]
		public long EventInvokedEffectAttributeValue;

		[Signal("event-invoked-effect-attribute-value-2")]
		public long EventInvokedEffectAttributeValue2;

		[Signal("event-invoked-effect-attribute-value-3")]
		public long EventInvokedEffectAttributeValue3;





		[Signal("max-attack-distance-1")] 
		public short MaxAttackDistance1;
		[Signal("max-attack-distance-2")] 
		public short MaxAttackDistance2;
		[Signal("max-attack-distance-3")] 
		public short MaxAttackDistance3;
		[Signal("max-attack-distance-4")] 
		public short MaxAttackDistance4;
		[Signal("max-attack-distance-5")] 
		public short MaxAttackDistance5;
		[Signal("max-attack-distance-6")] 
		public short MaxAttackDistance6;
		[Signal("max-attack-distance-7")] 
		public short MaxAttackDistance7;


		[Signal("max-attack-height-1")] 
		public short MaxAttackHeight1;
		[Signal("max-attack-height-2")] 
		public short MaxAttackHeight2;
		[Signal("max-attack-height-3")] 
		public short MaxAttackHeight3;
		[Signal("max-attack-height-4")] 
		public short MaxAttackHeight4;
		[Signal("max-attack-height-5")] 
		public short MaxAttackHeight5;
		[Signal("max-attack-height-6")] 
		public short MaxAttackHeight6;
		[Signal("max-attack-height-7")] 
		public short MaxAttackHeight7;

		[Signal("min-attack-distance-1")] 
		public short MinAttackDistance1;
		[Signal("min-attack-distance-2")] 
		public short MinAttackDistance2;
		[Signal("min-attack-distance-3")] 
		public short MinAttackDistance3;
		[Signal("min-attack-distance-4")] 
		public short MinAttackDistance4;
		[Signal("min-attack-distance-5")] 
		public short MinAttackDistance5;
		[Signal("min-attack-distance-6")] 
		public short MinAttackDistance6;
		[Signal("min-attack-distance-7")] 
		public short MinAttackDistance7;



		[Signal("max-close-in-distance-1")] 
		public short MaxCloseInDistance1;
		[Signal("max-close-in-distance-2")] 
		public short MaxCloseInDistance2;
		[Signal("max-close-in-distance-3")]
		public short MaxCloseInDistance3;
		[Signal("max-close-in-distance-4")] 
		public short MaxCloseInDistance4;
		[Signal("max-close-in-distance-5")]
		public short MaxCloseInDistance5;
		[Signal("max-close-in-distance-6")] 
		public short MaxCloseInDistance6;
		[Signal("max-close-in-distance-7")] 
		public short MaxCloseInDistance7;

		[Signal("min-close-in-distance-1")]
		public short MinCloseInDistance1;
		[Signal("min-close-in-distance-2")] 
		public short MinCloseInDistance2;
		[Signal("min-close-in-distance-3")] 
		public short MinCloseInDistance3;
		[Signal("min-close-in-distance-4")] 
		public short MinCloseInDistance4;
		[Signal("min-close-in-distance-5")] 
		public short MinCloseInDistance5;
		[Signal("min-close-in-distance-6")]
		public short MinCloseInDistance6;
		[Signal("min-close-in-distance-7")] 
		public short MinCloseInDistance7;


		[Obsolete] [Signal("skill-1")] public _Skill Skill_1;
		[Obsolete] [Signal("skill-2")] public _Skill Skill_2;
		[Obsolete] [Signal("skill-3")] public _Skill Skill_3;
		[Obsolete] [Signal("skill-4")] public _Skill Skill_4;
		[Obsolete] [Signal("skill-5")] public _Skill Skill_5;
		[Obsolete] [Signal("skill-6")] public _Skill Skill_6;
		[Obsolete] [Signal("skill-7")] public _Skill Skill_7;
		[Obsolete] [Signal("skill-8")] public _Skill Skill_8;
		[Obsolete] [Signal("skill-9")] public _Skill Skill_9;
		[Obsolete] [Signal("skill-10")] public _Skill Skill_10;
		[Obsolete] [Signal("skill-11")] public _Skill Skill_11;
		[Obsolete] [Signal("skill-12")] public _Skill Skill_12;
		[Obsolete] [Signal("skill-13")] public _Skill Skill_13;
		[Obsolete] [Signal("skill-14")] public _Skill Skill_14;
		[Obsolete] [Signal("skill-15")] public _Skill Skill_15;
		[Obsolete] [Signal("skill-16")] public _Skill Skill_16;
		[Obsolete] [Signal("skill-17")] public _Skill Skill_17;
		[Obsolete] [Signal("skill-18")] public _Skill Skill_18;
		[Obsolete] [Signal("skill-19")] public _Skill Skill_19;
		[Obsolete] [Signal("skill-20")] public _Skill Skill_20;

		[Signal("skill3-1")] public Skill Skill3_1;
		[Signal("skill3-2")] public Skill Skill3_2;
		[Signal("skill3-3")] public Skill Skill3_3;
		[Signal("skill3-4")] public Skill Skill3_4;
		[Signal("skill3-5")] public Skill Skill3_5;
		[Signal("skill3-6")] public Skill Skill3_6;
		[Signal("skill3-7")] public Skill Skill3_7;
		[Signal("skill3-8")] public Skill Skill3_8;
		[Signal("skill3-9")] public Skill Skill3_9;
		[Signal("skill3-10")] public Skill Skill3_10;
		[Signal("skill3-11")] public Skill Skill3_11;
		[Signal("skill3-12")] public Skill Skill3_12;
		[Signal("skill3-13")] public Skill Skill3_13;
		[Signal("skill3-14")] public Skill Skill3_14;
		[Signal("skill3-15")] public Skill Skill3_15;
		[Signal("skill3-16")] public Skill Skill3_16;
		[Signal("skill3-17")] public Skill Skill3_17;
		[Signal("skill3-18")] public Skill Skill3_18;
		[Signal("skill3-19")] public Skill Skill3_19;
		[Signal("skill3-20")] public Skill Skill3_20;

		[Signal("social-1")] public string Social1;
		[Signal("social-2")] public string Social2;
		[Signal("social-3")] public string Social3;
		[Signal("social-4")] public string Social4;
		[Signal("social-5")] public string Social5;
		[Signal("social-6")] public string Social6;
		[Signal("social-7")] public string Social7;
		[Signal("social-8")] public string Social8;
		[Signal("social-9")] public string Social9;
		[Signal("social-10")] public string Social10;
		[Signal("social-11")] public string Social11;
		[Signal("social-12")] public string Social12;


		[Signal("soul-npc-skill-category-1")]
		public SoulNpcSkillCategory SoulNpcSkillCategory1;

		[Signal("soul-npc-skill-category-2")]
		public SoulNpcSkillCategory SoulNpcSkillCategory2;

		[Signal("soul-npc-skill-category-3")]
		public SoulNpcSkillCategory SoulNpcSkillCategory3;

		[Signal("soul-npc-skill-category-4")]
		public SoulNpcSkillCategory SoulNpcSkillCategory4;


		[Signal("stance-1")]
		public StanceSeq Stance1;

		[Signal("stance-2")]
		public StanceSeq Stance2;

		[Signal("stance-3")]
		public StanceSeq Stance3;

		[Signal("stance-effect-1")]
		public string StanceEffect1;

		[Signal("stance-effect-2")]
		public string StanceEffect2;

		[Signal("stance-effect-3")]
		public string StanceEffect3;


		[Signal("targeting-type")]
		public TargetingType TargetingType;

		[Signal("transit-type")]
		public TransitType TransitType;


		[Signal("timer-1")]
		public short Timer1;
		[Signal("timer-2")]
		public short Timer2;
		[Signal("timer-3")]
		public short Timer3;
		[Signal("timer-4")]
		public short Timer4;
		[Signal("timer-5")]
		public short Timer5;
		[Signal("timer-6")]
		public short Timer6;
		[Signal("timer-7")]
		public short Timer7;
		[Signal("timer-8")]
		public short Timer8;


		[Signal("drop-weapon-cs")]
		public string DropWeaponCs;

		[Signal("weapon-1")]
		public string Weapon1;
		[Signal("weapon-2")]
		public string Weapon2;
		[Signal("weapon-3")]
		public string Weapon3;
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

		[Signal("hate-top")]
		HateTop,

		[Signal("hate-tour")]
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
		[Signal("by-event")]
		ByEvent,

		/// <summary>
		/// 定时器转移
		/// </summary>
		[Signal("by-timer")]
		ByTimer,
	}
}