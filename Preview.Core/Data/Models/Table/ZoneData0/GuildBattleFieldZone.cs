using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class GuildBattleFieldZone : ModelElement, IAttraction
{
	public Ref<Text> GuildBattleFieldZoneName2;
	public Ref<Text> GuildBattleFieldZoneDesc;

	public string Text => this.GuildBattleFieldZoneName2.GetText();
	public string Describe => this.GuildBattleFieldZoneDesc.GetText();
}