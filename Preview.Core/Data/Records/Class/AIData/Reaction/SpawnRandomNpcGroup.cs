using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("spawn-random-npc-group")]
public sealed class SpawnRandomNpcGroup : SpawnNpcBase
{
	[Signal("group"), Repeat(10)]
	public Script_obj[] Group;

	[Signal("prob"), Repeat(10)]
	public byte[] Prob;
}