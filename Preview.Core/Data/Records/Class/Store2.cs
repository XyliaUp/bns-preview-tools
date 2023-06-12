using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class Store2 : BaseRecord
	{
		public Text Name2;

		public string Icon;

		[Signal("none-selected-icon")]
		public string NoneSelectedIcon;

		public Faction Faction;
	}
}