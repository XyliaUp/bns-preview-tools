using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class ZoneNpcSpawn : Record
{
	public int Zone;
	public short Id;


	public string Alias;


	[Repeat(2)] public ActDetectSeq ActDetect;
	[Repeat(2)] public Ref<Social> ActSocial;

	public enum ActDetectSeq
	{

	}



	public Ref<ActSequence> ActSequence;
	public Ref<ZoneArea> Area;
	public Ref<Npc> Npc;

	public short YawMin;
	public short YawMax;


	public bool InitSpawn;
	public bool Respawn;
	public Msec RespawnDelayMax;
	public Msec RespawnDelayMin;
}