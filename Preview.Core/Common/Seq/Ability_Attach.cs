using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq;
public enum AttachAbility : byte
{
	None,

	[Signal("attack-power-creature-min-max")]
	AttackPowerCreatureMinMax,

	[Signal("pve-boss-level-npc-attack-power-creature-min-max")]
	PveBossLevelNpcAttackPowerCreatureMinMax,

	[Signal("pvp-attack-power-creature-min-max")]
	PvpAttackPowerCreatureMinMax,

	[Signal("attack-hit-value")]
	AttackHitValue,

	[Signal("attack-critical-value")]
	AttackCriticalValue,

	[Signal("attack-critical-damage-value")]
	AttackCriticalDamageValue,

	[Signal("attack-attribute-value")]
	AttackAttributeValue,

	[Signal("attack-pierce-value")]
	AttackPierceValue,

	[Signal("abnormal-attack-power-value")]
	AbnormalAttackPowerValue,






	[Signal("max-hp")]
	MaxHp = 29,

	[Signal("defend-power-creature-value")]
	DefendPowerCreatureValue,

	[Signal("pve-boss-level-npc-defend-power-creature-value")]
	PveBossLevelNpcDefendPowerCreatureValue,

	[Signal("pvp-defend-power-creature-value")]
	PvpDefendPowerCreatureValue,

	[Signal("defend-dodge-value")]
	DefendDodgeValue,

	[Signal("defend-parry-value")]
	DefendParryValue,

	[Signal("defend-critical-value")]
	DefendCriticalValue,

	[Signal("hp-regen")]
	HpRegen,

	[Signal("heal-power-value")]
	HealPowerValue,

	[Signal("abnormal-defend-power-value")]
	AbnormalDefendPowerValue,

	[Signal("r-attack-stiff-duration-value")]
	RAttackStiffDurationValue,

	[Signal("r-defend-stiff-duration-value")]
	RDefendStiffDurationValue,

	[Signal("r-attack-concentrate-value")]
	RAttackConcentrateValue,

	[Signal("r-aoe-defend-power-value")]
	RAoeDefendPowerValue,

	[Signal("r-defend-strength-creature-value")]
	RDefendStrengthCreatureValue,

	[Signal("r-attack-precise-creature-value")]
	RAttackPreciseCreatureValue,

	[Signal("r-attack-aoe-pierce-value")]
	RAttackAoePierceValue,

	[Signal("r-attack-abnormal-hit-value")]
	RAttackAbnormalHitValue,

	[Signal("r-defend-abnormal-dodge-value")]
	RDefendAbnormalDodgeValue,

	[Signal("r-support-power-value")]
	RSupportPowerValue,
}