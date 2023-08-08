using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Arg;
public class Creature
{
	[Signal("world-id")]
	public short WorldId;

	public RaceSeq Race;

	public SexSeq Sex;

	public Job Job;

	[Repeat(92)]
	public sbyte[] Appearance;



	public string Name;

	public short GeoZone;

	public int X;

	public int Y;

	public int Z;

	public sbyte Yaw;

	public sbyte Level;

	public int Exp;

	public sbyte MasteryLevel;

	public long MasteryExp;

	public long Hp;

	public sbyte GuardGauge;

	public int Money;

	public int MoneyDiff;







	public object Faction;

	public object Faction2;

	public object GuildId;

	public object ActivatedFaction;

	public object FactionReputation;

	public object AchievementId;

	public object AchievementStep;

	public object AbilityAchievementId;

	public object AbilityAchievementStep;

	public object Sp;

	public object Sp2;

	public object Stance;

	[Signal("link-target")]
	public object LinkTarget;

	[Signal("link-type")]
	public object LinkType;

	[Signal("link-state")]
	public object LinkState;

	[Signal("link-stage")]
	public object LinkStage;

	public object Target;

	[Signal("action-type")]
	public object ActionType;

	[Signal("action-target")]
	public object ActionTarget;

	[Signal("additional-hp")]
	public object AdditionalHp;

	[Signal("effect-attribute")]
	public object EffectAttribute;

	[Signal("effect-attribute-2")]
	public object EffectAttribute2;

	[Signal("effect-attribute-3")]
	public object EffectAttribute3;

	[Signal("immune-effect-attribute")]
	public object ImmuneEffectAttribute;

	[Signal("immune-effect-attribute-2")]
	public object ImmuneEffectAttribute2;

	[Signal("immune-effect-attribute-3")]
	public object ImmuneEffectAttribute3;

	[Signal("effect-flag")]
	public object EffectFlag;

	[Signal("effect-flag-2")]
	public object EffectFlag2;

	[Signal("effect-flag-3")]
	public object EffectFlag3;

	[Signal("effect-flag-4")]
	public object EffectFlag4;

	[Signal("field-item")]
	public object FieldItem;

	public object Summoned;

	[Signal("convoy-id")]
	public object ConvoyId;

	[Signal("top-contribution-group-id")]
	public object TopContributionGroupId;

	[Signal("inventory-size")]
	public object InventorySize;

	[Signal("depository-size")]
	public object DepositorySize;

	[Signal("wardrobe-size")]
	public object WardrobeSize;

	[Signal("premium-depository-size")]
	public object PremiumDepositorySize;

	[Signal("builder-right")]
	public object BuilderRight;

	public object Survey;

	[Signal("style-score")]
	public object StyleScore;

	[Signal("immune-breaker-attribute")]
	public object ImmuneBreakerAttribute;

	[Signal("immune-breaker-count")]
	public object ImmuneBreakerCount;

	[Signal("faction-score")]
	public object FactionScore;

	[Signal("faction-killed-count")]
	public object FactionKilledCount;

	[Signal("combat-side")]
	public object CombatSide;

	[Signal("duel-point")]
	public int DuelPoint;

	[Signal("party-battle-point")]
	public int PartyBattlePoint;

	[Signal("field-play-point")]
	public int FieldPlayPoint;

	[Signal("life-contents-point")]
	public int LifeContentsPoint;

	[Signal("duel-grade")]
	public object DuelGrade;

	[Signal("duel-grade-type")]
	public object DuelGradeType;

	public object Dead;

	[Signal("gm-mode")]
	public bool GmMode;

	[Signal("grade-level")]
	public object GradeLevel;

	[Signal("grade-broadcast-benefit-1")]
	public object GradeBroadcastBenefit1;

	[Signal("grade-broadcast-benefit-2")]
	public object GradeBroadcastBenefit2;

