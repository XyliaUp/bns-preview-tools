using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class FactionBattleFieldZone : Record, IAttraction
{
	public Ref<Text> FactionBattleFieldZoneName2;
	public Ref<Text> FactionBattleFieldZoneDesc;

	#region Interface
	public string Text => this.FactionBattleFieldZoneName2.GetText();

	public string Describe => this.FactionBattleFieldZoneDesc.GetText();
	#endregion
}