using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 设置 Env 初始激活状态
/// </summary>
[Signal("set-env-init-enable")]
public sealed class SetEnvInitEnable : Reaction
{
	[Obsolete]
	public Script_obj Target1;

	public Script_obj Target2;

	[Signal("init-enable")]
	public bool InitEnable;
}