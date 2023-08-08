using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("despawn-npc-index")]
public sealed class DespawnNpcIndex : DespawnNpcBase
{
	public Script_obj Group;

	[Signal("index"), Repeat(15)]
	public sbyte[] Index;
}