	[Signal("grade-broadcast-benefit-3")]
	public object GradeBroadcastBenefit3;

	[Signal("grade-broadcast-benefit-4")]
	public object GradeBroadcastBenefit4;

	[Signal("grade-broadcast-benefit-5")]
	public object GradeBroadcastBenefit5;

	[Signal("grade-broadcast-benefit-6")]
	public object GradeBroadcastBenefit6;

	[Signal("grade-broadcast-benefit-7")]
	public object GradeBroadcastBenefit7;

	[Signal("grade-broadcast-benefit-8")]
	public object GradeBroadcastBenefit8;

	[Signal("logout-time")]
	public object LogoutTime;

	[Signal("instant-heart-count")]
	public object InstantHeartCount;

	[Signal("heart-count")]
	public object HeartCount;

	[Signal("skill-score")]
	public object SkillScore;

	[Signal("bound-raid-dungeon-id-1")]
	public object BoundRaidDungeonId1;

	[Signal("bound-raid-dungeon-id-2")]
	public object BoundRaidDungeonId2;

	[Signal("bound-raid-dungeon-id-3")]
	public object BoundRaidDungeonId3;

	[Signal("bound-raid-dungeon-id-4")]
	public object BoundRaidDungeonId4;

	[Signal("bound-raid-dungeon-id-5")]
	public object BoundRaidDungeonId5;

	[Signal("bound-raid-dungeon-id-6")]
	public object BoundRaidDungeonId6;

	[Signal("bound-raid-dungeon-id-7")]
	public object BoundRaidDungeonId7;

	[Signal("bound-raid-dungeon-id-8")]
	public object BoundRaidDungeonId8;

	[Signal("bound-raid-dungeon-id-9")]
	public object BoundRaidDungeonId9;

	[Signal("bound-raid-dungeon-id-10")]
	public object BoundRaidDungeonId10;

	[Signal("bound-raid-dungeon-id-11")]
	public object BoundRaidDungeonId11;

	[Signal("bound-raid-dungeon-id-12")]
	public object BoundRaidDungeonId12;

	[Signal("bound-raid-dungeon-id-13")]
	public object BoundRaidDungeonId13;

	[Signal("bound-raid-dungeon-id-14")]
	public object BoundRaidDungeonId14;

	[Signal("bound-raid-dungeon-id-15")]
	public object BoundRaidDungeonId15;

	[Signal("bound-raid-dungeon-id-16")]
	public object BoundRaidDungeonId16;

	[Signal("bound-raid-dungeon-id-17")]
	public object BoundRaidDungeonId17;

	[Signal("boss-second-gauge")]
	public object BossSecondGauge;

	[Signal("accumulated-damage-slot-tao")]
	public object AccumulatedDamageSlotTao;

	[Signal("accumulated-damage-slot-1")]
	public object AccumulatedDamageSlot1;

	[Signal("accumulated-damage-slot-2")]
	public object AccumulatedDamageSlot2;

	[Signal("accumulated-damage-slot-3")]
	public object AccumulatedDamageSlot3;

	[Signal("skill-skin-id")]
	public object SkillSkinId;

	[Signal("pvp-mode-cooltime")]
	public object PvpModeCooltime;

	[Signal("ghost-type")]
	public object GhostType;

	[Signal("challenge-party-id")]
	public object ChallengePartyId;

	[Signal("challenge-party-out-time")]
	public object ChallengePartyOutTime;

	[Signal("acquired-skill-build-up-point")]
	public object AcquiredSkillBuildUpPoint;

	[Signal("usable-skill-build-up-point")]
	public object UsableSkillBuildUpPoint;

	[Signal("job-style")]
	public object JobStyle;

	[Signal("account-exp-to-add")]
	public object AccountExpToAdd;

	[Signal("account-exp-added")]
	public object AccountExpAdded;

	[Signal("account-exp-added-time")]
	public object AccountExpAddedTime;

	[Signal("account-exp-by-pc")]
	public object AccountExpByPc;

