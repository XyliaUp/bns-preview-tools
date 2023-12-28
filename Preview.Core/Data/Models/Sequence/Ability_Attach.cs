using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.Sequence;
public enum AttachAbility : byte
{
    None,

    [Name("Name.Item.attack-power-equip")]
    AttackPowerCreatureMinMax,

    [Name("Name.Item.boss-attack-power-equip")]
    PveBossLevelNpcAttackPowerCreatureMinMax,

    [Name("Name.Item.pc-attack-power-equip")]
    PvpAttackPowerCreatureMinMax,

    [Name("Name.Item.attack-hit-value")]
    AttackHitValue,

    [Name("Name.Item.attack-critical-value")]
    AttackCriticalValue,

    [Name("Name.Item.attack-critical-damage-value")]
    AttackCriticalDamageValue,

    [Name("Name.Item.attack-attribute-value")]
    AttackAttributeValue,

    [Name("Name.Item.attack-pierce-value")]
    AttackPierceValue,

    [Name("Name.Item.abnormal-attack-power-value")]
    AbnormalAttackPowerValue,


    [Name("Name.Item.max-hp")]
    MaxHp = 29,

    [Name("Name.Item.defend-power-equip-value")]
    DefendPowerCreatureValue,

    [Name("Name.Item.boss-defend-power-equip")]
    PveBossLevelNpcDefendPowerCreatureValue,

    [Name("Name.Item.pc-defend-power-equip")]
    PvpDefendPowerCreatureValue,

    [Name("Name.Item.defend-dodge-value")]
    DefendDodgeValue,

    [Name("Name.Item.defend-parry-value")]
    DefendParryValue,

    [Name("Name.Item.defend-critical-value")]
    DefendCriticalValue,

    [Name("Name.Item.hp-regen")]
    HpRegen,

    [Name("Name.Item.heal-power-value")]
    HealPowerValue,

    [Name("Name.Item.abnormal-defend-power-value")]
    AbnormalDefendPowerValue,




    [Name("Name.Item.r-attack-stiff-duration-value")]
    RAttackStiffDurationValue,

    [Name("Name.Item.r-defend-stiff-duration-value")]
    RDefendStiffDurationValue,

    [Name("Name.Item.r-attack-concentrate-value")]
    RAttackConcentrateValue,

    [Name("Name.Item.r-aoe-defend-power-value")]
    RAoeDefendPowerValue,

    [Name("Name.Item.r-defend-strength-creature-value")]
    RDefendStrengthCreatureValue,

    [Name("Name.Item.r-attack-precise-creature-value")]
    RAttackPreciseCreatureValue,

    [Name("Name.Item.r-attack-aoe-pierce-value")]
    RAttackAoePierceValue,

    [Name("Name.Item.r-attack-abnormal-hit-value")]
    RAttackAbnormalHitValue,

    [Name("Name.Item.r-defend-abnormal-dodge-value")]
    RDefendAbnormalDodgeValue,

    [Name("Name.Item.r-support-power-value")]
    RSupportPowerValue,

	COUNT
}