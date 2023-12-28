using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.Sequence;
public enum MainAbility : byte
{
    None,

    [Name("Name.Item.attack-power-equip")]
    AttackPowerEquipMinAndMax,

    [Name("Name.Item.defend-power-equip-value")]
    DefendPowerEquipValue,

    [Name("Name.Item.defend-resist-power-equip-value")]
    DefendResistPowerEquipValue,

    [Name("Name.Item.attack-hit-base-percent")]
    AttackHitBasePercent,

    [Name("Name.Item.attack-hit-value")]
    AttackHitValue,

    [Name("Name.Item.attack-critical-base-percent")]
    AttackCriticalBasePercent,

    [Name("Name.Item.attack-critical-value")]
    AttackCriticalValue,

    [Name("Name.Item.defend-critical-base-percent")]
    DefendCriticalBasePercent,

    [Name("Name.Item.defend-critical-value")]
    DefendCriticalValue,

    [Name("Name.Item.defend-dodge-base-percent")]
    DefendDodgeBasePercent,

    [Name("Name.Item.defend-dodge-value")]
    DefendDodgeValue,

    [Name("Name.Item.defend-parry-base-percent")]
    DefendParryBasePercent,

    [Name("Name.Item.defend-parry-value")]
    DefendParryValue,

    [Name("Name.Item.attack-stiff-duration-base-percent")]
    AttackStiffDurationBasePercent,

    [Name("Name.Item.attack-stiff-duration-value")]
    AttackStiffDurationValue,

    [Name("Name.Item.defend-stiff-duration-base-percent")]
    DefendStiffDurationBasePercent,

    [Name("Name.Item.defend-stiff-duration-value")]
    DefendStiffDurationValue,

    [Name("Name.Item.cast-duration-base-percent")]
    CastDurationBasePercent,

    [Name("Name.Item.cast-duration-value")]
    CastDurationValue,

    [Name("Name.Item.defend-physical-damage-reduce-percent")]
    DefendPhysicalDamageReducePercent,

    [Name("Name.Item.defend-force-damage-reduce-percent")]
    DefendForceDamageReducePercent,

    [Name("Name.Item.attack-damage-modify-percent")]
    AttackDamageModifyPercent,

    [Name("Name.Item.attack-damage-modify-diff")]
    AttackDamageModifyDiff,

    [Name("Name.Item.defend-damage-modify-percent")]
    DefendDamageModifyPercent,

    [Name("Name.Item.defend-damage-modify-diff")]
    DefendDamageModifyDiff,

    [Name("Name.Item.max-hp")]
    MaxHp,

    [Name("Name.Item.hp-regen")]
    HpRegen,

    [Name("Name.Item.hp-regen-combat")]
    HpRegenCombat,

    [Name("Name.Item.attack-pierce-value")]
    AttackPierceValue,

    [Name("Name.Item.attack-concentrate-value")]
    AttackConcentrateValue,

    [Name("Name.Item.defend-perfect-parry-reduce-percent")]
    DefendPerfectParryReducePercent,

    [Name("Name.Item.defend-counter-reduce-percent")]
    DefendCounterReducePercent,

    [Name("Name.Item.attack-critical-damage-percent")]
    AttackCriticalDamagePercent,

    [Name("Name.Item.boss-attack-power-equip")]
    PveBossLevelNpcAttackPowerEquipMinAndMax,

    [Name("Name.Item.boss-defend-power-equip")]
    PveBossLevelNpcDefendPowerEquipValue,

    [Name("Name.Item.pc-attack-power-equip")]
    PvpAttackPowerEquipMinAndMax,

    [Name("Name.Item.pc-defend-power-equip")]
    PvpDefendPowerEquipValue,

    [Name("Name.Item.attack-critical-damage-value")]
    AttackCriticalDamageValue,

    [Name("Name.Item.max-guard-gauge")]
    MaxGuardGauge,

    [Name("Name.Item.attack-attribute-value")]
    AttackAttributeValue,

    [Name("Name.Item.r-attack-stiff-duration-equip-value")]
    RAttackStiffDurationEquipValue,

    [Name("Name.Item.r-defend-stiff-duration-equip-value")]
    RDefendStiffDurationEquipValue,

    [Name("Name.Item.r-aoe-defend-power-value-equip")]
    RAoeDefendPowerValueEquip,

    [Name("Name.Item.r-heal-power-equip-value")]
    RHealPowerEquipValue,

    [Name("Name.Item.r-defend-strength-equip-value")]
    RDefendStrengthEquipValue,

    [Name("Name.Item.r-attack-precise-equip-value")]
    RAttackPreciseEquipValue,

    [Name("Name.Item.r-attack-aoe-pierce-value-equip")]
    RAttackAoePierceValueEquip,

    [Name("Name.Item.r-attack-abnormal-hit-equip-value")]
    RAttackAbnormalHitEquipValue,

    [Name("Name.Item.r-defend-abnormal-dodge-equip-value")]
    RDefendAbnormalDodgeEquipValue,

    [Name("Name.Item.r-support-power-equip-value")]
    RSupportPowerEquipValue,

    [Name("Name.Item.r-hypermove-power-equip-value")]
    RHypermovePowerEquipValue,

    [Name("Name.Item.attack-attribute-base-percent")]
    AttackAttributeBasePercent,

    [Name("Name.Item.defend-difficulty-type-damage-reduce-percent")]
    DefendDifficultyTypeDamageReducePercent,

    [Name("Name.Item.attack-attribute-value-equip")]
    AttackAttributeValueEquip,

    [Name("Name.Item.abnormal-attack-power-value")]
    AbnormalAttackPowerValue,

    [Name("Name.Item.abnormal-defend-power-value")]
    AbnormalDefendPowerValue,

    [Name("Name.Item.abnormal-attack-base-percent")]
    AbnormalAttackBasePercent,

    [Name("Name.Item.abnormal-defend-base-percent")]
    AbnormalDefendBasePercent,

    [Name("Name.Item.attack-pierce-base-percent")]
    AttackPierceBasePercent,

	COUNT
}