using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("spawn-random-npc")]
public sealed class SpawnRandomNpc : SpawnNpcBase
{
	public Script_obj Group;

	public byte Min;

	public byte Max;

	public byte Start;

	public byte End;
}