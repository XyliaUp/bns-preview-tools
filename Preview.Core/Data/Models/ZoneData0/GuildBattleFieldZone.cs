using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class GuildBattleFieldZone : Record, IAttraction
{
	public string Alias;


	public Ref<Text> GuildBattleFieldZoneName2;
	public Ref<Text> GuildBattleFieldZoneDesc;

	public string ThumbnailImage;

	public Ref<AttractionRewardSummary> RewardSummary;


	#region Interface
	public string Text => this.GuildBattleFieldZoneName2.GetText();

	public string GetDescribe() => this.GuildBattleFieldZoneDesc.GetText();
	#endregion
}