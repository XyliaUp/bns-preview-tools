using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.ScriptData.Decision
{
	public abstract class IDecision : BaseRecord
	{
		[Signal("filter-set")]
		public List<FilterSet> FilterSet;

		[Signal("reaction-set")]
		public List<ReactionSet> ReactionSet;
	}
}