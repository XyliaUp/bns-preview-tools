using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public sealed class Duel : Record, IAttraction
{
	public string Alias;

	public Ref<Text> DuelName2;

	public Ref<Text> DuelDesc;

	public Ref<AttractionRewardSummary> RewardSummary;


	public enum DuelType
	{
		None,

		[Name("death-match-1vs1")]
		DeathMatch1VS1,

		[Name("tag-match-3vs3")]
		TagMatch3VS3,

		[Name("sudden-death-3vs3")]
		SuddenDeath3VS3,
	}


	#region Interface
	public override string GetText => this.DuelName2.GetText();

	public string GetDescribe() => this.DuelDesc.GetText();
	#endregion
}