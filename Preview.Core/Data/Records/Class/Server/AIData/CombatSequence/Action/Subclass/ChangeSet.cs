using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 改变设置
	/// 特征Property都是索引, 可能是从智商设置中抽取对象
	/// </summary>
	[Signal("change-set")]
	public sealed class ChangeSet : IAction
	{
		public byte Stance;

		[Signal("stance-effect-1")]
		public byte StanceEffect1;

		public byte Weapon;
	}
}