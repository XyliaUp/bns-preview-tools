using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.Creature;
public enum CreatureField
{
    creature_field_none,

    WorldId,

    Race,

    Sex,

    Job,

    Appearance1,

    Appearance2,

    Appearance3,

    Appearance4,

    Appearance5,

    Appearance6,

    Appearance7,

    Appearance8,

    Appearance9,

    Appearance10,

    Appearance11,

    Appearance12,

    Appearance13,

    Appearance14,

    Appearance15,

    Appearance16,

    Appearance17,

    Appearance18,

    Appearance19,

    Appearance20,

    Appearance21,

    Appearance22,

    Appearance23,

    Appearance24,

    Appearance25,

    Appearance26,

    Appearance27,

    Appearance28,

    Appearance29,

    Appearance30,

    Appearance31,

    Appearance32,

    Appearance33,

    Appearance34,

    Appearance35,

    Appearance36,

    Appearance37,

    Appearance38,

    Appearance39,

    Appearance40,

    Appearance41,

    Appearance42,

    Appearance43,

    Appearance44,

    Appearance45,

    Appearance46,

    Appearance47,

    Appearance48,

    Appearance49,

    Appearance50,

    Appearance51,

    Appearance52,

    Appearance53,

    Appearance54,

    Appearance55,

    Appearance56,

    Appearance57,

    Appearance58,

    Appearance59,

    Appearance60,

    Appearance61,

    Appearance62,

    Appearance63,

    Appearance64,

    Appearance65,

    Appearance66,

    Appearance67,

    Appearance68,

    Appearance69,

    Appearance70,

    Appearance71,

    Appearance72,

    Appearance73,

    Appearance74,

    Appearance75,

    Appearance76,

    Appearance77,

    Appearance78,

    Appearance79,

    Appearance80,

    Appearance81,

    Appearance82,

    Appearance83,

    Appearance84,

    Appearance85,

    Appearance86,

    Appearance87,

    Appearance88,

    Appearance89,

    Appearance90,

    Appearance91,

    Appearance92,

    Name,

    GeoZone,

    X,

    Y,

    Z,

    Yaw,

    Level,

    Exp,

    MasteryLevel,

    MasteryExp,

    Hp,

    GuardGauge,

    Money,

    MoneyDiff,

    Faction,

    Faction2,

    GuildId,

    ActivatedFaction,

    FactionReputation,

    AchievementId,

    AchievementStep,

    AbilityAchievementId,

    AbilityAchievementStep,

    Sp,

    Sp2,

    Stance,

    LinkTarget,

    LinkType,

    LinkState,

    LinkStage,

    Target,

    ActionType,

    ActionTarget,

    AdditionalHp,

    [Name("effect-attribute")]
    EffectAttribute,

    [Name("effect-attribute-2")]
    EffectAttribute2,

    [Name("effect-attribute-3")]
    EffectAttribute3,

    [Name("immune-effect-attribute")]
    ImmuneEffectAttribute,

    [Name("immune-effect-attribute-2")]
    ImmuneEffectAttribute2,

    [Name("immune-effect-attribute-3")]
    ImmuneEffectAttribute3,

    [Name("effect-flag")]
    EffectFlag,

    [Name("effect-flag-2")]
    EffectFlag2,

    [Name("effect-flag-3")]
    EffectFlag3,

    [Name("effect-flag-4")]
    EffectFlag4,

    FieldItem,

    Summoned,

    ConvoyId,

    TopContributionGroupId,

    InventorySize,

    DepositorySize,

    WardrobeSize,

    PremiumDepositorySize,

    BuilderRight,

    Survey,

    StyleScore,

    ImmuneBreakerAttribute,

    ImmuneBreakerCount,

    FactionScore,

    FactionKilledCount,

    CombatSide,

    DuelPoint,

    PartyBattlePoint,

    FieldPlayPoint,

    LifeContentsPoint,

