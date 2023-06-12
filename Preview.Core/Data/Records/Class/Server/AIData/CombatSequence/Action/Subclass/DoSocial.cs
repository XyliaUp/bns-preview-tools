using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 执行特定社交
	/// </summary>
	[Signal("do-social")]
	public sealed class DoSocial : SkillBase
	{
		public Social Social;
	}
}