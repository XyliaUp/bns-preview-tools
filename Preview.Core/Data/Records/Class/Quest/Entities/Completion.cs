using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.QuestData
{
	public class Completion : BaseRecord
	{
		[Signal("next-quest")]
		public List<NextQuest> NextQuest;
	}
}