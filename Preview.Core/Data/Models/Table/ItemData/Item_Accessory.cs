using Xylia.Preview.Common.Extension;

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