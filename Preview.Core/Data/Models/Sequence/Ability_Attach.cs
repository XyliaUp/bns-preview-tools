using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models.Sequence;
public enum AttachAbility : byte
{
    None,

    [Name("attack-power-creature-min-max")]
    AttackPowerCreatureMinMax,

    [Name("pve-boss-level-npc-attack-power-creature-min-max")]
    PveBossLevelNpcAttackPowerCreatureMinMax,

    [Name("pvp-attack-power-creature-min-max")]
    PvpAttackPowerCreatureMinMax,

    [Name("attack-hit-value")]
    AttackHitValue,

    [Name("attack-critical-value")]
    AttackCriticalValue,

    [Name("attack-critical-damage-value")]
    AttackCriticalDamageValue,

    [Name("attack-attribute-value")]
    AttackAttributeValue,

    [Name("attack-pierce-value")]
    AttackPierceValue,

    [Name("abnormal-attack-power-value")]
    AbnormalAttackPowerValue,


    [Name("max-hp")]
    MaxHp = 29,

    [Name("defend-power-creature-value")]
    DefendPowerCreatureValue,

    [Name("pve-boss-level-npc-defend-power-creature-value")]
    PveBossLevelNpcDefendPowerCreatureValue,

    [Name("pvp-defend-power-creature-value")]
    PvpDefendPowerCreatureValue,

    [Name("defend-dodge-value")]
    DefendDodgeValue,

    [Name("defend-parry-value")]
    DefendParryValue,

    [Name("defend-critical-value")]
    DefendCriticalValue,

    [Name("hp-regen")]
    HpRegen,

    [Name("heal-power-value")]
    HealPowerValue,

    [Name("abnormal-defend-power-value")]
    AbnormalDefendPowerValue,

    [Name("r-attack-stiff-duration-value")]
    RAttackStiffDurationValue,

    [Name("r-defend-stiff-duration-value")]
    RDefendStiffDurationValue,

    [Name("r-attack-concentrate-value")]
    RAttackConcentrateValue,

    [Name("r-aoe-defend-power-value")]
    RAoeDefendPowerValue,

    [Name("r-defend-strength-creature-value")]
    RDefendStrengthCreatureValue,

    [Name("r-attack-precise-creature-value")]
    RAttackPreciseCreatureValue,

    [Name("r-attack-aoe-pierce-value")]
    RAttackAoePierceValue,

    [Name("r-attack-abnormal-hit-value")]
    RAttackAbnormalHitValue,

    [Name("r-defend-abnormal-dodge-value")]
    RDefendAbnormalDodgeValue,

    [Name("r-support-power-value")]
    RSupportPowerValue,
}