	[Signal("account-level")]
	public object AccountLevel;

	[Signal("account-exp")]
	public object AccountExp;

	[Signal("account-exp-quota-per-day")]
	public object AccountExpQuotaPerDay;

	[Signal("account-exp-last-update-time")]
	public object AccountExpLastUpdateTime;

	[Signal("activated-badge-page")]
	public object ActivatedBadgePage;

	[Signal("equip-badge-gear-score")]
	public object EquipBadgeGearScore;

	[Signal("combat-mode")]
	public object CombatMode;

	public object Falling;

	public object Convoy;

	public object Attackable;

	public object Casting;

	[Signal("immune-breaker-disable")]
	public object ImmuneBreakerDisable;

	[Signal("npc-interactive")]
	public object NpcInteractive;

	[Signal("npc-talkable")]
	public object NpcTalkable;

	[Signal("special-skill-gauge-full")]
	public object SpecialSkillGaugeFull;

	[Signal("pvp-mode")]
	public object PvpMode;

	[Signal("break-state")]
	public object BreakState;

	[Signal("break-gauge-block-state")]
	public object BreakGaugeBlockState;

	[Signal("public-reserve-5")]
	public object PublicReserve5;

	[Signal("public-reserve-6")]
	public object PublicReserve6;

	[Signal("public-reserve-7")]
	public object PublicReserve7;

	[Signal("public-reserve-8")]
	public object PublicReserve8;

	[Signal("detect-hiding")]
	public object DetectHiding;

	[Signal("guild-invitation-refusal")]
	public object GuildInvitationRefusal;

	[Signal("pvp-safe-area")]
	public object PvpSafeArea;

	[Signal("zone-move-state")]
	public object ZoneMoveState;

	[Signal("private-reserve-5")]
	public object PrivateReserve5;

	[Signal("private-reserve-6")]
	public object PrivateReserve6;

	[Signal("private-reserve-7")]
	public object PrivateReserve7;

	[Signal("private-reserve-8")]
	public object PrivateReserve8;

	[Signal("slate-page")]
	public object SlatePage;

	[Signal("guild-point")]
	public object GuildPoint;

	[Signal("activated-vehicle-id")]
	public object ActivatedVehicleId;

	[Signal("break-gauge")]
	public object BreakGauge;

	[Signal("break-state-count")]
	public object BreakStateCount;

	[Signal("personal-raid-dungeon-1-progress-step")]
	public object PersonalRaidDungeon1ProgressStep;

	[Signal("personal-raid-dungeon-1-expiration-time")]
	public object PersonalRaidDungeon1ExpirationTime;

	[Signal("personal-raid-dungeon-2-progress-step")]
	public object PersonalRaidDungeon2ProgressStep;

	[Signal("personal-raid-dungeon-2-expiration-time")]
	public object PersonalRaidDungeon2ExpirationTime;

	[Signal("personal-raid-dungeon-3-progress-step")]
	public object PersonalRaidDungeon3ProgressStep;

	[Signal("personal-raid-dungeon-3-expiration-time")]
	public object PersonalRaidDungeon3ExpirationTime;

	[Signal("login-reset-time")]
	public object LoginResetTime;

	[Signal("activated-glyph-page")]
	public object ActivatedGlyphPage;

	[Signal("enabled-glyph-slot")]
	public object EnabledGlyphSlot;

	public object Unk;

	[Signal("max-hp")]
	public object MaxHp;

	[Signal("max-hp-equip")]
	public object MaxHpEquip;

	[Signal("max-guard-gauge")]
	public object MaxGuardGauge;

	[Signal("max-guard-gauge-equip")]
	public object MaxGuardGaugeEquip;

	[Signal("max-sp")]
	public object MaxSp;

	[Signal("max-sp2")]
	public object MaxSp2;

	public object Speed;

	[Signal("vehicle-speed")]
	public object VehicleSpeed;

