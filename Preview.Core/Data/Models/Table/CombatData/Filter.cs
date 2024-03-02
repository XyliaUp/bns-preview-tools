using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public class Filter : ModelElement
{
	public Script_obj Subject { get; set; }
	public Script_obj Target { get; set; }
	public Script_obj Subject2 { get; set; }
	public Script_obj Target2 { get; set; }


	#region Expand Sub
	//public sealed class NpcSpawn : Filter
	//{
	//	[Side(ReleaseSide.Server)]
	//	public string Spawn { get; set; }
	//}

	//public sealed class NpcParty : Filter
	//{
	//	[Side(ReleaseSide.Server)]
	//	public bool Leader { get; set; }

	//	[Side(ReleaseSide.Server)]
	//	public Script_obj Party { get; set; }
	//}
	#endregion
}