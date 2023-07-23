using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public partial class Item : BaseRecord
{
	[Signal("item-combat"), Repeat(10)]
	public ItemCombat[] ItemCombat;

	public ItemBrand Brand;


	public int Price;

	[Signal("base-fee")]
	public int BaseFee;


	[Signal("game-category-2")]
	public GameCategory2Seq GameCategory2;

	[Signal("game-category-3")]
	public GameCategory3Seq GameCategory3;

	[Signal("market-category-2")]
	public MarketCategory2Seq MarketCategory2;

	[Signal("market-category-3")]
	public MarketCategory3Seq MarketCategory3;



	[Signal("cannot-dispose")]
	public bool CannotDispose;

	[Signal("cannot-sell")]
	public bool CannotSell;

	[Signal("cannot-trade")]
	public bool CannotTrade;

	[Signal("cannot-depot")]
	public bool CannotDepot;

	[Signal("cannot-use-restore-failed-enchant-cost")]
	public bool CannotUseRestoreFailedEnchantCost;

	[Signal("consume-durability")]
	public bool ConsumeDurability;

	public bool Auctionable;

	[Signal("seal-renewal-auctionable")]
	public bool SealRenewalAuctionable;

	[Signal("party-auction-exclusion")]
	public bool PartyAuctionExclusion;

	[Signal("acquire-used")]
	public bool AcquireUsed;

	[Signal("equip-used")]
	public bool EquipUsed;

	[Signal("account-used")]
	public bool AccountUsed;

	[Signal("equip-job-check"), Repeat(5)]
	public JobSeq[] EquipJobCheck;

	public bool CheckEquipJob(JobSeq TargetJob)
	{
		if (TargetJob == JobSeq.JobNone) return true;
		else if (EquipJobCheck[0] == JobSeq.JobNone) return true;

		foreach (var seq in EquipJobCheck)
			if (seq == TargetJob)
				return true;

		return false;
	}


	[Signal("equip-sex")]
	public SexSeq2 EquipSex;

	[Signal("equip-race")]
	public Race EquipRace => Race.Get(this.Attributes["equip-race"].ToEnum<RaceSeq2>());

	[Signal("equip-type")]
	public EquipType EquipType;

	[Signal("equip-faction")]
	public Faction EquipFaction;

	[Signal("equip-faction-level")]
	public short EquipFactionLevel;

	[Signal("item-grade")]
	public byte ItemGrade;



	public LegendGradeBackgroundParticleTypeSeq LegendGradeBackgroundParticleType => this.Attributes["legend-grade-background-particle-type"].ToEnum<LegendGradeBackgroundParticleTypeSeq>();
	public enum LegendGradeBackgroundParticleTypeSeq
	{
		None,

		[Signal("type-gold")]
		TypeGold,

		[Signal("type-redup")]
		TypeRedup,

		[Signal("type-goldup")]
		TypeGoldup,
	}



	public RecycleGroup UseGlobalRecycleGroup => this.Attributes["use-global-recycle-group"].ToEnum<RecycleGroup>();
	public byte UseGlobalRecycleGroupID => this.Attributes["use-global-recycle-group-id"].ToByte();
	public int UseGlobalRecycleGroupDuration => this.Attributes["use-global-recycle-group-duration"].ToInt();
	public RecycleGroup UseRecycleGroup => this.Attributes["use-recycle-group"].ToEnum<RecycleGroup>();
	public byte UseRecycleGroupID => this.Attributes["use-recycle-group-id"].ToByte();
	public int UseRecycleGroupDuration => this.Attributes["use-recycle-group-duration"].ToInt();



	public string ItemSoundMove => this.Attributes["item-sound-move"];

	public MainAbility MainAbility1 => this.Attributes["main-ability-1"].ToEnum<MainAbility>();
	public MainAbility MainAbility2 => this.Attributes["main-ability-2"].ToEnum<MainAbility>();




	public int UsableDuration => this.Attributes["usable-duration"].ToInt();
	public ItemEvent EventInfo => FileCache.Data.ItemEvent[this.Attributes["event-info"]];
	public bool ShowRewardPreview => this.Attributes["show-reward-preview"].ToBool();
	public AccountPostCharge AccountPostCharge => FileCache.Data.AccountPostCharge[this.Attributes["account-post-charge"]];






	public int ImproveId => this.Attributes["improve-id"].ToInt();
	public byte ImproveLevel => this.Attributes["improve-level"].ToByte();
	public string ImproveNextItem => this.Attributes["improve-next-item"];
	public string ImprovePrevItem => this.Attributes["improve-prev-item"];


	public string Name2 => this.Attributes["name2"].GetText();
	public string ItemName => Common.Tag.Link.ItemName.CreateLink(ItemNameOnly , this.Ref);
	public string ItemNameOnly => $"<font name=\"00008130.Program.Fontset_ItemGrade_{this.ItemGrade}\">{this.Name2}</font>";


	public string MainInfo => this.Attributes["main-info"].GetText();
	public string SubInfo => this.Attributes["sub-info"].GetText();
	public string IdentifyMainInfo => this.Attributes["identify-main-info"].GetText();
	public string IdentifySubInfo => this.Attributes["identify-sub-info"].GetText();
	public string IdentifyDescription => this.Attributes["identify-description"].GetText();
	public string Description2 => this.Attributes["description2"].GetText(true);
	public string Description4Title => this.Attributes["description4-title"].GetText(true);
	public string Description5Title => this.Attributes["description5-title"].GetText(true);
	public string Description6Title => this.Attributes["description6-title"].GetText(true);
	public string Description4 => this.Attributes["description4"].GetText(true);
	public string Description5 => this.Attributes["description5"].GetText(true);
	public string Description6 => this.Attributes["description6"].GetText(true);
	public string Description7 => this.Attributes["description7"].GetText(true);

	public int ClosetGroupId => this.Attributes["closet-group-id"].ToInt();


	public Bitmap TagIconGrade => this.Attributes["tag-icon-grade"].GetIcon();

	public Bitmap FrontIcon => this.Attributes["icon"].GetIcon();


	[Signal("hidden-power-attach")]
	public int HiddenPowerAttach;
}