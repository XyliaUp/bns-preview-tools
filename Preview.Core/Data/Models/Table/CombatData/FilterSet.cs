using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class FilterSet : ModelElement
{
	[Side(ReleaseSide.Client)]
	public string Name { get; set; }

	public List<Filter> Filter { get; set; }
}