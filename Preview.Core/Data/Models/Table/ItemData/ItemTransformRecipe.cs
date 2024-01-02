using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;
using static Xylia.Preview.Data.Models.Item;
using static Xylia.Preview.Data.Models.Item.Accessory;
using static Xylia.Preview.Data.Models.Item.Weapon;

namespace Xylia.Preview.Data.Models;
public sealed class ItemTransformRecipe : ModelElement
{
	#region Attributes
	public Ref<ItemTransformUpgradeItem> UpgradeGrocery { get; set; }

	public sbyte RequiredInvenCapacity { get; set; }

	public int MoneyCost { get; set; }

	public Ref<ModelElement> MainIngredient { get; set; }

	public ConditionType MainIngredientConditionType { get; set; }

	public sbyte MainIngredientMinLevel { get; set; }

	public short MainIngredientStackCount { get; set; }

	public Ref<Text> MainIngredientTitleName { get; set; }

	public Ref<Item> MainIngredientTitleItem { get; set; }

	public bool KeepMainIngredientWeaponGemSlot { get; set; }

	public bool KeepMainIngredientWeaponAppearance { get; set; }

	public bool KeepMainIngredientSpirit { get; set; }

	public bool ConsumeMainIngredient { get; set; }

	public Ref<ModelElement>[] SubIngredient { get; set; }

	public ConditionType[] SubIngredientConditionType { get; set; }

	public sbyte[] SubIngredientMinLevel { get; set; }

	public short[] SubIngredientStackCount { get; set; }

	public Ref<Text>[] SubIngredientTitleName { get; set; }

	public Ref<Item>[] SubIngredientTitleItem { get; set; }

	public bool ConsumeSubIngredient { get; set; }

	public Ref<Item>[] FixedIngredient { get; set; }

	public short[] FixedIngredientStackCount { get; set; }

	public bool ConsumeFixedIngredient { get; set; }

	public sbyte SpecialFixedIndex { get; set; }

	public bool EnableBatchTransform { get; set; }

	public bool IsFixedResultRecipe { get; set; }

	public short RareItemSuccessProbability { get; set; }

	public Ref<ModelElement>[] RareItem { get; set; }

	public sbyte RareItemTotalCount { get; set; }

	public sbyte[] RareItemSelectCount { get; set; }

	public sbyte[] RareItemStackCount { get; set; }

	public short NormalItemSuccessProbability { get; set; }

	public Ref<ModelElement>[] NormalItem { get; set; }

	public sbyte NormalItemTotalCount { get; set; }

	public sbyte[] NormalItemSelectCount { get; set; }

	public sbyte[] NormalItemStackCount { get; set; }

	public Ref<ModelElement>[] PremiumItem { get; set; }

	public sbyte PremiumItemTotalCount { get; set; }

	public sbyte[] PremiumItemSelectCount { get; set; }

	public sbyte[] PremiumItemStackCount { get; set; }

	public short RandomItemSuccessProbability { get; set; }

	public Ref<ModelElement>[] RandomItem { get; set; }

	public sbyte RandomItemTotalCount { get; set; }

	public short[] RandomItemSelectPropWeight { get; set; }

	public bool RandomFailureMileageSave { get; set; }

	public Ref<ItemTransformRecipe>[] RandomFailureMileageInfluenceRecipe { get; set; }

	public Ref<ItemTransformRetryCost> RandomRetryCost { get; set; }

	//public WeaponGemType MainIngredientWeaponGemType { get; set; }

	public short MainIngredientWeaponGemLevel { get; set; }

	public sbyte MainIngredientWeaponGemGrade { get; set; }

	//public WeaponGemType[] SubIngredientWeaponGemType { get; set; }

	public short[] SubIngredientWeaponGemLevel { get; set; }

	public sbyte[] SubIngredientWeaponGemGrade { get; set; }

	public Ref<Item> TitleItem { get; set; }

	public Ref<Text> TitleName { get; set; }

	public Ref<RandomboxPreview> TitleReward { get; set; }

	public bool UseRandom { get; set; }
	#endregion


	#region Methods
	protected internal override void LoadHiddenField()
	{
		var Warning = this.Attributes["warning"];
		if (Warning is "lower" or "lower-gemslotreset")
		{
			this.Attributes["random-result"] = "lower";
		}

		bool IsSure = Warning is null or "gemslotreset" or "delete-particle" or "delete-design";

		for (int i = 1; i <= 8; i++)
		{
			if (this.Attributes["fixed-ingredient-" + i] != null && this.Attributes["fixed-ingredient-stack-count-" + i] is null)
			{
				this.Attributes["fixed-ingredient-stack-count-" + i]  = 1;
			}
		}


		var UseRandom = this.Attributes["use-random"] == "y";
		if (UseRandom)
		{
			if (this.Attributes["random-item-success-probability"] != null)
				return;

			int RandomMax = 20;
			for (int i = 1; i <= RandomMax; i++)
			{
				var attr = this.Attributes[$"random-item-{i}"];
				if (attr != null)
				{
					this.Attributes["random-item-stack-count-" + i] = 1;
				}
				else if (i != 1)
				{
					const int q = 3;
					int TotalCount = i - 1;
					int TotalWeight = 1 * (1 - (int)Math.Pow(q, TotalCount)) / (1 - q);

					//概率权重和需要超过 1000
					int ExtraWeight = 0;
					if (TotalWeight < 1000)
						ExtraWeight = (int)Math.Ceiling((decimal)(1000 - TotalWeight) / TotalCount);

					for (int x = 1; x <= TotalCount; x++)
					{
						int Weight = 1 * (int)Math.Pow(q, x - 1) + ExtraWeight;
						this.Attributes["random-item-select-prop-weight-" + x] = Weight;
					}

					//最大值 100
					this.Attributes["random-item-success-probability"] = IsSure ? 100 : 20;
					this.Attributes["random-item-total-count"] = TotalCount;

					break;
				}
				else break;
			}
		}
		else
		{
			if (this.Attributes["normal-item-success-probability"] == null)
			{
				int MormalMax = 10;
				for (int i = 1; i <= MormalMax; i++)
				{
					var attr = this.Attributes[$"normal-item-{i}"];
					if (attr != null)
					{
						this.Attributes["normal-item-stack-count-" + i] = 1;
					}
					else if (i != 1)
					{
						this.Attributes["normal-item-success-probability"] = IsSure ? 100 : 20;
						this.Attributes["normal-item-select-count"] = i - 1;
						this.Attributes["normal-item-total-count"] = i - 1;

						break;
					}
					else break;
				}
			}

			if (this.Attributes["rare-item-success-probability"] == null)
			{
				int RareMax = 10;
				for (int i = 1; i <= RareMax; i++)
				{
					var attr = this.Attributes[$"rare-item-{i}"];
					if (attr != null)
					{
						this.Attributes["rare-item-stack-count-" + i] = 1;
					}
					else if (i != 1)
					{
						this.Attributes["rare-item-success-probability"] = IsSure ? 1000 : 190;
						this.Attributes["rare-item-select-count"] = i - 1;
						this.Attributes["rare-item-total-count"] = i - 1;
						break;
					}
					else break;
				}
			}
		}
	}

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