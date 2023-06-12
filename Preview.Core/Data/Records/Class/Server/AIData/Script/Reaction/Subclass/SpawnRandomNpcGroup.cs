using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record.ScriptData.Reaction.Base;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("spawn-random-npc-group")]
	public sealed class SpawnRandomNpcGroup : SpawnNpcBase
	{
		[Signal("group-1")] public Script_obj Group1;
		[Signal("group-2")] public Script_obj Group2;
		[Signal("group-3")] public Script_obj Group3;
		[Signal("group-4")] public Script_obj Group4;
		[Signal("group-5")] public Script_obj Group5;
		[Signal("group-6")] public Script_obj Group6;
		[Signal("group-7")] public Script_obj Group7;
		[Signal("group-8")] public Script_obj Group8;
		[Signal("group-9")] public Script_obj Group9;
		[Signal("group-10")] public Script_obj Group10;

		[Signal("prob-1")] public byte Prob1;
		[Signal("prob-2")] public byte Prob2;
		[Signal("prob-3")] public byte Prob3;
		[Signal("prob-4")] public byte Prob4;
		[Signal("prob-5")] public byte Prob5;
		[Signal("prob-6")] public byte Prob6;
		[Signal("prob-7")] public byte Prob7;
		[Signal("prob-8")] public byte Prob8;
		[Signal("prob-9")] public byte Prob9;
		[Signal("prob-10")] public byte Prob10;
	}
}