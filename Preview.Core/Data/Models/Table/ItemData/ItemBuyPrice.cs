using System.Text;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class ItemBuyPrice : ModelElement
{
	#region Fields
	public string Alias { get; set; }

	public int money { get; set; }

	public Ref<ItemBrand> RequiredItembrand { get; set; }

	public ConditionType RequiredItembrandConditionType { get; set; }

	public Ref<Item>[] RequiredItem { get; set; }

	public short[] RequiredItemCount { get; set; }

	public int RequiredFactionScore { get; set; }

	public int RequiredDuelPoint { get; set; }

	public int RequiredPartyBattlePoint { get; set; }

	public int RequiredFieldPlayPoint { get; set; }

	public int RequiredLifeContentsPoint { get; set; }

	public int RequiredAchievementScore { get; set; }

	public int RequiredAchievementId { get; set; }

	public short RequiredAchievementStepMin { get; set; }

	public short FactionLevel { get; set; }

	public sbyte CheckSoloDuelGrade { get; set; }

	public sbyte CheckTeamDuelGrade { get; set; }

	public sbyte CheckBattleFieldGradeOccupationWar { get; set; }

	public sbyte CheckBattleFieldGradeCaptureTheFlag { get; set; }

	public sbyte CheckBattleFieldGradeLeadTheBall { get; set; }

	public short CheckClosetCollectingGrade { get; set; }

	public Ref<ContentQuota> CheckContentQuota { get; set; }

	public int CheckSoulBoostSeasonBm { get; set; }

	public sbyte RequiredLevel { get; set; }

	public sbyte RequiredMasteryLevel { get; set; }

	public short RequiredAccountLevel { get; set; }
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
			if (RequiredItembrand.Instance is null) return null;
			return FileCache.Data.Provider.GetTable<ItemBrandTooltip>().FirstOrDefault(x =>
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

	// 999<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Gold" scalerate="1.2"/>99<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Silver" scalerate="1.2"/>99<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Bronze" scalerate="1.2"/>
	public string Money => new Integer(money).Money;
	#endregion
}