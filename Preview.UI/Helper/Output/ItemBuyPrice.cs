using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

namespace Xylia.Preview.Helper.Output;
public sealed class ItemBuyPriceOut : OutBase
{
	protected override string Name => "物品购买价格";

	protected override void CreateData()
	{
		#region Title
		ExcelInfo.SetColumn("alias", 70);
		ExcelInfo.SetColumn("钱币", 15);
		ExcelInfo.SetColumn("物品组", 20);
		ExcelInfo.SetColumn("物品1", 25);
		ExcelInfo.SetColumn("物品2", 25);
		ExcelInfo.SetColumn("物品3", 25);
		ExcelInfo.SetColumn("物品4", 25);
		ExcelInfo.SetColumn("灵气");
		ExcelInfo.SetColumn("仙豆");
		ExcelInfo.SetColumn("龙果");
		ExcelInfo.SetColumn("仙桃");
		ExcelInfo.SetColumn("珍珠");
		ExcelInfo.SetColumn("满足成就点数");
		ExcelInfo.SetColumn("满足完成成就");
		ExcelInfo.SetColumn("满足势力等级");
		ExcelInfo.SetColumn("满足个人战比武等级");
		ExcelInfo.SetColumn("满足车轮战比武等级");
		ExcelInfo.SetColumn("满足升龙谷等级");
		ExcelInfo.SetColumn("满足白鲸湖等级");
		ExcelInfo.SetColumn("满足银河遗迹等级");
		ExcelInfo.SetColumn("限购设置");
		#endregion


		FileCache.Data.ItemBuyPrice.ForEach(Info =>
		{
			var row = ExcelInfo.CreateRow();
			row.AddCell(Info.alias);
			row.AddCell(new Money(Info.Money).ToString(false));

			#region brand & item
			var ItemBrand = Info.RequiredItembrand;
			var ItemTooltip = FileCache.Data.ItemBrandTooltip[ItemBrand?.Id ?? 0, (byte)Info.RequiredItembrandConditionType];
			row.AddCell(ItemTooltip?.Name2.GetText() ?? Info.RequiredItembrand?.ToString());


			for (int i = 0; i < 4; i++)
			{
				var item = Info.RequiredItem[i];
				var count = Info.RequiredItemCount[i];

				if (item is null) row.AddCell("");
				else row.AddCell((item.Name2 ?? item.alias) + NumExtension.GetCount(count));
			}
			#endregion


			row.AddCell(Info.RequiredFactionScore, DisplayType.HideValue);
			row.AddCell(Info.RequiredDuelPoint, DisplayType.HideValue);
			row.AddCell(Info.RequiredPartyBattlePoint, DisplayType.HideValue);
			row.AddCell(Info.RequiredFieldPlayPoint, DisplayType.HideValue);
			row.AddCell(Info.RequiredLifeContentsPoint, DisplayType.HideValue);
			row.AddCell(Info.RequiredAchievementScore, DisplayType.HideValue);

			#region 获取成就名称
			string AchievementName = Info.RequiredAchievementId == 0 ? null :
				FileCache.Data.Achievement.FirstOrDefault(o => o.Id == Info.RequiredAchievementId && o.Step == Info.RequiredAchievementStepMin)?.Name.GetText();
			row.AddCell(AchievementName);
			#endregion

			row.AddCell(Info.FactionLevel, DisplayType.HideValue);
			row.AddCell(Info.CheckSoloDuelGrade, DisplayType.HideValue);
			row.AddCell(Info.CheckTeamDuelGrade, DisplayType.HideValue);
			row.AddCell(Info.CheckBattleFieldGradeOccupationWar, DisplayType.HideValue);
			row.AddCell(Info.CheckBattleFieldGradeCaptureTheFlag, DisplayType.HideValue);
			row.AddCell(Info.CheckBattleFieldGradeLeadTheBall, DisplayType.HideValue);
			row.AddCell(Info.CheckContentQuota);
		});
	}
}