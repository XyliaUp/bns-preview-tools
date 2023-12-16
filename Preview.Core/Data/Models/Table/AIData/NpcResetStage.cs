using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public sealed class NpcResetStage : ModelElement
{
	public string Alias;

	public bool ClearNpcReg;
	public bool ClearPartyReg;
	public bool DespawnMyParty;
	public bool SpawnMyParty;
}