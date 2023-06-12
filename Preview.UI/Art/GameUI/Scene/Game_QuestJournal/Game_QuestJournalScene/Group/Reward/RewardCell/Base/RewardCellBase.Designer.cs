using Xylia.Preview.GameUI.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardCell
{
	partial class RewardCellBase
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
			this.RewardTitle = new System.Windows.Forms.Label();
			this.panelContent1 = new Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// RewardTitle
			// 
			this.RewardTitle.AutoSize = true;
			this.RewardTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
			this.RewardTitle.Location = new System.Drawing.Point(-2, -2);
			this.RewardTitle.Name = "RewardTitle";
			this.RewardTitle.Size = new System.Drawing.Size(65, 20);
			this.RewardTitle.TabIndex = 24;
			this.RewardTitle.Text = "奖励类型";
			// 
			// panelContent1
			// 
			this.panelContent1.AutoSize = true;
			this.panelContent1.BackColor = System.Drawing.Color.Transparent;
			this.panelContent1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
			this.panelContent1.ForeColor = System.Drawing.Color.White;
			this.panelContent1.Location = new System.Drawing.Point(123, 0);
			this.panelContent1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.panelContent1.Name = "panelContent1";
			this.panelContent1.TabIndex = 26;
			this.panelContent1.Text = "奖励内容";
			// 
			// RewardCellBase
			// 

			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.panelContent1);
			this.Controls.Add(this.RewardTitle);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "RewardCellBase";
			this.Size = new System.Drawing.Size(323, 19);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label RewardTitle;
		protected Controls.ContentPanel panelContent1;
	}
}
