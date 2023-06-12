using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	[DefaultValue(None)]
	public enum ProgressMission
	{
		None,

		[Signal("reaction-only")]
		ReactionOnly,

		Y,

		N,
	}
}