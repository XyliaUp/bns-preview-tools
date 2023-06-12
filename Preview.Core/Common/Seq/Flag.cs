using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	public enum Flag
	{
		None,
		Stun,
		Airdash,
		Knockback,

		[Signal("bind-phantom")]
		BindPhantom,
		Link,
		Detection,

		[Signal("internal-injury")]
		InternalInjury,

		[Signal("immediately-exec")]
		ImmediatelyExec,
		Concentration,

		[Signal("infinity-shot")]
		InfinityShot,
		Down,
		Swoon,
		Defence,
		Kneel,
		Provocation,

		[Signal("midair-1")]
		Midair1,

		[Signal("midair-2")]
		Midair2,

		[Signal("midair-3")]
		Midair3,
		Frostbite,

		[Signal("magnetic-seal")]
		MagneticSeal,

		[Signal("fast-freezing")]
		FastFreezing,
		Rupture,
		Impregnability,
		Prickblood,

		[Signal("wildfire-1")]
		Wildfire1,

		[Signal("wildfire-2")]
		Wildfire2,

		[Signal("wildfire-3")]
		Wildfire3,

		Hide,
		Burrow,
		Smokescreen,
		Embers,
		Poison,

		[Signal("applied-poison")]
		AppliedPoison,

		[Signal("spider-web")]
		SpiderWeb,

		[Signal("time-bomb")]
		TimeBomb,

		[Signal("defence-block")]
		DefenceBlock,

		[Signal("dash-block")]
		DashBlock,

		Dexterity,

		[Signal("soulblade-1")]
		Soulblade1,

		[Signal("soulblade-2")]
		Soulblade2,

		[Signal("soulblade-3")]
		Soulblade3,
		Joint,
		Flydragon,
		Landdragon,

		[Signal("silverweb-1")]
		Silverweb1,

		[Signal("silverweb-2")]
		Silverweb2,
		Justguard,

		[Signal("force-flag-1")]
		ForceFlag1,

		[Signal("force-flag-2")]
		ForceFlag2,

		[Signal("force-flag-3")]
		ForceFlag3,

		Catchshield,
		Poundshort,
		Axechopshort,
		Swingstrikeshort,
		Chilblain,
		Frontdown,
		Bleeding,

		[Signal("npc-state-1")]
		NpcState1,

		[Signal("npc-state-2")]
		NpcState2,

		[Signal("npc-state-3")]
		NpcState3,

		[Signal("npc-state-4")]
		NpcState4,

		[Signal("npc-state-5")]
		NpcState5,

		[Signal("npc-state-6")]
		NpcState6,

		[Signal("npc-state-7")]
		NpcState7,

		[Signal("npc-state-8")]
		NpcState8,

		[Signal("hardwall-break")]
		HardwallBreak,

		[Signal("react-link")]
		ReactLink,

		Counter,
		Shadow,
		Shuriken,
		Instantkick,
		Hyperkick,
		Saver,
		HeartStab,
		LeafDodge,
		DragonKick,
		BurstBlow,
		Blood,
		BloodFullStack,
		Poison2,
		Poison2FullStack,
		TankingDrain,
		EmberFullStack,
		PoisonFullStack,
		FireBit,
		FireBitFullStack,
		IceBit,
		Hole,
		HoleFullStack,
		Bubble,
		Swallow,
		FireBit2,
		IceBit2,
		IceBitFullStack,
		FireWall1,
		FireWall2,
		FireWall3,
		FireWall4,
		HighKick,
		Abdomen,
		ShortBurst,
		MartialFullHit,
		Smash,
		Hole1,
		Hole2,
		Hole3,
		TargetHole1,
		TargetHole2,
		TargetHole3,
		Hornet,
		MorningGlory,

		[Signal("cannot-resurrect")]
		CannotResurrect,

		[Signal("enable-guild-battle-field")]
		EnableGuildBattleField,

		[Signal("Immune-link")]
		ImmuneLink,

		GhostX0,
		GhostX1,
		GhostX2,
		GhostX3,
		Thornbus,
		BloodBurst,
		AdventMark1,
		AdventMark2,
		HyperMove,
		Talisman,
		Spector01,
		Spector02,
		Spector03,
		Phantomsoul,
		Bluesky,
		EgoAutoParry,
		Pierce,
		Burn,
		AttackGlide,
		SecondGaugeStopped,
		BigBossJump,

		[Signal("bigbossstate-1")]
		Bigbossstate1,

		[Signal("JobSkillFlag-1")]
		JobSkillFlag1,

		[Signal("JobSkillFlag-2")]
		JobSkillFlag2,

		[Signal("JobSkillFlag-3")]
		JobSkillFlag3,

		[Signal("JobSkillFlag-4")]
		JobSkillFlag4,

		[Signal("JobSkillFlag-5")]
		JobSkillFlag5,

		[Signal("JobSkillFlag-6")]
		JobSkillFlag6,

		[Signal("JobSkillFlag-7")]
		JobSkillFlag7,

		[Signal("JobSkillFlag-8")]
		JobSkillFlag8,

		[Signal("JobSkillFlag-9")]
		JobSkillFlag9,

		[Signal("JobSkillFlag-10")]
		JobSkillFlag10,

		[Signal("JobSkillFlag-11")]
		JobSkillFlag11,

		[Signal("JobSkillFlag-12")]
		JobSkillFlag12,

		[Signal("JobSkillFlag-13")]
		JobSkillFlag13,

		[Signal("JobSkillFlag-14")]
		JobSkillFlag14,

		[Signal("JobSkillFlag-15")]
		JobSkillFlag15,

		[Signal("JobSkillFlag-16")]
		JobSkillFlag16,

		[Signal("JobSkillFlag-17")]
		JobSkillFlag17,

		[Signal("JobSkillFlag-18")]
		JobSkillFlag18,

		[Signal("JobSkillFlag-19")]
		JobSkillFlag19,

		[Signal("JobSkillFlag-20")]
		JobSkillFlag20,

		[Signal("JobSkillFlag-21")]
		JobSkillFlag21,

		[Signal("JobSkillFlag-22")]
		JobSkillFlag22,

		[Signal("JobSkillFlag-23")]
		JobSkillFlag23,

		[Signal("JobSkillFlag-24")]
		JobSkillFlag24,

		[Signal("JobSkillFlag-25")]
		JobSkillFlag25,

		[Signal("JobSkillFlag-26")]
		JobSkillFlag26,

		[Signal("JobSkillFlag-27")]
		JobSkillFlag27,

		[Signal("JobSkillFlag-28")]
		JobSkillFlag28,

		[Signal("JobSkillFlag-29")]
		JobSkillFlag29,

		[Signal("JobSkillFlag-30")]
		JobSkillFlag30,

		[Signal("JobSkillFlag-31")]
		JobSkillFlag31,

		[Signal("JobSkillFlag-32")]
		JobSkillFlag32,

		[Signal("JobSkillFlag-33")]
		JobSkillFlag33,

		[Signal("JobSkillFlag-34")]
		JobSkillFlag34,

		[Signal("JobSkillFlag-35")]
		JobSkillFlag35,

		[Signal("JobSkillFlag-36")]
		JobSkillFlag36,

		[Signal("JobSkillFlag-37")]
		JobSkillFlag37,

		[Signal("JobSkillFlag-38")]
		JobSkillFlag38,

		[Signal("JobSkillFlag-39")]
		JobSkillFlag39,

		[Signal("JobSkillFlag-40")]
		JobSkillFlag40,

		[Signal("JobSkillFlag-41")]
		JobSkillFlag41,

		[Signal("JobSkillFlag-42")]
		JobSkillFlag42,

		[Signal("JobSkillFlag-43")]
		JobSkillFlag43,

		[Signal("JobSkillFlag-44")]
		JobSkillFlag44,

		[Signal("JobSkillFlag-45")]
		JobSkillFlag45,

		[Signal("JobSkillFlag-46")]
		JobSkillFlag46,

		[Signal("npc-skill-1")]
		NpcSkill1,

		[Signal("npc-skill-2")]
		NpcSkill2,

		[Signal("npc-skill-3")]
		NpcSkill3,

		[Signal("npc-skill-4")]
		NpcSkill4,

		[Signal("npc-skill-5")]
		NpcSkill5,

		[Signal("npc-skill-6")]
		NpcSkill6,

		[Signal("npc-skill-7")]
		NpcSkill7,

		[Signal("npc-skill-8")]
		NpcSkill8,

		[Signal("npc-skill-9")]
		NpcSkill9,

		[Signal("npc-skill-10")]
		NpcSkill10,

		[Signal("npc-skill-11")]
		NpcSkill11,

		[Signal("npc-skill-12")]
		NpcSkill12,

		[Signal("stun-miss")]
		StunMiss,

		[Signal("down-miss")]
		DownMiss,
		Struggle,
		Boutiquefree,

		[Signal("tumbling-block")]
		TumblingBlock,
		PeaceArea,

		[Signal("perfect-dodged")]
		PerfectDodged,

		[Signal("kneel-miss")]
		KneelMiss,

		[Signal("glyph-1")]
		Glyph1,

		[Signal("glyph-2")]
		Glyph2,

		[Signal("glyph-3")]
		Glyph3,

		[Signal("glyph-4")]
		Glyph4,

		[Signal("glyph-5")]
		Glyph5,

		[Signal("glyph-6")]
		Glyph6,

		[Signal("glyph-7")]
		Glyph7,

		[Signal("glyph-8")]
		Glyph8,

		[Signal("glyph-9")]
		Glyph9,

		[Signal("glyph-10")]
		Glyph10,

		[Signal("glyph-11")]
		Glyph11,

		[Signal("glyph-12")]
		Glyph12,

		[Signal("glyph-13")]
		Glyph13,

		[Signal("glyph-14")]
		Glyph14,

		[Signal("glyph-15")]
		Glyph15,

		[Signal("glyph-16")]
		Glyph16,

		[Signal("glyph-17")]
		Glyph17,

		[Signal("glyph-18")]
		Glyph18,

		[Signal("glyph-19")]
		Glyph19,

		[Signal("glyph-20")]
		Glyph20,

		[Signal("Break-1")]
		Break1,

		[Signal("Break-2")]
		Break2,

		[Signal("Break-3")]
		Break3,

		[Signal("condition-event-1")]
		ConditionEvent1,

		[Signal("condition-event-2")]
		ConditionEvent2,

		[Signal("condition-event-3")]
		ConditionEvent3,

		[Signal("condition-event-4")]
		ConditionEvent4,

		[Signal("condition-event-5")]
		ConditionEvent5,

		[Signal("condition-event-6")]
		ConditionEvent6,

		[Signal("condition-event-7")]
		ConditionEvent7,

		[Signal("condition-event-8")]
		ConditionEvent8,

		[Signal("condition-event-9")]
		ConditionEvent9,

		[Signal("condition-event-10")]
		ConditionEvent10,

	}
}
