using Xylia.Preview.UI.Custom.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class Game_QuestJournalScene
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_QuestJournalScene));
			Quest_ICON = new PictureBox();
			Quest_Group = new Label();
			QuestName = new ContentPanel();
			DescInfo = new DescPanel();
			TaskInfo = new TaskPanel();
			MenuStrip = new ContextMenuStrip(components);
			SwitchTestMode = new ToolStripMenuItem();
			toolStripMenuItem1 = new ToolStripMenuItem();
			OpenFileData = new ToolStripMenuItem();
			RewardInfo = new RewardPanel();
			((System.ComponentModel.ISupportInitialize)Quest_ICON).BeginInit();
			MenuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// Quest_ICON
			// 
			Quest_ICON.Image = (Image)resources.GetObject("Quest_ICON.Image");
			Quest_ICON.Location = new Point(5, 26);
			Quest_ICON.Margin = new Padding(4);
			Quest_ICON.Name = "Quest_ICON";
			Quest_ICON.Size = new Size(32, 32);
			Quest_ICON.SizeMode = PictureBoxSizeMode.AutoSize;
			Quest_ICON.TabIndex = 7;
			Quest_ICON.TabStop = false;
			// 
			// Quest_Group
			// 
			Quest_Group.AutoSize = true;
			Quest_Group.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
			Quest_Group.ForeColor = SystemColors.ActiveBorder;
			Quest_Group.Location = new Point(4, 4);
			Quest_Group.Margin = new Padding(4, 0, 4, 0);
			Quest_Group.Name = "Quest_Group";
			Quest_Group.Size = new Size(48, 19);
			Quest_Group.TabIndex = 11;
			Quest_Group.Text = "组名称";
			// 
			// QuestName
			// 
			QuestName.BackColor = Color.Transparent;
			QuestName.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			QuestName.ForeColor = Color.LightGreen;
			QuestName.Location = new Point(48, 29);
			QuestName.Margin = new Padding(2, 7, 2, 7);
			QuestName.Name = "QuestName";
			QuestName.TabIndex = 14;
			QuestName.Text = "任务名称";
			// 
			// DescInfo
			// 
			DescInfo.AutoSize = true;
			DescInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			DescInfo.BackColor = Color.Transparent;
			DescInfo.Location = new Point(0, 66);
			DescInfo.Margin = new Padding(6, 8, 6, 8);
			DescInfo.Name = "DescInfo";
			DescInfo.Size = new Size(110, 57);
			DescInfo.TabIndex = 12;
			DescInfo.Text = "进行书信对话";
			DescInfo.Title = "内容";
			// 
			// TaskInfo
			// 
			TaskInfo.AutoSize = true;
			TaskInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			TaskInfo.BackColor = Color.Transparent;
			TaskInfo.Location = new Point(0, 162);
			TaskInfo.Margin = new Padding(6, 8, 6, 8);
			TaskInfo.Name = "TaskInfo";
			TaskInfo.Size = new Size(58, 26);
			TaskInfo.TabIndex = 10;
			TaskInfo.Title = "任务";
			// 
			// MenuStrip
			// 
			MenuStrip.Items.AddRange(new ToolStripItem[] { SwitchTestMode, toolStripMenuItem1, OpenFileData });
			MenuStrip.Name = "MenuStrip";
			MenuStrip.Size = new Size(214, 70);
			// 
			// SwitchTestMode
			// 
			SwitchTestMode.Checked = true;
			SwitchTestMode.CheckOnClick = true;
			SwitchTestMode.CheckState = CheckState.Checked;
			SwitchTestMode.Name = "SwitchTestMode";
			SwitchTestMode.Size = new Size(213, 22);
			SwitchTestMode.Text = "切换测试模式 *";
			SwitchTestMode.Click += SwitchTestMode_Click;
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.Font = new Font("Microsoft YaHei UI", 7F, FontStyle.Bold, GraphicsUnit.Point);
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new Size(213, 22);
			toolStripMenuItem1.Text = "* 表示下一次打开界面才会生效";
			toolStripMenuItem1.TextImageRelation = TextImageRelation.ImageAboveText;
			// 
			// OpenFileData
			// 
			OpenFileData.Name = "OpenFileData";
			OpenFileData.Size = new Size(213, 22);
			OpenFileData.Text = "查看任务文件";
			OpenFileData.Click += OpenFileData_Click;
			// 
			// RewardInfo
			// 
			RewardInfo.AutoSize = true;
			RewardInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			RewardInfo.BackColor = Color.Transparent;
			RewardInfo.ForeColor = Color.White;
			RewardInfo.Location = new Point(0, 230);
			RewardInfo.Margin = new Padding(5, 6, 5, 6);
			RewardInfo.Name = "RewardInfo";
			RewardInfo.Size = new Size(335, 50);
			RewardInfo.TabIndex = 15;
			RewardInfo.Title = "奖励";
			// 
			// Game_QuestJournalScene
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoScroll = true;
			BackColor = Color.Black;
			ClientSize = new Size(846, 318);
			ContextMenuStrip = MenuStrip;
			Controls.Add(QuestName);
			Controls.Add(Quest_Group);
			Controls.Add(Quest_ICON);
			Controls.Add(DescInfo);
			Controls.Add(TaskInfo);
			Controls.Add(RewardInfo);
			ForeColor = Color.White;
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Margin = new Padding(4);
			Name = "Game_QuestJournalScene";
			StartPosition = FormStartPosition.Manual;
			Title = "任务预览";
			FormClosing += QuestPreview_FormClosing;
			Shown += QuestPreview_Shown;
			((System.ComponentModel.ISupportInitialize)Quest_ICON).EndInit();
			MenuStrip.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
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
