using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;
public sealed class FactionBattleFieldZone : BaseRecord, IAttraction
{
	public string Zone;

	public string Group;

	[Signal("ui-filter-attraction-quest-only")]
	public bool UiFilterAttractionQuestOnly;

	[Signal("respawn-confirm-text")]
	public Text RespawnConfirmText;

	[Signal("required-level")]
	public sbyte RequiredLevel;

	[Signal("required-faction-level")]
	public sbyte RequiredFactionLevel;

	[Signal("faction-battle-field-zone-name2")]
	public Text FactionBattleFieldZoneName2;

	[Signal("faction-battle-field-zone-desc")]
	public Text FactionBattleFieldZoneDesc;

	[Signal("thumbnail-image")]
	public string ThumbnailImage;

	[Signal("reward-summary")]
	public AttractionRewardSummary RewardSummary;


	#region Interface
	public string GetName() => this.FactionBattleFieldZoneName2.GetText();

	public string GetDescribe() => this.FactionBattleFieldZoneDesc.GetText();
	#endregion
}