    DuelGrade,

    DuelGradeType,

    Dead,

    GmMode,

    GradeLevel,

    [Name("grade-broadcast-benefit-1")]
    GradeBroadcastBenefit1,

    [Name("grade-broadcast-benefit-2")]
    GradeBroadcastBenefit2,

    [Name("grade-broadcast-benefit-3")]
    GradeBroadcastBenefit3,

    [Name("grade-broadcast-benefit-4")]
    GradeBroadcastBenefit4,

    [Name("grade-broadcast-benefit-5")]
    GradeBroadcastBenefit5,

    [Name("grade-broadcast-benefit-6")]
    GradeBroadcastBenefit6,

    [Name("grade-broadcast-benefit-7")]
    GradeBroadcastBenefit7,

    [Name("grade-broadcast-benefit-8")]
    GradeBroadcastBenefit8,

    LogoutTime,

    InstantHeartCount,

    HeartCount,

    SkillScore,

    [Name("bound-raid-dungeon-id-1")]
    BoundRaidDungeonId1,

    [Name("bound-raid-dungeon-id-2")]
    BoundRaidDungeonId2,

    [Name("bound-raid-dungeon-id-3")]
    BoundRaidDungeonId3,

    [Name("bound-raid-dungeon-id-4")]
    BoundRaidDungeonId4,

    [Name("bound-raid-dungeon-id-5")]
    BoundRaidDungeonId5,

    [Name("bound-raid-dungeon-id-6")]
    BoundRaidDungeonId6,

    [Name("bound-raid-dungeon-id-7")]
    BoundRaidDungeonId7,

    [Name("bound-raid-dungeon-id-8")]
    BoundRaidDungeonId8,

    [Name("bound-raid-dungeon-id-9")]
    BoundRaidDungeonId9,

    [Name("bound-raid-dungeon-id-10")]
    BoundRaidDungeonId10,

    [Name("bound-raid-dungeon-id-11")]
    BoundRaidDungeonId11,

    [Name("bound-raid-dungeon-id-12")]
    BoundRaidDungeonId12,

    [Name("bound-raid-dungeon-id-13")]
    BoundRaidDungeonId13,

    [Name("bound-raid-dungeon-id-14")]
    BoundRaidDungeonId14,

    [Name("bound-raid-dungeon-id-15")]
    BoundRaidDungeonId15,

    [Name("bound-raid-dungeon-id-16")]
    BoundRaidDungeonId16,

    [Name("bound-raid-dungeon-id-17")]
    BoundRaidDungeonId17,

    BossSecondGauge,

    AccumulatedDamageSlotTao,

    [Name("accumulated-damage-slot-1")]
    AccumulatedDamageSlot1,

    [Name("accumulated-damage-slot-2")]
    AccumulatedDamageSlot2,

    [Name("accumulated-damage-slot-3")]
    AccumulatedDamageSlot3,

    SkillSkinId,

    PvpModeCooltime,

    GhostType,

    ChallengePartyId,

    ChallengePartyOutTime,

    AcquiredSkillBuildUpPoint,

    UsableSkillBuildUpPoint,

    JobStyle,

    AccountExpToAdd,

    AccountExpAdded,

    AccountExpAddedTime,

    AccountExpByPc,

    AccountLevel,

    AccountExp,

    AccountExpQuotaPerDay,

    AccountExpLastUpdateTime,

    ActivatedBadgePage,

    EquipBadgeGearScore,

    CombatMode,

    Falling,

    Convoy,

    Attackable,

    Casting,

    ImmuneBreakerDisable,

    NpcInteractive,

    NpcTalkable,

    SpecialSkillGaugeFull,

    PvpMode,

    BreakState,

    BreakGaugeBlockState,

    [Name("public-reserve-5")]
    PublicReserve5,

    [Name("public-reserve-6")]
    PublicReserve6,

