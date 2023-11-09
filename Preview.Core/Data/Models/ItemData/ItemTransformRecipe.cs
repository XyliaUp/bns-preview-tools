using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;

using static Xylia.Preview.Data.Models.Item;
using static Xylia.Preview.Data.Models.Item.Accessory;

namespace Xylia.Preview.Data.Models;
public sealed class ItemTransformRecipe : Record
{
	public string Alias;



	[Name("money-cost")]
	public int MoneyCost;

	[Name("title-item")]
	public Ref<Item> TitleItem;


	public Ref<Record> MainIngredient;

	public ConditionType MainIngredientConditionType = ConditionType.All;

	public CategorySeq Category;
	public enum CategorySeq : byte
	{
		None,

		Event,

		Material,

		Costume,

		Weapon,

		[Name("legendary-weapon")]
		LegendaryWeapon,

		Accessory,

		[Name("weapon-gem-adder")]
		WeaponGemAdder,

		[Name("weapon-gem2")]
		WeaponGem2,

		Piece,

		Purification,

		Special,

		Pet,

		[Name("pet-legend")]
		PetLegend,

		[Name("pet-change")]
		PetChange,

		[Name("taiji-gem")]
		TaijiGem,

		Division,

		[Name("weapon-enchant-gem")]
		WeaponEnchantGem,

		Sewing,

		[Name("weapon-transform")]
		WeaponTransform,

		[Name("accessory-transform")]
		AccessoryTransform,

		[Name("equip-gem")]
		EquipGem,
	}


	[Name("use-random")]
	public bool UseRandom;


	public WarningSeq Warning;
	public enum WarningSeq : byte
	{
		None,

		Fail,

		Stuck,

		Gemslotreset,

		[Name("fail-gemslotreset")]
		FailGemslotreset,

		[Name("stuck-gemslotreset")]
		StuckGemslotreset,

		Change,

		Lower,

		[Name("lower-gemslotreset")]
		LowerGemslotreset,

		Partialfail,

		Tradeimpossible,

		[Name("delete-particle")]
		DeleteParticle,

		[Name("delete-design")]
		DeleteDesign,

		Spiritreset,

		[Name("fail-spiritreset")]
		FailSpiritreset,

		[Name("gemslotreset-spiritreset")]
		GemslotresetSpiritreset,

		[Name("fail-gemslotreset-spiritreset")]
		FailGemslotresetSpiritreset,

		[Name("lower-spiritreset")]
		LowerSpiritreset,

		[Name("lower-gemslotreset-spiritreset")]
		LowerGemslotresetSpiritreset,

		[Name("partialfail-spiritreset")]
		PartialfailSpiritreset,

		[Name("cannot-division")]
		CannotDivision,

		[Name("fail-cannot-division")]
		FailCannotDivision,
	}



	#region Functions
	public static IEnumerable<ItemTransformRecipe> QueryRecipe(Item Item) => FileCache.Data.ItemTransformRecipe.Where(o =>
	{
		var MainIngredient = o.MainIngredient.Instance;
		if (MainIngredient is Item item) return item == Item;
		else if (MainIngredient is ItemBrand itembrand)
		{
			if (itembrand != Item.Brand.Instance) return false;

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