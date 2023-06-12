using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record.ScriptData.Reaction.Base;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("despawn-npc-index")]
	public sealed class DespawnNpcIndex : DespawnNpcBase
	{
		public Script_obj Group;

		[Signal("index-1")] public byte Index1;
		[Signal("index-2")] public byte Index2;
		[Signal("index-3")] public byte Index3;
		[Signal("index-4")] public byte Index4;
		[Signal("index-5")] public byte Index5;
		[Signal("index-6")] public byte Index6;
		[Signal("index-7")] public byte Index7;
		[Signal("index-8")] public byte Index8;
		[Signal("index-9")] public byte Index9;
		[Signal("index-10")] public byte Index10;
		[Signal("index-11")] public byte Index11;
		[Signal("index-12")] public byte Index12;
		[Signal("index-13")] public byte Index13;
		[Signal("index-14")] public byte Index14;
		[Signal("index-15")] public byte Index15;
	}
}