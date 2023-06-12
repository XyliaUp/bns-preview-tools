using System;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置 Env 初始激活状态
	/// </summary>
	[Signal("set-env-init-enable")]
	public sealed class SetEnvInitEnable : IReaction
	{
		[Obsolete]
		public Script_obj Target1;

		public Script_obj Target2;

		[Signal("init-enable")]
		public bool InitEnable;
	}
}