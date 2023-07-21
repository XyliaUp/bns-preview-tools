using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;

[DesignTimeVisible(false)]
public partial class BonusRewardPreview : UserControl
{
	#region Constructor
	public BonusRewardPreview()
	{
		InitializeComponent();

		this.Title.Text = "UI.BonusReward.Title".GetText();
	}
	#endregion

	#region Functions
	public bool INVALID = true;

	public void LoadData(QuestBonusRewardSetting QuestBonusRewardSetting)
	{
		int ContentY = 0;
		this.BonusRewardPanel.Controls.Remove<ItemIconCell>();
		this.PaidBonusRewardPanel.Controls.Remove<ItemIconCell>();

		if (INVALID = QuestBonusRewardSetting is null) return;

		#region	BasicQuota
		ContentY = AttractionReward_ChanceNum.Bottom;

		var BasicQuota = QuestBonusRewardSetting.BasicQuota;
		if (BasicQuota is null) this.WarningPreview.Visible = false;
		else
		{
			#region ContentQuota
			this.AttractionReward_ChanceNum.Text = "UI.AttractionReward.ChanceNum".GetText() + $" {BasicQuota.MaxValue}/{BasicQuota.MaxValue}";

			var ContentsReset = QuestBonusRewardSetting.ContentsReset.First();
			if (ContentsReset is null) this.AttractionReward_ChargeChanceNum.Visible = false;
			else
			{
				var ResetItem = FileCache.Data.Item[ContentsReset.Attributes["reset-item-1"]];
				//System.Diagnostics.Trace.WriteLine(ResetItem.GetName());

				this.AttractionReward_ChargeChanceNum.Visible = true;
				this.AttractionReward_ChargeChanceNum.Text = "UI.AttractionReward.ChargeChanceNum".GetText() + $" ({ResetItem.ItemName})";

				ContentY = AttractionReward_ChargeChanceNum.Bottom;
			}
			#endregion

			#region WarningPreview
			this.WarningPreview.Visible = true;
			this.WarningPreview.Params[1] = BasicQuota.ChargeTime.GetHourPart();
			this.WarningPreview.Params[2] = BasicQuota.ChargeTime;

			if (BasicQuota.ChargeInterval == ResetType.Daily) this.WarningPreview.Text = "UI.DungeonBonusReward.Guide.QuotaDesc.Daily".GetText();
			else if (BasicQuota.ChargeInterval == ResetType.Weekly)
			{
				this.WarningPreview.Text = BasicQuota.ChargeDayOfWeek switch
				{
					BDayOfWeek.Sun => "UI.DungeonBonusReward.Guide.QuotaDesc.Sun".GetText(),
					BDayOfWeek.Mon => "UI.DungeonBonusReward.Guide.QuotaDesc.Mon".GetText(),
					BDayOfWeek.Tue => "UI.DungeonBonusReward.Guide.QuotaDesc.Tue".GetText(),
					BDayOfWeek.Wed => "UI.DungeonBonusReward.Guide.QuotaDesc.Wed".GetText(),
					BDayOfWeek.Thu => "UI.DungeonBonusReward.Guide.QuotaDesc.Thu".GetText(),
					BDayOfWeek.Fri => "UI.DungeonBonusReward.Guide.QuotaDesc.Fri".GetText(),
					BDayOfWeek.Sat => "UI.DungeonBonusReward.Guide.QuotaDesc.Sat".GetText(),

					_ => null,
				};
			}
			else this.WarningPreview.Text = "未知重置信息";
			#endregion
		}
		#endregion


		#region BonusReward
		ContentY += 15;

		var Reward = FileCache.Data.QuestBonusReward[QuestBonusRewardSetting.Reward];
		//System.Diagnostics.Trace.WriteLine(Reward.Attributes);

		void RandomItemClickEvent(bool Paid = false)
		{
			var TotalInputCount = Paid ? Reward.PaidRandomItemTotalInputCount : Reward.RandomItemTotalInputCount;
			for (int idx = 1; idx <= TotalInputCount; idx++)
			{
				var Prefix = Paid ? "paid-" : null;
				var RandomItem = FileCache.Data.Item[Reward.Attributes[Prefix + "random-item-" + idx]];
				var StackCountMin = Reward.Attributes[Prefix + "random-item-stack-count-min-" + idx].ToShort();
				var StackCountMax = Reward.Attributes[Prefix + "random-item-stack-count-max-" + idx].ToShort();

				System.Diagnostics.Trace.WriteLine($"{RandomItem?.Name2}  {StackCountMin}~{StackCountMax}");
			}
		}


		#region	NormalBonusReward
		if (Reward.NormalBonusRewardTotalCount == 0) this.BonusRewardPanel.Visible = false;
		else
		{
			this.BonusRewardPanel.Visible = true;
			this.BonusRewardPanel.Location = new Point(0, ContentY);

			#region Items
			var items = new List<ItemIconCell>();

			// FixedItem
			//System.Diagnostics.Trace.WriteLine("FixedItemTotalCount: " + Reward.FixedItemTotalCount);
			Linq.For(Num: 4, (i) => items.AddItem(Reward.FixedItem[i].GetObjIcon(Reward.FixedItemCount[i])));

			// RandomItem
			//System.Diagnostics.Trace.WriteLine("RandomItemSelectedCount: " + Reward.RandomItemSelectedCount);
			for (int i = 0; i < Reward.RandomItemSelectedCount; i++)
			{
				var o = Resource_Common.RandomItem.GetObjIcon();
				o.Click += new((s, e) => RandomItemClickEvent());

				items.Add(o);
			}


			int ContentX = 0;
			foreach (var c in items)
			{
				this.BonusRewardPanel.Controls.Add(c);

				c.Location = new Point(ContentX, 0);
				ContentX = c.Right + 5;
			}
			#endregion

			ContentY = BonusRewardPanel.Bottom;
		}
		#endregion

		#region PaidBonusReward
		if (Reward.PaidBonusRewardTotalCount == 0) this.PaidBonusRewardPanel.Visible = false;
		else
		{
			this.PaidBonusRewardPanel.Visible = true;
			this.PaidBonusRewardPanel.Location = new Point(0, ContentY);

			#region PaidItemCost
			if (Reward.PaidItemCost == 0) this.CostToggle.Visible = false;
			else
			{
				this.CostToggle.Visible = true;
				this.CostToggle.Params[1] = new Money(Reward.PaidItemCost);
				this.CostToggle.Text = "UI.DungeonBonusReward.Cost.Toggle".GetText();
			}
			#endregion


			#region Items
			var items = new List<ItemIconCell>();

			// FixedItem
			//System.Diagnostics.Trace.WriteLine("FixedItemTotalCount: " + Reward.PaidFixedItemTotalCount);
			Linq.For(Num: 4 , (i) => items.AddItem(Reward.PaidFixedItem[i].GetObjIcon(Reward.FixedItemCount[i])));

			// RandomItem
			//System.Diagnostics.Trace.WriteLine("PaidRandomItemSelectedCount: " + Reward.PaidRandomItemSelectedCount);
			for (int i = 0; i < Reward.RandomItemSelectedCount; i++)
			{
				var o = Resource_Common.RandomItem.GetObjIcon();
				o.Click += new((s, e) => RandomItemClickEvent(true));

				items.Add(o);
			}


			int ContentX = 0;
			foreach (var c in items)
			{
				this.PaidBonusRewardPanel.Controls.Add(c);

				c.Location = new Point(ContentX, 30);
				ContentX = c.Right + 5;
			}
			#endregion

			ContentY = PaidBonusRewardPanel.Bottom;
		}
		#endregion
		#endregion

		#region End
		if (BasicQuota != null)
		{
			this.WarningPreview.Location = new Point(this.WarningPreview.Left, ContentY + 20);
			ContentY = this.WarningPreview.Bottom;
		}

		this.Height = ContentY;
		#endregion
	}
	#endregion
}