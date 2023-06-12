using System;

using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 伤害
	/// </summary>
	public sealed class Damage : IReaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;


		public long Amount;

		public byte Percent;
	}
}