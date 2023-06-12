using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 清除负面效果
	/// </summary>
	[Signal("dispel-debuff")]
	public sealed class DispelDebuff : IReaction
	{
		public Script_obj Target;
	}
}