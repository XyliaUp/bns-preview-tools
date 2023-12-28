using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.Sequence;
public enum MarketCategorySeq
{
	None,

	Equip,

	Enchant,

	Consumable,

	Production,

	Exchange,

	Box,

	COUNT
}

public enum MarketCategory2Seq
{
	None,

	[Name("Name.item.game-category-2.weapon")]
	Weapon,

	[Name("Name.item.game-category-2.equip-gem")]
	EquipGem,

	[Name("Name.item.game-category-2.accessory")]
	Accessory,

	[Name("Name.item.game-category-2.dress")]
	Dress,

	[Name("Name.item.game-category-2.weapon-gem")]
	WeaponGem,

	[Name("Name.item.game-category-2.medicine")]
	Medicine,

	[Name("Name.item.game-category-2.food")]
	Food,

	[Name("Name.item.game-category-2.build-up-stone")]
	BuildUpStone,

	[Name("Name.item.game-category-2.talisman")]
	Talisman,

	[Name("Name.item.game-category-2.tool")]
	Tool,

	[Name("Name.item.game-category-2.equip-material")]
	EquipMaterial,

	[Name("Name.item.game-category-2.union-material")]
	UnionMaterial,

	[Name("Name.item.game-category-2.dress-material")]
	DressMaterial,

	[Name("Name.item.game-category-2.etc-material")]
	EtcMaterial,

	[Name("Name.item.game-category-2.coin")]
	Coin,

	[Name("Name.item.game-category-2.deed")]
	Deed,

	[Name("Name.item.game-category-2.quest")]
	Quest,

	[Name("Name.item.game-category-2.etc-change")]
	EtcChange,

	[Name("Name.item.game-category-2.etc-box")]
	EtcBox,

	[Name("Name.item.game-category-2.badge")]
	Badge,
}

public enum MarketCategory3Seq
{
	None,

	[Name("Name.item.game-category-3.sword")]
	Sword,

	[Name("Name.item.game-category-3.gauntlet")]
	Gauntlet,

	[Name("Name.item.game-category-3.axe")]
	Axe,

	[Name("Name.item.game-category-3.staff")]
	Staff,

	[Name("Name.item.game-category-3.aura-bangle")]
	AuraBangle,

	[Name("Name.item.game-category-3.dagger")]
	Dagger,

	[Name("Name.item.game-category-3.gam1")]
	Gam1,

	[Name("Name.item.game-category-3.gan2")]
	Gan2,

	[Name("Name.item.game-category-3.jin3")]
	Jin3,

	[Name("Name.item.game-category-3.son4")]
	Son4,

	[Name("Name.item.game-category-3.ri5")]
	Ri5,

	[Name("Name.item.game-category-3.gon6")]
	Gon6,

	[Name("Name.item.game-category-3.tae7")]
	Tae7,

	[Name("Name.item.game-category-3.geon8")]
	Geon8,

	[Name("Name.item.game-category-3.great-sword")]
	GreatSword,

	[Name("Name.item.game-category-3.ring")]
	Ring,

	[Name("Name.item.game-category-3.earring")]
	Earring,

	[Name("Name.item.game-category-3.necklace")]
	Necklace,

	[Name("Name.item.game-category-3.costume")]
	Costume,

	[Name("Name.item.game-category-3.head-attach")]
	HeadAttach,

	[Name("Name.item.game-category-3.face-attach")]
	FaceAttach,

	[Name("Name.item.game-category-3.costume-attach")]
	CostumeAttach,

	[Name("Name.item.game-category-3.summoned-pet-costume")]
	SummonedPetCostume,

	[Name("Name.item.game-category-3.summoned-pet-hat")]
	SummonedPetHat,

	[Name("Name.item.game-category-3.summoned-pet-attach")]
	SummonedPetAttach,

	[Name("Name.item.game-category-3.ruby")]
	Ruby,

	[Name("Name.item.game-category-3.topaz")]
	Topaz,

	[Name("Name.item.game-category-3.sapphire")]
	Sapphire,

	[Name("Name.item.game-category-3.jade")]
	Jade,

	[Name("Name.item.game-category-3.amethyst")]
	Amethyst,

