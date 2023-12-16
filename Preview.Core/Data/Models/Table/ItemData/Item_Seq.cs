using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public partial class Item
{
	#region GameCategory
	public enum GameCategorySeq
	{
		None,

		Equip,

		Enchantm,

		Consumable,

		Production,

		Exchange,

		Box,
	}

	public enum GameCategory2Seq
	{
		None,

		Weapon,

		Accessory,

		Dress,

		EquipGem,

		WeaponGem,

		BuildUpStone,

		Badge,

		Medicine,

		Food,

		Tool,

		Talisman,

		EquipMaterial,

		UnionMaterial,

		DressMaterial,

		EtcMaterial,

		Coin,

		Deed,

		Quest,

		EtcChange,

		WeaponBox,

		AccessoryBox,

		DressBox,

		EquipGemBox,

		WeaponGemBox,

		MaterialBox,

		BootyBox,

		EtcBox,
	}

	public enum GameCategory3Seq
	{
		None,

		Sword,

		Gauntlet,

		AuraBangle,

		Axe,

		Dagger,

		Staff,

		LynSword,

		WarlockDagger,

		SoulFighterGauntlet,

		Gun,

		GreatSword,

		LongBow,

		Spear,

		Orb,

		DualBlade,

		Instrument,

		Necklace,

		Ring,

		Earring,

		Bracelet,

		Belt,

		Gloves,

		Soul,

		Soul2,

		Rune1,

		Rune2,

		Pet,

		Nova,

		Vehicle,


		AppearanceChatting,

		AppearanceIdleState,

		AppearancePortrait,

		AppearanceNormalState,

		AppearanceHypermove,

		AppearanceNamePlate,

		AppearanceSpeechBubble,


		Costume,

		HeadAttach,

		FaceAttach,

		CostumeAttach,

		SummonedPetCostume,

		SummonedPetHat,

		SummonedPetAttach,

		Gam1,

		Gan2,

		Jin3,

		Son4,

		Ri5,

		Gon6,

		Tae7,

		Geon8,

		SynthesisGem,

		FeedGem,

		Diamond,

		Ruby,

		Topaz,

		Sapphire,

		Jade,

		Emerald,

		Amethyst,

		Aquamarine,

		Amber,

		Obsidian,

		Garnet,

		RubyTopaz,

		RubySapphire,

		RubyJade,

		RubyAmethyst,

		RubyEmerald,

		RubyDiamond,

		TopazSapphire,

		TopazJade,

		TopazAmethyst,

		TopazEmerald,

		TopazDiamond,

		SapphireJade,

		SapphireAmethyst,

		SapphireEmerald,

		SapphireDiamond,

		JadeAmethyst,

		JadeEmerald,

		JadeDiamond,

		AmethystEmerald,

		AmethystDiamond,

		EmeraldDiamond,

		AquamarineDiamond,

		AmberDiamond,

		ObsidianGarnet,

		CorundumWhite,

		CorundumBlack,

		CorundumPink,

		CorundumYellow,

		CorundumBluegreen,

		CorundumBlue,

		CorundumAquamarine,

		CorundumAmber,

		CorundumRuby,

		CorundumAmethyst,

		CorundumJade,

		SkillStone,

		SkillStone1,

		SkillStone2,

		SkillStone3,

		RegeneratePotion,

		HealPotion,

		SecretPotion,

		DetoxPotion,

		MagicPotion,

		HwanDan,

		Cook,

		ExpCook,

		Alcohol,

		NormalRepairTool,

		UrgencyRepairTool,

		Key,

		WeaponGemMake,

		FestivalTool,

		FishingGoods,

		ResetTalisman,

		ReviveTalisman,

		PartyReviveTalisman,

		GrowthTalisman,

		UnsealTalisman,

		SealTalisman,

		EscapeTalisman,

		BuildUpTalisman,

		Valuables,

		Wealth,

		HolyMaterial,

		WeaponMaterial,

		PartyBattleMaterial,

		RaidMaterial,

		WeaponSeedMaterial,

		AccessoryMaterial,

		SyntheticMaterial,

		WeaponExp,

		AccessoryExp,

		WeaponMaker,

		TalisMaker,

		EquipGemMaker,

		AccessoryMaker,

		MedicineMaker,

		FoodMaker,

		CommonMaker,

		HypermoveMaterial,

		ProductionMaterial,

		Cloth,

		DressDesign,

		ColorMaterial,

		Pattern,

		SpecialMaterial,

		NormalMaterial,

		WeaponCoin,

		Token,

		NaryuCoin,

		PvpCoin,

		RvrCoin,

		FestivalCoin,

		HeroCoin,

		SpiritCoin,

		NormalCoin,

		SkillDeed,

		SkillTakeDeed,

		Ticket,

		ResetDeed,

		ExtendDeed,

		ExchangeDeed,

		SwitchDeed,

		NormalDeed,

		GuildDeed,

		QuestStart,

		QuestVirtual,

		ChackItem,

		SundryItem,

		NormalWeaponBox,

		ShapeWeaponBox,

		NormalAccessoryBox,

		NormalDressBox,

		NormalEquipGemBox,

		NormalWeaponGemBox,

		NormalMaterialBox,

		NormalBootyBox,

		NormalEtcBox,

		Badge1Premium,

		Badge2Premium,

		Badge3Premium,

		Badge1Normal,

		Badge2Normal,

		Badge3Normal,

		BadgeAppearance,

		FusionMaterial,

		Card,
	}
	#endregion

	#region MarketCategory
	public static Dictionary<MarketCategory2Seq, List<MarketCategory3Seq>> MarketCategory2Group()
	{
		var data = new Dictionary<MarketCategory2Seq, List<MarketCategory3Seq>>();
		Array.ForEach(Enum.GetValues<MarketCategory2Seq>(), seq => data[seq] = new());

		#region Weapon
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Sword);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Gauntlet);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Axe);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Staff);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.AuraBangle);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Dagger);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.LynSword);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.WarlockDagger);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.SoulFighterGauntlet);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Gun);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.LongBow);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.GreatSword);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Orb);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.DualBlade);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Instrument);
		data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Spear);
		#endregion

		#region	EquipGem
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Gam1);
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Gan2);
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Jin3);
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Son4);
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Ri5);
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Gon6);
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Tae7);
		data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Geon8);
		#endregion

		#region	Accessory
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Ring);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Earring);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Necklace);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Belt);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Bracelet);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Soul);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Soul2);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Gloves);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Pet1);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Nova);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Rune1);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Rune2);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Vehicle);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.NamePlateAppearance);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.SpeechBubbleAppearance);
		#endregion

		#region	Dress
		data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.Costume);
		data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.HeadAttach);
		data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.FaceAttach);
		data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.CostumeAttach);
		data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.SummonedPetCostume);
		data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.SummonedPetHat);
		data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.SummonedPetAttach);
		#endregion

		#region	WeaponGem
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Ruby);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Topaz);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Sapphire);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Jade);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Amethyst);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Emerald);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Diamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Obsidian);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Amber);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Garnet);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Aquamarine);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.RubyDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.TopazDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.SapphireDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.JadeDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.AmethystDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.EmeraldDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.AquamarineDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.AmberDiamond);
		data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.ObsidianGarnet);
		//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void10);
		//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void11);
		//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void12);
		//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void13);
		//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void14);
		//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void15);
		#endregion

		#region	Medicine
		data[MarketCategory2Seq.Medicine].Add(MarketCategory3Seq.RegeneratePotion);
		data[MarketCategory2Seq.Medicine].Add(MarketCategory3Seq.HealPotion);
		data[MarketCategory2Seq.Medicine].Add(MarketCategory3Seq.SecretPotion);
		data[MarketCategory2Seq.Medicine].Add(MarketCategory3Seq.DetoxPotion);
		data[MarketCategory2Seq.Medicine].Add(MarketCategory3Seq.MagicPotion);
		data[MarketCategory2Seq.Medicine].Add(MarketCategory3Seq.HwanDan);
		#endregion

		#region	Food
		data[MarketCategory2Seq.Food].Add(MarketCategory3Seq.Cook);
		data[MarketCategory2Seq.Food].Add(MarketCategory3Seq.Alcohol);
		data[MarketCategory2Seq.Food].Add(MarketCategory3Seq.ExpCook);

		#endregion

		#region	BuildUpStone
		data[MarketCategory2Seq.BuildUpStone].Add(MarketCategory3Seq.SkillStone1);
		data[MarketCategory2Seq.BuildUpStone].Add(MarketCategory3Seq.SkillStone2);
		#endregion

		#region	Talisman	   			
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.ReviveTalisman);
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.EscapeTalisman);
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.PartyReviveTalisman);
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.ResetTalisman);
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.GrowthTalisman);
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.BuildUpTalisman);
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.SealTalisman);
		data[MarketCategory2Seq.Talisman].Add(MarketCategory3Seq.UnsealTalisman);
		#endregion

		#region	Tool
		data[MarketCategory2Seq.Tool].Add(MarketCategory3Seq.FestivalTool);
		data[MarketCategory2Seq.Tool].Add(MarketCategory3Seq.NormalRepairTool);
		data[MarketCategory2Seq.Tool].Add(MarketCategory3Seq.UrgencyRepairTool);
		data[MarketCategory2Seq.Tool].Add(MarketCategory3Seq.Key);
		data[MarketCategory2Seq.Tool].Add(MarketCategory3Seq.WeaponGemMake);
		data[MarketCategory2Seq.Tool].Add(MarketCategory3Seq.FishingGoods);
		data[MarketCategory2Seq.Tool].Add(MarketCategory3Seq.Card);
		#endregion

		#region	EquipMaterial
		data[MarketCategory2Seq.EquipMaterial].Add(MarketCategory3Seq.GrowthMaterial);
		data[MarketCategory2Seq.EquipMaterial].Add(MarketCategory3Seq.HolyMaterial);
		#endregion

		#region	UnionMaterial
		data[MarketCategory2Seq.UnionMaterial].Add(MarketCategory3Seq.ProductionMaterial);
		data[MarketCategory2Seq.UnionMaterial].Add(MarketCategory3Seq.HypermoveMaterial);
		#endregion

		#region	DressMaterial
		data[MarketCategory2Seq.DressMaterial].Add(MarketCategory3Seq.ColorMaterial);
		data[MarketCategory2Seq.DressMaterial].Add(MarketCategory3Seq.Cloth);
		#endregion

		#region	EtcMaterial
		data[MarketCategory2Seq.EtcMaterial].Add(MarketCategory3Seq.SpecialMaterial);
		data[MarketCategory2Seq.EtcMaterial].Add(MarketCategory3Seq.NormalMaterial);
		#endregion

		#region	Coin

		#endregion

		#region	Deed
		data[MarketCategory2Seq.Deed].Add(MarketCategory3Seq.NormalDeed);
		data[MarketCategory2Seq.Deed].Add(MarketCategory3Seq.SkillTakeDeed);
		#endregion

		#region	Quest

		#endregion

		#region	EtcChange
		data[MarketCategory2Seq.EtcChange].Add(MarketCategory3Seq.SundryItem);
		data[MarketCategory2Seq.EtcChange].Add(MarketCategory3Seq.ChackItem);
		#endregion

		#region	EtcBox
		data[MarketCategory2Seq.EtcBox].Add(MarketCategory3Seq.NormalEtcBox);
		#endregion

		#region	Badge

		#endregion

		return data;
	}
	#endregion



	public enum RaceSeq2
	{
		RaceNone,

		All,

		Jin,

		Gon,

		Lyn,

		Kun,

		SummonedAll,

		SummonedCat,
	}

	public enum SexSeq2
	{
		SexNone,

		All,

		Male,

		Female,
	}


	public enum CustomDressDesignStateSeq
	{
		None,
		Disabled,
		Activated,
	}

	public enum WeaponTypeSeq
	{
		None,
		BareHand,
		Sword,
		Gauntlet,
		AuraBangle,
		Pistol,
		Rifle,
		TwoHandedAxe,
		Bow,
		Staff,
		Dagger,
		Pet1,
		Pet2,
		Gun,
		GreatSword,
		LongBow,
		Spear,
		Orb,
		DualBlade,
	}
}