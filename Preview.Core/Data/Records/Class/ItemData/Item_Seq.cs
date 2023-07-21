/// 枚举支持
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
public partial class Item
{
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

		[Signal("equip-gem")]
		EquipGem,

		[Signal("weapon-gem")]
		WeaponGem,

		[Signal("build-up-stone")]
		BuildUpStone,

		Badge,

		Medicine,

		Food,

		Tool,

		Talisman,

		[Signal("equip-material")]
		EquipMaterial,

		[Signal("union-material")]
		UnionMaterial,

		[Signal("dress-material")]
		DressMaterial,

		[Signal("etc-material")]
		EtcMaterial,

		Coin,

		Deed,

		Quest,

		[Signal("etc-change")]
		EtcChange,

		[Signal("weapon-box")]
		WeaponBox,

		[Signal("accessory-box")]
		AccessoryBox,

		[Signal("dress-box")]
		DressBox,

		[Signal("equip-gem-box")]
		EquipGemBox,

		[Signal("weapon-gem-box")]
		WeaponGemBox,

		[Signal("material-box")]
		MaterialBox,

		[Signal("booty-box")]
		BootyBox,

		[Signal("etc-box")]
		EtcBox,
	}

	public enum GameCategory3Seq
	{
		None,

		Sword,

		Gauntlet,

		[Signal("aura-bangle")]
		AuraBangle,

		Axe,

		Dagger,

		Staff,

		[Signal("lyn-sword")]
		LynSword,

		[Signal("warlock-dagger")]
		WarlockDagger,

		[Signal("soul-fighter-gauntlet")]
		SoulFighterGauntlet,

		Gun,

		[Signal("great-sword")]
		GreatSword,

		[Signal("long-bow")]
		LongBow,

		Spear,

		Orb,

		[Signal("dual-blade")]
		DualBlade,

		Instrument,

		Necklace,

		Ring,

		Earring,

		Bracelet,

		Belt,

		Gloves,

		Soul,

		[Signal("soul-2")]
		Soul2,

		[Signal("rune-1")]
		Rune1,

		[Signal("rune-2")]
		Rune2,

		Pet,

		Nova,

		Vehicle,


		[Signal("appearance-chatting")]
		AppearanceChatting,

		[Signal("appearance-idle-state")]
		AppearanceIdleState,

		[Signal("appearance-portrait")]
		AppearancePortrait,

		[Signal("appearance-normal-state")]
		AppearanceNormalState,

		[Signal("appearance-hypermove")]
		AppearanceHypermove,

		[Signal("appearance-name-plate")]
		AppearanceNamePlate,

		[Signal("appearance-speech-bubble")]
		AppearanceSpeechBubble,


		Costume,

		[Signal("head-attach")]
		HeadAttach,

		[Signal("face-attach")]
		FaceAttach,

		[Signal("costume-attach")]
		CostumeAttach,

		[Signal("summoned-pet-costume")]
		SummonedPetCostume,

		[Signal("summoned-pet-hat")]
		SummonedPetHat,

		[Signal("summoned-pet-attach")]
		SummonedPetAttach,

		Gam1,

		Gan2,

		Jin3,

		Son4,

		Ri5,

		Gon6,

		Tae7,

		Geon8,

		[Signal("synthesis-gem")]
		SynthesisGem,

		[Signal("feed-gem")]
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

		[Signal("ruby-topaz")]
		RubyTopaz,

		[Signal("ruby-sapphire")]
		RubySapphire,

		[Signal("ruby-jade")]
		RubyJade,

		[Signal("ruby-amethyst")]
		RubyAmethyst,

		[Signal("ruby-emerald")]
		RubyEmerald,

		[Signal("ruby-diamond")]
		RubyDiamond,

		[Signal("topaz-sapphire")]
		TopazSapphire,

		[Signal("topaz-jade")]
		TopazJade,

		[Signal("topaz-amethyst")]
		TopazAmethyst,

		[Signal("topaz-emerald")]
		TopazEmerald,

		[Signal("topaz-diamond")]
		TopazDiamond,

		[Signal("sapphire-jade")]
		SapphireJade,

		[Signal("sapphire-amethyst")]
		SapphireAmethyst,

		[Signal("sapphire-emerald")]
		SapphireEmerald,

		[Signal("sapphire-diamond")]
		SapphireDiamond,

		[Signal("jade-amethyst")]
		JadeAmethyst,

		[Signal("jade-emerald")]
		JadeEmerald,

		[Signal("jade-diamond")]
		JadeDiamond,

		[Signal("amethyst-emerald")]
		AmethystEmerald,

		[Signal("amethyst-diamond")]
		AmethystDiamond,

		[Signal("emerald-diamond")]
		EmeraldDiamond,

		[Signal("aquamarine-diamond")]
		AquamarineDiamond,

		[Signal("amber-diamond")]
		AmberDiamond,

		[Signal("obsidian-garnet")]
		ObsidianGarnet,

		[Signal("corundum-white")]
		CorundumWhite,

		[Signal("corundum-black")]
		CorundumBlack,

		[Signal("corundum-pink")]
		CorundumPink,

		[Signal("corundum-yellow")]
		CorundumYellow,

		[Signal("corundum-bluegreen")]
		CorundumBluegreen,

		[Signal("corundum-blue")]
		CorundumBlue,

		[Signal("corundum-aquamarine")]
		CorundumAquamarine,

		[Signal("corundum-amber")]
		CorundumAmber,

		[Signal("corundum-ruby")]
		CorundumRuby,

		[Signal("corundum-amethyst")]
		CorundumAmethyst,

		[Signal("corundum-jade")]
		CorundumJade,

		[Signal("skill-stone")]
		SkillStone,

		[Signal("skill-stone-1")]
		SkillStone1,

		[Signal("skill-stone-2")]
		SkillStone2,

		[Signal("skill-stone-3")]
		SkillStone3,

		[Signal("regenerate-potion")]
		RegeneratePotion,

		[Signal("heal-potion")]
		HealPotion,

		[Signal("secret-potion")]
		SecretPotion,

		[Signal("detox-potion")]
		DetoxPotion,

		[Signal("magic-potion")]
		MagicPotion,

		[Signal("hwan-dan")]
		HwanDan,

		Cook,

		[Signal("exp-cook")]
		ExpCook,

		Alcohol,

		[Signal("normal-repair-tool")]
		NormalRepairTool,

		[Signal("urgency-repair-tool")]
		UrgencyRepairTool,

		Key,

		[Signal("weapon-gem-make")]
		WeaponGemMake,

		[Signal("festival-tool")]
		FestivalTool,

		[Signal("fishing-goods")]
		FishingGoods,

		[Signal("reset-talisman")]
		ResetTalisman,

		[Signal("revive-talisman")]
		ReviveTalisman,

		[Signal("party-revive-talisman")]
		PartyReviveTalisman,

		[Signal("growth-talisman")]
		GrowthTalisman,

		[Signal("unseal-talisman")]
		UnsealTalisman,

		[Signal("seal-talisman")]
		SealTalisman,

		[Signal("escape-talisman")]
		EscapeTalisman,

		[Signal("build-up-talisman")]
		BuildUpTalisman,

		Valuables,

		Wealth,

		[Signal("holy-material")]
		HolyMaterial,

		[Signal("weapon-material")]
		WeaponMaterial,

		[Signal("party-battle-material")]
		PartyBattleMaterial,

		[Signal("raid-material")]
		RaidMaterial,

		[Signal("weapon-seed-material")]
		WeaponSeedMaterial,

		[Signal("accessory-material")]
		AccessoryMaterial,

		[Signal("synthetic-material")]
		SyntheticMaterial,

		[Signal("weapon-exp")]
		WeaponExp,

		[Signal("accessory-exp")]
		AccessoryExp,

		[Signal("weapon-maker")]
		WeaponMaker,

		[Signal("talis-maker")]
		TalisMaker,

		[Signal("equip-gem-maker")]
		EquipGemMaker,

		[Signal("accessory-maker")]
		AccessoryMaker,

		[Signal("medicine-maker")]
		MedicineMaker,

		[Signal("food-maker")]
		FoodMaker,

		[Signal("common-maker")]
		CommonMaker,

		[Signal("hypermove-material")]
		HypermoveMaterial,

		[Signal("production-material")]
		ProductionMaterial,

		Cloth,

		[Signal("dress-design")]
		DressDesign,

		[Signal("color-material")]
		ColorMaterial,

		Pattern,

		[Signal("special-material")]
		SpecialMaterial,

		[Signal("normal-material")]
		NormalMaterial,

		[Signal("weapon-coin")]
		WeaponCoin,

		Token,

		[Signal("naryu-coin")]
		NaryuCoin,

		[Signal("pvp-coin")]
		PvpCoin,

		[Signal("rvr-coin")]
		RvrCoin,

		[Signal("festival-coin")]
		FestivalCoin,

		[Signal("hero-coin")]
		HeroCoin,

		[Signal("spirit-coin")]
		SpiritCoin,

		[Signal("normal-coin")]
		NormalCoin,

		[Signal("skill-deed")]
		SkillDeed,

		[Signal("skill-take-deed")]
		SkillTakeDeed,

		Ticket,

		[Signal("reset-deed")]
		ResetDeed,

		[Signal("extend-deed")]
		ExtendDeed,

		[Signal("exchange-deed")]
		ExchangeDeed,

		[Signal("switch-deed")]
		SwitchDeed,

		[Signal("normal-deed")]
		NormalDeed,

		[Signal("guild-deed")]
		GuildDeed,

		[Signal("quest-start")]
		QuestStart,

		[Signal("quest-virtual")]
		QuestVirtual,

		[Signal("chack-item")]
		ChackItem,

		[Signal("sundry-item")]
		SundryItem,

		[Signal("normal-weapon-box")]
		NormalWeaponBox,

		[Signal("shape-weapon-box")]
		ShapeWeaponBox,

		[Signal("normal-accessory-box")]
		NormalAccessoryBox,

		[Signal("normal-dress-box")]
		NormalDressBox,

		[Signal("normal-equip-gem-box")]
		NormalEquipGemBox,

		[Signal("normal-weapon-gem-box")]
		NormalWeaponGemBox,

		[Signal("normal-material-box")]
		NormalMaterialBox,

		[Signal("normal-booty-box")]
		NormalBootyBox,

		[Signal("normal-etc-box")]
		NormalEtcBox,

		[Signal("badge-1-premium")]
		Badge1Premium,

		[Signal("badge-2-premium")]
		Badge2Premium,

		[Signal("badge-3-premium")]
		Badge3Premium,

		[Signal("badge-1-normal")]
		Badge1Normal,

		[Signal("badge-2-normal")]
		Badge2Normal,

		[Signal("badge-3-normal")]
		Badge3Normal,

		[Signal("badge-appearance")]
		BadgeAppearance,

		[Signal("fusion-material")]
		FusionMaterial,

		Card,
	}


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

		[Signal("equip-gem")]
		EquipGem,

		Accessory,

		Dress,

		[Signal("weapon-gem")]
		WeaponGem,

		Medicine,

		Food,

		[Signal("build-up-stone")]
		BuildUpStone,

		Talisman,

		Tool,

		[Signal("equip-material")]
		EquipMaterial,

		[Signal("union-material")]
		UnionMaterial,

		[Signal("dress-material")]
		DressMaterial,

		[Signal("etc-material")]
		EtcMaterial,

		Coin,

		Deed,

		Quest,

		[Signal("etc-change")]
		EtcChange,

		[Signal("etc-box")]
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

		[Signal("aura-bangle")]
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

		[Signal("great-sword")]
		GreatSword,

		Ring,

		Earring,

		Necklace,

		Costume,

		[Signal("head-attach")]
		HeadAttach,

		[Signal("face-attach")]
		FaceAttach,

		[Signal("costume-attach")]
		CostumeAttach,

		[Signal("summoned-pet-costume")]
		SummonedPetCostume,

		[Signal("summoned-pet-hat")]
		SummonedPetHat,

		[Signal("summoned-pet-attach")]
		SummonedPetAttach,

		Ruby,

		Topaz,

		Sapphire,

		Jade,

		Amethyst,

		Emerald,

		Diamond,

		[Signal("regenerate-potion")]
		RegeneratePotion,

		[Signal("heal-potion")]
		HealPotion,

		[Signal("secret-potion")]
		SecretPotion,

		[Signal("detox-potion")]
		DetoxPotion,

		[Signal("lyn-sword")]
		LynSword,

		[Signal("warlock-dagger")]
		WarlockDagger,

		[Signal("soul-fighter-gauntlet")]
		SoulFighterGauntlet,

		Gun,

		Cook,

		Alcohol,

		[Signal("normal-repair-tool")]
		NormalRepairTool,

		[Signal("urgency-repair-tool")]
		UrgencyRepairTool,

		[Signal("unseal-talisman")]
		UnsealTalisman,

		[Signal("revive-talisman")]
		ReviveTalisman,

		[Signal("escape-talisman")]
		EscapeTalisman,

		[Signal("feed-gem")]
		FeedGem,

		[Signal("weapon-gem-etc")]
		WeaponGemEtc,

		[Signal("long-bow")]
		LongBow,

		[Signal("magic-potion")]
		MagicPotion,

		Key,

		[Signal("hwan-dan")]
		HwanDan,

		[Signal("exp-cook")]
		ExpCook,

		[Signal("weapon-gem-make")]
		WeaponGemMake,

		[Signal("festival-tool")]
		FestivalTool,

		[Signal("sundry-item")]
		SundryItem,

		[Signal("reset-talisman")]
		ResetTalisman,

		[Signal("party-revive-talisman")]
		PartyReviveTalisman,

		[Signal("weapon-maker")]
		WeaponMaker,

		[Signal("talis-maker")]
		TalisMaker,

		[Signal("equip-gem-maker")]
		EquipGemMaker,

		[Signal("accessory-maker")]
		AccessoryMaker,

		[Signal("medicine-maker")]
		MedicineMaker,

		[Signal("food-maker")]
		FoodMaker,

		[Signal("growth-talisman")]
		GrowthTalisman,

		[Signal("build-up-talisman")]
		BuildUpTalisman,

		Valuables,

		Wealth,

		[Signal("growth-material")]
		GrowthMaterial,

		[Signal("holy-material")]
		HolyMaterial,

		[Signal("common-maker")]
		CommonMaker,

		[Signal("special-material")]
		SpecialMaterial,

		[Signal("hypermove-material")]
		HypermoveMaterial,

		[Signal("normal-material")]
		NormalMaterial,

		[Signal("production-material")]
		ProductionMaterial,

		Cloth,

		Obsidian,

		Amber,

		Garnet,

		Aquamarine,

		Belt,

		Bracelet,

		[Signal("seal-talisman")]
		SealTalisman,

		[Signal("dress-design")]
		DressDesign,

		[Signal("fishing-goods")]
		FishingGoods,

		[Signal("badge-1")]
		Badge1,

		[Signal("weapon-gem-guardian")]
		WeaponGemGuardian,

		Spear,

		Orb,

		[Signal("ruby-diamond")]
		RubyDiamond,

		Vehicle,

		Card,

		[Signal("dual-blade")]
		DualBlade,

		Glyph,

		[Signal("topaz-diamond")]
		TopazDiamond,

		[Signal("void-10")]
		Void10,

		[Signal("void-11")]
		Void11,

		[Signal("void-12")]
		Void12,

		[Signal("sapphire-diamond")]
		SapphireDiamond,

		[Signal("void-13")]
		Void13,

		[Signal("void-14")]
		Void14,

		[Signal("jade-diamond")]
		JadeDiamond,

		[Signal("void-15")]
		Void15,

		[Signal("amethyst-diamond")]
		AmethystDiamond,

		[Signal("emerald-diamond")]
		EmeraldDiamond,

		Soul,

		[Signal("soul-2")]
		Soul2,

		Gloves,

		[Signal("pet-1")]
		Pet1,

		Nova,

		[Signal("color-material")]
		ColorMaterial,

		[Signal("rune-1")]
		Rune1,

		[Signal("rune-2")]
		Rune2,

		[Signal("aquamarine-diamond")]
		AquamarineDiamond,

		Pattern,

		[Signal("normal-coin")]
		NormalCoin,

		[Signal("skill-take-deed")]
		SkillTakeDeed,

		[Signal("normal-deed")]
		NormalDeed,

		[Signal("quest-start")]
		QuestStart,

		[Signal("chack-item")]
		ChackItem,

		[Signal("normal-etc-box")]
		NormalEtcBox,

		[Signal("skill-stone-1")]
		SkillStone1,

		[Signal("skill-stone-2")]
		SkillStone2,

		[Signal("amber-diamond")]
		AmberDiamond,

		[Signal("obsidian-garnet")]
		ObsidianGarnet,

		NamePlate,

		SpeechBubble,

		Instrument,
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