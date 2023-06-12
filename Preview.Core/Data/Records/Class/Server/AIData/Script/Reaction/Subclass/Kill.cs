using System;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	public sealed class Kill : IReaction
	{
		[Obsolete] 
		[Signal("target")] 
		public Script_obj Target;

		[Signal("target-1")] 
		public Script_obj Target1;

		[Signal("target-2")] 
		public Script_obj Target2;
	}
}