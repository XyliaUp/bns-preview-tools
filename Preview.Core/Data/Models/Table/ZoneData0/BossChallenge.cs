using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class BossChallenge : ModelElement, IAttraction
{
	public sbyte UiTextGrade { get; set; }

	public Ref<AttractionRewardSummary> RewardSummary { get; set; }


	#region IAttraction
	public string Text => this.Attributes["boss-challenge-name2"].GetText();

	public string Describe => this.Attributes["boss-challenge-desc"].GetText();
	#endregion
}