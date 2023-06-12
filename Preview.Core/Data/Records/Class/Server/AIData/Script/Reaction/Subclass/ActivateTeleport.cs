using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;


namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	/// <summary>
	/// 激活遁地点
	/// </summary>
	[Signal("activate-teleport")]
	public sealed class ActivateTeleport : IReaction
	{
		public Script_obj Target;

		public Teleport Teleport;
	}
}