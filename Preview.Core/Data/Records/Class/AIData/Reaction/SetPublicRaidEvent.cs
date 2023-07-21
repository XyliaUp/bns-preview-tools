using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-public-raid-event")]
public sealed class SetPublicRaidEvent : Reaction
{
	[Signal("public-raid-event")]
	public PublicRaidEvent PublicRaidEvent;
}