using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[Signal("filter-set")]
	public sealed class FilterSet : BaseRecord
	{
		[Side(ReleaseSide.Client)]
		public string Name;

		public List<Filter2> Filter;
	}
}