using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ChallengeToday;
partial class Game_ChallengeTodayScene
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_ChallengeTodayScene));
		this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.OutputList = new System.Windows.Forms.ToolStripMenuItem();
		this.ModifyFilterRule = new System.Windows.Forms.ToolStripMenuItem();
		this.CancelFilter = new System.Windows.Forms.ToolStripMenuItem();
		this.DaySelect = new HZH_Controls.Controls.UCCombox();
		this.ChallengeToday_ChallengeRewardGuide = new System.Windows.Forms.Label();
		this.RewardPreview = new ChallengeListRewardPreview();
		this.TaskPanel = new System.Windows.Forms.Panel();
		this.challengeCell1 = new ChallengeCell();
		this.RequiredTime = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
		this.ChallengeToday_CompleteCount = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
		this.ChallengeToday_TodayReward = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
		this.ChallengeToday_TodayQuest = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
		this.ChallengeToday_State = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
		this.contentPanel1 = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
		this.MenuStrip.SuspendLayout();
		this.TaskPanel.SuspendLayout();
		this.SuspendLayout();
		// 
		// MenuStrip
		// 
		this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OutputList});
		this.MenuStrip.Name = "Menu";
		this.MenuStrip.Size = new System.Drawing.Size(149, 26);
		// 
		// OutputList
		// 
		this.OutputList.Name = "OutputList";
		this.OutputList.Size = new System.Drawing.Size(148, 22);
		this.OutputList.Text = "输出挑战任务";
		this.OutputList.Click += new System.EventHandler(this.OutputList_Click);
		// 
		// ModifyFilterRule
		// 
		this.ModifyFilterRule.Name = "ModifyFilterRule";
		this.ModifyFilterRule.Size = new System.Drawing.Size(32, 19);
		// 
		// CancelFilter
		// 
		this.CancelFilter.Name = "CancelFilter";
		this.CancelFilter.Size = new System.Drawing.Size(32, 19);
		// 
		// DaySelect
		// 
		this.DaySelect.BackColor = System.Drawing.Color.Sienna;
		this.DaySelect.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.DaySelect.ConerRadius = 10;
		this.DaySelect.DropPanelHeight = -1;
		this.DaySelect.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.DaySelect.ForeColor = System.Drawing.Color.White;
		this.DaySelect.IsRadius = true;
		this.DaySelect.IsShowRect = true;
		this.DaySelect.ItemWidth = 40;
		this.DaySelect.Location = new System.Drawing.Point(14, 7);
		this.DaySelect.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
		this.DaySelect.Name = "DaySelect";
		this.DaySelect.RectColor = System.Drawing.Color.Sienna;
		this.DaySelect.RectWidth = 1;
		this.DaySelect.SelectedIndex = -1;
		this.DaySelect.Size = new System.Drawing.Size(109, 45);
		this.DaySelect.TabIndex = 110;
		this.DaySelect.TextValue = "内容选择";
		this.DaySelect.TriangleColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
		this.DaySelect.SelectedChangedEvent += new System.EventHandler(this.DaySelect_SelectedChangedEvent);
		// 
		// ChallengeToday_ChallengeRewardGuide
		// 
		this.ChallengeToday_ChallengeRewardGuide.AutoSize = true;
		this.ChallengeToday_ChallengeRewardGuide.Font = new System.Drawing.Font("Microsoft YaHei UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.ChallengeToday_ChallengeRewardGuide.ForeColor = System.Drawing.Color.White;
		this.ChallengeToday_ChallengeRewardGuide.Location = new System.Drawing.Point(297, 28);
		this.ChallengeToday_ChallengeRewardGuide.Name = "ChallengeToday_ChallengeRewardGuide";
		this.ChallengeToday_ChallengeRewardGuide.Size = new System.Drawing.Size(82, 24);
		this.ChallengeToday_ChallengeRewardGuide.TabIndex = 111;
		this.ChallengeToday_ChallengeRewardGuide.Text = "挑战奖励";
		// 
		// RewardPreview
		// 
		this.RewardPreview.AutoSize = true;
		this.RewardPreview.BackColor = System.Drawing.Color.Transparent;
		this.RewardPreview.ForeColor = System.Drawing.Color.White;
		this.RewardPreview.Location = new System.Drawing.Point(151, 87);
		this.RewardPreview.Name = "RewardPreview";
		this.RewardPreview.Size = new System.Drawing.Size(356, 51);
		this.RewardPreview.TabIndex = 112;
		this.RewardPreview.PrevSeleted += new System.EventHandler(this.RewardPreview_PrevSeleted);
		this.RewardPreview.NextSeleted += new System.EventHandler(this.RewardPreview_NextSeleted);
		// 
		// TaskPanel
		// 
		this.TaskPanel.AutoScroll = true;
		this.TaskPanel.ContextMenuStrip = this.MenuStrip;
		this.TaskPanel.Controls.Add(this.challengeCell1);
		this.TaskPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.TaskPanel.Location = new System.Drawing.Point(0, 215);
		this.TaskPanel.Name = "TaskPanel";
		this.TaskPanel.Size = new System.Drawing.Size(672, 338);
		this.TaskPanel.TabIndex = 116;
		// 
		// challengeCell1
		// 
		this.challengeCell1.AutoSize = true;
		this.challengeCell1.BackColor = System.Drawing.Color.Transparent;
		this.challengeCell1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.challengeCell1.ForeColor = System.Drawing.Color.White;
		this.challengeCell1.Location = new System.Drawing.Point(0, 0);
		this.challengeCell1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.challengeCell1.Name = "challengeCell1";
		this.challengeCell1.Size = new System.Drawing.Size(672, 95);
		this.challengeCell1.TabIndex = 0;
		this.challengeCell1.Visible = false;
		// 
		// RequiredTime
		// 
		this.RequiredTime.BackColor = System.Drawing.Color.Transparent;
		this.RequiredTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.RequiredTime.ForeColor = System.Drawing.Color.White;
		this.RequiredTime.Location = new System.Drawing.Point(506, 7);
		this.RequiredTime.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
		this.RequiredTime.Name = "RequiredTime";
		this.RequiredTime.TabIndex = 117;
		this.RequiredTime.Text = "今日挑战<br/><image enablescale=\"true\" imagesetpath=\"00009076.RequiredTime\" scalerate" +
    "=\"1.4\"/>还剩";
		// 
		// ChallengeToday_CompleteCount
		// 
		this.ChallengeToday_CompleteCount.BackColor = System.Drawing.Color.Transparent;
		this.ChallengeToday_CompleteCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.ChallengeToday_CompleteCount.ForeColor = System.Drawing.Color.White;
		this.ChallengeToday_CompleteCount.Location = new System.Drawing.Point(308, 63);
		this.ChallengeToday_CompleteCount.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
		this.ChallengeToday_CompleteCount.Name = "ChallengeToday_CompleteCount";
		this.ChallengeToday_CompleteCount.TabIndex = 118;
		this.ChallengeToday_CompleteCount.Text = "完成数量";
		// 
		// ChallengeToday_TodayReward
		// 
		this.ChallengeToday_TodayReward.BackColor = System.Drawing.Color.Transparent;
		this.ChallengeToday_TodayReward.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.ChallengeToday_TodayReward.ForeColor = System.Drawing.Color.White;
		this.ChallengeToday_TodayReward.Location = new System.Drawing.Point(318, 184);
		this.ChallengeToday_TodayReward.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
		this.ChallengeToday_TodayReward.Name = "ChallengeToday_TodayReward";
		this.ChallengeToday_TodayReward.TabIndex = 120;
		this.ChallengeToday_TodayReward.Text = "今日任务奖励";
		// 
		// ChallengeToday_TodayQuest
		// 
		this.ChallengeToday_TodayQuest.BackColor = System.Drawing.Color.Transparent;
		this.ChallengeToday_TodayQuest.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.ChallengeToday_TodayQuest.ForeColor = System.Drawing.Color.White;
		this.ChallengeToday_TodayQuest.Location = new System.Drawing.Point(93, 184);
		this.ChallengeToday_TodayQuest.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
		this.ChallengeToday_TodayQuest.Name = "ChallengeToday_TodayQuest";
		this.ChallengeToday_TodayQuest.TabIndex = 121;
		this.ChallengeToday_TodayQuest.Text = "挑战任务";
		// 
		// ChallengeToday_State
		// 
		this.ChallengeToday_State.BackColor = System.Drawing.Color.Transparent;
		this.ChallengeToday_State.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.ChallengeToday_State.ForeColor = System.Drawing.Color.White;
		this.ChallengeToday_State.Location = new System.Drawing.Point(581, 184);
		this.ChallengeToday_State.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
		this.ChallengeToday_State.Name = "ChallengeToday_State";
		this.ChallengeToday_State.TabIndex = 122;
		this.ChallengeToday_State.Text = "状态";
		// 
		// contentPanel1
		// 
		this.contentPanel1.BackColor = System.Drawing.Color.Transparent;
		this.contentPanel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.contentPanel1.ForeColor = System.Drawing.Color.White;
		this.contentPanel1.Location = new System.Drawing.Point(638, 164);
		this.contentPanel1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
		this.contentPanel1.Name = "contentPanel1";
		this.contentPanel1.TabIndex = 123;
		this.contentPanel1.Text = "0/99";
		// 
		// ChallengeListFrm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(26)))), ((int)(((byte)(35)))));
		this.ClientSize = new System.Drawing.Size(672, 553);
		this.Controls.Add(this.contentPanel1);
		this.Controls.Add(this.ChallengeToday_State);
		this.Controls.Add(this.ChallengeToday_TodayReward);
		this.Controls.Add(this.ChallengeToday_CompleteCount);
		this.Controls.Add(this.RequiredTime);
		this.Controls.Add(this.TaskPanel);
		this.Controls.Add(this.RewardPreview);
		this.Controls.Add(this.ChallengeToday_ChallengeRewardGuide);
		this.Controls.Add(this.DaySelect);
		this.Controls.Add(this.ChallengeToday_TodayQuest);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Margin = new System.Windows.Forms.Padding(4);
		this.Name = "ChallengeListFrm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Title = "每日挑战";
		this.MenuStrip.ResumeLayout(false);
		this.TaskPanel.ResumeLayout(false);
		this.TaskPanel.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.ContextMenuStrip MenuStrip;
	private System.Windows.Forms.ToolStripMenuItem ModifyFilterRule;
	private System.Windows.Forms.ToolStripMenuItem CancelFilter;
	private HZH_Controls.Controls.UCCombox DaySelect;
	private System.Windows.Forms.ToolStripMenuItem OutputList;
	private System.Windows.Forms.Label ChallengeToday_ChallengeRewardGuide;
	private ChallengeListRewardPreview RewardPreview;
	private System.Windows.Forms.Panel TaskPanel;
	private ContentPanel RequiredTime;
	private ContentPanel ChallengeToday_CompleteCount;
	private ChallengeCell challengeCell1;
	private ContentPanel ChallengeToday_TodayReward;
	private ContentPanel ChallengeToday_TodayQuest;
	private ContentPanel ChallengeToday_State;
	private ContentPanel contentPanel1;
}