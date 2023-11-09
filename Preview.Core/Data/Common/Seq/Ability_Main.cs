using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Common.Seq;
public enum MainAbility : byte
{
	None,

	[Name("attack-power-equip-min-and-max")]
	AttackPowerEquipMinAndMax,

	[Name("defend-power-equip-value")]
	DefendPowerEquipValue,

	[Name("defend-resist-power-equip-value")]
	DefendResistPowerEquipValue,

	[Name("attack-hit-base-percent")]
	AttackHitBasePercent,

	[Name("attack-hit-value")]
	AttackHitValue,

	[Name("attack-critical-base-percent")]
	AttackCriticalBasePercent,

	[Name("attack-critical-value")]
	AttackCriticalValue,

	[Name("defend-critical-base-percent")]
	DefendCriticalBasePercent,

	[Name("defend-critical-value")]
	DefendCriticalValue,

	[Name("defend-dodge-base-percent")]
	DefendDodgeBasePercent,

	[Name("defend-dodge-value")]
	DefendDodgeValue,

	[Name("defend-parry-base-percent")]
	DefendParryBasePercent,

	[Name("defend-parry-value")]
	DefendParryValue,

	[Name("attack-stiff-duration-base-percent")]
	AttackStiffDurationBasePercent,

	[Name("attack-stiff-duration-value")]
	AttackStiffDurationValue,

	[Name("defend-stiff-duration-base-percent")]
	DefendStiffDurationBasePercent,

	[Name("defend-stiff-duration-value")]
	DefendStiffDurationValue,

	[Name("cast-duration-base-percent")]
	CastDurationBasePercent,

	[Name("cast-duration-value")]
	CastDurationValue,

	[Name("defend-physical-damage-reduce-percent")]
	DefendPhysicalDamageReducePercent,

	[Name("defend-force-damage-reduce-percent")]
	DefendForceDamageReducePercent,

	[Name("attack-damage-modify-percent")]
	AttackDamageModifyPercent,

	[Name("attack-damage-modify-diff")]
	AttackDamageModifyDiff,

	[Name("defend-damage-modify-percent")]
	DefendDamageModifyPercent,

	[Name("defend-damage-modify-diff")]
	DefendDamageModifyDiff,

	[Name("max-hp")]
	MaxHp,

	[Name("hp-regen")]
	HpRegen,

	[Name("hp-regen-combat")]
	HpRegenCombat,

	[Name("attack-pierce-value")]
	AttackPierceValue,

	[Name("attack-concentrate-value")]
	AttackConcentrateValue,

	[Name("defend-perfect-parry-reduce-percent")]
	DefendPerfectParryReducePercent,

	[Name("defend-counter-reduce-percent")]
	DefendCounterReducePercent,

	[Name("attack-critical-damage-percent")]
	AttackCriticalDamagePercent,

	[Name("pve-boss-level-npc-attack-power-equip-min-and-max")]
	PveBossLevelNpcAttackPowerEquipMinAndMax,

	[Name("pve-boss-level-npc-defend-power-equip-value")]
	PveBossLevelNpcDefendPowerEquipValue,

	[Name("pvp-attack-power-equip-min-and-max")]
	PvpAttackPowerEquipMinAndMax,

	[Name("pvp-defend-power-equip-value")]
	PvpDefendPowerEquipValue,

	[Name("attack-critical-damage-value")]
	AttackCriticalDamageValue,

	[Name("max-guard-gauge")]
	MaxGuardGauge,

	[Name("attack-attribute-value")]
	AttackAttributeValue,

	[Name("r-attack-stiff-duration-equip-value")]
	RAttackStiffDurationEquipValue,

	[Name("r-defend-stiff-duration-equip-value")]
	RDefendStiffDurationEquipValue,

	[Name("r-aoe-defend-power-value-equip")]
	RAoeDefendPowerValueEquip,

	[Name("r-heal-power-equip-value")]
	RHealPowerEquipValue,

	[Name("r-defend-strength-equip-value")]
	RDefendStrengthEquipValue,

	[Name("r-attack-precise-equip-value")]
	RAttackPreciseEquipValue,

	[Name("r-attack-aoe-pierce-value-equip")]
	RAttackAoePierceValueEquip,

	[Name("r-attack-abnormal-hit-equip-value")]
	RAttackAbnormalHitEquipValue,

	[Name("r-defend-abnormal-dodge-equip-value")]
	RDefendAbnormalDodgeEquipValue,

	[Name("r-support-power-equip-value")]
	RSupportPowerEquipValue,

	[Name("r-hypermove-power-equip-value")]
	RHypermovePowerEquipValue,

	[Name("attack-attribute-base-percent")]
	AttackAttributeBasePercent,

	[Name("defend-difficulty-type-damage-reduce-percent")]
	DefendDifficultyTypeDamageReducePercent,

	[Name("attack-attribute-value-equip")]
	AttackAttributeValueEquip,

	[Name("abnormal-attack-power-value")]
	AbnormalAttackPowerValue,

	[Name("abnormal-defend-power-value")]
	AbnormalDefendPowerValue,

	[Name("abnormal-attack-base-percent")]
	AbnormalAttackBasePercent,

	[Name("abnormal-defend-base-percent")]
	AbnormalDefendBasePercent,

	[Name("attack-pierce-base-percent")]
	AttackPierceBasePercent,
}