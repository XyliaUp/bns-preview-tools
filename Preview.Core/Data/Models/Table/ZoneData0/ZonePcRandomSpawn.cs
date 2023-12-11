using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class ZonePcRandomSpawn : ModelElement
{
	public int Zone;
	public short Id;

	public string Alias;

	[Repeat(10)]
	public Ref<ZonePcSpawn>[] PcSpawn;
}