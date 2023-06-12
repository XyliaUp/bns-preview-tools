using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Controls.List;
using Xylia.Preview.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	public partial class Store2ItemCell : ItemListCell
	{
		public Store2ItemCell(Item ItemInfo, ItemBuyPrice ItemBuyPrice = null) : base(ItemInfo)
		{
			InitializeComponent();

			this.AutoSize = false;

			//禁止显示右侧文本
			this.ShowRightText = false;

			this.LoadData(ItemBuyPrice);
		}


		private void LoadData(ItemBuyPrice ItemBuyPrice)
		{
			if (ItemBuyPrice is null)
			{
				this.ItemShow.IconCell.FrameImage = Resource_Common.ItemError;
				this.ItemShow.IconCell.FrameType = false;

				return;
			}

			this.ItemShow.IconCell.FrameImage = null;
			this.BuyPriceCell.LoadData(ItemBuyPrice);

			#region 处理限购策略信息
			List<string> TipInfo = new();

			var Quota = FileCache.Data.ContentQuota[ItemBuyPrice.CheckContentQuota];
			if (Quota != null)
			{
				this.quotaTxt.Visible = true;
				this.quotaTxt.BringToFront();

				//Load 目标信息
				string TargetInfo = ("UI.ItemStore.ContentQuota." + Quota.TargetType).GetText();
				string ChargeInfo = Quota.ChargeInterval == ResetType.None ? null : Quota.ChargeInterval.GetDescription() + " ";

				this.quotaTxt.Text = $"{ TargetInfo } { ChargeInfo }{ Quota.MaxValue }个";


				//UI.ItemStore.ContentQuota.ItemBuyCount
				//UI.ItemStore.ContentQuota.ExpirationTime
				//UI.Confirm.AccountLimit 购买限制


				//Load 重置信息
				if (Quota.ExpirationTime != default)
				{
					string QuotaDesc2 = "UI.ItemStore.BuyConfirm.Expiration.QuotaDesc".GetText();
					TipInfo.Add(new ContentParams(Quota.ExpirationTime, Quota.ExpirationTime.Hour.GetHourPart(), Quota.ExpirationTime.Hour).Handle(QuotaDesc2));
				}


				string QuotaDesc = null;
				if (Quota.ChargeInterval == ResetType.None) QuotaDesc = "UI.ItemStore.BuyConfirm.None.QuotaDesc".GetText();
				else if (Quota.ChargeInterval == ResetType.Hourly) QuotaDesc = "UI.ItemStore.BuyConfirm.EveryHour.QuotaDesc".GetText();
				else if (Quota.ChargeInterval == ResetType.Daily) QuotaDesc = "UI.ItemStore.BuyConfirm.Daily.QuotaDesc".GetText();
				else if (Quota.ChargeInterval == ResetType.Weekly)
				{
					QuotaDesc = Quota.ChargeDayOfWeek switch
					{
						BDayOfWeek.Sun => "UI.ItemStore.BuyConfirm.Sun.QuotaDesc".GetText(),
						BDayOfWeek.Mon => "UI.ItemStore.BuyConfirm.Mon.QuotaDesc".GetText(),
						BDayOfWeek.Tue => "UI.ItemStore.BuyConfirm.Tue.QuotaDesc".GetText(),
						BDayOfWeek.Wed => "UI.ItemStore.BuyConfirm.Wed.QuotaDesc".GetText(),
						BDayOfWeek.Thu => "UI.ItemStore.BuyConfirm.Thu.QuotaDesc".GetText(),
						BDayOfWeek.Fri => "UI.ItemStore.BuyConfirm.Fri.QuotaDesc".GetText(),
						BDayOfWeek.Sat => "UI.ItemStore.BuyConfirm.Sat.QuotaDesc".GetText(),

						_ => null,
					};
				}
				
				TipInfo.Add(new ContentParams(Quota.ChargeDayOfWeek, Quota.ChargeTime, Quota.ChargeTime.GetHourPart()).Handle(QuotaDesc));
			}
			#endregion

			#region 处理购买价格信息
			if (ItemBuyPrice.RequiredAchievementScore != 0)
			{
				TipInfo.Add("需要成就点数：" + ItemBuyPrice.RequiredAchievementScore);
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_Achievement);
			}

			if (ItemBuyPrice.RequiredAchievementId != 0)
			{
				string AchievementName = FileCache.Data.Achievement.FirstOrDefault(o => o.Key() == ItemBuyPrice.RequiredAchievementId && o.Step == ItemBuyPrice.RequiredAchievementStepMin)?.Name.GetText();

				TipInfo.Add("需要完成成就：" + AchievementName);
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_Achievement);
			}

			if (ItemBuyPrice.FactionLevel != 0)
			{
				var FactionLevel = FileCache.Data.FactionLevel[ItemBuyPrice.FactionLevel];
				var MainFaction1 = FactionLevel?.GradeName1.GetText();
				var MainFaction2 = FactionLevel?.GradeName2.GetText();

				TipInfo.Add($"需要势力阶级\n武林盟：{ MainFaction1 }以上\n浑天教：{ MainFaction2 }以上");
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_lock);
			}

			if (ItemBuyPrice.CheckSoloDuelGrade != 0)
			{
				TipInfo.Add("需要个人战：" + ItemBuyPrice.CheckSoloDuelGrade + "以上");
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_lock);
			}

			if (ItemBuyPrice.CheckTeamDuelGrade != 0)
			{
				TipInfo.Add("需要车轮战：" + ItemBuyPrice.CheckTeamDuelGrade + "以上");
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_lock);
			}

			if (ItemBuyPrice.CheckBattleFieldGradeOccupationWar != 0)
			{
				TipInfo.Add("需要升龙谷：" + ItemBuyPrice.CheckBattleFieldGradeOccupationWar + "以上");
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_lock);
			}

			if (ItemBuyPrice.CheckBattleFieldGradeCaptureTheFlag != 0)
			{
				TipInfo.Add("需要白鲸湖：" + ItemBuyPrice.CheckBattleFieldGradeCaptureTheFlag + "以上");
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_lock);
			}

			if (ItemBuyPrice.CheckBattleFieldGradeLeadTheBall != 0)
			{
				TipInfo.Add("需要银河遗迹：" + ItemBuyPrice.CheckBattleFieldGradeLeadTheBall + "以上");
				this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_lock);
			}
			#endregion


			//set tips
			if (TipInfo.Count != 0)
				TestTooltip2.SetTooltip(this, TipInfo.Aggregate((sum, now) => sum + "\n" + now));
		}

		private void Store2ItemCell_SizeChanged(object sender, System.EventArgs e)
		{
			if (this.Height < this.BuyPriceCell.Height) this.Height = this.BuyPriceCell.Height;
			this.BuyPriceCell.Location = new Point(this.Width - this.BuyPriceCell.Width, (this.Height - this.BuyPriceCell.Height) / 2);

			//控制物品名称不与购买价格重叠
			this.ItemShow.MaximumSize = new Size(this.BuyPriceCell.Left - 3, 0);
		}
	}
}