	[Signal("modify-cast-speed-percent")]
	public object ModifyCastSpeedPercent;

	[Signal("hp-regen")]
	public object HpRegen;

	[Signal("hp-regen-equip")]
	public object HpRegenEquip;

	[Signal("hp-regen-combat")]
	public object HpRegenCombat;

	[Signal("hp-regen-combat-equip")]
	public object HpRegenCombatEquip;

	[Signal("attack-hit-base-percent")]
	public object AttackHitBasePercent;

	[Signal("attack-hit-value")]
	public object AttackHitValue;

	[Signal("attack-hit-value-equip")]
	public object AttackHitValueEquip;

	[Signal("attack-pierce-base-percent")]
	public object AttackPierceBasePercent;

	[Signal("attack-parry-pierce-percent")]
	public object AttackParryPiercePercent;

	[Signal("attack-pierce-value")]
	public object AttackPierceValue;

	[Signal("attack-pierce-value-equip")]
	public object AttackPierceValueEquip;

	[Signal("attack-critical-base-percent")]
	public object AttackCriticalBasePercent;

	[Signal("attack-critical-damage-percent")]
	public object AttackCriticalDamagePercent;

	[Signal("attack-critical-value")]
	public object AttackCriticalValue;

	[Signal("attack-critical-value-equip")]
	public object AttackCriticalValueEquip;

	[Signal("attack-critical-damage-value")]
	public object AttackCriticalDamageValue;

	[Signal("attack-critical-damage-value-equip")]
	public object AttackCriticalDamageValueEquip;

	[Signal("defend-critical-base-percent")]
	public object DefendCriticalBasePercent;

	[Signal("defend-critical-damage-percent")]
	public object DefendCriticalDamagePercent;

	[Signal("defend-critical-value")]
	public object DefendCriticalValue;

	[Signal("defend-critical-value-equip")]
	public object DefendCriticalValueEquip;

	[Signal("defend-bounce-percent")]
	public object DefendBouncePercent;

	[Signal("defend-dodge-base-percent")]
	public object DefendDodgeBasePercent;

	[Signal("defend-dodge-value")]
	public object DefendDodgeValue;

	[Signal("defend-dodge-value-equip")]
	public object DefendDodgeValueEquip;

	[Signal("defend-parry-base-percent")]
	public object DefendParryBasePercent;

	[Signal("defend-parry-value")]
	public object DefendParryValue;

	[Signal("defend-parry-value-equip")]
	public object DefendParryValueEquip;

	[Signal("defend-parry-reduce-percent")]
	public object DefendParryReducePercent;

	[Signal("defend-parry-reduce-diff")]
	public object DefendParryReduceDiff;

	[Signal("defend-perfect-parry-base-percent")]
	public object DefendPerfectParryBasePercent;

	[Signal("defend-immune-base-percent")]
	public object DefendImmuneBasePercent;

	[Signal("attack-power-creature-min")]
	public object AttackPowerCreatureMin;

	[Signal("attack-power-creature-max")]
	public object AttackPowerCreatureMax;

	[Signal("attack-power-equip-min")]
	public object AttackPowerEquipMin;

	[Signal("attack-power-equip-max")]
	public object AttackPowerEquipMax;

	[Signal("defend-power-creature-value")]
	public object DefendPowerCreatureValue;

	[Signal("defend-power-equip-value")]
	public object DefendPowerEquipValue;

	[Signal("defend-resist-power-creature-value")]
	public object DefendResistPowerCreatureValue;

	[Signal("defend-resist-power-equip-value")]
	public object DefendResistPowerEquipValue;

	[Signal("defend-physical-damage-reduce-percent")]
	public object DefendPhysicalDamageReducePercent;

	[Signal("defend-force-damage-reduce-percent")]
	public object DefendForceDamageReducePercent;

	[Signal("attack-damage-modify-percent")]
	public object AttackDamageModifyPercent;

