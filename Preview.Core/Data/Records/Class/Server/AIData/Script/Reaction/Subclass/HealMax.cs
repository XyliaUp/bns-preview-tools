using System;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("heal-max")]
	public sealed class HealMax : IReaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;
	}
}