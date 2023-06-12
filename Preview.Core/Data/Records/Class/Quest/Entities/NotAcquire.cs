using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData
{
	[Signal("not-acquire")]
	public sealed class NotAcquire : BaseRecord
	{
		[DefaultValue(null)]
		[Signal("zone-index")]
		public byte ZoneIndex;

		[Side(ReleaseSide.Client)]
		public string Kismet;
	}
}