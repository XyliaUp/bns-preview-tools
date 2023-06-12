using Xylia.Preview.GameUI.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class Game_QuestJournalScene
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_QuestJournalScene));
			this.Quest_ICON = new System.Windows.Forms.PictureBox();
			this.Quest_Group = new System.Windows.Forms.Label();
			this.QuestName = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.DescInfo = new Xylia.Preview.GameUI.Scene.Game_QuestJournal.DescPanel();
			this.TaskInfo = new Xylia.Preview.GameUI.Scene.Game_QuestJournal.TaskPanel();
			this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SwitchTestMode = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenFileData = new System.Windows.Forms.ToolStripMenuItem();
			this.RewardInfo = new Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardPanel();
			((System.ComponentModel.ISupportInitialize)(this.Quest_ICON)).BeginInit();
			this.MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// Quest_ICON
			// 
			this.Quest_ICON.Image = ((System.Drawing.Image)(resources.GetObject("Quest_ICON.Image")));
			this.Quest_ICON.Location = new System.Drawing.Point(5, 26);
			this.Quest_ICON.Margin = new System.Windows.Forms.Padding(4);
			this.Quest_ICON.Name = "Quest_ICON";
			this.Quest_ICON.Size = new System.Drawing.Size(32, 32);
			this.Quest_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.Quest_ICON.TabIndex = 7;
			this.Quest_ICON.TabStop = false;
			// 
			// Quest_Group
			// 
			this.Quest_Group.AutoSize = true;
			this.Quest_Group.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.Quest_Group.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.Quest_Group.Location = new System.Drawing.Point(4, 4);
			this.Quest_Group.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Quest_Group.Name = "Quest_Group";
			this.Quest_Group.Size = new System.Drawing.Size(48, 19);
			this.Quest_Group.TabIndex = 11;
			this.Quest_Group.Text = "组名称";
			// 
			// QuestName
			// 
			this.QuestName.BackColor = System.Drawing.Color.Transparent;
			this.QuestName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.QuestName.ForeColor = System.Drawing.Color.LightGreen;
			this.QuestName.Location = new System.Drawing.Point(48, 29);
			this.QuestName.Margin = new System.Windows.Forms.Padding(2, 7, 2, 7);
			this.QuestName.Name = "QuestName";
			this.QuestName.TabIndex = 14;
			this.QuestName.Text = "任务名称";
			// 
			// DescInfo
			// 
			this.DescInfo.Location = new System.Drawing.Point(0, 66);
			this.DescInfo.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
			this.DescInfo.Name = "DescInfo";
			this.DescInfo.Size = new System.Drawing.Size(110, 57);
			this.DescInfo.TabIndex = 12;
			this.DescInfo.Text = "进行书信对话";
			this.DescInfo.Title = "内容";
			// 
			// TaskInfo
			// 
			this.TaskInfo.Location = new System.Drawing.Point(0, 162);
			this.TaskInfo.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
			this.TaskInfo.Name = "TaskInfo";
			this.TaskInfo.Size = new System.Drawing.Size(58, 26);
			this.TaskInfo.TabIndex = 10;
			this.TaskInfo.Title = "任务";
			// 
			// MenuStrip
			// 
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SwitchTestMode,
            this.toolStripMenuItem1,
            this.OpenFileData});
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(214, 70);
			// 
			// SwitchTestMode
			// 
			this.SwitchTestMode.Checked = true;
			this.SwitchTestMode.CheckOnClick = true;
			this.SwitchTestMode.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SwitchTestMode.Name = "SwitchTestMode";
			this.SwitchTestMode.Size = new System.Drawing.Size(213, 22);
			this.SwitchTestMode.Text = "切换测试模式 *";
			this.SwitchTestMode.Click += new System.EventHandler(this.SwitchTestMode_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Font = new System.Drawing.Font("Microsoft YaHei UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
			this.toolStripMenuItem1.Text = "* 表示下一次打开界面才会生效";
			this.toolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// OpenFileData
			// 
			this.OpenFileData.Name = "OpenFileData";
			this.OpenFileData.Size = new System.Drawing.Size(213, 22);
			this.OpenFileData.Text = "查看任务文件";
			this.OpenFileData.Click += new System.EventHandler(this.OpenFileData_Click);
			// 
			// RewardInfo
			// 
			this.RewardInfo.Location = new System.Drawing.Point(0, 230);
			this.RewardInfo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.RewardInfo.Name = "RewardInfo";
			this.RewardInfo.Size = new System.Drawing.Size(335, 50);
			this.RewardInfo.TabIndex = 15;
			this.RewardInfo.Title = "奖励";
			// 
			// QuestPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(846, 318);
			this.ContextMenuStrip = this.MenuStrip;
			this.Controls.Add(this.QuestName);
			this.Controls.Add(this.Quest_Group);
			this.Controls.Add(this.Quest_ICON);
			this.Controls.Add(this.DescInfo);
			this.Controls.Add(this.TaskInfo);
			this.Controls.Add(this.RewardInfo);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "QuestPreview";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "任务预览";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestPreview_FormClosing);
			this.Load += new System.EventHandler(this.QuestPreview_Load);
			((System.ComponentModel.ISupportInitialize)(this.Quest_ICON)).EndInit();
			this.MenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public System.Windows.Forms.PictureBox Quest_ICON;
		private TaskPanel TaskInfo;
		public System.Windows.Forms.Label Quest_Group;
		private DescPanel DescInfo;
	
		private ContentPanel QuestName;
		private System.Windows.Forms.ContextMenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem SwitchTestMode;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem OpenFileData;
		private RewardPanel RewardInfo;
	}
}
