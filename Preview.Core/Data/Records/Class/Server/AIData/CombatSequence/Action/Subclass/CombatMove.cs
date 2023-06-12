using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record.CombatSequenceData.Enums;


namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 战斗移动
	/// </summary>
	[Signal("combat-move")]
	public sealed class CombatMove : IAction
	{
		[Signal("move-msec")]
		public int MoveMsec;

		/// <summary>
		/// 移动类型
		/// </summary>
		[Signal("move-type")]
		public MoveType MoveType;

		[Signal("range-within")]
		public short RangeWithin;
	}
}