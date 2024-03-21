using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.Sequence;
public enum AttachAbility : byte
{
    None,

    [Text("Name.Item.attack-power-equip")]
    AttackPowerCreatureMinMax,

    [Text("Name.Item.boss-attack-power-equip")]
    PveBossLevelNpcAttackPowerCreatureMinMax,

    [Text("Name.Item.pc-attack-power-equip")]
    PvpAttackPowerCreatureMinMax,

    [Text("Name.Item.attack-hit-value")]
    AttackHitValue,

    [Text("Name.Item.attack-critical-value")]
    AttackCriticalValue,

    [Text("Name.Item.attack-critical-damage-value")]
    AttackCriticalDamageValue,

    [Text("Name.Item.attack-attribute-value")]
    AttackAttributeValue,

    [Text("Name.Item.attack-pierce-value")]
    AttackPierceValue,

    [Text("Name.Item.abnormal-attack-power-value")]
    AbnormalAttackPowerValue,


    [Text("Name.Item.max-hp")]
    MaxHp = 29,

    [Text("Name.Item.defend-power-equip-value")]
    DefendPowerCreatureValue,

    [Text("Name.Item.boss-defend-power-equip")]
    PveBossLevelNpcDefendPowerCreatureValue,

    [Text("Name.Item.pc-defend-power-equip")]
    PvpDefendPowerCreatureValue,

    [Text("Name.Item.defend-dodge-value")]
    DefendDodgeValue,

    [Text("Name.Item.defend-parry-value")]
    DefendParryValue,

    [Text("Name.Item.defend-critical-value")]
    DefendCriticalValue,

    [Text("Name.Item.hp-regen")]
    HpRegen,

    [Text("Name.Item.heal-power-value")]
    HealPowerValue,

    [Text("Name.Item.abnormal-defend-power-value")]
    AbnormalDefendPowerValue,


    [Text("Name.Item.r-attack-stiff-duration-value")]
    RAttackStiffDurationValue,

    [Text("Name.Item.r-defend-stiff-duration-value")]
    RDefendStiffDurationValue,

    [Text("Name.Item.r-attack-concentrate-value")]
    RAttackConcentrateValue,

    [Text("Name.Item.r-aoe-defend-power-value")]
    RAoeDefendPowerValue,

    [Text("Name.Item.r-defend-strength-creature-value")]
    RDefendStrengthCreatureValue,

    [Text("Name.Item.r-attack-precise-creature-value")]
    RAttackPreciseCreatureValue,

    [Text("Name.Item.r-attack-aoe-pierce-value")]
    RAttackAoePierceValue,

    [Text("Name.Item.r-attack-abnormal-hit-value")]
    RAttackAbnormalHitValue,

    [Text("Name.Item.r-defend-abnormal-dodge-value")]
    RDefendAbnormalDodgeValue,

    [Text("Name.Item.r-support-power-value")]
    RSupportPowerValue,

	COUNT
}