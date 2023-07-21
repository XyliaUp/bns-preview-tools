
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq;
public enum EquipType
{
	None,

	Weapon,

	Costume,

	Earring,

	Eyeglass,

	Hat,

	Ring,

	Necklace,

	[Signal("Gem-1")]
	Gem1,

	[Signal("Gem-2")]
	Gem2,

	[Signal("Gem-3")]
	Gem3,

	[Signal("Gem-4")]
	Gem4,

	[Signal("Gem-5")]
	Gem5,

	[Signal("Gem-6")]
	Gem6,

	[Signal("Gem-7")]
	Gem7,

	[Signal("Gem-8")]
	Gem8,

	Attach,

	Belt,

	Bracelet,

	Soul,

	[Signal("soul-2")]
	Soul2,

	Gloves,

	[Signal("pet-1")]
	Pet1,

	[Signal("pet-2")]
	Pet2,

	[Signal("rune-1")]
	Rune1,

	[Signal("rune-2")]
	Rune2,

	[Signal("nova")]
	Nova,

	[Signal("badge-1-premium")]
	Badge1Premium,

	[Signal("badge-2-premium")]
	Badge2Premium,

	[Signal("badge-3-premium")]
	Badge3Premium,

	[Signal("badge-1-normal")]
	Badge1Normal,

	[Signal("badge-2-normal")]
	Badge2Normal,

	[Signal("badge-3-normal")]
	Badge3Normal,

	[Signal("badge-appearance")]
	BadgeAppearance,

	[Signal("vehicle")]
	Vehicle,

	[Signal("appearance-normal-state")]
	AppearanceNormalState,

	[Signal("appearance-idle-state")]
	AppearanceIdleState,

	[Signal("appearance-chatting")]
	AppearanceChatting,

	[Signal("appearance-portrait")]
	AppearancePortrait,

	[Signal("appearance-hypermove")]
	AppearanceHypermove,

	[Signal("appearance-name-plate")]
	AppearanceNamePlate,

	[Signal("appearance-speech-bubble")]
	AppearanceSpeechBubble,
}