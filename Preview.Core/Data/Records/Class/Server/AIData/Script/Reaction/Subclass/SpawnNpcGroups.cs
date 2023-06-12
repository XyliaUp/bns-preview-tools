using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record.ScriptData.Reaction.Base;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("spawn-npc-groups")]
	public sealed class SpawnNpcGroups : SpawnNpcBase
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
	}
}