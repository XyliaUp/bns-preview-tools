using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;

using static Xylia.Preview.Data.Record.Item;
using static Xylia.Preview.Data.Record.Item.Accessory;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class ItemTransformRecipe : BaseRecord
{
	[Signal("money-cost")]
	public int MoneyCost;

	[Signal("title-item")]
	public string TitleItem;


	[Signal("main-ingredient")]
	public string MainIngredient;

	[Signal("main-ingredient-condition-type")]
	public ConditionType MainIngredientConditionType = ConditionType.All;

	public CategorySeq Category;
	public enum CategorySeq : byte
	{
		None,

		Event,

		Material,

		Costume,

		Weapon,

		[Signal("legendary-weapon")]
		LegendaryWeapon,

		Accessory,

		[Signal("weapon-gem-adder")]
		WeaponGemAdder,

		[Signal("weapon-gem2")]
		WeaponGem2,

		Piece,

		Purification,

		Special,

		Pet,

		[Signal("pet-legend")]
		PetLegend,

		[Signal("pet-change")]
		PetChange,

		[Signal("taiji-gem")]
		TaijiGem,

		Division,

		[Signal("weapon-enchant-gem")]
		WeaponEnchantGem,

		Sewing,

		[Signal("weapon-transform")]
		WeaponTransform,

		[Signal("accessory-transform")]
		AccessoryTransform,

		[Signal("equip-gem")]
		EquipGem,
	}


	[Signal("use-random")]
	public bool UseRandom;


	public WarningSeq Warning;
	public enum WarningSeq : byte
	{
		None,

		Fail,

		Stuck,

		Gemslotreset,

		[Signal("fail-gemslotreset")]
		FailGemslotreset,

		[Signal("stuck-gemslotreset")]
		StuckGemslotreset,

		Change,

		Lower,

		[Signal("lower-gemslotreset")]
		LowerGemslotreset,

		Partialfail,

		Tradeimpossible,

		[Signal("delete-particle")]
		DeleteParticle,

		[Signal("delete-design")]
		DeleteDesign,

		Spiritreset,

		[Signal("fail-spiritreset")]
		FailSpiritreset,

		[Signal("gemslotreset-spiritreset")]
		GemslotresetSpiritreset,

		[Signal("fail-gemslotreset-spiritreset")]
		FailGemslotresetSpiritreset,

		[Signal("lower-spiritreset")]
		LowerSpiritreset,

		[Signal("lower-gemslotreset-spiritreset")]
		LowerGemslotresetSpiritreset,

		[Signal("partialfail-spiritreset")]
		PartialfailSpiritreset,

		[Signal("cannot-division")]
		CannotDivision,

		[Signal("fail-cannot-division")]
		FailCannotDivision,
	}



	#region Functions
	public static IEnumerable<ItemTransformRecipe> QueryRecipe(Item Item) => FileCache.Data.ItemTransformRecipe.Where(o =>
	{
		var MainIngredient = o.Attributes["main-ingredient"].CastObject();
		if (MainIngredient is Item item) return item == Item;
		else if (MainIngredient is ItemBrand itembrand)
		{
			if (itembrand != Item.Brand) return false;

			var type = o.MainIngredientConditionType;
			if (type == ConditionType.All || type == ConditionType.None) return true;
			else if (Item is Item.Weapon Weapon)
			{
				var WeaponType = Weapon.WeaponType;
				if (type == ConditionType.Weapon && WeaponType != WeaponTypeSeq.Pet1 && WeaponType != WeaponTypeSeq.Pet2) return true;

				return type == WeaponType switch
				{
					WeaponTypeSeq.Sword => ConditionType.Sword,
					WeaponTypeSeq.Gauntlet => ConditionType.Gauntlet,
					WeaponTypeSeq.AuraBangle => ConditionType.AuraBangle,
					WeaponTypeSeq.TwoHandedAxe => ConditionType.Axe,
					WeaponTypeSeq.Staff => ConditionType.Staff,
					WeaponTypeSeq.Dagger => ConditionType.Dagger,
					WeaponTypeSeq.Pet1 => ConditionType.Pet1,
					WeaponTypeSeq.Pet2 => ConditionType.Pet2,
					WeaponTypeSeq.Gun => ConditionType.ShooterGun,
					WeaponTypeSeq.GreatSword => ConditionType.GreatSword,
					WeaponTypeSeq.LongBow => ConditionType.LongBow,
					WeaponTypeSeq.Spear => ConditionType.Spear,
					WeaponTypeSeq.Orb => ConditionType.Orb,

					_ => ConditionType.None,
				};
			}
			else if (Item is Item.Accessory Accessory)
			{
				return type == Accessory.AccessoryType switch
				{
					AccessoryTypeSeq.Ring => ConditionType.Ring,
					AccessoryTypeSeq.Earring => ConditionType.Earring,
					AccessoryTypeSeq.Necklace => ConditionType.Necklace,
					AccessoryTypeSeq.Belt => ConditionType.Belt,
					AccessoryTypeSeq.Bracelet => ConditionType.Bracelet,
					AccessoryTypeSeq.Soul => ConditionType.Soul,
					AccessoryTypeSeq.Soul2 => ConditionType.Soul2,
					AccessoryTypeSeq.Gloves => ConditionType.Gloves,
					AccessoryTypeSeq.Rune1 => ConditionType.Rune1,
					AccessoryTypeSeq.Rune2 => ConditionType.Rune2,
					AccessoryTypeSeq.Nova => ConditionType.Nova,

					_ => ConditionType.None,
				};
			}
		}


		return false;
	});
	#endregion
}