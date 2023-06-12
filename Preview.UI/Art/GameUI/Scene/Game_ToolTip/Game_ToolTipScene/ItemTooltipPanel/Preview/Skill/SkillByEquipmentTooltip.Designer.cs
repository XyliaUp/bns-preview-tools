namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class SkillByEquipmentTooltip
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
			this.SkillIcon = new System.Windows.Forms.PictureBox();
			this.skillModifyCell1 = new Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell.SkillModifyCell();
			((System.ComponentModel.ISupportInitialize)(this.SkillIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// SkillIcon
			// 
			this.SkillIcon.Image = global::Xylia.Preview.Resources.Resource_Common.ItemIcon_Bg_Grade_1;
			this.SkillIcon.Location = new System.Drawing.Point(8, 47);
			this.SkillIcon.Name = "SkillIcon";
			this.SkillIcon.Size = new System.Drawing.Size(64, 64);
			this.SkillIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.SkillIcon.TabIndex = 2;
			this.SkillIcon.TabStop = false;
			// 
			// skillModifyCell1
			// 
			this.skillModifyCell1.AutoSize = true;
			this.skillModifyCell1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.skillModifyCell1.BackColor = System.Drawing.Color.Transparent;
			this.skillModifyCell1.Location = new System.Drawing.Point(120, 47);
			this.skillModifyCell1.Margin = new System.Windows.Forms.Padding(4);
			this.skillModifyCell1.Name = "skillModifyCell1";
			this.skillModifyCell1.Size = new System.Drawing.Size(143, 58);
			this.skillModifyCell1.SkillName = "龙腾九天";
			this.skillModifyCell1.TabIndex = 3;
			this.skillModifyCell1.TooltipText = "最多可以连续使用7次\r\n";
			// 
			// SkillByEquipmentTooltip
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.skillModifyCell1);
			this.Controls.Add(this.SkillIcon);
			this.Name = "SkillByEquipmentTooltip";
			this.Size = new System.Drawing.Size(267, 114);
			this.Title = "变更武功";
			this.Controls.SetChildIndex(this.SkillIcon, 0);
			this.Controls.SetChildIndex(this.skillModifyCell1, 0);
			((System.ComponentModel.ISupportInitialize)(this.SkillIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox SkillIcon;
		private Cell.SkillModifyCell skillModifyCell1;
	}
}
