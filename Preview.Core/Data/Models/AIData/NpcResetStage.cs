using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class NpcResetStage : Record
{
	public string Alias;

	public bool ClearNpcReg;
	public bool ClearPartyReg;
	public bool DespawnMyParty;
	public bool SpawnMyParty;
}