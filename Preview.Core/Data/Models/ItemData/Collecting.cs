using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class Collecting : Record
{
	public string Alias;



	public Ref<Text> Name;

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


	
	[Name("collect-skill-skin") , Repeat(4)]
	public SkillSkin[] CollectSkillSkin;

	[Name("reward-item"), Repeat(6)]
	public string[] RewardItem;

	[Name("reward-item-count"), Repeat(6)]
	public short[] RewardItemCount;

	[Name("reward-money")]
	public int RewardMoney;

	[Name("reward-collecting-score")]
	public int RewardCollectingScore;

	[Name("ability"), Repeat(3)]
	public AttachAbility[] Ability;

	[Name("ability-value"), Repeat(3)]
	public int[] AbilityValue;

	[Name("expiration-time")]
	public DateTime ExpirationTime;

	[Name("can-not-used")]
	public bool CanNotUsed;
}