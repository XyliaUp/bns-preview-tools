using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class GameMessage : Record
{
	public string Alias;


	public CategorySeq Category;
	public enum CategorySeq
	{
		None,
		ChatSight,
		ChatParty,
		ChatTeam,
		ChatTeamLeader,
		ChatOne,
		ChatInDomain,
		ChatFaction,
		ChatGuild,
		ChatGuildManager,
		ChatPartySearch,
		ChatNpc,
		ChatGodsayNormal,
		ChatGodsayCampaign,
		ChatGodsayEmergency,
		ChatFieldDungeon,
		ChatQqC2c,
		ChatQqGroup,
		ChatGuildSearch,
		ChatWatch,
		Default,
		Warning,
		Info,
		Party,
		PartyMatch,
		Team,
		Faction,
		Guild,
		GuildMatch,
		Exhaustion,
		ExhaustionPc,
		ExpAcquisition,
		ExpLoss,
		Levelup,
		MoneyAcquisition,
		MoneyLoss,
		ItemAcquisition,
		ItemLoss,
		SkillBuildUpPointAcquisition,
		QuestAcquisition,
		QuestComplete,
		TalkSocial,
		FieldDungeon,
		Qq,
		CombatNormal,
		CombatCritical,
		CombatHeal,
		CombatDefend,
		CombatParry,
		CombatAbnormal,
		CombatAttackedNormal,
		CombatAttackedCritical,
		CombatTargetHeal,
		CombatTargetDefend,
		CombatTargetAbnormal,
		CombatOtherNormal,
		CombatOtherCritical,
		CombatOtherHeal,
		CombatOtherDefend,
		CombatOtherAbnormal,
		CombatPartyNormal,
		CombatPartyCritical,
		CombatPartyHeal,
		CombatPartyDefend,
		CombatPartyAbnormal,
		CombatPartyAttackedNormal,
		CombatPartyAttackedCritical,
		CombatPartyTargetDefend,
		Mentoring,
		Skill,
	}

	public Ref<Text> Text;
	public bool Chatting;
	public ObjectPath ChattingFontset;
	public bool Headline2;
	public ObjectPath Headline2Fontset;
	public Ref<Text> HeadlineText;
	public ObjectPath HeadlineFontset;
	public ObjectPath HeadlineParticle;
	public Ref<Text> BossHeadlineText;
	public ObjectPath BossHeadlineFontset;
	public SoundTrackSeq SoundTrack;
	public enum SoundTrackSeq
	{
		None,
		N1,
		N2,
	}

	public bool StopPreviousTrackSound;
	public bool DuplicationCheck;
	public ObjectPath Sound;
	public SoundTypeSeq SoundType;
	public enum SoundTypeSeq
	{
		None,
		Voice,
		Effect,
	}

	public ObjectPath Sound2;
	public Sound2TypeSeq Sound2Type;
	public enum Sound2TypeSeq
	{
		None,
		Voice,
		Effect,
	}


	public GuildBattleTypeSeq GuildBattleType;
	public enum GuildBattleTypeSeq
	{
		None,
		N1,
		N2,
		N3,
		N4,
		N5,
	}


	public Ref<Text> GuildBattleText;

	public GhostModeTypeSeq GhostModeType;
	public enum GhostModeTypeSeq
	{
		None,
		Normal,
		Ghost,
	}
}