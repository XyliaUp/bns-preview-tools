using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public class ItemBuyPrice : BaseRecord
{
	public int Money;

	[Signal("required-itembrand")]
	public ItemBrand RequiredItembrand;

	[Signal("required-itembrand-condition-type")]
	public ConditionType RequiredItembrandConditionType = ConditionType.All;


	[Signal("required-item"), Repeat(4)]
	public Item[] RequiredItem;

	[Signal("required-item-count"), Repeat(4)]
	public short[] RequiredItemCount;


	[Signal("required-faction-score")]
	public int RequiredFactionScore;

	[Signal("required-duel-point")]
	public int RequiredDuelPoint;

	[Signal("required-party-battle-point")]
	public int RequiredPartyBattlePoint;

	[Signal("required-field-play-point")]
	public int RequiredFieldPlayPoint;

	[Signal("required-life-contents-point")]
	public int RequiredLifeContentsPoint;

	[Signal("required-achievement-score")]
	public int RequiredAchievementScore;

	[Signal("required-achievement-id")]
	public int RequiredAchievementId;

	[Signal("required-achievement-step-min")]
	public short RequiredAchievementStepMin;

	[Signal("faction-level")]
	public short FactionLevel;

	[Signal("check-solo-duel-grade")]
	public byte CheckSoloDuelGrade;

	[Signal("check-team-duel-grade")]
	public byte CheckTeamDuelGrade;

	[Signal("check-battle-field-grade-occupation-war")]
	public byte CheckBattleFieldGradeOccupationWar;

	[Signal("check-battle-field-grade-capture-the-flag")]
	public byte CheckBattleFieldGradeCaptureTheFlag;

	[Signal("check-battle-field-grade-lead-the-ball")]
	public byte CheckBattleFieldGradeLeadTheBall;

	[Signal("check-closet-collecting-grade")]
	public byte CheckClosetCollectingGrade;

	[Signal("check-content-quota")]
	public ContentQuota CheckContentQuota;

	[Signal("check-soul-boost-season-bm")]
	public int CheckSoulBoostSeasonBm;

	[Signal("required-level")]
	public byte RequiredLevel;

	[Signal("required-mastery-level")]
	public byte RequiredMasteryLevel;

	[Signal("required-account-level")]
	public short RequiredAccountLevel;
}