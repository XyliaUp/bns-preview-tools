using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public partial class Item
{
	#region Sub
	public sealed class Weapon : Item
	{
			public WeaponTypeSeq WeaponType { get; set; }
	}

	public sealed class Costume : Item
	{
			public CustomDressDesignStateSeq CustomDressDesignState { get; set; }
	}

	public sealed class Grocery : Item
	{
			public GroceryTypeSeq GroceryType => this.Attributes["grocery-type"].ToEnum<GroceryTypeSeq>();
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

		
		public int BonusExp { get; set; }
		public int BonusMasteryExp { get; set; }
		public int BonusAccountExp { get; set; }

		public string UnsealAcquireItem1 => this.Attributes["unseal-acquire-item-1"];
		public string UnsealAcquireItem2 => this.Attributes["unseal-acquire-item-2"];
		public string UnsealAcquireItem3 => this.Attributes["unseal-acquire-item-3"];
		public string UnsealAcquireItem4 => this.Attributes["unseal-acquire-item-4"];


			public int BadgeGearScore { get; set; }

			public int BadgeSynthesisScore { get; set; }

			public Ref<SlateScroll> SlateScroll { get; set; }

			public Ref<WorldAccountCard> Card { get; set; }
	}

	public sealed class Gem : Item
	{

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
}