
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ChallengeToday
{
	partial class ChallengeCell
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
			this.AttractionInfo = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.ChallengeName = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.QuestList_RewardQuestName = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// AttractionInfo
			// 
			this.AttractionInfo.BackColor = System.Drawing.Color.Transparent;
			this.AttractionInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.AttractionInfo.ForeColor = System.Drawing.Color.White;
			this.AttractionInfo.Location = new System.Drawing.Point(18, 55);
			this.AttractionInfo.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.AttractionInfo.Name = "AttractionInfo";
			this.AttractionInfo.TabIndex = 8;
			this.AttractionInfo.Text = "Attraction";
			// 
			// ChallengeName
			// 
			this.ChallengeName.BackColor = System.Drawing.Color.Transparent;
			this.ChallengeName.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ChallengeName.ForeColor = System.Drawing.Color.White;
			this.ChallengeName.Location = new System.Drawing.Point(18, 26);
			this.ChallengeName.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.ChallengeName.Name = "ChallengeName";
			this.ChallengeName.TabIndex = 13;
			this.ChallengeName.Text = "课题名称";
			// 
			// QuestList_RewardQuestName
			// 
			this.QuestList_RewardQuestName.BackColor = System.Drawing.Color.Transparent;
			this.QuestList_RewardQuestName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.QuestList_RewardQuestName.ForeColor = System.Drawing.Color.White;
			this.QuestList_RewardQuestName.Location = new System.Drawing.Point(247, 15);
			this.QuestList_RewardQuestName.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.QuestList_RewardQuestName.Name = "QuestList_RewardQuestName";
			this.QuestList_RewardQuestName.TabIndex = 14;
			this.QuestList_RewardQuestName.Text = "任务时, 可以执行任务";
			// 
			// ChallengeCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.QuestList_RewardQuestName);
			this.Controls.Add(this.ChallengeName);
			this.Controls.Add(this.AttractionInfo);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ForeColor = System.Drawing.Color.White;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ChallengeCell";
			this.Size = new System.Drawing.Size(618, 110);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private ContentPanel AttractionInfo;
		private ContentPanel ChallengeName;
		private ContentPanel QuestList_RewardQuestName;
	}
}
