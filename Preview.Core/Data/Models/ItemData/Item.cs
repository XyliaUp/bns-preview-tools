using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports.Texture;

using CUE4Parse_Conversion.Textures;

using SkiaSharp;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;

using static Xylia.Preview.Data.Models.Item.Grocery;

namespace Xylia.Preview.Data.Models;
public abstract partial class Item : Record
{
	public string Alias;


	[Repeat(10)]
	public Ref<ItemCombat>[] ItemCombat;

	public Ref<ItemBrand> Brand;


	public int Price;

	[Name("base-fee")]
	public int BaseFee;


	[Name("game-category-2")]
	public GameCategory2Seq GameCategory2;

	[Name("game-category-3")]
	public GameCategory3Seq GameCategory3;

	[Name("market-category-2")]
	public MarketCategory2Seq MarketCategory2;

	[Name("market-category-3")]
	public MarketCategory3Seq MarketCategory3;



	[Name("cannot-dispose")]
	public bool CannotDispose;

	[Name("cannot-sell")]
	public bool CannotSell;

	[Name("cannot-trade")]
	public bool CannotTrade;

	[Name("cannot-depot")]
	public bool CannotDepot;

	[Name("cannot-use-restore-failed-enchant-cost")]
	public bool CannotUseRestoreFailedEnchantCost;

	[Name("consume-durability")]
	public bool ConsumeDurability;

	public bool Auctionable;

	[Name("seal-renewal-auctionable")]
	public bool SealRenewalAuctionable;

	[Name("party-auction-exclusion")]
	public bool PartyAuctionExclusion;

	[Name("acquire-used")]
	public bool AcquireUsed;

	[Name("equip-used")]
	public bool EquipUsed;

	[Name("account-used")]
	public bool AccountUsed;

	[Name("equip-job-check"), Repeat(5)]
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


	[Name("equip-sex")]
	public SexSeq2 EquipSex;

	[Name("equip-race")]
	public Race EquipRace => Race.Get(this.Attributes["equip-race"].ToEnum<RaceSeq2>());

	[Name("equip-type")]
	public EquipType EquipType;

	[Name("equip-faction")]
	public Ref<Faction> EquipFaction;

	[Name("equip-faction-level")]
	public short EquipFactionLevel;

	[Name("item-grade")]
	public sbyte ItemGrade;



	public LegendGradeBackgroundParticleTypeSeq LegendGradeBackgroundParticleType => this.Attributes["legend-grade-background-particle-type"].ToEnum<LegendGradeBackgroundParticleTypeSeq>();
	public enum LegendGradeBackgroundParticleTypeSeq
	{
		None,

		[Name("type-gold")]
		TypeGold,

		[Name("type-redup")]
		TypeRedup,

		[Name("type-goldup")]
		TypeGoldup,
	}



	public RecycleGroup UseGlobalRecycleGroup => this.Attributes["use-global-recycle-group"].ToEnum<RecycleGroup>();
	public sbyte UseGlobalRecycleGroupID => this.Attributes["use-global-recycle-group-id"].ToInt8();
	public Msec UseGlobalRecycleGroupDuration => this.Attributes["use-global-recycle-group-duration"].ToInt32();
	public RecycleGroup UseRecycleGroup => this.Attributes["use-recycle-group"].ToEnum<RecycleGroup>();
	public sbyte UseRecycleGroupID => this.Attributes["use-recycle-group-id"].ToInt8();
	public Msec UseRecycleGroupDuration => this.Attributes.Get<Msec>("use-recycle-group-duration");



	public string ItemSoundMove => this.Attributes["item-sound-move"];

	public MainAbility MainAbility1 => this.Attributes["main-ability-1"].ToEnum<MainAbility>();
	public MainAbility MainAbility2 => this.Attributes["main-ability-2"].ToEnum<MainAbility>();




	public Msec UsableDuration => this.Attributes["usable-duration"].ToInt32();
	public Ref<ItemEvent> EventInfo;
	public bool ShowRewardPreview => this.Attributes["show-reward-preview"].ToBool();
	public Ref<AccountPostCharge> AccountPostCharge;






	public int ImproveId => this.Attributes["improve-id"].ToInt32();
	public sbyte ImproveLevel => this.Attributes["improve-level"].ToInt8();
	public Ref<Item> ImproveNextItem => new(this.Attributes["improve-next-item"]);
	public Ref<Item> ImprovePrevItem => new(this.Attributes["improve-prev-item"]);

	public Ref<Text> Name2;

	public string ItemName => $"<font name=\"00008130.Program.Fontset_ItemGrade_{this.ItemGrade}\">{this.Name2.GetText()}</font>";
	public string ItemNameOnly => this.Name2.GetText();


	public string MainInfo => this.Attributes["main-info"].GetText();
	public string SubInfo => this.Attributes["sub-info"].GetText();
	public string IdentifyMainInfo => this.Attributes["identify-main-info"].GetText();
	public string IdentifySubInfo => this.Attributes["identify-sub-info"].GetText();
	public string IdentifyDescription => this.Attributes["identify-description"].GetText();
	public string Description2 => this.Attributes["description2"].GetText();
	public string Description4Title => this.Attributes["description4-title"].GetText();
	public string Description5Title => this.Attributes["description5-title"].GetText();
	public string Description6Title => this.Attributes["description6-title"].GetText();
	public string Description4 => this.Attributes["description4"].GetText();
	public string Description5 => this.Attributes["description5"].GetText();
	public string Description6 => this.Attributes["description6"].GetText();
	public string Description7 => this.Attributes["description7"].GetText();

	public int ClosetGroupId => this.Attributes["closet-group-id"].ToInt32();


	public SKBitmap TagIconGrade => this.Attributes["tag-icon-grade"].GetIcon();

	public string icon;

	public SKBitmap FrontIcon => icon.GetIcon();
	public SKBitmap Icon => ItemGrade.GetBackground().Compose(FrontIcon);
	public SKBitmap IconExtra
	{
		get
		{
			var bmp = Icon;
			if (bmp is null) return null;


			#region TopLeft
			//SKImage TopLeft = null;
			//var state = CustomDressDesignStateSeq.None;
			//if (this is Costume costume) state = costume.CustomDressDesignState;
			//else if (this is Accessory accessory) state = accessory.CustomDressDesignState;

			//if (state == CustomDressDesignStateSeq.Disabled) TopLeft = Resource_Common.Sewing;
			//else if (state == CustomDressDesignStateSeq.Activated) TopLeft = Resource_Common.Sewing2;

			//if (TopLeft != null) bmp = bmp.Combine(TopLeft, DrawLocation.TopLeft, false);
			#endregion

			#region TopRight
			SKBitmap TopRight = null;

			if (AccountUsed) TopRight = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/SlotItem_privateSale")?.Decode();
			else if (Auctionable) TopRight = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/SlotItem_marketBusiness")?.Decode();

			if (TopRight != null) bmp = bmp.Compose(TopRight);
			#endregion

			#region BottomLeft
			SKBitmap BottomLeft = null;
			if (EventInfo.Instance?.IsExpiration ?? false) 
				BottomLeft = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/unuseable_olditem_3")?.Decode();
			else if (this is Grocery grocery && grocery.GroceryType == GroceryTypeSeq.Sealed)
				BottomLeft = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/Weapon_Lock_04")?.Decode();
			//else BottomLeft = new DecomposeInfo(item).GetExtra();

			if (BottomLeft != null) bmp = bmp.Compose(BottomLeft);
			#endregion

			return bmp;
		}
	}



	[Name("hidden-power-attach")]
	public int HiddenPowerAttach;
}