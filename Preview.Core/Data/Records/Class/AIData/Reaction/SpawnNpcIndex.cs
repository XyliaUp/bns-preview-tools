using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("spawn-npc-index")]
public sealed class SpawnNpcIndex : SpawnNpcBase
{
	public Script_obj Target;
	public Script_obj Party;
	public Script_obj Group;

	[Signal("index") , Repeat(15)] 
	public sbyte[] Index;
}