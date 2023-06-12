using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	public enum ConditionType
	{
		None,

		All,

		Weapon,

		Sword,

		[Signal("blade-master-sword")]
		BladeMasterSword,

		[Signal("lyn-sword")]
		LynSword,

		Gauntlet,

		Staff,

		[Signal("aura-bangle")]
		AuraBangle,

		Dagger,

		Axe,

		Accessory,

		Ring,

		Earring,

		Necklace,

		Belt,

		Bracelet,

		Soul,

		[Signal("assassin-dagger")]
		AssassinDagger,

		[Signal("warlock-dagger")]
		WarlockDagger,

		[Signal("gem-1")]
		Gem1,

		[Signal("gem-2")]
		Gem2,

		[Signal("gem-3")]
		Gem3,

		[Signal("gem-4")]
		Gem4,

		[Signal("gem-5")]
		Gem5,

		[Signal("gem-6")]
		Gem6,

		[Signal("gem-7")]
		Gem7,

		[Signal("gem-8")]
		Gem8,

		[Signal("soul-2")]
		Soul2,

		Gloves,

		[Signal("pet-1")]
		Pet1,

		[Signal("pet-2")]
		Pet2,

		[Signal("kung-fu-fighter-gauntlet")]
		KungFuFighterGauntlet,

		[Signal("soul-fighter-gauntlet")]
		SoulFighterGauntlet,

		[Signal("shooter-gun")]
		ShooterGun,

		[Signal("rune-1")]
		Rune1,

		[Signal("rune-2")]
		Rune2,

		[Signal("weapon-enchant-gem")]
		WeaponEnchantGem,

		[Signal("weapon-enchant-gem-1")]
		WeaponEnchantGem1,

		[Signal("weapon-enchant-gem-2")]
		WeaponEnchantGem2,

		[Signal("great-sword")]
		GreatSword,

		[Signal("long-bow")]
		LongBow,

		Spear,

		Orb,

		Nova,

		[Signal("badge-1")]
		Badge1,

		[Signal("badge-2")]
		Badge2,

		[Signal("badge-3")]
		Badge3,

		[Signal("badge-appearance")]
		BadgeAppearance,

		Vehicle,
	}
}