using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("despawn-npc-groups")]
public sealed class DespawnNpcGroups : DespawnNpcBase
{
	[Signal("target")] 
	public Script_obj Target;

	[Signal("target"), Repeat(10)] 
	public Script_obj[] Targets;

	[Signal("group"), Repeat(10)] 
	public Script_obj[] Group;
}