using System.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

namespace Xylia.Preview.Data.Helper.Output.Preset
{
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
                row.AddCell(new MoneyConvert(Info.Money).ToString(false));

                #region RequiredItembrand
                var ItemBrand = FileCache.Data.ItemBrand[Info.RequiredItembrand];

                var Type = Info.RequiredItembrandConditionType;
                if (Type == ConditionType.None) Type = ConditionType.All;
                var ItemTooltip = FileCache.Data.ItemBrandTooltip[ItemBrand?.Key() ?? 0, (byte)Type];

                row.AddCell(ItemTooltip?.Name2.GetText() ?? Info.RequiredItembrand);
                #endregion

                #region RequiredItem
                void GetRequiredItem(Item RequiredItem, short RequiredItemCount)
                {
                    if (RequiredItem is null) row.AddCell("");
                    else
                    {
                        string ItemName = FileCache.Data.Item[RequiredItem]?.Name2 ?? RequiredItem.alias;
                        row.AddCell(ItemName + GetCount(RequiredItemCount));
                    }
                }

                GetRequiredItem(Info.RequiredItem1, Info.RequiredItemCount1);
                GetRequiredItem(Info.RequiredItem2, Info.RequiredItemCount2);
                GetRequiredItem(Info.RequiredItem3, Info.RequiredItemCount3);
                GetRequiredItem(Info.RequiredItem4, Info.RequiredItemCount4);
                #endregion

                row.AddCell(Info.RequiredFactionScore, DisplayType.HideValue);
                row.AddCell(Info.RequiredDuelPoint, DisplayType.HideValue);
                row.AddCell(Info.RequiredPartyBattlePoint, DisplayType.HideValue);
                row.AddCell(Info.RequiredFieldPlayPoint, DisplayType.HideValue);
                row.AddCell(Info.RequiredLifeContentsPoint, DisplayType.HideValue);
                row.AddCell(Info.RequiredAchievementScore, DisplayType.HideValue);

                #region 获取成就名称
                string AchievementName = Info.RequiredAchievementId == 0 ? null :
                    FileCache.Data.Achievement.FirstOrDefault(o => o.Key() == Info.RequiredAchievementId && o.Step == Info.RequiredAchievementStepMin)?.Name.GetText();
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
}