	[Name("Name.item.game-category-3.emerald")]
	Emerald,

	[Name("Name.item.game-category-3.diamond")]
	Diamond,

	[Name("Name.item.game-category-3.regenerate-potion")]
	RegeneratePotion,

	[Name("Name.item.game-category-3.heal-potion")]
	HealPotion,

	[Name("Name.item.game-category-3.secret-potion")]
	SecretPotion,

	[Name("Name.item.game-category-3.detox-potion")]
	DetoxPotion,

	[Name("Name.item.game-category-3.lyn-sword")]
	LynSword,

	[Name("Name.item.game-category-3.warlock-dagger")]
	WarlockDagger,

	[Name("Name.item.game-category-3.soul-fighter-gauntlet")]
	SoulFighterGauntlet,

	[Name("Name.item.game-category-3.gun")]
	Gun,

	[Name("Name.item.game-category-3.cook")]
	Cook,

	[Name("Name.item.game-category-3.alcohol")]
	Alcohol,

	[Name("Name.item.game-category-3.normal-repair-tool")]
	NormalRepairTool,

	[Name("Name.item.game-category-3.urgency-repair-tool")]
	UrgencyRepairTool,

	[Name("Name.item.game-category-3.unseal-talisman")]
	UnsealTalisman,

	[Name("Name.item.game-category-3.revive-talisman")]
	ReviveTalisman,

	[Name("Name.item.game-category-3.escape-talisman")]
	EscapeTalisman,

	[Name("Name.item.game-category-3.feed-gem")]
	FeedGem,

	[Name("Name.item.game-category-3.weapon-gem-etc")]
	WeaponGemEtc,

	[Name("Name.item.game-category-3.long-bow")]
	LongBow,

	[Name("Name.item.game-category-3.magic-potion")]
	MagicPotion,

	[Name("Name.item.game-category-3.key")]
	Key,

	[Name("Name.item.game-category-3.hwan-dan")]
	HwanDan,

	[Name("Name.item.game-category-3.exp-cook")]
	ExpCook,

	[Name("Name.item.game-category-3.weapon-gem-make")]
	WeaponGemMake,

	[Name("Name.item.game-category-3.festival-tool")]
	FestivalTool,

	[Name("Name.item.game-category-3.sundry-item")]
	SundryItem,

	[Name("Name.item.game-category-3.reset-talisman")]
	ResetTalisman,

	[Name("Name.item.game-category-3.party-revive-talisman")]
	PartyReviveTalisman,

	[Name("Name.item.game-category-3.weapon-maker")]
	WeaponMaker,

	[Name("Name.item.game-category-3.talis-maker")]
	TalisMaker,

	[Name("Name.item.game-category-3.equip-gem-maker")]
	EquipGemMaker,

	[Name("Name.item.game-category-3.accessory-maker")]
	AccessoryMaker,

	[Name("Name.item.game-category-3.medicine-maker")]
	MedicineMaker,

	[Name("Name.item.game-category-3.food-maker")]
	FoodMaker,

	[Name("Name.item.game-category-3.growth-talisman")]
	GrowthTalisman,

	[Name("Name.item.game-category-3.build-up-talisman")]
	BuildUpTalisman,

	[Name("Name.item.game-category-3.valuables")]
	Valuables,

	[Name("Name.item.game-category-3.wealth")]
	Wealth,

	[Name("Name.item.game-category-3.growth-material")]
	GrowthMaterial,

	[Name("Name.item.game-category-3.holy-material")]
	HolyMaterial,

	[Name("Name.item.game-category-3.common-maker")]
	CommonMaker,

	[Name("Name.item.game-category-3.special-material")]
	SpecialMaterial,

	[Name("Name.item.game-category-3.hypermove-material")]
	HypermoveMaterial,

	[Name("Name.item.game-category-3.normal-material")]
	NormalMaterial,

	[Name("Name.item.game-category-3.production-material")]
	ProductionMaterial,

	[Name("Name.item.game-category-3.cloth")]
	Cloth,

	[Name("Name.item.game-category-3.obsidian")]
	Obsidian,

