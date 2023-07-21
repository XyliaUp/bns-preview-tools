using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("spawn-npc-groups")]
public sealed class SpawnNpcGroups : SpawnNpcBase
{
	[Signal("group"), Repeat(10)]
	public Script_obj[] Group;
}