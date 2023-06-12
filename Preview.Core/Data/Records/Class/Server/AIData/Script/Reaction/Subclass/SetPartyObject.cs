using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置NPC队伍的对象
	/// </summary>
	[Signal("set-party-object")]
	public sealed class SetPartyObject : IReaction
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
}