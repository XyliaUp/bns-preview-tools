using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 等待 <see cref="IAction.Duration"/> (ms)
	/// </summary>
	public sealed class Stay : IAction
	{
		public Script_obj Target;
	}
}