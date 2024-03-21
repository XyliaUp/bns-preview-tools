using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.Sequence;
public enum MainAbility : byte
{
    None,

    [Text("Name.Item.attack-power-equip")]
    AttackPowerEquipMinAndMax,

    [Text("Name.Item.defend-power-equip-value")]
    DefendPowerEquipValue,

    [Text("Name.Item.defend-resist-power-equip-value")]
    DefendResistPowerEquipValue,

    [Text("Name.Item.attack-hit-base-percent")]
    AttackHitBasePercent,

    [Text("Name.Item.attack-hit-value")]
    AttackHitValue,

    [Text("Name.Item.attack-critical-base-percent")]
    AttackCriticalBasePercent,

    [Text("Name.Item.attack-critical-value")]
    AttackCriticalValue,

    [Text("Name.Item.defend-critical-base-percent")]
    DefendCriticalBasePercent,

    [Text("Name.Item.defend-critical-value")]
    DefendCriticalValue,

    [Text("Name.Item.defend-dodge-base-percent")]
    DefendDodgeBasePercent,

    [Text("Name.Item.defend-dodge-value")]
    DefendDodgeValue,

    [Text("Name.Item.defend-parry-base-percent")]
    DefendParryBasePercent,

    [Text("Name.Item.defend-parry-value")]
    DefendParryValue,

    [Text("Name.Item.attack-stiff-duration-base-percent")]
    AttackStiffDurationBasePercent,

    [Text("Name.Item.attack-stiff-duration-value")]
    AttackStiffDurationValue,

    [Text("Name.Item.defend-stiff-duration-base-percent")]
    DefendStiffDurationBasePercent,

    [Text("Name.Item.defend-stiff-duration-value")]
    DefendStiffDurationValue,

    [Text("Name.Item.cast-duration-base-percent")]
    CastDurationBasePercent,

    [Text("Name.Item.cast-duration-value")]
    CastDurationValue,

    [Text("Name.Item.defend-physical-damage-reduce-percent")]
    DefendPhysicalDamageReducePercent,

    [Text("Name.Item.defend-force-damage-reduce-percent")]
    DefendForceDamageReducePercent,

    [Text("Name.Item.attack-damage-modify-percent")]
    AttackDamageModifyPercent,

    [Text("Name.Item.attack-damage-modify-diff")]
    AttackDamageModifyDiff,

    [Text("Name.Item.defend-damage-modify-percent")]
    DefendDamageModifyPercent,

    [Text("Name.Item.defend-damage-modify-diff")]
    DefendDamageModifyDiff,

    [Text("Name.Item.max-hp")]
    MaxHp,

    [Text("Name.Item.hp-regen")]
    HpRegen,

    [Text("Name.Item.hp-regen-combat")]
    HpRegenCombat,

    [Text("Name.Item.attack-pierce-value")]
    AttackPierceValue,

    [Text("Name.Item.attack-concentrate-value")]
    AttackConcentrateValue,

    [Text("Name.Item.defend-perfect-parry-reduce-percent")]
    DefendPerfectParryReducePercent,

    [Text("Name.Item.defend-counter-reduce-percent")]
    DefendCounterReducePercent,

    [Text("Name.Item.attack-critical-damage-percent")]
    AttackCriticalDamagePercent,

    [Text("Name.Item.boss-attack-power-equip")]
    PveBossLevelNpcAttackPowerEquipMinAndMax,

    [Text("Name.Item.boss-defend-power-equip")]
    PveBossLevelNpcDefendPowerEquipValue,

    [Text("Name.Item.pc-attack-power-equip")]
    PvpAttackPowerEquipMinAndMax,

    [Text("Name.Item.pc-defend-power-equip")]
    PvpDefendPowerEquipValue,

    [Text("Name.Item.attack-critical-damage-value")]
    AttackCriticalDamageValue,

    [Text("Name.Item.max-guard-gauge")]
    MaxGuardGauge,

    [Text("Name.Item.attack-attribute-value")]
    AttackAttributeValue,

    [Text("Name.Item.r-attack-stiff-duration-equip-value")]
    RAttackStiffDurationEquipValue,

    [Text("Name.Item.r-defend-stiff-duration-equip-value")]
    RDefendStiffDurationEquipValue,

    [Text("Name.Item.r-aoe-defend-power-value-equip")]
    RAoeDefendPowerValueEquip,

    [Text("Name.Item.r-heal-power-equip-value")]
    RHealPowerEquipValue,

    [Text("Name.Item.r-defend-strength-equip-value")]
    RDefendStrengthEquipValue,

    [Text("Name.Item.r-attack-precise-equip-value")]
    RAttackPreciseEquipValue,

    [Text("Name.Item.r-attack-aoe-pierce-value-equip")]
    RAttackAoePierceValueEquip,

    [Text("Name.Item.r-attack-abnormal-hit-equip-value")]
    RAttackAbnormalHitEquipValue,

    [Text("Name.Item.r-defend-abnormal-dodge-equip-value")]
    RDefendAbnormalDodgeEquipValue,

    [Text("Name.Item.r-support-power-equip-value")]
    RSupportPowerEquipValue,

    [Text("Name.Item.r-hypermove-power-equip-value")]
    RHypermovePowerEquipValue,

    [Text("Name.Item.attack-attribute-base-percent")]
    AttackAttributeBasePercent,

    [Text("Name.Item.defend-difficulty-type-damage-reduce-percent")]
    DefendDifficultyTypeDamageReducePercent,

    [Text("Name.Item.attack-attribute-value-equip")]
    AttackAttributeValueEquip,

    [Text("Name.Item.abnormal-attack-power-value")]
    AbnormalAttackPowerValue,

    [Text("Name.Item.abnormal-defend-power-value")]
    AbnormalDefendPowerValue,

    [Text("Name.Item.abnormal-attack-base-percent")]
    AbnormalAttackBasePercent,

    [Text("Name.Item.abnormal-defend-base-percent")]
    AbnormalDefendBasePercent,

    [Text("Name.Item.attack-pierce-base-percent")]
    AttackPierceBasePercent,

	COUNT
}