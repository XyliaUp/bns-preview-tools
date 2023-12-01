using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class ZoneAreaGroup : Record
{
	public int Zone;
	public short Id;

	public string Alias;

	[Repeat(2)] public short[] AreaIdMin;
	[Repeat(2)] public short[] AreaIdMax;
	[Repeat(2)] public short[] AdditionalAreaId;
}