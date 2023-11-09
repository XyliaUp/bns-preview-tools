﻿using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Common.Seq;
public enum Flag
{
	None,
	Stun,
	Airdash,
	Knockback,
	BindPhantom,
	Link,
	Detection,
	InternalInjury,
	ImmediatelyExec,
	Concentration,
	InfinityShot,
	Down,
	Swoon,
	Defence,
	Kneel,
	Provocation,
	[Name("midair-1")]
	Midair1,
	[Name("midair-2")]
	Midair2,
	[Name("midair-3")]
	Midair3,
	Frostbite,
	MagneticSeal,
	FastFreezing,
	Rupture,
	Impregnability,
	Prickblood,
	[Name("wildfire-1")]
	Wildfire1,
	[Name("wildfire-2")]
	Wildfire2,
	[Name("wildfire-3")]
	Wildfire3,
	Hide,
	Burrow,
	Smokescreen,
	Embers,
	Poison,
	AppliedPoison,
	SpiderWeb,
	TimeBomb,
	DefenceBlock,
	DashBlock,
	Dexterity,
	[Name("soulblade-1")]
	Soulblade1,
	[Name("soulblade-2")]
	Soulblade2,
	[Name("soulblade-3")]
	Soulblade3,
	Joint,
	Flydragon,
	Landdragon,
	[Name("silverweb-1")]
	Silverweb1,
	[Name("silverweb-2")]
	Silverweb2,
	Justguard,
	[Name("force-flag-1")]
	ForceFlag1,
	[Name("force-flag-2")]
	ForceFlag2,
	[Name("force-flag-3")]
	ForceFlag3,
	Catchshield,
	Poundshort,
	Axechopshort,
	Swingstrikeshort,
	Chilblain,
	Frontdown,
	Bleeding,
	[Name("npc-state-1")]
	NpcState1,
	[Name("npc-state-2")]
	NpcState2,
	[Name("npc-state-3")]
	NpcState3,
	[Name("npc-state-4")]
	NpcState4,
	[Name("npc-state-5")]
	NpcState5,
	[Name("npc-state-6")]
	NpcState6,
	[Name("npc-state-7")]
	NpcState7,
	[Name("npc-state-8")]
	NpcState8,
	HardwallBreak,
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
	CannotResurrect,
	EnableGuildBattleField,
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
	BlueSky,
	EgoAutoParry,
	Pierce,
	Burn,
	AttackGlide,
	SecondGaugeStopped,
	BigBossJump,
	[Name("BigBossState-1")]
	BigBossState1,
	[Name("JobSkillFlag-1")]
	JobSkillFlag1,
	[Name("JobSkillFlag-2")]
	JobSkillFlag2,
	[Name("JobSkillFlag-3")]
	JobSkillFlag3,
	[Name("JobSkillFlag-4")]
	JobSkillFlag4,
	[Name("JobSkillFlag-5")]
	JobSkillFlag5,
	[Name("JobSkillFlag-6")]
	JobSkillFlag6,
	[Name("JobSkillFlag-7")]
	JobSkillFlag7,
	[Name("JobSkillFlag-8")]
	JobSkillFlag8,
	[Name("JobSkillFlag-9")]
	JobSkillFlag9,
	[Name("JobSkillFlag-10")]
	JobSkillFlag10,
	[Name("JobSkillFlag-11")]
	JobSkillFlag11,
	[Name("JobSkillFlag-12")]
	JobSkillFlag12,
	[Name("JobSkillFlag-13")]
	JobSkillFlag13,
	[Name("JobSkillFlag-14")]
	JobSkillFlag14,
	[Name("JobSkillFlag-15")]
	JobSkillFlag15,
	[Name("JobSkillFlag-16")]
	JobSkillFlag16,
	[Name("JobSkillFlag-17")]
	JobSkillFlag17,
	[Name("JobSkillFlag-18")]
	JobSkillFlag18,
	[Name("JobSkillFlag-19")]
	JobSkillFlag19,
	[Name("JobSkillFlag-20")]
	JobSkillFlag20,
	[Name("JobSkillFlag-21")]
	JobSkillFlag21,
	[Name("JobSkillFlag-22")]
	JobSkillFlag22,
	[Name("JobSkillFlag-23")]
	JobSkillFlag23,
	[Name("JobSkillFlag-24")]
	JobSkillFlag24,
	[Name("JobSkillFlag-25")]
	JobSkillFlag25,
	[Name("JobSkillFlag-26")]
	JobSkillFlag26,
	[Name("JobSkillFlag-27")]
	JobSkillFlag27,
	[Name("JobSkillFlag-28")]
	JobSkillFlag28,
	[Name("JobSkillFlag-29")]
	JobSkillFlag29,
	[Name("JobSkillFlag-30")]
	JobSkillFlag30,
	[Name("JobSkillFlag-31")]
	JobSkillFlag31,
	[Name("JobSkillFlag-32")]
	JobSkillFlag32,
	[Name("JobSkillFlag-33")]
	JobSkillFlag33,
	[Name("JobSkillFlag-34")]
	JobSkillFlag34,
	[Name("JobSkillFlag-35")]
	JobSkillFlag35,
	[Name("JobSkillFlag-36")]
	JobSkillFlag36,
	[Name("JobSkillFlag-37")]
	JobSkillFlag37,
	[Name("JobSkillFlag-38")]
	JobSkillFlag38,
	[Name("JobSkillFlag-39")]
	JobSkillFlag39,
	[Name("JobSkillFlag-40")]
	JobSkillFlag40,
	[Name("JobSkillFlag-41")]
	JobSkillFlag41,
	[Name("JobSkillFlag-42")]
	JobSkillFlag42,
	[Name("JobSkillFlag-43")]
	JobSkillFlag43,
	[Name("JobSkillFlag-44")]
	JobSkillFlag44,
	[Name("JobSkillFlag-45")]
	JobSkillFlag45,
	[Name("JobSkillFlag-46")]
	JobSkillFlag46,
	[Name("npc-skill-1")]
	NpcSkill1,
	[Name("npc-skill-2")]
	NpcSkill2,
	[Name("npc-skill-3")]
	NpcSkill3,
	[Name("npc-skill-4")]
	NpcSkill4,
	[Name("npc-skill-5")]
	NpcSkill5,
	[Name("npc-skill-6")]
	NpcSkill6,
	[Name("npc-skill-7")]
	NpcSkill7,
	[Name("npc-skill-8")]
	NpcSkill8,
	[Name("npc-skill-9")]
	NpcSkill9,
	[Name("npc-skill-10")]
	NpcSkill10,
	[Name("npc-skill-11")]
	NpcSkill11,
	[Name("npc-skill-12")]
	NpcSkill12,
	StunMiss,
	DownMiss,
	Struggle,
	Boutiquefree,
	TumblingBlock,
	PeaceArea,
	PerfectDodged,
	KneelMiss,
	[Name("glyph-1")]
	Glyph1,
	[Name("glyph-2")]
	Glyph2,
	[Name("glyph-3")]
	Glyph3,
	[Name("glyph-4")]
	Glyph4,
	[Name("glyph-5")]
	Glyph5,
	[Name("glyph-6")]
	Glyph6,
	[Name("glyph-7")]
	Glyph7,
	[Name("glyph-8")]
	Glyph8,
	[Name("glyph-9")]
	Glyph9,
	[Name("glyph-10")]
	Glyph10,
	[Name("glyph-11")]
	Glyph11,
	[Name("glyph-12")]
	Glyph12,
	[Name("glyph-13")]
	Glyph13,
	[Name("glyph-14")]
	Glyph14,
	[Name("glyph-15")]
	Glyph15,
	[Name("glyph-16")]
	Glyph16,
	[Name("glyph-17")]
	Glyph17,
	[Name("glyph-18")]
	Glyph18,
	[Name("glyph-19")]
	Glyph19,
	[Name("glyph-20")]
	Glyph20,
	[Name("Break-1")]
	Break1,
	[Name("Break-2")]
	Break2,
	[Name("Break-3")]
	Break3,
	[Name("condition-event-1")]
	ConditionEvent1,
	[Name("condition-event-2")]
	ConditionEvent2,
	[Name("condition-event-3")]
	ConditionEvent3,
	[Name("condition-event-4")]
	ConditionEvent4,
	[Name("condition-event-5")]
	ConditionEvent5,
	[Name("condition-event-6")]
	ConditionEvent6,
	[Name("condition-event-7")]
	ConditionEvent7,
	[Name("condition-event-8")]
	ConditionEvent8,
	[Name("condition-event-9")]
	ConditionEvent9,
	[Name("condition-event-1O")]
	ConditionEvent10,
}