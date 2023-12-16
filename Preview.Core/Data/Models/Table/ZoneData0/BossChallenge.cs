using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class BossChallenge : ModelElement, IAttraction
{
	public Ref<Text> BossChallengeName2 { get; set; }

	public Ref<Text> BossChallengeDesc { get; set; }

	public sbyte UiTextGrade { get; set; }

	public Ref<AttractionRewardSummary> RewardSummary { get; set; }


	#region Interface
	public string Text => this.BossChallengeName2.GetText();

	public string Describe => this.BossChallengeDesc.GetText();
	#endregion
}