using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 清除负面效果
/// </summary>
[Signal("dispel-debuff")]
public sealed class DispelDebuff : Reaction
{
	public Script_obj Target;
}