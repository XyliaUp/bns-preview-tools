using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class Faction : BaseRecord
{
	public Text Name2;

	[Signal("tag-name")]
	public Text TagName;


	public string Icon;

	public Text Text;
}