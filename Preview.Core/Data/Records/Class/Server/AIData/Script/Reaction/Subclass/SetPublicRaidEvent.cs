using Xylia.Preview.Common.Attribute;

using  Xylia.Preview.Data.Record;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("set-public-raid-event")]
	public sealed class SetPublicRaidEvent : IReaction
	{
		[Signal("public-raid-event")]
		public PublicRaidEvent PublicRaidEvent;
	}
}