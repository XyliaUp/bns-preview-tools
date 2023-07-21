using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class Duel : BaseRecord, IAttraction
{
	[Signal("duel-name2")]
	public Text DuelName2;

	[Signal("duel-desc")]
	public Text DuelDesc;

	[Signal("reward-summary")]
	public AttractionRewardSummary RewardSummary;

	#region Interface Functions
	public string GetName() => this.DuelName2.GetText();

	public string GetDescribe() => this.DuelDesc.GetText();
	#endregion




	public enum DuelType
	{
		None,

		[Signal("death-match-1vs1")]
		DeathMatch1VS1,

		[Signal("tag-match-3vs3")]
		TagMatch3VS3,

		[Signal("sudden-death-3vs3")]
		SuddenDeath3VS3,
	}
}