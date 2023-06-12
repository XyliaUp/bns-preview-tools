using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 设置为不会死亡的状态
	/// </summary>
	[Signal("set-undying")]
	public sealed class SetUndying : IReaction
	{
		public Script_obj Target;

		public bool Undying;

		public bool Flag;
	}
}