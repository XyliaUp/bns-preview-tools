using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class BonusRewardPreview
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源, 为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的Functions - 不要修改
		/// 使用代码编辑器修改此Functions的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BonusRewardPreview));
			this.AttractionReward_ChanceNum = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.Title = new System.Windows.Forms.Label();
			this.itemIconCell1 = new Xylia.Preview.GameUI.Controls.ItemIconCell();
			this.WarningPreview = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.WarningPreview();
			this.CostToggle = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.itemIconCell2 = new Xylia.Preview.GameUI.Controls.ItemIconCell();
			this.PaidBonusRewardPanel = new System.Windows.Forms.Panel();
			this.BonusRewardPanel = new System.Windows.Forms.Panel();
			this.AttractionReward_ChargeChanceNum = new Xylia.Preview.GameUI.Controls.ContentPanel();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell2)).BeginInit();
			this.PaidBonusRewardPanel.SuspendLayout();
			this.BonusRewardPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// AttractionReward_ChanceNum
			// 
			this.AttractionReward_ChanceNum.BackColor = System.Drawing.Color.Transparent;
			this.AttractionReward_ChanceNum.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.AttractionReward_ChanceNum.ForeColor = System.Drawing.Color.White;
			this.AttractionReward_ChanceNum.Location = new System.Drawing.Point(0, 35);
			this.AttractionReward_ChanceNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.AttractionReward_ChanceNum.Name = "AttractionReward_ChanceNum";
			this.AttractionReward_ChanceNum.TabIndex = 9;
			this.AttractionReward_ChanceNum.Text = "基本特别奖励次数\r\n";
			// 
			// Title
			// 
			this.Title.AutoSize = true;
			this.Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Title.ForeColor = System.Drawing.Color.White;
			this.Title.Location = new System.Drawing.Point(0, 0);
			this.Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(93, 20);
			this.Title.TabIndex = 10;
			this.Title.Text = "副本特别奖励";
			// 
			// itemIconCell1
			// 
			this.itemIconCell1.BackColor = System.Drawing.Color.Transparent;
			this.itemIconCell1.ForeColor = System.Drawing.Color.Black;
			this.itemIconCell1.FrameImage = null;
			this.itemIconCell1.FrameType = true;
			this.itemIconCell1.Location = new System.Drawing.Point(2, 3);
			this.itemIconCell1.Name = "itemIconCell1";
			this.itemIconCell1.Scale = 45;
			this.itemIconCell1.ShowFrameImage = true;
			this.itemIconCell1.ShowStackCount = false;
			this.itemIconCell1.ShowStackCountOnlyOne = false;
			this.itemIconCell1.Size = new System.Drawing.Size(45, 45);
			this.itemIconCell1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.itemIconCell1.StackCount = 0;
			this.itemIconCell1.TabIndex = 12;
			this.itemIconCell1.TabStop = false;
			this.itemIconCell1.Visible = false;
			// 
			// WarningPreview
			// 
			this.WarningPreview.AutoSize = true;
			this.WarningPreview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.WarningPreview.BackColor = System.Drawing.Color.Transparent;
			this.WarningPreview.Location = new System.Drawing.Point(0, 239);
			this.WarningPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.WarningPreview.Name = "WarningPreview";
			this.WarningPreview.Size = new System.Drawing.Size(103, 30);
			this.WarningPreview.TabIndex = 21;
			this.WarningPreview.Text = "提示消息";
			this.WarningPreview.Visible = false;
			// 
			// CostToggle
			// 
			this.CostToggle.BackColor = System.Drawing.Color.Transparent;
			this.CostToggle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CostToggle.ForeColor = System.Drawing.Color.White;
			this.CostToggle.Location = new System.Drawing.Point(2, 5);
			this.CostToggle.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.CostToggle.Name = "CostToggle";
			this.CostToggle.TabIndex = 22;
			this.CostToggle.Text = "领取更多";
			this.CostToggle.Visible = false;
			// 
			// itemIconCell2
			// 
			this.itemIconCell2.BackColor = System.Drawing.Color.Transparent;
			this.itemIconCell2.ForeColor = System.Drawing.Color.Black;
			this.itemIconCell2.FrameImage = null;
			this.itemIconCell2.FrameType = true;
			this.itemIconCell2.Location = new System.Drawing.Point(3, 30);
			this.itemIconCell2.Name = "itemIconCell2";
			this.itemIconCell2.Scale = 45;
			this.itemIconCell2.ShowFrameImage = true;
			this.itemIconCell2.ShowStackCount = false;
			this.itemIconCell2.ShowStackCountOnlyOne = false;
			this.itemIconCell2.Size = new System.Drawing.Size(45, 45);
			this.itemIconCell2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.itemIconCell2.StackCount = 0;
			this.itemIconCell2.TabIndex = 23;
			this.itemIconCell2.TabStop = false;
			this.itemIconCell2.Visible = false;
			// 
			// PaidBonusRewardPanel
			// 
			this.PaidBonusRewardPanel.AutoSize = true;
			this.PaidBonusRewardPanel.Controls.Add(this.CostToggle);
			this.PaidBonusRewardPanel.Controls.Add(this.itemIconCell2);
			this.PaidBonusRewardPanel.Location = new System.Drawing.Point(0, 152);
			this.PaidBonusRewardPanel.Name = "PaidBonusRewardPanel";
			this.PaidBonusRewardPanel.Size = new System.Drawing.Size(412, 78);
			this.PaidBonusRewardPanel.TabIndex = 24;
			// 
			// BonusRewardPanel
			// 
			this.BonusRewardPanel.AutoSize = true;
			this.BonusRewardPanel.Controls.Add(this.itemIconCell1);
			this.BonusRewardPanel.Location = new System.Drawing.Point(0, 93);
			this.BonusRewardPanel.Name = "BonusRewardPanel";
			this.BonusRewardPanel.Size = new System.Drawing.Size(412, 51);
			this.BonusRewardPanel.TabIndex = 25;
			// 
			// AttractionReward_ChargeChanceNum
			// 
			this.AttractionReward_ChargeChanceNum.BackColor = System.Drawing.Color.Transparent;
			this.AttractionReward_ChargeChanceNum.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.AttractionReward_ChargeChanceNum.ForeColor = System.Drawing.Color.White;
			this.AttractionReward_ChargeChanceNum.Location = new System.Drawing.Point(0, 58);
			this.AttractionReward_ChargeChanceNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.AttractionReward_ChargeChanceNum.Name = "AttractionReward_ChargeChanceNum";
			this.AttractionReward_ChargeChanceNum.TabIndex = 26;
			this.AttractionReward_ChargeChanceNum.Text = "充值特别奖励次数";
			// 
			// BonusRewardPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.AttractionReward_ChargeChanceNum);
			this.Controls.Add(this.BonusRewardPanel);
			this.Controls.Add(this.PaidBonusRewardPanel);
			this.Controls.Add(this.WarningPreview);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.AttractionReward_ChanceNum);
			this.Name = "BonusRewardPreview";
			this.Size = new System.Drawing.Size(415, 275);
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell2)).EndInit();
			this.PaidBonusRewardPanel.ResumeLayout(false);
			this.PaidBonusRewardPanel.PerformLayout();
			this.BonusRewardPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ContentPanel AttractionReward_ChanceNum;
		private System.Windows.Forms.Label Title;
		private ItemIconCell itemIconCell1;
		public WarningPreview WarningPreview;
		private ContentPanel CostToggle;
		private ItemIconCell itemIconCell2;
		private System.Windows.Forms.Panel PaidBonusRewardPanel;
		private System.Windows.Forms.Panel BonusRewardPanel;
		private ContentPanel AttractionReward_ChargeChanceNum;
	}
}
