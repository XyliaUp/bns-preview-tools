using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 使用指定的特殊战斗序列
	/// </summary>
	[Signal("npc-fire-special")]
	public sealed class NpcFireSpecial : IReaction
	{
		public Script_obj Target;

		/// <summary>
		/// 特殊序列编号
		/// </summary>
		[Signal("special-id")]
		public byte SpecialId;


		public Script_obj Requester;
	}
}