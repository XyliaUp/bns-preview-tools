using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class SealedDungeonGimmick : BaseRecord
	{
		public Text Name;

		[Signal("icon-name")]
		public Text IconName;

		[Signal("icon-tooltip")]
		public Text IconTooltip;

		public string Icon;
	}
}