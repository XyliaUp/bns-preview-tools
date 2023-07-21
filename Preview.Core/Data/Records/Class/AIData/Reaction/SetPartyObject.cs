using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("set-party-object")]
public sealed class SetPartyObject : Reaction
{
	public byte Reg;

	/// <summary>
	/// NPC队伍
	/// </summary>
	public Script_obj Target;

	/// <summary>
	/// 设置指向对象
	/// </summary>
	public Script_obj Object;
}