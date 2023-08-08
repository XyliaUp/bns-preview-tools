using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 使用指定的特殊战斗序列
/// </summary>
[Signal("npc-fire-special")]
public sealed class NpcFireSpecial : Reaction
{
	public Script_obj Target;

	/// <summary>
	/// 特殊序列编号
	/// </summary>
	[Signal("special-id")]
	public sbyte SpecialId;


	public Script_obj Requester;
}