    [Name("public-reserve-7")]
    PublicReserve7,

    [Name("public-reserve-8")]
    PublicReserve8,

    DetectHiding,

    GuildInvitationRefusal,

    PvpSafeArea,

    ZoneMoveState,

    [Name("private-reserve-5")]
    PrivateReserve5,

    [Name("private-reserve-6")]
    PrivateReserve6,

    [Name("private-reserve-7")]
    PrivateReserve7,

    [Name("private-reserve-8")]
    PrivateReserve8,

    SlatePage,

    GuildPoint,

    ActivatedVehicleId,

    BreakGauge,

    BreakStateCount,

    PersonalRaidDungeon1ProgressStep,

    PersonalRaidDungeon1ExpirationTime,

    PersonalRaidDungeon2ProgressStep,

    PersonalRaidDungeon2ExpirationTime,

    PersonalRaidDungeon3ProgressStep,

    PersonalRaidDungeon3ExpirationTime,

    LoginResetTime,

    ActivatedGlyphPage,

    EnabledGlyphSlot,

    MaxHp,

    MaxHpEquip,

    MaxGuardGauge,

    MaxGuardGaugeEquip,

    MaxSp,

    MaxSp2,

    Speed,

    VehicleSpeed,

    ModifyCastSpeedPercent,

    HpRegen,

    HpRegenEquip,

    HpRegenCombat,

    HpRegenCombatEquip,

	AttackHitBasePercent,

    AttackHitValue,

    AttackHitValueEquip,

    AttackPierceBasePercent,

    AttackParryPiercePercent,

    AttackPierceValue,

    AttackPierceValueEquip,

    AttackCriticalBasePercent,

    AttackCriticalDamagePercent,

    AttackCriticalValue,

    AttackCriticalValueEquip,

    AttackCriticalDamageValue,

    AttackCriticalDamageValueEquip,

    DefendCriticalBasePercent,

    DefendCriticalDamagePercent,

    DefendCriticalValue,

    DefendCriticalValueEquip,

    DefendBouncePercent,

    DefendDodgeBasePercent,

    DefendDodgeValue,

    DefendDodgeValueEquip,

    DefendParryBasePercent,

    DefendParryValue,

    DefendParryValueEquip,

    DefendParryReducePercent,

    DefendParryReduceDiff,

    DefendPerfectParryBasePercent,

    DefendImmuneBasePercent,

    AttackPowerCreatureMin,

    AttackPowerCreatureMax,

    AttackPowerEquipMin,

    AttackPowerEquipMax,

    DefendPowerCreatureValue,

    DefendPowerEquipValue,

    DefendResistPowerCreatureValue,

    DefendResistPowerEquipValue,

    DefendPhysicalDamageReducePercent,

    DefendForceDamageReducePercent,

    AttackDamageModifyPercent,

    AttackDamageModifyDiff,

    DefendDamageModifyPercent,

    DefendDamageModifyDiff,

    DefendMissBasePercent,

    AttackStiffDurationBasePercent,

    AttackStiffDurationValue,

    DefendStiffDurationBasePercent,

    DefendStiffDurationValue,

    CastDurationBasePercent,

    CastDurationValue,

    AttackConcentrateValue,

    AttackConcentrateValueEquip,

    DefendPerfectParryReducePercent,

    DefendCounterReducePercent,

    PveBossLevelNpcAttackPowerCreatureMin,

    PveBossLevelNpcAttackPowerCreatureMax,

    PveBossLevelNpcAttackPowerEquipMin,

    PveBossLevelNpcAttackPowerEquipMax,

    PveBossLevelNpcDefendPowerCreatureValue,

    PveBossLevelNpcDefendPowerEquipValue,

    PvpAttackPowerCreatureMin,

    PvpAttackPowerCreatureMax,

    PvpAttackPowerEquipMin,

    PvpAttackPowerEquipMax,

    PvpDefendPowerCreatureValue,

