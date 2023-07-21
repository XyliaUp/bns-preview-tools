using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("spawn-npc")]
public sealed class SpawnNpc : SpawnNpcBase
{
	[Signal("target")] 
	public Script_obj Target;

	[Signal("target"), Repeat(10)] 
	public Script_obj[] Targets;
	
	[Signal("spawn"), Repeat(10)]
	public string[] Spawn;
}