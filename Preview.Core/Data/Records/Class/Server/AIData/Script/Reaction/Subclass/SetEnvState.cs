using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置 Env 状态
	/// </summary>
	[Signal("set-env-state")]
	public sealed class SetEnvState : IReaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;

		[Obsolete]
		public ZoneEnv2.EnvState State;

		public ZoneEnv2.EnvState State2;
	}
}