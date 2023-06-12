using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 执行社交 (索引)
	/// </summary>
	[Signal("do-indexed-social")]
	public sealed class DoIndexedSocial : IAction
	{
		public byte Social;
	}
}