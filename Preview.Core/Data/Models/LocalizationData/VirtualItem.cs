using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class VirtualItem : Record
{
	public string Alias;
}