	[Signal("attack-damage-modify-diff")]
	public object AttackDamageModifyDiff;

	[Signal("defend-damage-modify-percent")]
	public object DefendDamageModifyPercent;

	[Signal("defend-damage-modify-diff")]
	public object DefendDamageModifyDiff;

	[Signal("defend-miss-base-percent")]
	public object DefendMissBasePercent;

	[Signal("attack-stiff-duration-base-percent")]
	public object AttackStiffDurationBasePercent;

	[Signal("attack-stiff-duration-value")]
	public object AttackStiffDurationValue;

	[Signal("defend-stiff-duration-base-percent")]
	public object DefendStiffDurationBasePercent;

	[Signal("defend-stiff-duration-value")]
	public object DefendStiffDurationValue;

	[Signal("cast-duration-base-percent")]
	public object CastDurationBasePercent;

	[Signal("cast-duration-value")]
	public object CastDurationValue;

	[Signal("attack-concentrate-value")]
	public object AttackConcentrateValue;

	[Signal("attack-concentrate-value-equip")]
	public object AttackConcentrateValueEquip;

	[Signal("defend-perfect-parry-reduce-percent")]
	public object DefendPerfectParryReducePercent;

	[Signal("defend-counter-reduce-percent")]
	public object DefendCounterReducePercent;

	[Signal("pve-boss-level-npc-attack-power-creature-min")]
	public object PveBossLevelNpcAttackPowerCreatureMin;

	[Signal("pve-boss-level-npc-attack-power-creature-max")]
	public object PveBossLevelNpcAttackPowerCreatureMax;

	[Signal("pve-boss-level-npc-attack-power-equip-min")]
	public object PveBossLevelNpcAttackPowerEquipMin;

	[Signal("pve-boss-level-npc-attack-power-equip-max")]
	public object PveBossLevelNpcAttackPowerEquipMax;

	[Signal("pve-boss-level-npc-defend-power-creature-value")]
	public object PveBossLevelNpcDefendPowerCreatureValue;

	[Signal("pve-boss-level-npc-defend-power-equip-value")]
	public object PveBossLevelNpcDefendPowerEquipValue;

	[Signal("pvp-attack-power-creature-min")]
	public object PvpAttackPowerCreatureMin;

	[Signal("pvp-attack-power-creature-max")]
	public object PvpAttackPowerCreatureMax;

	[Signal("pvp-attack-power-equip-min")]
	public object PvpAttackPowerEquipMin;

	[Signal("pvp-attack-power-equip-max")]
	public object PvpAttackPowerEquipMax;

	[Signal("pvp-defend-power-creature-value")]
	public object PvpDefendPowerCreatureValue;

	[Signal("pvp-defend-power-equip-value")]
	public object PvpDefendPowerEquipValue;

	[Signal("job-ability-1")]
	public object JobAbility1;

	[Signal("job-ability-2")]
	public object JobAbility2;

	[Signal("heal-power-base-percent")]
	public object HealPowerBasePercent;

	[Signal("heal-power-value")]
	public object HealPowerValue;

	[Signal("heal-power-diff")]
	public object HealPowerDiff;

	[Signal("aoe-defend-base-percent")]
	public object AoeDefendBasePercent;

	[Signal("aoe-defend-power-value")]
	public object AoeDefendPowerValue;

	[Signal("abnormal-attack-base-percent")]
	public object AbnormalAttackBasePercent;

	[Signal("abnormal-attack-power-value")]
	public object AbnormalAttackPowerValue;

	[Signal("abnormal-attack-power-value-equip")]
	public object AbnormalAttackPowerValueEquip;

	[Signal("abnormal-defend-base-percent")]
	public object AbnormalDefendBasePercent;

	[Signal("abnormal-defend-power-value")]
	public object AbnormalDefendPowerValue;

	[Signal("hate-base-percent")]
	public object HateBasePercent;

