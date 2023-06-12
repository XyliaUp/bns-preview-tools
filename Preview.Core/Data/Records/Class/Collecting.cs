using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
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

	
		
		[Signal("collect-skill-skin-1")]
		public string CollectSkillSkin1;

		[Signal("collect-skill-skin-2")]
		public string CollectSkillSkin2;

		[Signal("collect-skill-skin-3")]
		public string CollectSkillSkin3;

		[Signal("collect-skill-skin-4")]
		public string CollectSkillSkin4;

		[Signal("reward-item-1")]
		public string RewardItem1;

		[Signal("reward-item-2")]
		public string RewardItem2;

		[Signal("reward-item-3")]
		public string RewardItem3;

		[Signal("reward-item-4")]
		public string RewardItem4;

		[Signal("reward-item-5")]
		public string RewardItem5;

		[Signal("reward-item-6")]
		public string RewardItem6;

		[Signal("reward-item-count-1")]
		public short RewardItemCount1;

		[Signal("reward-item-count-2")]
		public short RewardItemCount2;

		[Signal("reward-item-count-3")]
		public short RewardItemCount3;

		[Signal("reward-item-count-4")]
		public short RewardItemCount4;

		[Signal("reward-item-count-5")]
		public short RewardItemCount5;

		[Signal("reward-item-count-6")]
		public short RewardItemCount6;

		[Signal("reward-money")]
		public int RewardMoney;

		[Signal("reward-collecting-score")]
		public int RewardCollectingScore;

		[Signal("ability-1")]
		public AttachAbility Ability1;

		[Signal("ability-2")]
		public AttachAbility Ability2;

		[Signal("ability-3")]
		public AttachAbility Ability3;

		[Signal("ability-value-1")]
		public int AbilityValue1;

		[Signal("ability-value-2")]
		public int AbilityValue2;

		[Signal("ability-value-3")]
		public int AbilityValue3;

		[Signal("expiration-time")]
		public DateTime ExpirationTime;

		[Signal("can-not-used")]
		public bool CanNotUsed;
	}
}