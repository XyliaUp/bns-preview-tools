using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;
using static Xylia.Preview.Data.Models.Item;
using static Xylia.Preview.Data.Models.Item.Accessory;

namespace Xylia.Preview.Data.Models;
public sealed class ItemTransformRecipe : ModelElement
{	 
	public int MoneyCost { get; set; }

	public Ref<Item> TitleItem { get; set; }


	public Ref<ModelElement> MainIngredient { get; set; }

	public ConditionType MainIngredientConditionType { get; set; }

	public CategorySeq Category { get; set; }
	public enum CategorySeq : byte
	{
		None,

		Event,

		Material,

		Costume,

		Weapon,

			LegendaryWeapon,

		Accessory,

			WeaponGemAdder,

			WeaponGem2,

		Piece,

		Purification,

		Special,

		Pet,

			PetLegend,

			PetChange,

			TaijiGem,

		Division,

			WeaponEnchantGem,

		Sewing,

			WeaponTransform,

			AccessoryTransform,

			EquipGem,
	}

	public bool UseRandom { get; set; }


	public WarningSeq Warning { get; set; }
	public enum WarningSeq : byte
	{
		None,

		Fail,

		Stuck,

		Gemslotreset,

			FailGemslotreset,

			StuckGemslotreset,

		Change,

		Lower,

			LowerGemslotreset,

		Partialfail,

		Tradeimpossible,

			DeleteParticle,

			DeleteDesign,

		Spiritreset,

			FailSpiritreset,

			GemslotresetSpiritreset,

			FailGemslotresetSpiritreset,

			LowerSpiritreset,

			LowerGemslotresetSpiritreset,

			PartialfailSpiritreset,

			CannotDivision,

			FailCannotDivision,
	}



	#region Functions
	public static IEnumerable<ItemTransformRecipe> QueryRecipe(Item Item) => FileCache.Data.Get<ItemTransformRecipe>().Where(o =>
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