using Xylia.Preview.Common.Extension;
using static Xylia.Preview.Data.Models.Item.Costume;

namespace Xylia.Preview.Data.Models;
public partial class Item
{
	#region Sub
	public sealed class Weapon : Item
	{
		public WeaponTypeSeq WeaponType { get; set; }

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

	public sealed class Costume : Item
	{
		public CustomDressDesignStateSeq CustomDressDesignState { get; set; }

		public enum CustomDressDesignStateSeq
		{
			None,
			Disabled,
			Activated,
		}
	}

	public sealed class Grocery : Item
	{
		public GroceryTypeSeq GroceryType => this.Attributes["grocery-type"].ToString().ToEnum<GroceryTypeSeq>();
		public enum GroceryTypeSeq
		{
			Other,

			Repair,

			Seal,

			RandomBox,

			CaveEscape,

			Key,

			WeaponGemSlotExpander,

			Sealed,

			WeaponGemSlotAdder,

			Messenger,

			QuestReplayEpic,

			BaseCampWarp,

			PetFood,

			ResetDungeon,

			SkillBook,

			FishingPaste,

			Badge,

			Scroll,

			FusionSubitem,

			Card,

			Glyph,

			SoulBoost,
		}

		public short StackCount { get; set; }
	}

	public sealed class Gem : Item
	{
		public WeaponEnchantGemSlotTypeSeq WeaponEnchantGemSlotType { get; set; }

		public AccessoryEnchantGemEquipAccessoryTypeSeq AccessoryEnchantGemEquipAccessoryType { get; set; }



		public enum WeaponEnchantGemSlotTypeSeq
		{
			None,
			First,
			Second,
		}

		public enum AccessoryEnchantGemEquipAccessoryTypeSeq
		{
			None,
			Ring,
			Earring,
			Necklace,
			Belt,
			Bracelet,
			Gloves,
		}
	}

	public sealed class Accessory : Item
	{
		public AccessoryTypeSeq AccessoryType { get; set; }
		public enum AccessoryTypeSeq
		{
			Accessory,

			CostumeAttach,

			Ring,

			Earring,

			Necklace,

			Belt,

			Bracelet,

			Soul,

			Soul2,

			Gloves,

			Rune1,

			Rune2,

			Nova,

			Vehicle,

			AppearanceNormalState,

			AppearanceIdleState,

			AppearanceChatting,

			AppearancePortrait,

			AppearanceHypermove,

			AppearanceNamePlate,

			AppearanceSpeechBubble,
		}

		public CustomDressDesignStateSeq CustomDressDesignState { get; set; }
	}

	public sealed class Enchant : Item
	{

	}
	#endregion


	#region Sequence
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

	public enum LegendGradeBackgroundParticleTypeSeq
	{
		None,

		TypeGold,

		TypeRedup,

		TypeGoldup,
	}
	#endregion
}