using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 设置NPC的注册值
/// </summary>
[Signal("set-npc-number")]
public class SetNpcNumber : Reaction
{
	public Script_obj Target;

	public sbyte Reg;

	public int Amount;
}