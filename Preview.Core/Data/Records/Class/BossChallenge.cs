using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class BossChallenge : BaseRecord, IAttraction
{
	[Signal("boss-challenge-name2")]
	public Text BossChallengeName2;

	[Signal("boss-challenge-desc")]
	public Text BossChallengeDesc;

	[Signal("ui-text-grade")]
	public byte UiTextGrade;

	[Signal("reward-summary")]
	public AttractionRewardSummary RewardSummary;


	#region Interface
	public string GetName() => this.BossChallengeName2.GetText();

	public string GetDescribe() => this.BossChallengeDesc.GetText();
	#endregion
}