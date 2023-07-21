using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class Collecting : BaseRecord
{
	public Text Name;

	public CategorySeq Category;
	public enum CategorySeq
	{
		None,
		Category1,
		Category2,
		Category3,
		Category4,
		Category5,
		Category6,
		Category7,
		Category8,
		Category9,
		Category10,
		CategorySpecial,
	}


	
	[Signal("collect-skill-skin") , Repeat(4)]
	public SkillSkin[] CollectSkillSkin;

	[Signal("reward-item"), Repeat(6)]
	public string[] RewardItem;

	[Signal("reward-item-count"), Repeat(6)]
	public short[] RewardItemCount;

	[Signal("reward-money")]
	public int RewardMoney;

	[Signal("reward-collecting-score")]
	public int RewardCollectingScore;

	[Signal("ability"), Repeat(3)]
	public AttachAbility[] Ability;

	[Signal("ability-value"), Repeat(3)]
	public int[] AbilityValue;

	[Signal("expiration-time")]
	public DateTime ExpirationTime;

	[Signal("can-not-used")]
	public bool CanNotUsed;
}