using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public sealed class GuildBattleFieldZone : Record, IAttraction
{
	#region Fields
	public string Alias;


	public Ref<Text> GuildBattleFieldZoneName2;
	public Ref<Text> GuildBattleFieldZoneDesc;

	public string ThumbnailImage;

	public Ref<AttractionRewardSummary> RewardSummary;
	#endregion

	#region Interface
	public override string GetText => this.GuildBattleFieldZoneName2.GetText();

	public string GetDescribe() => this.GuildBattleFieldZoneDesc.GetText();
	#endregion
}