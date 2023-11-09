using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ChallengeListReward : Record
{
	public string Alias;


	[Name("reward-money")]
	public int RewardMoney;

	[Name("reward-account-exp")]
	public int RewardAccountExp;
}