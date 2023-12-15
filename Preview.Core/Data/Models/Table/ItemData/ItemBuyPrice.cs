using System.Text;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class ItemBuyPrice : ModelElement
{
	#region Fields
	public string Alias;

	public int money;

	[Name("required-itembrand")]
	public Ref<ItemBrand> RequiredItembrand;

	[Name("required-itembrand-condition-type")]
	public ConditionType RequiredItembrandConditionType = ConditionType.All;

	[Name("required-item"), Repeat(4)]
	public Ref<Item>[] RequiredItem;

	[Name("required-item-count"), Repeat(4)]
	public short[] RequiredItemCount;

	[Name("required-faction-score")]
	public int RequiredFactionScore;

	[Name("required-duel-point")]
	public int RequiredDuelPoint;

	[Name("required-party-battle-point")]
	public int RequiredPartyBattlePoint;

	[Name("required-field-play-point")]
	public int RequiredFieldPlayPoint;

	[Name("required-life-contents-point")]
	public int RequiredLifeContentsPoint;

	[Name("required-achievement-score")]
	public int RequiredAchievementScore;

	[Name("required-achievement-id")]
	public int RequiredAchievementId;

	[Name("required-achievement-step-min")]
	public short RequiredAchievementStepMin;

	[Name("faction-level")]
	public short FactionLevel;

	[Name("check-solo-duel-grade")]
	public sbyte CheckSoloDuelGrade;

	[Name("check-team-duel-grade")]
	public sbyte CheckTeamDuelGrade;

	[Name("check-battle-field-grade-occupation-war")]
	public sbyte CheckBattleFieldGradeOccupationWar;

	[Name("check-battle-field-grade-capture-the-flag")]
	public sbyte CheckBattleFieldGradeCaptureTheFlag;

	[Name("check-battle-field-grade-lead-the-ball")]
	public sbyte CheckBattleFieldGradeLeadTheBall;

	[Name("check-closet-collecting-grade")]
	public sbyte CheckClosetCollectingGrade;

	[Name("check-content-quota")]
	public Ref<ContentQuota> CheckContentQuota;

	[Name("check-soul-boost-season-bm")]
	public int CheckSoulBoostSeasonBm;

	[Name("required-level")]
	public sbyte RequiredLevel;

	[Name("required-mastery-level")]
	public sbyte RequiredMasteryLevel;

	[Name("required-account-level")]
	public short RequiredAccountLevel;
	#endregion


	#region Properties
	public object ExtraCost_DisposeItem
	{
		get
		{
			var source = new KeyValuePair<Item, short>[4];
			for (int x = 0; x < source.Length; x++)
				source[x] = new(RequiredItem[x].Instance, RequiredItemCount[x]);

			return source.Where(x => x.Key != null);
		}
	}

	public ItemBrandTooltip ExtraCost_ItemBrand
	{
		get
		{
			if (RequiredItembrand.IsNull) return null;
			return FileCache.Data.Get<ItemBrandTooltip>().FirstOrDefault(x =>
				x.BrandId == RequiredItembrand.Instance.Id && 
				x.ItemConditionType == RequiredItembrandConditionType);
		}
	}

	public string Coin
	{
		get
		{
			// 0000<image enablescale="true" imagesetpath="00009076.Peach_small" scalerate="1.2"/>
			// 0000<image enablescale="true" imagesetpath="00009076.DragonFruit_small" scalerate="1.2"/>
			// 0000<image enablescale="true" imagesetpath="00015590.ArenaRank_Chat_DuelPoint" scalerate="1.2"/>
			// 0000<image enablescale="true" imagesetpath="00009076.GuildBattleField_History_05" scalerate="1.2"/>

			var result = new StringBuilder();
			if (RequiredFieldPlayPoint > 0) result.Append(RequiredFieldPlayPoint + """<image enablescale="true" imagesetpath="00009076.Peach_small" scalerate="1.2"/>""");
			if (RequiredPartyBattlePoint > 0) result.Append(RequiredPartyBattlePoint + """<image enablescale="true" imagesetpath="00009076.DragonFruit_small" scalerate="1.2"/>""");
			if (RequiredDuelPoint > 0) result.Append(RequiredDuelPoint + """<image enablescale="true" imagesetpath="00015590.ArenaRank_Chat_DuelPoint" scalerate="1.2"/>""");
			if (RequiredFactionScore > 0) result.Append(RequiredFactionScore + """<image enablescale="true" imagesetpath="00009076.GuildBattleField_History_05" scalerate="1.2"/>""");
			if (RequiredLifeContentsPoint > 0) result.Append(RequiredLifeContentsPoint + """<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_LivingCoin" scalerate="1.2"/>""");

			return result.ToString();
		}
	}

	public string Money
	{
		get
		{
			// 999<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Gold" scalerate="1.2"/>99<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Silver" scalerate="1.2"/>99<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Bronze" scalerate="1.2"/>

			var result = new StringBuilder();
			var money = (Money)this.money;
			if (money.Gold > 0) result.Append(money.Gold + """<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Gold" scalerate="1.2"/>""");
			if (money.Silver > 0) result.Append(money.Silver + """<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Silver" scalerate="1.2"/>""");
			if (money.Copper > 0) result.Append(money.Copper + """<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Bronze" scalerate="1.2"/>""");

			return result.ToString();
		}
	}
	#endregion
}