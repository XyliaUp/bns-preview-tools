using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;
public sealed class ClassicFieldZone : BaseRecord, IAttraction
{
	[Signal("zone-1")]
	public string Zone1;

	[Signal("zone-2")]
	public string Zone2;

	public string Group;


	[Signal("ui-filter-attraction-quest-only")]
	public bool UiFilterAttractionQuestOnly;

	[Signal("respawn-confirm-text")]
	public string RespawnConfirmText;

	[Signal("escape-cave-confirm-text")]
	public string EscapeCaveConfirmText;


	[Signal("standard-gear-weapon")]
	public string StandardGearWeapon;

	[Signal("classic-field-zone-name2")]
	public Text ClassicFieldZoneName2;

	[Signal("classic-field-zone-desc")]
	public Text ClassicFieldZoneDesc;

	[Signal("thumbnail-image")]
	public string ThumbnailImage;

	[Signal("reward-summary")]
	public AttractionRewardSummary RewardSummary;

	[Signal("ui-text-grade")]
	public byte UiTextGrade;



	#region Interface
	public string GetName() => this.ClassicFieldZoneName2.GetText();

	public string GetDescribe() => this.ClassicFieldZoneDesc.GetText();
	#endregion
}