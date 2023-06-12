using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 侦测生物
	/// </summary>
	[Signal("detect-creature")]
	public sealed class DetectCreature : IAction
	{
		public short Height;

		public short Radius;
	}
}