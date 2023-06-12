using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 清除效果
	/// </summary>
	[Signal("dispel-buff")]
	public sealed class DispelBuff : IReaction
	{
		public Script_obj Target;
	}
}