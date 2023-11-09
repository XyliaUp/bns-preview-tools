using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class BossChallenge : Record, IAttraction
{
	public string Alias;



	[Name("boss-challenge-name2")]
	public Ref<Text> BossChallengeName2;

	[Name("boss-challenge-desc")]
	public Ref<Text> BossChallengeDesc;

	[Name("ui-text-grade")]
	public sbyte UiTextGrade;

	[Name("reward-summary")]
	public Ref<AttractionRewardSummary> RewardSummary;


	#region Interface
	public string Text => this.BossChallengeName2.GetText();

	public string GetDescribe() => this.BossChallengeDesc.GetText();
	#endregion
}