using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.QuestData
{
	public class Complete : BaseRecord
	{
		[Signal("zone-index")]
		public byte ZoneIndex;
	}
}