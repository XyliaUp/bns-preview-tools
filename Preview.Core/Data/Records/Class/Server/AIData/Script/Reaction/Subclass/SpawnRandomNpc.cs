using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record.ScriptData.Reaction.Base;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("spawn-random-npc")]
	public sealed class SpawnRandomNpc : SpawnNpcBase
	{
		//public byte Probability;


		public Script_obj Group;

		public byte Min;

		public byte Max;

		public byte Start;

		public byte End;
	}
}