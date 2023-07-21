using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardCell
{
	 partial  class OptionaItem
	{
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 20);
			this.label1.TabIndex = 10;
			this.label1.Text = "选择";
			// 
			// OptionaItem
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.label1);
			this.Name = "OptionaItem";
			this.Size = new System.Drawing.Size(332, 47);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.Label label1;
	}
}
