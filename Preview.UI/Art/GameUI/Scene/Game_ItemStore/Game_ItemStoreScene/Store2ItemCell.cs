using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.UI.Custom.Controls.List;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Resources;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore;
public partial class Store2ItemCell : ListItemCell
{
	public Store2ItemCell(Item ItemInfo, int StackCount = 1, ItemBuyPrice ItemBuyPrice = null) : base(ItemInfo)
	{
		InitializeComponent();

		this.AutoSize = false;
		this.LoadData(ItemBuyPrice);

		// StackCount
		this.ItemShow.IconCell.StackCount = StackCount;
		this.ItemShow.IconCell.ShowStackCountOnlyOne = false;
		this.ItemShow.IconCell.ShowStackCount = true;
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

		#region quota
		List<string> TipInfo = new();

		var Quota = ItemBuyPrice.CheckContentQuota;
		if (Quota != null)
		{
			this.ItemShow.HeightDiff = -5;
			this.QuotaText.Visible = true;
			this.QuotaText.BringToFront();

			string TargetInfo = $"UI.ItemStore.ContentQuota.{Quota.TargetType}".GetText();
			string ChargeInfo = Quota.ChargeInterval == ResetType.None ? null : Quota.ChargeInterval.GetDescription() + " ";
			this.QuotaText.Text = $"{TargetInfo} {ChargeInfo}{Quota.MaxValue}";


			//UI.ItemStore.ContentQuota.ItemBuyCount
			//UI.ItemStore.ContentQuota.ExpirationTime
			//UI.Confirm.AccountLimit

			if (Quota.ExpirationTime != default)
			{
				TipInfo.Add(new ContentParams(Quota.ExpirationTime, Quota.ExpirationTime.Hour.GetHourPart(), Quota.ExpirationTime.Hour)
					.Handle("UI.ItemStore.BuyConfirm.Expiration.QuotaDesc"));
			}


			string QuotaDesc = null;
			if (Quota.ChargeInterval == ResetType.None) QuotaDesc = "UI.ItemStore.BuyConfirm.None.QuotaDesc";
			else if (Quota.ChargeInterval == ResetType.Hourly) QuotaDesc = "UI.ItemStore.BuyConfirm.EveryHour.QuotaDesc";
			else if (Quota.ChargeInterval == ResetType.Daily) QuotaDesc = "UI.ItemStore.BuyConfirm.Daily.QuotaDesc";
			else if (Quota.ChargeInterval == ResetType.Weekly)
			{
				QuotaDesc = Quota.ChargeDayOfWeek switch
				{
					BDayOfWeek.Sun => "UI.ItemStore.BuyConfirm.Sun.QuotaDesc",
					BDayOfWeek.Mon => "UI.ItemStore.BuyConfirm.Mon.QuotaDesc",
					BDayOfWeek.Tue => "UI.ItemStore.BuyConfirm.Tue.QuotaDesc",
					BDayOfWeek.Wed => "UI.ItemStore.BuyConfirm.Wed.QuotaDesc",
					BDayOfWeek.Thu => "UI.ItemStore.BuyConfirm.Thu.QuotaDesc",
					BDayOfWeek.Fri => "UI.ItemStore.BuyConfirm.Fri.QuotaDesc",
					BDayOfWeek.Sat => "UI.ItemStore.BuyConfirm.Sat.QuotaDesc",

					_ => null,
				};
			}

			TipInfo.Add(new ContentParams(Quota.ChargeDayOfWeek, Quota.ChargeTime, Quota.ChargeTime.GetHourPart()).Handle(QuotaDesc));
		}
		#endregion

		#region price
		if (ItemBuyPrice.RequiredAchievementScore != 0)
		{
			TipInfo.Add("需要成就点数：" + ItemBuyPrice.RequiredAchievementScore);
			this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_Achievement);
		}

		if (ItemBuyPrice.RequiredAchievementId != 0)
		{
			string AchievementName = FileCache.Data.Achievement.FirstOrDefault(o => o.Id == ItemBuyPrice.RequiredAchievementId && o.Step == ItemBuyPrice.RequiredAchievementStepMin)?.Name.GetText();

			TipInfo.Add("需要完成成就：" + AchievementName);
			this.ItemShow.IconCell.SetExtraImg(DrawLocation.BottomLeft, Resource_BNSR.unuseable_Achievement);
		}

		if (ItemBuyPrice.FactionLevel != 0)
		{
			var FactionLevel = FileCache.Data.FactionLevel[ItemBuyPrice.FactionLevel];
			var MainFaction1 = FactionLevel?.GradeName1.GetText();
			var MainFaction2 = FactionLevel?.GradeName2.GetText();

			TipInfo.Add($"需要势力阶级\n武林盟：{MainFaction1}以上\n浑天教：{MainFaction2}以上");
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

	private void Store2ItemCell_SizeChanged(object sender, EventArgs e)
	{
		this.Height = Math.Max(Height, this.BuyPriceCell.Height);
		this.BuyPriceCell.Location = new Point(this.Width - this.BuyPriceCell.Width, (this.Height - this.BuyPriceCell.Height) / 2);

		this.ItemShow.MaximumSize = new Size(this.BuyPriceCell.Left - 3, 99999);
	}
}