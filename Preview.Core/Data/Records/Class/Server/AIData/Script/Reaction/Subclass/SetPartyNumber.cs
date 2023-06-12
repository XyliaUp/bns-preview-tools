using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("set-party-number")]
	public sealed class SetPartyNumber : IReaction
	{
		public Script_obj Target;

		public byte Reg;

		public int Amount;
	}
}