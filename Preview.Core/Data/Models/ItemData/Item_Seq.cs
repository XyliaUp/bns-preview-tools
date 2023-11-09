using Xylia.Preview.Data.Common.Attribute;

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

		[Name("equip-gem")]
		EquipGem,

		[Name("weapon-gem")]
		WeaponGem,

		[Name("build-up-stone")]
		BuildUpStone,

		Badge,

		Medicine,

		Food,

		Tool,

		Talisman,

		[Name("equip-material")]
		EquipMaterial,

		[Name("union-material")]
		UnionMaterial,

		[Name("dress-material")]
		DressMaterial,

		[Name("etc-material")]
		EtcMaterial,

		Coin,

		Deed,

		Quest,

		[Name("etc-change")]
		EtcChange,

		[Name("weapon-box")]
		WeaponBox,

		[Name("accessory-box")]
		AccessoryBox,

		[Name("dress-box")]
		DressBox,

		[Name("equip-gem-box")]
		EquipGemBox,

		[Name("weapon-gem-box")]
		WeaponGemBox,

		[Name("material-box")]
		MaterialBox,

		[Name("booty-box")]
		BootyBox,

		[Name("etc-box")]
		EtcBox,
	}

	public enum GameCategory3Seq
	{
		None,

		Sword,

		Gauntlet,

		[Name("aura-bangle")]
		AuraBangle,

		Axe,

		Dagger,

		Staff,

		[Name("lyn-sword")]
		LynSword,

		[Name("warlock-dagger")]
		WarlockDagger,

		[Name("soul-fighter-gauntlet")]
		SoulFighterGauntlet,

		Gun,

		[Name("great-sword")]
		GreatSword,

		[Name("long-bow")]
		LongBow,

		Spear,

		Orb,

		[Name("dual-blade")]
		DualBlade,

		Instrument,

		Necklace,

		Ring,

		Earring,

		Bracelet,

		Belt,

		Gloves,

		Soul,

		[Name("soul-2")]
		Soul2,

		[Name("rune-1")]
		Rune1,

		[Name("rune-2")]
		Rune2,

		Pet,

		Nova,

		Vehicle,


		[Name("appearance-chatting")]
		AppearanceChatting,

		[Name("appearance-idle-state")]
		AppearanceIdleState,

		[Name("appearance-portrait")]
		AppearancePortrait,

		[Name("appearance-normal-state")]
		AppearanceNormalState,

		[Name("appearance-hypermove")]
		AppearanceHypermove,

		[Name("appearance-name-plate")]
		AppearanceNamePlate,

		[Name("appearance-speech-bubble")]
		AppearanceSpeechBubble,


		Costume,

		[Name("head-attach")]
		HeadAttach,

		[Name("face-attach")]
		FaceAttach,

		[Name("costume-attach")]
		CostumeAttach,

		[Name("summoned-pet-costume")]
		SummonedPetCostume,

		[Name("summoned-pet-hat")]
		SummonedPetHat,

		[Name("summoned-pet-attach")]
		SummonedPetAttach,

		Gam1,

		Gan2,

		Jin3,

		Son4,

		Ri5,

		Gon6,

		Tae7,

		Geon8,

		[Name("synthesis-gem")]
		SynthesisGem,

		[Name("feed-gem")]
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

		[Name("ruby-topaz")]
		RubyTopaz,

		[Name("ruby-sapphire")]
		RubySapphire,

		[Name("ruby-jade")]
		RubyJade,

		[Name("ruby-amethyst")]
		RubyAmethyst,

		[Name("ruby-emerald")]
		RubyEmerald,

		[Name("ruby-diamond")]
		RubyDiamond,

		[Name("topaz-sapphire")]
		TopazSapphire,

		[Name("topaz-jade")]
		TopazJade,

		[Name("topaz-amethyst")]
		TopazAmethyst,

		[Name("topaz-emerald")]
		TopazEmerald,

		[Name("topaz-diamond")]
		TopazDiamond,

		[Name("sapphire-jade")]
		SapphireJade,

		[Name("sapphire-amethyst")]
		SapphireAmethyst,

		[Name("sapphire-emerald")]
		SapphireEmerald,

		[Name("sapphire-diamond")]
		SapphireDiamond,

		[Name("jade-amethyst")]
		JadeAmethyst,

		[Name("jade-emerald")]
		JadeEmerald,

		[Name("jade-diamond")]
		JadeDiamond,

		[Name("amethyst-emerald")]
		AmethystEmerald,

		[Name("amethyst-diamond")]
		AmethystDiamond,

		[Name("emerald-diamond")]
		EmeraldDiamond,

		[Name("aquamarine-diamond")]
		AquamarineDiamond,

		[Name("amber-diamond")]
		AmberDiamond,

		[Name("obsidian-garnet")]
		ObsidianGarnet,

		[Name("corundum-white")]
		CorundumWhite,

		[Name("corundum-black")]
		CorundumBlack,

		[Name("corundum-pink")]
		CorundumPink,

		[Name("corundum-yellow")]
		CorundumYellow,

		[Name("corundum-bluegreen")]
		CorundumBluegreen,

		[Name("corundum-blue")]
		CorundumBlue,

		[Name("corundum-aquamarine")]
		CorundumAquamarine,

		[Name("corundum-amber")]
		CorundumAmber,

		[Name("corundum-ruby")]
		CorundumRuby,

		[Name("corundum-amethyst")]
		CorundumAmethyst,

		[Name("corundum-jade")]
		CorundumJade,

		[Name("skill-stone")]
		SkillStone,

		[Name("skill-stone-1")]
		SkillStone1,

		[Name("skill-stone-2")]
		SkillStone2,

		[Name("skill-stone-3")]
		SkillStone3,

		[Name("regenerate-potion")]
		RegeneratePotion,

		[Name("heal-potion")]
		HealPotion,

		[Name("secret-potion")]
		SecretPotion,

		[Name("detox-potion")]
		DetoxPotion,

		[Name("magic-potion")]
		MagicPotion,

		[Name("hwan-dan")]
		HwanDan,

		Cook,

		[Name("exp-cook")]
		ExpCook,

		Alcohol,

		[Name("normal-repair-tool")]
		NormalRepairTool,

		[Name("urgency-repair-tool")]
		UrgencyRepairTool,

		Key,

		[Name("weapon-gem-make")]
		WeaponGemMake,

		[Name("festival-tool")]
		FestivalTool,

		[Name("fishing-goods")]
		FishingGoods,

		[Name("reset-talisman")]
		ResetTalisman,

		[Name("revive-talisman")]
		ReviveTalisman,

		[Name("party-revive-talisman")]
		PartyReviveTalisman,

		[Name("growth-talisman")]
		GrowthTalisman,

		[Name("unseal-talisman")]
		UnsealTalisman,

		[Name("seal-talisman")]
		SealTalisman,

		[Name("escape-talisman")]
		EscapeTalisman,

		[Name("build-up-talisman")]
		BuildUpTalisman,

		Valuables,

		Wealth,

		[Name("holy-material")]
		HolyMaterial,

		[Name("weapon-material")]
		WeaponMaterial,

		[Name("party-battle-material")]
		PartyBattleMaterial,

		[Name("raid-material")]
		RaidMaterial,

		[Name("weapon-seed-material")]
		WeaponSeedMaterial,

		[Name("accessory-material")]
		AccessoryMaterial,

		[Name("synthetic-material")]
		SyntheticMaterial,

		[Name("weapon-exp")]
		WeaponExp,

		[Name("accessory-exp")]
		AccessoryExp,

		[Name("weapon-maker")]
		WeaponMaker,

		[Name("talis-maker")]
		TalisMaker,

		[Name("equip-gem-maker")]
		EquipGemMaker,

		[Name("accessory-maker")]
		AccessoryMaker,

		[Name("medicine-maker")]
		MedicineMaker,

		[Name("food-maker")]
		FoodMaker,

		[Name("common-maker")]
		CommonMaker,

		[Name("hypermove-material")]
		HypermoveMaterial,

		[Name("production-material")]
		ProductionMaterial,

		Cloth,

		[Name("dress-design")]
		DressDesign,

		[Name("color-material")]
		ColorMaterial,

		Pattern,

		[Name("special-material")]
		SpecialMaterial,

		[Name("normal-material")]
		NormalMaterial,

		[Name("weapon-coin")]
		WeaponCoin,

		Token,

		[Name("naryu-coin")]
		NaryuCoin,

		[Name("pvp-coin")]
		PvpCoin,

		[Name("rvr-coin")]
		RvrCoin,

		[Name("festival-coin")]
		FestivalCoin,

		[Name("hero-coin")]
		HeroCoin,

		[Name("spirit-coin")]
		SpiritCoin,

		[Name("normal-coin")]
		NormalCoin,

		[Name("skill-deed")]
		SkillDeed,

		[Name("skill-take-deed")]
		SkillTakeDeed,

		Ticket,

		[Name("reset-deed")]
		ResetDeed,

		[Name("extend-deed")]
		ExtendDeed,

		[Name("exchange-deed")]
		ExchangeDeed,

		[Name("switch-deed")]
		SwitchDeed,

		[Name("normal-deed")]
		NormalDeed,

		[Name("guild-deed")]
		GuildDeed,

		[Name("quest-start")]
		QuestStart,

		[Name("quest-virtual")]
		QuestVirtual,

		[Name("chack-item")]
		ChackItem,

		[Name("sundry-item")]
		SundryItem,

		[Name("normal-weapon-box")]
		NormalWeaponBox,

		[Name("shape-weapon-box")]
		ShapeWeaponBox,

		[Name("normal-accessory-box")]
		NormalAccessoryBox,

		[Name("normal-dress-box")]
		NormalDressBox,

		[Name("normal-equip-gem-box")]
		NormalEquipGemBox,

		[Name("normal-weapon-gem-box")]
		NormalWeaponGemBox,

		[Name("normal-material-box")]
		NormalMaterialBox,

		[Name("normal-booty-box")]
		NormalBootyBox,

		[Name("normal-etc-box")]
		NormalEtcBox,

		[Name("badge-1-premium")]
		Badge1Premium,

		[Name("badge-2-premium")]
		Badge2Premium,

		[Name("badge-3-premium")]
		Badge3Premium,

		[Name("badge-1-normal")]
		Badge1Normal,

		[Name("badge-2-normal")]
		Badge2Normal,

		[Name("badge-3-normal")]
		Badge3Normal,

		[Name("badge-appearance")]
		BadgeAppearance,

		[Name("fusion-material")]
		FusionMaterial,

		Card,
	}
	#endregion

	#region MarketCategory
	public enum MarketCategorySeq
	{
		None,

		Equip,

		Enchant,

		Consumable,

		Production,

		Exchange,

		Box,
	}

	public enum MarketCategory2Seq
	{
		None,

		Weapon,

		[Name("equip-gem")]
		EquipGem,

		Accessory,

		Dress,

		[Name("weapon-gem")]
		WeaponGem,

		Medicine,

		Food,

		[Name("build-up-stone")]
		BuildUpStone,

		Talisman,

		Tool,

		[Name("equip-material")]
		EquipMaterial,

		[Name("union-material")]
		UnionMaterial,

		[Name("dress-material")]
		DressMaterial,

		[Name("etc-material")]
		EtcMaterial,

		Coin,

		Deed,

		Quest,

		[Name("etc-change")]
		EtcChange,

		[Name("etc-box")]
		EtcBox,

		Badge,
	}

	public enum MarketCategory3Seq
	{
		None,

		Sword,

		Gauntlet,

		Axe,

		Staff,

		[Name("aura-bangle")]
		AuraBangle,

		Dagger,

		Gam1,

		Gan2,

		Jin3,

		Son4,

		Ri5,

		Gon6,

		Tae7,

		Geon8,

		[Name("great-sword")]
		GreatSword,

		Ring,

		Earring,

		Necklace,

		Costume,

		[Name("head-attach")]
		HeadAttach,

		[Name("face-attach")]
		FaceAttach,

		[Name("costume-attach")]
		CostumeAttach,

		[Name("summoned-pet-costume")]
		SummonedPetCostume,

		[Name("summoned-pet-hat")]
		SummonedPetHat,

		[Name("summoned-pet-attach")]
		SummonedPetAttach,

		Ruby,

		Topaz,

		Sapphire,

		Jade,

		Amethyst,

		Emerald,

		Diamond,

		[Name("regenerate-potion")]
		RegeneratePotion,

		[Name("heal-potion")]
		HealPotion,

		[Name("secret-potion")]
		SecretPotion,

		[Name("detox-potion")]
		DetoxPotion,

		[Name("lyn-sword")]
		LynSword,

		[Name("warlock-dagger")]
		WarlockDagger,

		[Name("soul-fighter-gauntlet")]
		SoulFighterGauntlet,

		Gun,

		Cook,

		Alcohol,

		[Name("normal-repair-tool")]
		NormalRepairTool,

		[Name("urgency-repair-tool")]
		UrgencyRepairTool,

		[Name("unseal-talisman")]
		UnsealTalisman,

		[Name("revive-talisman")]
		ReviveTalisman,

		[Name("escape-talisman")]
		EscapeTalisman,

		[Name("feed-gem")]
		FeedGem,

		[Name("weapon-gem-etc")]
		WeaponGemEtc,

		[Name("long-bow")]
		LongBow,

		[Name("magic-potion")]
		MagicPotion,

		Key,

		[Name("hwan-dan")]
		HwanDan,

		[Name("exp-cook")]
		ExpCook,

		[Name("weapon-gem-make")]
		WeaponGemMake,

		[Name("festival-tool")]
		FestivalTool,

		[Name("sundry-item")]
		SundryItem,

		[Name("reset-talisman")]
		ResetTalisman,

		[Name("party-revive-talisman")]
		PartyReviveTalisman,

		[Name("weapon-maker")]
		WeaponMaker,

		[Name("talis-maker")]
		TalisMaker,

		[Name("equip-gem-maker")]
		EquipGemMaker,

		[Name("accessory-maker")]
		AccessoryMaker,

		[Name("medicine-maker")]
		MedicineMaker,

		[Name("food-maker")]
		FoodMaker,

		[Name("growth-talisman")]
		GrowthTalisman,

		[Name("build-up-talisman")]
		BuildUpTalisman,

		Valuables,

		Wealth,

		[Name("growth-material")]
		GrowthMaterial,

		[Name("holy-material")]
		HolyMaterial,

		[Name("common-maker")]
		CommonMaker,

		[Name("special-material")]
		SpecialMaterial,

		[Name("hypermove-material")]
		HypermoveMaterial,

		[Name("normal-material")]
		NormalMaterial,

		[Name("production-material")]
		ProductionMaterial,

		Cloth,

		Obsidian,

		Amber,

		Garnet,

		Aquamarine,

		Belt,

		Bracelet,

		[Name("seal-talisman")]
		SealTalisman,

		[Name("dress-design")]
		DressDesign,

		[Name("fishing-goods")]
		FishingGoods,

		[Name("badge-1")]
		Badge1,

		[Name("weapon-gem-guardian")]
		WeaponGemGuardian,

		Spear,

		Orb,

		[Name("ruby-diamond")]
		RubyDiamond,

		Vehicle,

		Card,

		[Name("dual-blade")]
		DualBlade,

		Glyph,

		[Name("topaz-diamond")]
		TopazDiamond,

		[Name("void-10")]
		Void10,

		[Name("void-11")]
		Void11,

		[Name("void-12")]
		Void12,

		[Name("sapphire-diamond")]
		SapphireDiamond,

		[Name("void-13")]
		Void13,

		[Name("void-14")]
		Void14,

		[Name("jade-diamond")]
		JadeDiamond,

		[Name("void-15")]
		Void15,

		[Name("amethyst-diamond")]
		AmethystDiamond,

		[Name("emerald-diamond")]
		EmeraldDiamond,

		Soul,

		[Name("soul-2")]
		Soul2,

		Gloves,

		[Name("pet-1")]
		Pet1,

		Nova,

		[Name("color-material")]
		ColorMaterial,

		[Name("rune-1")]
		Rune1,

		[Name("rune-2")]
		Rune2,

		[Name("aquamarine-diamond")]
		AquamarineDiamond,

		Pattern,

		[Name("normal-coin")]
		NormalCoin,

		[Name("skill-take-deed")]
		SkillTakeDeed,

		[Name("normal-deed")]
		NormalDeed,

		[Name("quest-start")]
		QuestStart,

		[Name("chack-item")]
		ChackItem,

		[Name("normal-etc-box")]
		NormalEtcBox,

		[Name("skill-stone-1")]
		SkillStone1,

		[Name("skill-stone-2")]
		SkillStone2,

		[Name("amber-diamond")]
		AmberDiamond,

		[Name("obsidian-garnet")]
		ObsidianGarnet,

		NamePlate,

		SpeechBubble,

		Instrument,
	}

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
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.NamePlate);
		data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.SpeechBubble);
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