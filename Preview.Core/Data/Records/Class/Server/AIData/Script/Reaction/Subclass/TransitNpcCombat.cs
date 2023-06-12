using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 对于 manual 条件序列, 指示转移到下一个战斗序列
	/// </summary>
	[Signal("transit-npc-combat")]
	public class TransitNpcCombat : IReaction
	{
		/// <summary>
		/// 指定转移的目标
		/// </summary>
		public Script_obj Target;

		/// <summary>
		/// 转移条件序号
		/// </summary>
		[Signal("transit-cond-idx")]
		public byte TransitCondIdx;

		/// <summary>
		/// 立即转移
		/// </summary>
		public bool Immediately;
	}
}