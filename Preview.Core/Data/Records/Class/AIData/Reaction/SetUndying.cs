using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 设置为不会死亡的状态
/// </summary>
[Signal("set-undying")]
public sealed class SetUndying : Reaction
{
	public Script_obj Target;

	public bool Undying;

	public bool Flag;
}