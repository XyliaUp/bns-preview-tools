using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置NPC的注册值
	/// </summary>
	[Signal("set-npc-number")]
	public class SetNpcNumber : IReaction
	{
		public Script_obj Target;

		public byte Reg;

		public int Amount;
	}
}