    PvpDefendPowerEquipValue,

    [Name("job-ability-1")]
    JobAbility1,

    [Name("job-ability-2")]
    JobAbility2,

    HealPowerBasePercent,

    HealPowerValue,

    HealPowerDiff,

    AoeDefendBasePercent,

    AoeDefendPowerValue,

    AbnormalAttackBasePercent,

    AbnormalAttackPowerValue,

    AbnormalAttackPowerValueEquip,

    AbnormalDefendBasePercent,

    AbnormalDefendPowerValue,

    HateBasePercent,

    HatePowerCreatureValue,

    HatePowerEquipValue,

    AdditionalExpDiffByKill,

    AdditionalExpPercentByKill,

    AdditionalMasteryExpDiffByKill,

    AdditionalMasteryExpPercentByKill,

    AdditionalFactionScoreMaxPercent,

    AdditionalSealedDungeonExpDiffByKill,

    AdditionalSealedDungeonExpPercentByKill,

    AttackAttributeValue,

    AttackAttributeValueEquip,

    AttackAttributeBasePercent,

    DefendDifficultyTypeDamageReducePercent,

    Invisible,

    BlockMove,

    BlockSkill,

    BlockPhysicalSkill,

    BlockForceSkill,

    ImmuneDamage,

    ImmuneDeath,

    ImmuneDebuff,

    ExceptionTarget,

    ExceptionDetect,

    ExceptionHostileTarget,

    ImmuneCastingDelay,

    BlockDodge,

    BlockBounce,

    BlockParry,

    BlockPerfectParry,

    FrontPerfectParry,

    BackPerfectParry,

    FrontBounce,

    BackBounce,

    DebugInvincible,

    DebugInvisible,

    [Name("abnormality-reserve-7")]
    AbnormalityReserve7,

    [Name("abnormality-reserve-8")]
    AbnormalityReserve8,

    EquipHand,

    EquipHandLevel,

    EquipHandAppearanceItem,

    EquipHandAppearanceItemLevel,

    EquipHandAlt,

    EquipBody,

    EquipBodyGuildId,

    EquipBodyCustom1,

    EquipBodyCustom2,

    EquipBodyCustom3,

    EquipBodyCustom4,

    EquipBodyCustom5,

    EquipBodyCustom6,

    EquipBodyCustom7,

    EquipBodyCustom8,

    EquipEar,

    EquipEye,

    EquipEyeCustom1,

    EquipEyeCustom2,

    EquipEyeCustom3,

    EquipHead,

    EquipHeadCustom1,

    EquipHeadCustom2,

    EquipHeadCustom3,

    EquipBodyAttach,

    EquipBodyAttachCustom1,

    EquipBodyAttachCustom2,

    EquipBodyAttachCustom3,

    EquipWeaponGem,

    [Name("equip-pet-1")]
    EquipPet1,

    EquipPet1AppearanceItem,

    [Name("equip-pet-2")]
    EquipPet2,

    EquipPet2AppearanceItem,

    EquipBadgeAppearanceItem,

    AttackStiffDurationEquipValue,

    DefendStiffDurationEquipValue,

    AoeDefendPowerValueEquip,

    HealPowerEquipValue,

    DefendStrengthCreatureValue,

    DefendStrengthEquipValue,

    AttackPreciseCreatureValue,

    AttackPreciseEquipValue,

    AttackAoePierceValue,

    AttackAoePierceValueEquip,

    AttackAbnormalHitBasePercent,

    AttackAbnormalHitValue,

    AttackAbnormalHitEquipValue,

    DefendAbnormalDodgeBasePercent,

    DefendAbnormalDodgeValue,

    DefendAbnormalDodgeEquipValue,

    SupportPowerBasePercent,

    SupportPowerValue,

    SupportPowerEquipValue,

    HypermovePowerValue,

    HypermovePowerEquipValue,
}