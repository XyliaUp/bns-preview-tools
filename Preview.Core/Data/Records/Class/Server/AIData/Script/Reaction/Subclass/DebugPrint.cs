using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("debug-print")]
	public sealed class DebugPrint : IReaction
	{
		public string text;

		[Signal("param-1")]
		public Script_obj Param1;

		[Signal("param-2")]
		public Script_obj Param2;

		[Signal("param-3")]
		public Script_obj Param3;

		[Signal("param-4")]
		public Script_obj Param4;

		[Signal("param-5")]
		public Script_obj Param5;

		[Signal("param-6")]
		public Script_obj Param6;

		[Signal("param-7")]
		public Script_obj Param7;

		[Signal("param-8")]
		public Script_obj Param8;
	}
}