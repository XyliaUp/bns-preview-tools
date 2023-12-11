using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models.Sequence;
public enum EquipType
{
    None,

	[Name("Name.item.equip-type.weapon")]
	Weapon,

	[Name("Name.item.equip-type.costume")]
	Costume,

	[Name("Name.item.equip-type.earring")]
	Earring,

	[Name("Name.item.equip-type.eyeglass")]
	Eyeglass,

	[Name("Name.item.equip-type.hat")]
	Hat,

	[Name("Name.item.equip-type.ring")]
	Ring,

	[Name("Name.item.equip-type.necklace")]
	Necklace,

    [Name("Name.item.equip-type.Gem-1")]
    Gem1,

    [Name("Name.item.equip-type.Gem-2")]
    Gem2,

    [Name("Name.item.equip-type.Gem-3")]
    Gem3,

    [Name("Name.item.equip-type.Gem-4")]
    Gem4,

    [Name("Name.item.equip-type.Gem-5")]
    Gem5,

    [Name("Name.item.equip-type.Gem-6")]
    Gem6,

    [Name("Name.item.equip-type.Gem-7")]
    Gem7,

    [Name("Name.item.equip-type.Gem-8")]
    Gem8,

	[Name("Name.item.equip-type.attach")]
	Attach,

	[Name("Name.item.equip-type.belt")]
	Belt,

	[Name("Name.item.equip-type.bracelet")]
	Bracelet,

	[Name("Name.item.equip-type.soul")]
	Soul,

    [Name("Name.item.equip-type.soul-2")]
    Soul2,

	[Name("Name.item.equip-type.gloves")]
	Gloves,

    [Name("Name.item.equip-type.pet-1")]
    Pet1,

    [Name("Name.item.equip-type.pet-2")]
    Pet2,

    [Name("Name.item.equip-type.rune-1")]
    Rune1,

    [Name("Name.item.equip-type.rune-2")]
    Rune2,

    [Name("Name.item.equip-type.nova")]
    Nova,

    [Name("Name.item.equip-type.badge-1-premium")]
    Badge1Premium,

    [Name("Name.item.equip-type.badge-2-premium")]
    Badge2Premium,

    [Name("Name.item.equip-type.badge-3-premium")]
    Badge3Premium,

    [Name("Name.item.equip-type.badge-1-normal")]
    Badge1Normal,

    [Name("Name.item.equip-type.badge-2-normal")]
    Badge2Normal,

    [Name("Name.item.equip-type.badge-3-normal")]
    Badge3Normal,

    [Name("Name.item.equip-type.badge-appearance")]
    BadgeAppearance,

    [Name("Name.item.equip-type.vehicle")]
    Vehicle,

    [Name("Name.EquipSlot.NormalStateAppearance")]
    AppearanceNormalState,

    [Name("Name.EquipSlot.IdleStateAppearance")]
    AppearanceIdleState,

    [Name("Name.EquipSlot.ChattingSymbol")]
    AppearanceChatting,

    [Name("Name.EquipSlot.PortraitAppearance")]
    AppearancePortrait,

    [Name("Name.EquipSlot.HypermoveAppearance")]
    AppearanceHypermove,

    [Name("Name.EquipSlot.NameplateAppearance")]
    AppearanceNamePlate,

    [Name("Name.EquipSlot.SpeechBubble")]
    AppearanceSpeechBubble,
}