	[Signal("hate-power-creature-value")]
	public object HatePowerCreatureValue;

	[Signal("hate-power-equip-value")]
	public object HatePowerEquipValue;

	[Signal("additional-exp-diff-by-kill")]
	public object AdditionalExpDiffByKill;

	[Signal("additional-exp-percent-by-kill")]
	public object AdditionalExpPercentByKill;

	[Signal("additional-mastery-exp-diff-by-kill")]
	public object AdditionalMasteryExpDiffByKill;

	[Signal("additional-mastery-exp-percent-by-kill")]
	public object AdditionalMasteryExpPercentByKill;

	[Signal("additional-faction-score-max-percent")]
	public object AdditionalFactionScoreMaxPercent;

	[Signal("additional-sealed-dungeon-exp-diff-by-kill")]
	public object AdditionalSealedDungeonExpDiffByKill;

	[Signal("additional-sealed-dungeon-exp-percent-by-kill")]
	public object AdditionalSealedDungeonExpPercentByKill;

	[Signal("attack-attribute-value")]
	public object AttackAttributeValue;

	[Signal("attack-attribute-value-equip")]
	public object AttackAttributeValueEquip;

	[Signal("attack-attribute-base-percent")]
	public object AttackAttributeBasePercent;

	[Signal("defend-difficulty-type-damage-reduce-percent")]
	public object DefendDifficultyTypeDamageReducePercent;

	public object Invisible;

	[Signal("block-move")]
	public object BlockMove;

	[Signal("block-skill")]
	public object BlockSkill;

	[Signal("block-physical-skill")]
	public object BlockPhysicalSkill;

	[Signal("block-force-skill")]
	public object BlockForceSkill;

	[Signal("immune-damage")]
	public object ImmuneDamage;

	[Signal("immune-death")]
	public object ImmuneDeath;

	[Signal("immune-debuff")]
	public object ImmuneDebuff;

	[Signal("exception-target")]
	public object ExceptionTarget;

	[Signal("exception-detect")]
	public object ExceptionDetect;

	[Signal("exception-hostile-target")]
	public object ExceptionHostileTarget;

	[Signal("immune-casting-delay")]
	public object ImmuneCastingDelay;

	[Signal("block-dodge")]
	public object BlockDodge;

	[Signal("block-bounce")]
	public object BlockBounce;

	[Signal("block-parry")]
	public object BlockParry;

	[Signal("block-perfect-parry")]
	public object BlockPerfectParry;

	[Signal("front-perfect-parry")]
	public object FrontPerfectParry;

	[Signal("back-perfect-parry")]
	public object BackPerfectParry;

	[Signal("front-bounce")]
	public object FrontBounce;

	[Signal("back-bounce")]
	public object BackBounce;

	[Signal("debug-invincible")]
	public object DebugInvincible;

	[Signal("debug-invisible")]
	public object DebugInvisible;

	[Signal("abnormality-reserve-7")]
	public object AbnormalityReserve7;

	[Signal("abnormality-reserve-8")]
	public object AbnormalityReserve8;

	[Signal("equip-hand")]
	public object EquipHand;

	[Signal("equip-hand-level")]
	public object EquipHandLevel;

	[Signal("equip-hand-appearance-item")]
	public object EquipHandAppearanceItem;

	[Signal("equip-hand-appearance-item-level")]
	public object EquipHandAppearanceItemLevel;

	[Signal("equip-hand-alt")]
	public object EquipHandAlt;

	[Signal("equip-body")]
	public object EquipBody;

	[Signal("equip-body-guild-id")]
	public object EquipBodyGuildId;

	[Signal("equip-body-custom1")]
	public object EquipBodyCustom1;

	[Signal("equip-body-custom2")]
	public object EquipBodyCustom2;

	[Signal("equip-body-custom3")]
	public object EquipBodyCustom3;

	[Signal("equip-body-custom4")]
	public object EquipBodyCustom4;

