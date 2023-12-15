using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
public partial class Item
{
	#region Sub
	public sealed class Weapon : Item
	{
		[Name("weapon-type")]
		public WeaponTypeSeq WeaponType;
	}

	public sealed class Costume : Item
	{
		[Name("custom-dress-design-state")]
		public CustomDressDesignStateSeq CustomDressDesignState;
	}

	public sealed class Grocery : Item
	{
		[Name("grocery-type")]
		public GroceryTypeSeq GroceryType => this.Attributes["grocery-type"].ToEnum<GroceryTypeSeq>();
		public enum GroceryTypeSeq
		{
			Other,

			Repair,

			Seal,

			[Name("random-box")]
			RandomBox,

			[Name("cave-escape")]
			CaveEscape,

			Key,

			[Name("weapon-gem-slot-expander")]
			WeaponGemSlotExpander,

			Sealed,

			[Name("weapon-gem-slot-adder")]
			WeaponGemSlotAdder,

			Messenger,

			[Name("quest-replay-epic")]
			QuestReplayEpic,

			[Name("base-camp-warp")]
			BaseCampWarp,

			[Name("pet-food")]
			PetFood,

			[Name("reset-dungeon")]
			ResetDungeon,

			[Name("skill-book")]
			SkillBook,

			[Name("fishing-paste")]
			FishingPaste,

			Badge,

			Scroll,

			[Name("fusion-subitem")]
			FusionSubitem,

			Card,

			Glyph,

			[Name("soul-boost")]
			SoulBoost,
		}

		[Name("stack-count")]
		public short StackCount;

		
		[Name("bonus-exp")] public int BonusExp;
		[Name("bonus-mastery-exp")] public int BonusMasteryExp;
		[Name("bonus-account-exp")] public int BonusAccountExp;

		public string UnsealAcquireItem1 => this.Attributes["unseal-acquire-item-1"];
		public string UnsealAcquireItem2 => this.Attributes["unseal-acquire-item-2"];
		public string UnsealAcquireItem3 => this.Attributes["unseal-acquire-item-3"];
		public string UnsealAcquireItem4 => this.Attributes["unseal-acquire-item-4"];


		[Name("badge-gear-score")]
		public int BadgeGearScore;

		[Name("badge-synthesis-score")]
		public int BadgeSynthesisScore;

		[Name("slate-scroll")]
		public Ref<SlateScroll> SlateScroll;

		[Name("card")]
		public Ref<WorldAccountCard> Card;
	}

	public sealed class Gem : Item
	{

	}

	public sealed class Accessory : Item
	{
		public AccessoryTypeSeq AccessoryType;
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


		[Name("custom-dress-design-state")]
		public CustomDressDesignStateSeq CustomDressDesignState;
	}

	public sealed class Enchant : Item
	{

	}
	#endregion
}