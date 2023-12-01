using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class ZoneNpcRandomSpawn : Record
{
	public int Zone;
	public short Id;


	public string Alias;

	public Ref<ZoneAreaGroup> AreaGroup;

	[Repeat(10)]
	public Ref<Npc>[] Npc;

	[Repeat(10)]
	public sbyte[] NpcCount;

	[Repeat(10)]
	public Distance[] NpcMoveDistance;


	public bool Respawn;
	public Msec RespawnDelayMax;
	public Msec RespawnDelayMin;
}