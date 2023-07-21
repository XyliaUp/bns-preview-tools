using Xylia.Preview.UI.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class SkillChangedTooltip
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
			SkillIcon = new PictureBox();
			skillModifyCell1 = new Cell.SkillModifyCell();
			((System.ComponentModel.ISupportInitialize)SkillIcon).BeginInit();
			SuspendLayout();
			// 
			// SkillIcon
			// 
			SkillIcon.Image = Resource_Common.ItemIcon_Bg_Grade_1;
			SkillIcon.Location = new Point(8, 47);
			SkillIcon.Name = "SkillIcon";
			SkillIcon.Size = new Size(64, 64);
			SkillIcon.SizeMode = PictureBoxSizeMode.StretchImage;
			SkillIcon.TabIndex = 2;
			SkillIcon.TabStop = false;
			// 
			// skillModifyCell1
			// 
			skillModifyCell1.AutoSize = true;
			skillModifyCell1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			skillModifyCell1.BackColor = Color.Transparent;
			skillModifyCell1.Location = new Point(120, 47);
			skillModifyCell1.Margin = new Padding(4);
			skillModifyCell1.Name = "skillModifyCell1";
			skillModifyCell1.Size = new Size(143, 58);
			skillModifyCell1.SkillName = "龙腾九天";
			skillModifyCell1.TabIndex = 3;
			skillModifyCell1.TooltipText = "最多可以连续使用7次\r\n";
			// 
			// SkillByEquipmentTooltip
			// 
			BackColor = Color.Transparent;
			Controls.Add(skillModifyCell1);
			Controls.Add(SkillIcon);
			Name = "SkillByEquipmentTooltip";
			Size = new Size(267, 114);
			Title = "变更武功";
			Controls.SetChildIndex(SkillIcon, 0);
			Controls.SetChildIndex(skillModifyCell1, 0);
			((System.ComponentModel.ISupportInitialize)SkillIcon).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.PictureBox SkillIcon;
		private Cell.SkillModifyCell skillModifyCell1;
	}
}
