
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

	public byte Appearance1;

	public byte Appearance2;

	public byte Appearance3;

	public byte Appearance4;

	public byte Appearance5;

	public byte Appearance6;

	public byte Appearance7;

	public byte Appearance8;

	public byte Appearance9;

	public byte Appearance10;

	public byte Appearance11;

	public byte Appearance12;

	public byte Appearance13;

	public byte Appearance14;

	public byte Appearance15;

	public byte Appearance16;

	public byte Appearance17;

	public byte Appearance18;

	public byte Appearance19;

	public byte Appearance20;

	public byte Appearance21;

	public byte Appearance22;

	public byte Appearance23;

	public byte Appearance24;

	public byte Appearance25;

	public byte Appearance26;

	public byte Appearance27;

	public byte Appearance28;

	public byte Appearance29;

	public byte Appearance30;

	public byte Appearance31;

	public byte Appearance32;

	public byte Appearance33;

	public byte Appearance34;

	public byte Appearance35;

	public byte Appearance36;

	public byte Appearance37;

	public byte Appearance38;

	public byte Appearance39;

	public byte Appearance40;

	public byte Appearance41;

	public byte Appearance42;

	public byte Appearance43;

	public byte Appearance44;

	public byte Appearance45;

	public byte Appearance46;

	public byte Appearance47;

	public byte Appearance48;

	public byte Appearance49;

	public byte Appearance50;

	public byte Appearance51;

	public byte Appearance52;

	public byte Appearance53;

	public byte Appearance54;

	public byte Appearance55;

	public byte Appearance56;

	public byte Appearance57;

	public byte Appearance58;

	public byte Appearance59;

	public byte Appearance60;

	public byte Appearance61;

	public byte Appearance62;

	public byte Appearance63;

	public byte Appearance64;

	public byte Appearance65;

	public byte Appearance66;

	public byte Appearance67;

	public byte Appearance68;

	public byte Appearance69;

	public byte Appearance70;

	public byte Appearance71;

	public byte Appearance72;

	public byte Appearance73;

	public byte Appearance74;

	public byte Appearance75;

	public byte Appearance76;

	public byte Appearance77;

	public byte Appearance78;

	public byte Appearance79;

	public byte Appearance80;

	public byte Appearance81;

	public byte Appearance82;

	public byte Appearance83;

	public byte Appearance84;

	public byte Appearance85;

	public byte Appearance86;

	public byte Appearance87;

	public byte Appearance88;

	public byte Appearance89;

	public byte Appearance90;

	public byte Appearance91;

	public byte Appearance92;

	public string Name = "用户名称";

	[Signal("geo-zone")]
	public short GeoZone;

	public int X;

	public int Y;

	public int Z;

	public byte Yaw;

	public byte Level;

	public byte Exp;

	[Signal("mastery-level")]
	public byte MasteryLevel;

	[Signal("mastery-exp")]
	public byte MasteryExp;

	public long Hp;

	[Signal("guard-gauge")]
	public byte GuardGauge;

	public int Money;

	[Signal("money-diff")]
	public int MoneyDiff;

	public byte Faction;

	public byte Faction2;

	[Signal("guild-id")]
	public byte GuildId;

	[Signal("activated-faction")]
	public byte ActivatedFaction;

	[Signal("faction-reputation")]
	public byte FactionReputation;

	[Signal("achievement-id")]
	public byte AchievementId;

	[Signal("achievement-step")]
	public byte AchievementStep;

	[Signal("ability-achievement-id")]
	public byte AbilityAchievementId;

	[Signal("ability-achievement-step")]
	public byte AbilityAchievementStep;

	public byte Sp;

	public byte Sp2;

	public byte Stance;

	[Signal("link-target")]
	public byte LinkTarget;

	[Signal("link-type")]
	public byte LinkType;

	[Signal("link-state")]
	public byte LinkState;

	[Signal("link-stage")]
	public byte LinkStage;

	public byte Target;

	[Signal("action-type")]
	public byte ActionType;

	[Signal("action-target")]
	public byte ActionTarget;

	[Signal("additional-hp")]
	public byte AdditionalHp;

	[Signal("effect-attribute")]
	public byte EffectAttribute;

	[Signal("effect-attribute-2")]
	public byte EffectAttribute2;

	[Signal("effect-attribute-3")]
	public byte EffectAttribute3;

	[Signal("immune-effect-attribute")]
	public byte ImmuneEffectAttribute;

	[Signal("immune-effect-attribute-2")]
	public byte ImmuneEffectAttribute2;

	[Signal("immune-effect-attribute-3")]
	public byte ImmuneEffectAttribute3;

	[Signal("effect-flag")]
	public byte EffectFlag;

	[Signal("effect-flag-2")]
	public byte EffectFlag2;

	[Signal("effect-flag-3")]
	public byte EffectFlag3;

	[Signal("effect-flag-4")]
	public byte EffectFlag4;

	[Signal("field-item")]
	public byte FieldItem;

	public byte Summoned;

	[Signal("convoy-id")]
	public byte ConvoyId;

	[Signal("top-contribution-group-id")]
	public byte TopContributionGroupId;

	[Signal("inventory-size")]
	public byte InventorySize;

	[Signal("depository-size")]
	public byte DepositorySize;

	[Signal("wardrobe-size")]
	public byte WardrobeSize;

	[Signal("premium-depository-size")]
	public byte PremiumDepositorySize;

	[Signal("builder-right")]
	public byte BuilderRight;

	public byte Survey;

	[Signal("style-score")]
	public byte StyleScore;

	[Signal("immune-breaker-attribute")]
	public byte ImmuneBreakerAttribute;

	[Signal("immune-breaker-count")]
	public byte ImmuneBreakerCount;

	[Signal("faction-score")]
	public byte FactionScore;

	[Signal("faction-killed-count")]
	public byte FactionKilledCount;

	[Signal("combat-side")]
	public byte CombatSide;

	[Signal("duel-point")]
	public int DuelPoint;

	[Signal("party-battle-point")]
	public int PartyBattlePoint;

	[Signal("field-play-point")]
	public int FieldPlayPoint;

	[Signal("life-contents-point")]
	public int LifeContentsPoint;

	[Signal("duel-grade")]
	public byte DuelGrade;

	[Signal("duel-grade-type")]
	public byte DuelGradeType;

	public byte Dead;

	[Signal("gm-mode")]
	public bool GmMode;

	[Signal("grade-level")]
	public byte GradeLevel;

	[Signal("grade-broadcast-benefit-1")]
	public byte GradeBroadcastBenefit1;

	[Signal("grade-broadcast-benefit-2")]
	public byte GradeBroadcastBenefit2;

	[Signal("grade-broadcast-benefit-3")]
	public byte GradeBroadcastBenefit3;

	[Signal("grade-broadcast-benefit-4")]
	public byte GradeBroadcastBenefit4;

	[Signal("grade-broadcast-benefit-5")]
	public byte GradeBroadcastBenefit5;

	[Signal("grade-broadcast-benefit-6")]
	public byte GradeBroadcastBenefit6;

	[Signal("grade-broadcast-benefit-7")]
	public byte GradeBroadcastBenefit7;

	[Signal("grade-broadcast-benefit-8")]
	public byte GradeBroadcastBenefit8;

	[Signal("logout-time")]
	public byte LogoutTime;

	[Signal("instant-heart-count")]
	public byte InstantHeartCount;

	[Signal("heart-count")]
	public byte HeartCount;

	[Signal("skill-score")]
	public byte SkillScore;

	[Signal("bound-raid-dungeon-id-1")]
	public byte BoundRaidDungeonId1;

	[Signal("bound-raid-dungeon-id-2")]
	public byte BoundRaidDungeonId2;

	[Signal("bound-raid-dungeon-id-3")]
	public byte BoundRaidDungeonId3;

	[Signal("bound-raid-dungeon-id-4")]
	public byte BoundRaidDungeonId4;

	[Signal("bound-raid-dungeon-id-5")]
	public byte BoundRaidDungeonId5;

	[Signal("bound-raid-dungeon-id-6")]
	public byte BoundRaidDungeonId6;

	[Signal("bound-raid-dungeon-id-7")]
	public byte BoundRaidDungeonId7;

	[Signal("bound-raid-dungeon-id-8")]
	public byte BoundRaidDungeonId8;

	[Signal("bound-raid-dungeon-id-9")]
	public byte BoundRaidDungeonId9;

	[Signal("bound-raid-dungeon-id-10")]
	public byte BoundRaidDungeonId10;

	[Signal("bound-raid-dungeon-id-11")]
	public byte BoundRaidDungeonId11;

	[Signal("bound-raid-dungeon-id-12")]
	public byte BoundRaidDungeonId12;

	[Signal("bound-raid-dungeon-id-13")]
	public byte BoundRaidDungeonId13;

	[Signal("bound-raid-dungeon-id-14")]
	public byte BoundRaidDungeonId14;

	[Signal("bound-raid-dungeon-id-15")]
	public byte BoundRaidDungeonId15;

	[Signal("bound-raid-dungeon-id-16")]
	public byte BoundRaidDungeonId16;

	[Signal("bound-raid-dungeon-id-17")]
	public byte BoundRaidDungeonId17;

	[Signal("boss-second-gauge")]
	public byte BossSecondGauge;

	[Signal("accumulated-damage-slot-tao")]
	public byte AccumulatedDamageSlotTao;

	[Signal("accumulated-damage-slot-1")]
	public byte AccumulatedDamageSlot1;

	[Signal("accumulated-damage-slot-2")]
	public byte AccumulatedDamageSlot2;

	[Signal("accumulated-damage-slot-3")]
	public byte AccumulatedDamageSlot3;

	[Signal("skill-skin-id")]
	public byte SkillSkinId;

	[Signal("pvp-mode-cooltime")]
	public byte PvpModeCooltime;

	[Signal("ghost-type")]
	public byte GhostType;

	[Signal("challenge-party-id")]
	public byte ChallengePartyId;

	[Signal("challenge-party-out-time")]
	public byte ChallengePartyOutTime;

	[Signal("acquired-skill-build-up-point")]
	public byte AcquiredSkillBuildUpPoint;

	[Signal("usable-skill-build-up-point")]
	public byte UsableSkillBuildUpPoint;

	[Signal("job-style")]
	public byte JobStyle;

	[Signal("account-exp-to-add")]
	public byte AccountExpToAdd;

	[Signal("account-exp-added")]
	public byte AccountExpAdded;

	[Signal("account-exp-added-time")]
	public byte AccountExpAddedTime;

	[Signal("account-exp-by-pc")]
	public byte AccountExpByPc;

	[Signal("account-level")]
	public byte AccountLevel;

	[Signal("account-exp")]
	public byte AccountExp;

	[Signal("account-exp-quota-per-day")]
	public byte AccountExpQuotaPerDay;

	[Signal("account-exp-last-update-time")]
	public byte AccountExpLastUpdateTime;

	[Signal("activated-badge-page")]
	public byte ActivatedBadgePage;

	[Signal("equip-badge-gear-score")]
	public byte EquipBadgeGearScore;

	[Signal("combat-mode")]
	public byte CombatMode;

	public byte Falling;

	public byte Convoy;

	public byte Attackable;

	public byte Casting;

	[Signal("immune-breaker-disable")]
	public byte ImmuneBreakerDisable;

	[Signal("npc-interactive")]
	public byte NpcInteractive;

	[Signal("npc-talkable")]
	public byte NpcTalkable;

	[Signal("special-skill-gauge-full")]
	public byte SpecialSkillGaugeFull;

	[Signal("pvp-mode")]
	public byte PvpMode;

	[Signal("break-state")]
	public byte BreakState;

	[Signal("break-gauge-block-state")]
	public byte BreakGaugeBlockState;

	[Signal("public-reserve-5")]
	public byte PublicReserve5;

	[Signal("public-reserve-6")]
	public byte PublicReserve6;

	[Signal("public-reserve-7")]
	public byte PublicReserve7;

	[Signal("public-reserve-8")]
	public byte PublicReserve8;

	[Signal("detect-hiding")]
	public byte DetectHiding;

	[Signal("guild-invitation-refusal")]
	public byte GuildInvitationRefusal;

	[Signal("pvp-safe-area")]
	public byte PvpSafeArea;

	[Signal("zone-move-state")]
	public byte ZoneMoveState;

	[Signal("private-reserve-5")]
	public byte PrivateReserve5;

	[Signal("private-reserve-6")]
	public byte PrivateReserve6;

	[Signal("private-reserve-7")]
	public byte PrivateReserve7;

	[Signal("private-reserve-8")]
	public byte PrivateReserve8;

	[Signal("slate-page")]
	public byte SlatePage;

	[Signal("guild-point")]
	public byte GuildPoint;

	[Signal("activated-vehicle-id")]
	public byte ActivatedVehicleId;

	[Signal("break-gauge")]
	public byte BreakGauge;

	[Signal("break-state-count")]
	public byte BreakStateCount;

	[Signal("personal-raid-dungeon-1-progress-step")]
	public byte PersonalRaidDungeon1ProgressStep;

	[Signal("personal-raid-dungeon-1-expiration-time")]
	public byte PersonalRaidDungeon1ExpirationTime;

	[Signal("personal-raid-dungeon-2-progress-step")]
	public byte PersonalRaidDungeon2ProgressStep;

	[Signal("personal-raid-dungeon-2-expiration-time")]
	public byte PersonalRaidDungeon2ExpirationTime;

	[Signal("personal-raid-dungeon-3-progress-step")]
	public byte PersonalRaidDungeon3ProgressStep;

	[Signal("personal-raid-dungeon-3-expiration-time")]
	public byte PersonalRaidDungeon3ExpirationTime;

	[Signal("login-reset-time")]
	public byte LoginResetTime;

	[Signal("activated-glyph-page")]
	public byte ActivatedGlyphPage;

	[Signal("enabled-glyph-slot")]
	public byte EnabledGlyphSlot;

	public byte Unk;

	[Signal("max-hp")]
	public byte MaxHp;

	[Signal("max-hp-equip")]
	public byte MaxHpEquip;

	[Signal("max-guard-gauge")]
	public byte MaxGuardGauge;

	[Signal("max-guard-gauge-equip")]
	public byte MaxGuardGaugeEquip;

	[Signal("max-sp")]
	public byte MaxSp;

	[Signal("max-sp2")]
	public byte MaxSp2;

	public byte Speed;

	[Signal("vehicle-speed")]
	public byte VehicleSpeed;

	[Signal("modify-cast-speed-percent")]
	public byte ModifyCastSpeedPercent;

	[Signal("hp-regen")]
	public byte HpRegen;

	[Signal("hp-regen-equip")]
	public byte HpRegenEquip;

	[Signal("hp-regen-combat")]
	public byte HpRegenCombat;

	[Signal("hp-regen-combat-equip")]
	public byte HpRegenCombatEquip;

	[Signal("attack-hit-base-percent")]
	public byte AttackHitBasePercent;

	[Signal("attack-hit-value")]
	public byte AttackHitValue;

	[Signal("attack-hit-value-equip")]
	public byte AttackHitValueEquip;

	[Signal("attack-pierce-base-percent")]
	public byte AttackPierceBasePercent;

	[Signal("attack-parry-pierce-percent")]
	public byte AttackParryPiercePercent;

	[Signal("attack-pierce-value")]
	public byte AttackPierceValue;

	[Signal("attack-pierce-value-equip")]
	public byte AttackPierceValueEquip;

	[Signal("attack-critical-base-percent")]
	public byte AttackCriticalBasePercent;

	[Signal("attack-critical-damage-percent")]
	public byte AttackCriticalDamagePercent;

	[Signal("attack-critical-value")]
	public byte AttackCriticalValue;

	[Signal("attack-critical-value-equip")]
	public byte AttackCriticalValueEquip;

	[Signal("attack-critical-damage-value")]
	public byte AttackCriticalDamageValue;

	[Signal("attack-critical-damage-value-equip")]
	public byte AttackCriticalDamageValueEquip;

	[Signal("defend-critical-base-percent")]
	public byte DefendCriticalBasePercent;

	[Signal("defend-critical-damage-percent")]
	public byte DefendCriticalDamagePercent;

	[Signal("defend-critical-value")]
	public byte DefendCriticalValue;

	[Signal("defend-critical-value-equip")]
	public byte DefendCriticalValueEquip;

	[Signal("defend-bounce-percent")]
	public byte DefendBouncePercent;

	[Signal("defend-dodge-base-percent")]
	public byte DefendDodgeBasePercent;

	[Signal("defend-dodge-value")]
	public byte DefendDodgeValue;

	[Signal("defend-dodge-value-equip")]
	public byte DefendDodgeValueEquip;

	[Signal("defend-parry-base-percent")]
	public byte DefendParryBasePercent;

	[Signal("defend-parry-value")]
	public byte DefendParryValue;

	[Signal("defend-parry-value-equip")]
	public byte DefendParryValueEquip;

	[Signal("defend-parry-reduce-percent")]
	public byte DefendParryReducePercent;

	[Signal("defend-parry-reduce-diff")]
	public byte DefendParryReduceDiff;

	[Signal("defend-perfect-parry-base-percent")]
	public byte DefendPerfectParryBasePercent;

	[Signal("defend-immune-base-percent")]
	public byte DefendImmuneBasePercent;

	[Signal("attack-power-creature-min")]
	public byte AttackPowerCreatureMin;

	[Signal("attack-power-creature-max")]
	public byte AttackPowerCreatureMax;

	[Signal("attack-power-equip-min")]
	public byte AttackPowerEquipMin;

	[Signal("attack-power-equip-max")]
	public byte AttackPowerEquipMax;

	[Signal("defend-power-creature-value")]
	public byte DefendPowerCreatureValue;

	[Signal("defend-power-equip-value")]
	public byte DefendPowerEquipValue;

	[Signal("defend-resist-power-creature-value")]
	public byte DefendResistPowerCreatureValue;

	[Signal("defend-resist-power-equip-value")]
	public byte DefendResistPowerEquipValue;

	[Signal("defend-physical-damage-reduce-percent")]
	public byte DefendPhysicalDamageReducePercent;

	[Signal("defend-force-damage-reduce-percent")]
	public byte DefendForceDamageReducePercent;

	[Signal("attack-damage-modify-percent")]
	public byte AttackDamageModifyPercent;

	[Signal("attack-damage-modify-diff")]
	public byte AttackDamageModifyDiff;

	[Signal("defend-damage-modify-percent")]
	public byte DefendDamageModifyPercent;

	[Signal("defend-damage-modify-diff")]
	public byte DefendDamageModifyDiff;

	[Signal("defend-miss-base-percent")]
	public byte DefendMissBasePercent;

	[Signal("attack-stiff-duration-base-percent")]
	public byte AttackStiffDurationBasePercent;

	[Signal("attack-stiff-duration-value")]
	public byte AttackStiffDurationValue;

	[Signal("defend-stiff-duration-base-percent")]
	public byte DefendStiffDurationBasePercent;

	[Signal("defend-stiff-duration-value")]
	public byte DefendStiffDurationValue;

	[Signal("cast-duration-base-percent")]
	public byte CastDurationBasePercent;

	[Signal("cast-duration-value")]
	public byte CastDurationValue;

	[Signal("attack-concentrate-value")]
	public byte AttackConcentrateValue;

	[Signal("attack-concentrate-value-equip")]
	public byte AttackConcentrateValueEquip;

	[Signal("defend-perfect-parry-reduce-percent")]
	public byte DefendPerfectParryReducePercent;

	[Signal("defend-counter-reduce-percent")]
	public byte DefendCounterReducePercent;

	[Signal("pve-boss-level-npc-attack-power-creature-min")]
	public byte PveBossLevelNpcAttackPowerCreatureMin;

	[Signal("pve-boss-level-npc-attack-power-creature-max")]
	public byte PveBossLevelNpcAttackPowerCreatureMax;

	[Signal("pve-boss-level-npc-attack-power-equip-min")]
	public byte PveBossLevelNpcAttackPowerEquipMin;

	[Signal("pve-boss-level-npc-attack-power-equip-max")]
	public byte PveBossLevelNpcAttackPowerEquipMax;

	[Signal("pve-boss-level-npc-defend-power-creature-value")]
	public byte PveBossLevelNpcDefendPowerCreatureValue;

	[Signal("pve-boss-level-npc-defend-power-equip-value")]
	public byte PveBossLevelNpcDefendPowerEquipValue;

	[Signal("pvp-attack-power-creature-min")]
	public byte PvpAttackPowerCreatureMin;

	[Signal("pvp-attack-power-creature-max")]
	public byte PvpAttackPowerCreatureMax;

	[Signal("pvp-attack-power-equip-min")]
	public byte PvpAttackPowerEquipMin;

	[Signal("pvp-attack-power-equip-max")]
	public byte PvpAttackPowerEquipMax;

	[Signal("pvp-defend-power-creature-value")]
	public byte PvpDefendPowerCreatureValue;

	[Signal("pvp-defend-power-equip-value")]
	public byte PvpDefendPowerEquipValue;

	[Signal("job-ability-1")]
	public byte JobAbility1;

	[Signal("job-ability-2")]
	public byte JobAbility2;

	[Signal("heal-power-base-percent")]
	public byte HealPowerBasePercent;

	[Signal("heal-power-value")]
	public byte HealPowerValue;

	[Signal("heal-power-diff")]
	public byte HealPowerDiff;

	[Signal("aoe-defend-base-percent")]
	public byte AoeDefendBasePercent;

	[Signal("aoe-defend-power-value")]
	public byte AoeDefendPowerValue;

	[Signal("abnormal-attack-base-percent")]
	public byte AbnormalAttackBasePercent;

	[Signal("abnormal-attack-power-value")]
	public byte AbnormalAttackPowerValue;

	[Signal("abnormal-attack-power-value-equip")]
	public byte AbnormalAttackPowerValueEquip;

	[Signal("abnormal-defend-base-percent")]
	public byte AbnormalDefendBasePercent;

	[Signal("abnormal-defend-power-value")]
	public byte AbnormalDefendPowerValue;

	[Signal("hate-base-percent")]
	public byte HateBasePercent;

	[Signal("hate-power-creature-value")]
	public byte HatePowerCreatureValue;

	[Signal("hate-power-equip-value")]
	public byte HatePowerEquipValue;

	[Signal("additional-exp-diff-by-kill")]
	public byte AdditionalExpDiffByKill;

	[Signal("additional-exp-percent-by-kill")]
	public byte AdditionalExpPercentByKill;

	[Signal("additional-mastery-exp-diff-by-kill")]
	public byte AdditionalMasteryExpDiffByKill;

	[Signal("additional-mastery-exp-percent-by-kill")]
	public byte AdditionalMasteryExpPercentByKill;

	[Signal("additional-faction-score-max-percent")]
	public byte AdditionalFactionScoreMaxPercent;

	[Signal("additional-sealed-dungeon-exp-diff-by-kill")]
	public byte AdditionalSealedDungeonExpDiffByKill;

	[Signal("additional-sealed-dungeon-exp-percent-by-kill")]
	public byte AdditionalSealedDungeonExpPercentByKill;

	[Signal("attack-attribute-value")]
	public byte AttackAttributeValue;

	[Signal("attack-attribute-value-equip")]
	public byte AttackAttributeValueEquip;

	[Signal("attack-attribute-base-percent")]
	public byte AttackAttributeBasePercent;

	[Signal("defend-difficulty-type-damage-reduce-percent")]
	public byte DefendDifficultyTypeDamageReducePercent;

	public byte Invisible;

	[Signal("block-move")]
	public byte BlockMove;

	[Signal("block-skill")]
	public byte BlockSkill;

	[Signal("block-physical-skill")]
	public byte BlockPhysicalSkill;

	[Signal("block-force-skill")]
	public byte BlockForceSkill;

	[Signal("immune-damage")]
	public byte ImmuneDamage;

	[Signal("immune-death")]
	public byte ImmuneDeath;

	[Signal("immune-debuff")]
	public byte ImmuneDebuff;

	[Signal("exception-target")]
	public byte ExceptionTarget;

	[Signal("exception-detect")]
	public byte ExceptionDetect;

	[Signal("exception-hostile-target")]
	public byte ExceptionHostileTarget;

	[Signal("immune-casting-delay")]
	public byte ImmuneCastingDelay;

	[Signal("block-dodge")]
	public byte BlockDodge;

	[Signal("block-bounce")]
	public byte BlockBounce;

	[Signal("block-parry")]
	public byte BlockParry;

	[Signal("block-perfect-parry")]
	public byte BlockPerfectParry;

	[Signal("front-perfect-parry")]
	public byte FrontPerfectParry;

	[Signal("back-perfect-parry")]
	public byte BackPerfectParry;

	[Signal("front-bounce")]
	public byte FrontBounce;

	[Signal("back-bounce")]
	public byte BackBounce;

	[Signal("debug-invincible")]
	public byte DebugInvincible;

	[Signal("debug-invisible")]
	public byte DebugInvisible;

	[Signal("abnormality-reserve-7")]
	public byte AbnormalityReserve7;

	[Signal("abnormality-reserve-8")]
	public byte AbnormalityReserve8;

	[Signal("equip-hand")]
	public byte EquipHand;

	[Signal("equip-hand-level")]
	public byte EquipHandLevel;

	[Signal("equip-hand-appearance-item")]
	public byte EquipHandAppearanceItem;

	[Signal("equip-hand-appearance-item-level")]
	public byte EquipHandAppearanceItemLevel;

	[Signal("equip-hand-alt")]
	public byte EquipHandAlt;

	[Signal("equip-body")]
	public byte EquipBody;

	[Signal("equip-body-guild-id")]
	public byte EquipBodyGuildId;

	[Signal("equip-body-custom1")]
	public byte EquipBodyCustom1;

	[Signal("equip-body-custom2")]
	public byte EquipBodyCustom2;

	[Signal("equip-body-custom3")]
	public byte EquipBodyCustom3;

	[Signal("equip-body-custom4")]
	public byte EquipBodyCustom4;

	[Signal("equip-body-custom5")]
	public byte EquipBodyCustom5;

	[Signal("equip-body-custom6")]
	public byte EquipBodyCustom6;

	[Signal("equip-body-custom7")]
	public byte EquipBodyCustom7;

	[Signal("equip-body-custom8")]
	public byte EquipBodyCustom8;

	[Signal("equip-ear")]
	public byte EquipEar;

	[Signal("equip-eye")]
	public byte EquipEye;

	[Signal("equip-eye-custom1")]
	public byte EquipEyeCustom1;

	[Signal("equip-eye-custom2")]
	public byte EquipEyeCustom2;

	[Signal("equip-eye-custom3")]
	public byte EquipEyeCustom3;

	[Signal("equip-head")]
	public byte EquipHead;

	[Signal("equip-head-custom1")]
	public byte EquipHeadCustom1;

	[Signal("equip-head-custom2")]
	public byte EquipHeadCustom2;

	[Signal("equip-head-custom3")]
	public byte EquipHeadCustom3;

	[Signal("equip-body-attach")]
	public byte EquipBodyAttach;

	[Signal("equip-body-attach-custom1")]
	public byte EquipBodyAttachCustom1;

	[Signal("equip-body-attach-custom2")]
	public byte EquipBodyAttachCustom2;

	[Signal("equip-body-attach-custom3")]
	public byte EquipBodyAttachCustom3;

	[Signal("equip-weapon-gem")]
	public byte EquipWeaponGem;

	[Signal("equip-pet-1")]
	public byte EquipPet1;

	[Signal("equip-pet-1-appearance-item")]
	public byte EquipPet1AppearanceItem;

	[Signal("equip-pet-2")]
	public byte EquipPet2;

	[Signal("equip-pet-2-appearance-item")]
	public byte EquipPet2AppearanceItem;

	[Signal("equip-badge-appearance-item")]
	public byte EquipBadgeAppearanceItem;

	[Signal("attack-stiff-duration-equip-value")]
	public byte AttackStiffDurationEquipValue;

	[Signal("defend-stiff-duration-equip-value")]
	public byte DefendStiffDurationEquipValue;

	[Signal("aoe-defend-power-value-equip")]
	public byte AoeDefendPowerValueEquip;

	[Signal("heal-power-equip-value")]
	public byte HealPowerEquipValue;

	[Signal("defend-strength-creature-value")]
	public byte DefendStrengthCreatureValue;

	[Signal("defend-strength-equip-value")]
	public byte DefendStrengthEquipValue;

	[Signal("attack-precise-creature-value")]
	public byte AttackPreciseCreatureValue;

	[Signal("attack-precise-equip-value")]
	public byte AttackPreciseEquipValue;

	[Signal("attack-aoe-pierce-value")]
	public byte AttackAoePierceValue;

	[Signal("attack-aoe-pierce-value-equip")]
	public byte AttackAoePierceValueEquip;

	[Signal("attack-abnormal-hit-base-percent")]
	public byte AttackAbnormalHitBasePercent;

	[Signal("attack-abnormal-hit-value")]
	public byte AttackAbnormalHitValue;

	[Signal("attack-abnormal-hit-equip-value")]
	public byte AttackAbnormalHitEquipValue;

	[Signal("defend-abnormal-dodge-base-percent")]
	public byte DefendAbnormalDodgeBasePercent;

	[Signal("defend-abnormal-dodge-value")]
	public byte DefendAbnormalDodgeValue;

	[Signal("defend-abnormal-dodge-equip-value")]
	public byte DefendAbnormalDodgeEquipValue;

	[Signal("support-power-base-percent")]
	public byte SupportPowerBasePercent;

	[Signal("support-power-value")]
	public byte SupportPowerValue;

	[Signal("support-power-equip-value")]
	public byte SupportPowerEquipValue;

	[Signal("hypermove-power-value")]
	public byte HypermovePowerValue;

	[Signal("hypermove-power-equip-value")]
	public byte HypermovePowerEquipValue;
}