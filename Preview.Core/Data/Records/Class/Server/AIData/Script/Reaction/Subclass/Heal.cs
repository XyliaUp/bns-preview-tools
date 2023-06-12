using System;

using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	public sealed class Heal : IReaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;


		public byte Percent;
	}
}