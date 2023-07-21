using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class Npc : BaseRecord
{
	public Text Name2;

	public Text Title2;

	[Repeat(6)]
	public Store2[] Store2;

	public CreatureAppearance Appearance;


	public string AnimSet;
}