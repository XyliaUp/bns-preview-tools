using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Data.Record;
public partial class Item
{
	public enum ItemType
	{
		Weapon,

		Costume,

		Grocery,

		Gem,

		Accessory,

		Enchant,
	}


	#region Sub
	public sealed class Weapon : Item
	{
		[Signal("weapon-type")]
		public WeaponTypeSeq WeaponType;


		[Signal("weapon-appearance-change-type")]
		public WeaponAppearanceChangeTypeSeq WeaponAppearanceChangeType => this.Attributes["weapon-appearance-change-type"]?.ToEnum<WeaponAppearanceChangeTypeSeq>() ?? 0;
		public enum WeaponAppearanceChangeTypeSeq
		{
			None,

			[Signal("used-only-as-target-weapon")]
			UsedOnlyAsTargetWeapon,

			[Signal("used-only-as-applying-weapon")]
			UsedOnlyAsApplyingWeapon,

			[Signal("both")]
			Both,
		}

		[Signal("skill-by-equipment")]
		public SkillByEquipment SkillByEquipment;
	}

	public sealed class Costume : Item
	{
		[Signal("custom-dress-design-state")]
		public CustomDressDesignStateSeq CustomDressDesignState;
	}

	public sealed class Grocery : Item
	{
		[Signal("grocery-type")]
		public GroceryTypeSeq GroceryType => this.Attributes["grocery-type"].ToEnum<GroceryTypeSeq>();
		public enum GroceryTypeSeq
		{
			Other,

			Repair,

			Seal,

			[Signal("random-box")]
			RandomBox,

			[Signal("cave-escape")]
			CaveEscape,

			Key,

			[Signal("weapon-gem-slot-expander")]
			WeaponGemSlotExpander,

			Sealed,

			[Signal("weapon-gem-slot-adder")]
			WeaponGemSlotAdder,

			Messenger,

			[Signal("quest-replay-epic")]
			QuestReplayEpic,

			[Signal("base-camp-warp")]
			BaseCampWarp,

			[Signal("pet-food")]
			PetFood,

			[Signal("reset-dungeon")]
			ResetDungeon,

			[Signal("skill-book")]
			SkillBook,

			[Signal("fishing-paste")]
			FishingPaste,

			Badge,

			Scroll,

			[Signal("fusion-subitem")]
			FusionSubitem,

			Card,

			Glyph,

			[Signal("soul-boost")]
			SoulBoost,
		}

		[Signal("stack-count")]
		public short StackCount;

		public Skill3 Skill3 => FileCache.Data.Skill3[this.Attributes["skill3"]];
		public Skill3 DuelSkill3 => FileCache.Data.Skill3[this.Attributes["duel-skill3"]];


		[Signal("bonus-exp")] public int BonusExp;
		[Signal("bonus-mastery-exp")] public int BonusMasteryExp;
		[Signal("bonus-account-exp")] public int BonusAccountExp;

		public string UnsealAcquireItem1 => this.Attributes["unseal-acquire-item-1"];
		public string UnsealAcquireItem2 => this.Attributes["unseal-acquire-item-2"];
		public string UnsealAcquireItem3 => this.Attributes["unseal-acquire-item-3"];
		public string UnsealAcquireItem4 => this.Attributes["unseal-acquire-item-4"];



		[Signal("badge-gear-score")]
		public int BadgeGearScore;

		[Signal("badge-synthesis-score")]
		public int BadgeSynthesisScore;

		[Signal("slate-scroll")]
		public SlateScroll SlateScroll;

		[Signal("card")]
		public WorldAccountCard Card;
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



		[Signal("custom-dress-design-state")]
		public CustomDressDesignStateSeq CustomDressDesignState;
	}

	public sealed class Enchant : Item
	{

	}
	#endregion
}