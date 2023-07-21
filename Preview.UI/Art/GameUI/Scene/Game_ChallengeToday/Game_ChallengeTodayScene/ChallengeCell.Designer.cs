
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ChallengeToday
{
	partial class ChallengeCell
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
			AttractionInfo = new ContentPanel();
			ChallengeName = new ContentPanel();
			QuestList_RewardQuestName = new ContentPanel();
			SuspendLayout();
			// 
			// AttractionInfo
			// 
			AttractionInfo.BackColor = Color.Transparent;
			AttractionInfo.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			AttractionInfo.ForeColor = Color.White;
			AttractionInfo.Location = new Point(18, 55);
			AttractionInfo.Margin = new Padding(2, 5, 2, 5);
			AttractionInfo.Name = "AttractionInfo";
			AttractionInfo.TabIndex = 8;
			AttractionInfo.Text = "Attraction";
			// 
			// ChallengeName
			// 
			ChallengeName.BackColor = Color.Transparent;
			ChallengeName.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			ChallengeName.ForeColor = Color.White;
			ChallengeName.Location = new Point(18, 26);
			ChallengeName.Margin = new Padding(2, 5, 2, 5);
			ChallengeName.Name = "ChallengeName";
			ChallengeName.TabIndex = 13;
			ChallengeName.Text = "课题名称";
			// 
			// QuestList_RewardQuestName
			// 
			QuestList_RewardQuestName.BackColor = Color.Transparent;
			QuestList_RewardQuestName.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			QuestList_RewardQuestName.ForeColor = Color.White;
			QuestList_RewardQuestName.Location = new Point(247, 15);
			QuestList_RewardQuestName.Margin = new Padding(2, 5, 2, 5);
			QuestList_RewardQuestName.Name = "QuestList_RewardQuestName";
			QuestList_RewardQuestName.TabIndex = 14;
			QuestList_RewardQuestName.Text = "任务时, 可以执行任务";
			// 
			// ChallengeCell
			// 
			AutoScaleDimensions = new SizeF(8F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			BackColor = Color.Transparent;
			Controls.Add(AttractionInfo);
			Controls.Add(QuestList_RewardQuestName);
			Controls.Add(ChallengeName);
			Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			ForeColor = Color.White;
			Margin = new Padding(3, 4, 3, 4);
			Name = "ChallengeCell";
			Size = new Size(618, 110);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private ContentPanel AttractionInfo;
		private ContentPanel ChallengeName;
		private ContentPanel QuestList_RewardQuestName;
	}
}
