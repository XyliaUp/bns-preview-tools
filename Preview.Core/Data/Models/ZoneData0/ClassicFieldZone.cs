using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public sealed class ClassicFieldZone : Record, IAttraction
{
	public string Alias;



	[Repeat(2)]
	public Ref<Zone>[] Zone;

	public Ref<AttractionGroup> Group;

	[Repeat(5)]
	public Ref<Quest>[] AttractionQuest;

	public bool UiFilterAttractionQuestOnly;

	public Ref<Text> RespawnConfirmText;

	public Ref<Text> EscapeCaveConfirmText;

	public short RecommendAttackPower;

	public Ref<Item> StandardGearWeapon;

	public Ref<Text> ClassicFieldZoneName2;
	public Ref<Text> ClassicFieldZoneDesc;

	public string ThumbnailImage;

	public Ref<AttractionRewardSummary> RewardSummary;

	public sbyte UiTextGrade;

	public Ref<Text> Tactic;

	public Ref<ContentsJournalRecommendItem> RecommendAlias;

	public sbyte RecommendLevelMin;
	public sbyte RecommendLevelMax;
	public sbyte RecommendMasteryLevelMin;
	public sbyte RecommendMasteryLevelMax;



	#region Interface
	public override string GetText => this.ClassicFieldZoneName2.GetText();

	public string GetDescribe() => this.ClassicFieldZoneDesc.GetText();
	#endregion
}