using Xylia.Extension;

namespace Xylia.Preview.Data.Record;

public sealed partial class Item
{
	public AccessoryTypeSeq AccessoryType => this.Attributes["accessory-type"].ToEnum<AccessoryTypeSeq>();
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

	//public CustomDressDesignStateSeq CustomDressDesignState => this.Attributes["custom-dress-design-state"].ToEnum<CustomDressDesignStateSeq>();
}