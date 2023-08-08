using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("spawn-random-npc")]
public sealed class SpawnRandomNpc : SpawnNpcBase
{
	public Script_obj Group;

	public sbyte Min;

	public sbyte Max;

	public sbyte Start;

	public sbyte End;
}