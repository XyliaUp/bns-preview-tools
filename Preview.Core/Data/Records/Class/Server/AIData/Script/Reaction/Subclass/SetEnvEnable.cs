using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{

	[Signal("set-env-enable")]
	public sealed class SetEnvEnable : IReaction
	{
		[Obsolete]
		public Script_obj Target;

		[Obsolete]
		public Script_obj Target1;


		public Script_obj Target2;


		public bool Enable;

		public bool State;
	}
}