	[Name("Name.item.game-category-3.amber")]
	Amber,

	[Name("Name.item.game-category-3.garnet")]
	Garnet,

	[Name("Name.item.game-category-3.aquamarine")]
	Aquamarine,

	[Name("Name.item.game-category-3.belt")]
	Belt,

	[Name("Name.item.game-category-3.bracelet")]
	Bracelet,

	[Name("Name.item.game-category-3.seal-talisman")]
	SealTalisman,

	[Name("Name.item.game-category-3.dress-design")]
	DressDesign,

	[Name("Name.item.game-category-3.fishing-goods")]
	FishingGoods,

	[Name("Name.item.game-category-3.badge-1")]
	Badge1,

	[Name("Name.item.game-category-3.weapon-gem-guardian")]
	WeaponGemGuardian,

	[Name("Name.item.game-category-3.spear")]
	Spear,

	[Name("Name.item.game-category-3.orb")]
	Orb,

	[Name("Name.item.game-category-3.ruby-diamond")]
	RubyDiamond,

	[Name("Name.item.game-category-3.vehicle")]
	Vehicle,

	[Name("Name.item.game-category-3.card")]
	Card,

	[Name("Name.item.game-category-3.dual-blade")]
	DualBlade,

	[Name("Name.item.game-category-3.glyph")]
	Glyph,

	[Name("Name.item.game-category-3.topaz-diamond")]
	TopazDiamond,

	[Name("Name.item.game-category-3.void-10")]
	Void10,

	[Name("Name.item.game-category-3.void-11")]
	Void11,

	[Name("Name.item.game-category-3.void-12")]
	Void12,

	[Name("Name.item.game-category-3.sapphire-diamond")]
	SapphireDiamond,

	[Name("Name.item.game-category-3.void-13")]
	Void13,

	[Name("Name.item.game-category-3.void-14")]
	Void14,

	[Name("Name.item.game-category-3.jade-diamond")]
	JadeDiamond,

	[Name("Name.item.game-category-3.void-15")]
	Void15,

	[Name("Name.item.game-category-3.amethyst-diamond")]
	AmethystDiamond,

	[Name("Name.item.game-category-3.emerald-diamond")]
	EmeraldDiamond,

	[Name("Name.item.game-category-3.soul")]
	Soul,

	[Name("Name.item.game-category-3.soul-2")]
	Soul2,

	[Name("Name.item.game-category-3.gloves")]
	Gloves,

	[Name("Name.item.game-category-3.pet")]
	Pet1,

	[Name("Name.item.game-category-3.nova")]
	Nova,

	[Name("Name.item.game-category-3.color-material")]
	ColorMaterial,

	[Name("Name.item.game-category-3.rune-1")]
	Rune1,

	[Name("Name.item.game-category-3.rune-2")]
	Rune2,

	[Name("Name.item.game-category-3.aquamarine-diamond")]
	AquamarineDiamond,

	[Name("Name.item.game-category-3.pattern")]
	Pattern,

	[Name("Name.item.game-category-3.normal-coin")]
	NormalCoin,

	[Name("Name.item.game-category-3.skill-take-deed")]
	SkillTakeDeed,

	[Name("Name.item.game-category-3.normal-deed")]
	NormalDeed,

	[Name("Name.item.game-category-3.quest-start")]
	QuestStart,

	[Name("Name.item.game-category-3.chack-item")]
	ChackItem,

	[Name("Name.item.game-category-3.normal-etc-box")]
	NormalEtcBox,

	[Name("Name.item.game-category-3.skill-stone-1")]
	SkillStone1,

	[Name("Name.item.game-category-3.skill-stone-2")]
	SkillStone2,

	[Name("Name.item.game-category-3.amber-diamond")]
	AmberDiamond,

	[Name("Name.item.game-category-3.obsidian-garnet")]
	ObsidianGarnet,

	[Name("Name.item.market-category-3.name-plate-appearance")]
	NamePlateAppearance,

	[Name("Name.item.market-category-3.speech-bubble-appearance")]
	SpeechBubbleAppearance,

	[Name("Name.item.game-category-3.instrument")]
	Instrument,

	COUNT
}
