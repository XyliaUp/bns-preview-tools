using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
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

	public enum MainAbility : byte
	{
		None,

		[Signal("attack-power-equip-min-and-max")]
		AttackPowerEquipMinAndMax,

		[Signal("defend-power-equip-value")]
		DefendPowerEquipValue,

		[Signal("defend-resist-power-equip-value")]
		DefendResistPowerEquipValue,

		[Signal("attack-hit-base-percent")]
		AttackHitBasePercent,

		[Signal("attack-hit-value")]
		AttackHitValue,

		[Signal("attack-critical-base-percent")]
		AttackCriticalBasePercent,

		[Signal("attack-critical-value")]
		AttackCriticalValue,

		[Signal("defend-critical-base-percent")]
		DefendCriticalBasePercent,

		[Signal("defend-critical-value")]
		DefendCriticalValue,

		[Signal("defend-dodge-base-percent")]
		DefendDodgeBasePercent,

		[Signal("defend-dodge-value")]
		DefendDodgeValue,

		[Signal("defend-parry-base-percent")]
		DefendParryBasePercent,

		[Signal("defend-parry-value")]
		DefendParryValue,

		[Signal("attack-stiff-duration-base-percent")]
		AttackStiffDurationBasePercent,

		[Signal("attack-stiff-duration-value")]
		AttackStiffDurationValue,

		[Signal("defend-stiff-duration-base-percent")]
		DefendStiffDurationBasePercent,

		[Signal("defend-stiff-duration-value")]
		DefendStiffDurationValue,

		[Signal("cast-duration-base-percent")]
		CastDurationBasePercent,

		[Signal("cast-duration-value")]
		CastDurationValue,

		[Signal("defend-physical-damage-reduce-percent")]
		DefendPhysicalDamageReducePercent,

		[Signal("defend-force-damage-reduce-percent")]
		DefendForceDamageReducePercent,

		[Signal("attack-damage-modify-percent")]
		AttackDamageModifyPercent,

		[Signal("attack-damage-modify-diff")]
		AttackDamageModifyDiff,

		[Signal("defend-damage-modify-percent")]
		DefendDamageModifyPercent,

		[Signal("defend-damage-modify-diff")]
		DefendDamageModifyDiff,

		[Signal("max-hp")]
		MaxHp,

		[Signal("hp-regen")]
		HpRegen,

		[Signal("hp-regen-combat")]
		HpRegenCombat,

		[Signal("attack-pierce-value")]
		AttackPierceValue,

		[Signal("attack-concentrate-value")]
		AttackConcentrateValue,

		[Signal("defend-perfect-parry-reduce-percent")]
		DefendPerfectParryReducePercent,

		[Signal("defend-counter-reduce-percent")]
		DefendCounterReducePercent,

		[Signal("attack-critical-damage-percent")]
		AttackCriticalDamagePercent,

		[Signal("pve-boss-level-npc-attack-power-equip-min-and-max")]
		PveBossLevelNpcAttackPowerEquipMinAndMax,

		[Signal("pve-boss-level-npc-defend-power-equip-value")]
		PveBossLevelNpcDefendPowerEquipValue,

		[Signal("pvp-attack-power-equip-min-and-max")]
		PvpAttackPowerEquipMinAndMax,

		[Signal("pvp-defend-power-equip-value")]
		PvpDefendPowerEquipValue,

		[Signal("attack-critical-damage-value")]
		AttackCriticalDamageValue,

		[Signal("max-guard-gauge")]
		MaxGuardGauge,

		[Signal("attack-attribute-value")]
		AttackAttributeValue,

		[Signal("r-attack-stiff-duration-equip-value")]
		RAttackStiffDurationEquipValue,

		[Signal("r-defend-stiff-duration-equip-value")]
		RDefendStiffDurationEquipValue,

		[Signal("r-aoe-defend-power-value-equip")]
		RAoeDefendPowerValueEquip,

		[Signal("r-heal-power-equip-value")]
		RHealPowerEquipValue,

		[Signal("r-defend-strength-equip-value")]
		RDefendStrengthEquipValue,

		[Signal("r-attack-precise-equip-value")]
		RAttackPreciseEquipValue,

		[Signal("r-attack-aoe-pierce-value-equip")]
		RAttackAoePierceValueEquip,

		[Signal("r-attack-abnormal-hit-equip-value")]
		RAttackAbnormalHitEquipValue,

		[Signal("r-defend-abnormal-dodge-equip-value")]
		RDefendAbnormalDodgeEquipValue,

		[Signal("r-support-power-equip-value")]
		RSupportPowerEquipValue,

		[Signal("r-hypermove-power-equip-value")]
		RHypermovePowerEquipValue,

		[Signal("attack-attribute-base-percent")]
		AttackAttributeBasePercent,

		[Signal("defend-difficulty-type-damage-reduce-percent")]
		[Description("狂暴伤害减免")]
		DefendDifficultyTypeDamageReducePercent,

		[Signal("abnormal-attack-power-value")]
		AbnormalAttackPowerValue,

		[Signal("abnormal-attack-power-value-equip")]
		AbnormalAttackPowerValueEquip,

		[Signal("abnormal-defend-power-value")]
		AbnormalDefendPowerValue,

		[Signal("abnormal-attack-base-percent")]
		AbnormalAttackBasePercent,

		[Signal("abnormal-defend-base-percent")]
		AbnormalDefendBasePercent,

		[Signal("attack-pierce-base-percent")]
		AttackPierceBasePercent,
	}
}