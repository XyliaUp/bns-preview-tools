using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	public enum BroadcastCategory
	{
		None,

		Field,

		Always,

		[Signal("solo-quartet")]
		SoloQuartet,

		Sextet,
	}
}