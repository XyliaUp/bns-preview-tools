
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class GuildBattleFieldZone : BaseRecord, Attraction
	{
		[Signal("guild-battle-field-zone-name2")]
		public Text GuildBattleFieldZoneName2;

		[Signal("guild-battle-field-zone-desc")]
		public Text GuildBattleFieldZoneDesc;

		[Signal("thumbnail-image")]
		public string ThumbnailImage;

		[Signal("reward-summary")]
		public AttractionRewardSummary RewardSummary;


		#region Interface
		public string GetName() => this.GuildBattleFieldZoneName2.GetText();

		public string GetDescribe() => this.GuildBattleFieldZoneDesc.GetText();
		#endregion
	}
}