	[Signal("equip-body-custom5")]
	public object EquipBodyCustom5;

	[Signal("equip-body-custom6")]
	public object EquipBodyCustom6;

	[Signal("equip-body-custom7")]
	public object EquipBodyCustom7;

	[Signal("equip-body-custom8")]
	public object EquipBodyCustom8;

	[Signal("equip-ear")]
	public object EquipEar;

	[Signal("equip-eye")]
	public object EquipEye;

	[Signal("equip-eye-custom1")]
	public object EquipEyeCustom1;

	[Signal("equip-eye-custom2")]
	public object EquipEyeCustom2;

	[Signal("equip-eye-custom3")]
	public object EquipEyeCustom3;

	[Signal("equip-head")]
	public object EquipHead;

	[Signal("equip-head-custom1")]
	public object EquipHeadCustom1;

	[Signal("equip-head-custom2")]
	public object EquipHeadCustom2;

	[Signal("equip-head-custom3")]
	public object EquipHeadCustom3;

	[Signal("equip-body-attach")]
	public object EquipBodyAttach;

	[Signal("equip-body-attach-custom1")]
	public object EquipBodyAttachCustom1;

	[Signal("equip-body-attach-custom2")]
	public object EquipBodyAttachCustom2;

	[Signal("equip-body-attach-custom3")]
	public object EquipBodyAttachCustom3;

	[Signal("equip-weapon-gem")]
	public object EquipWeaponGem;

	[Signal("equip-pet-1")]
	public object EquipPet1;

	[Signal("equip-pet-1-appearance-item")]
	public object EquipPet1AppearanceItem;

	[Signal("equip-pet-2")]
	public object EquipPet2;

	[Signal("equip-pet-2-appearance-item")]
	public object EquipPet2AppearanceItem;

	[Signal("equip-badge-appearance-item")]
	public object EquipBadgeAppearanceItem;

	[Signal("attack-stiff-duration-equip-value")]
	public object AttackStiffDurationEquipValue;

	[Signal("defend-stiff-duration-equip-value")]
	public object DefendStiffDurationEquipValue;

	[Signal("aoe-defend-power-value-equip")]
	public object AoeDefendPowerValueEquip;

	[Signal("heal-power-equip-value")]
	public object HealPowerEquipValue;

	[Signal("defend-strength-creature-value")]
	public object DefendStrengthCreatureValue;

	[Signal("defend-strength-equip-value")]
	public object DefendStrengthEquipValue;

	[Signal("attack-precise-creature-value")]
	public object AttackPreciseCreatureValue;

	[Signal("attack-precise-equip-value")]
	public object AttackPreciseEquipValue;

	[Signal("attack-aoe-pierce-value")]
	public object AttackAoePierceValue;

	[Signal("attack-aoe-pierce-value-equip")]
	public object AttackAoePierceValueEquip;

	[Signal("attack-abnormal-hit-base-percent")]
	public object AttackAbnormalHitBasePercent;

	[Signal("attack-abnormal-hit-value")]
	public object AttackAbnormalHitValue;

	[Signal("attack-abnormal-hit-equip-value")]
	public object AttackAbnormalHitEquipValue;

	[Signal("defend-abnormal-dodge-base-percent")]
	public object DefendAbnormalDodgeBasePercent;

	[Signal("defend-abnormal-dodge-value")]
	public object DefendAbnormalDodgeValue;

	[Signal("defend-abnormal-dodge-equip-value")]
	public object DefendAbnormalDodgeEquipValue;

	[Signal("support-power-base-percent")]
	public object SupportPowerBasePercent;

	[Signal("support-power-value")]
	public object SupportPowerValue;

	[Signal("support-power-equip-value")]
	public object SupportPowerEquipValue;

	[Signal("hypermove-power-value")]
	public object HypermovePowerValue;

	[Signal("hypermove-power-equip-value")]
	public object HypermovePowerEquipValue;
}