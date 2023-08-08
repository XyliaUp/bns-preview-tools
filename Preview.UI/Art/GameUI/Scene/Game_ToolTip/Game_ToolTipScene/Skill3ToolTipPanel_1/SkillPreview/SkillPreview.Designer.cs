using Xylia.Preview.GameUI.Scene.Game_ToolTipScene.Skill3ToolTipPanel_1.SkillPreview;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
    partial class SkillPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillPreview));
			SkillName = new ItemNamePanel();
			SkillIcon = new ItemIconCell();
			M1_Panel = new SkillTooltipPanel();
			M2_Panel = new SkillTooltipPanel();
			SUB_Panel = new SkillTooltipPanel();
			CONDITION_Panel = new SkillTooltipPanel();
			DamageRateStandardStats = new Label();
			DamageRatePvp = new Label();
			SkillGatherType = new PictureBox();
			reuse = new Label();
			scale = new Label();
			UI_Tooltip_Reuse = new Label();
			UI_Tooltip_Casting = new Label();
			UI_Tooltip_Scale = new Label();
			UI_Tooltip_Distance = new Label();
			Distance = new Label();
			Casting = new Label();
			STANCE_Panel = new SkillTooltipPanel();
			((System.ComponentModel.ISupportInitialize)SkillIcon).BeginInit();
			((System.ComponentModel.ISupportInitialize)SkillGatherType).BeginInit();
			SuspendLayout();
			// 
			// SkillName
			// 
			SkillName.BackColor = Color.Transparent;
			SkillName.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
			SkillName.ItemGrade = 4;
			SkillName.Location = new Point(3, 0);
			SkillName.Margin = new Padding(7, 10, 7, 10);
			SkillName.Name = "SkillName";
			SkillName.TabIndex = 24;
			SkillName.TagImage = null;
			SkillName.Text = "SkillName";
			// 
			// SkillIcon
			// 
			SkillIcon.BackColor = Color.Transparent;
			SkillIcon.ForeColor = Color.Black;
			SkillIcon.FrameImage = null;
			SkillIcon.FrameType = true;
			SkillIcon.Location = new Point(6, 28);
			SkillIcon.Margin = new Padding(4, 3, 4, 3);
			SkillIcon.Name = "SkillIcon";
			SkillIcon.Scale = 90;
			SkillIcon.ShowFrameImage = true;
			SkillIcon.ShowStackCount = false;
			SkillIcon.ShowStackCountOnlyOne = true;
			SkillIcon.Size = new Size(90, 90);
			SkillIcon.SizeMode = PictureBoxSizeMode.StretchImage;
			SkillIcon.StackCount = 0;
			SkillIcon.TabIndex = 25;
			SkillIcon.TabStop = false;
			// 
			// M1_Panel
			// 
			M1_Panel.Location = new Point(99, 28);
			M1_Panel.Margin = new Padding(3, 2, 3, 2);
			M1_Panel.Name = "M1_Panel";
			M1_Panel.Size = new Size(326, 36);
			M1_Panel.TabIndex = 26;
			// 
			// M2_Panel
			// 
			M2_Panel.Location = new Point(99, 78);
			M2_Panel.Margin = new Padding(3, 2, 3, 2);
			M2_Panel.Name = "M2_Panel";
			M2_Panel.Size = new Size(326, 41);
			M2_Panel.TabIndex = 27;
			// 
			// SUB_Panel
			// 
			SUB_Panel.Location = new Point(6, 137);
			SUB_Panel.Margin = new Padding(3, 2, 3, 2);
			SUB_Panel.Name = "SUB_Panel";
			SUB_Panel.Size = new Size(419, 115);
			SUB_Panel.TabIndex = 28;
			// 
			// CONDITION_Panel
			// 
			CONDITION_Panel.Location = new Point(6, 437);
			CONDITION_Panel.Margin = new Padding(3, 2, 3, 2);
			CONDITION_Panel.Name = "CONDITION_Panel";
			CONDITION_Panel.Size = new Size(419, 58);
			CONDITION_Panel.TabIndex = 29;
			// 
			// DamageRateStandardStats
			// 
			DamageRateStandardStats.AutoSize = true;
			DamageRateStandardStats.ForeColor = Color.White;
			DamageRateStandardStats.Location = new Point(99, 281);
			DamageRateStandardStats.Name = "DamageRateStandardStats";
			DamageRateStandardStats.Size = new Size(39, 17);
			DamageRateStandardStats.TabIndex = 29;
			DamageRateStandardStats.Text = "1.000";
			// 
			// DamageRatePvp
			// 
			DamageRatePvp.AutoSize = true;
			DamageRatePvp.ForeColor = Color.White;
			DamageRatePvp.Location = new Point(316, 281);
			DamageRatePvp.Name = "DamageRatePvp";
			DamageRatePvp.Size = new Size(39, 17);
			DamageRatePvp.TabIndex = 30;
			DamageRatePvp.Text = "1.000";
			// 
			// SkillGatherType
			// 
			SkillGatherType.BackColor = Color.Transparent;
			SkillGatherType.Image = (Image)resources.GetObject("SkillGatherType.Image");
			SkillGatherType.Location = new Point(121, 368);
			SkillGatherType.Margin = new Padding(4, 3, 4, 3);
			SkillGatherType.Name = "SkillGatherType";
			SkillGatherType.Size = new Size(50, 50);
			SkillGatherType.SizeMode = PictureBoxSizeMode.AutoSize;
			SkillGatherType.TabIndex = 31;
			SkillGatherType.TabStop = false;
			// 
			// reuse
			// 
			reuse.AutoSize = true;
			reuse.ForeColor = Color.White;
			reuse.Location = new Point(368, 386);
			reuse.Name = "reuse";
			reuse.Size = new Size(27, 17);
			reuse.TabIndex = 32;
			reuse.Text = "0秒";
			// 
			// scale
			// 
			scale.AutoSize = true;
			scale.ForeColor = Color.White;
			scale.Location = new Point(130, 386);
			scale.Name = "scale";
			scale.Size = new Size(32, 17);
			scale.TabIndex = 33;
			scale.Text = "测试";
			// 
			// UI_Tooltip_Reuse
			// 
			UI_Tooltip_Reuse.AutoSize = true;
			UI_Tooltip_Reuse.ForeColor = Color.White;
			UI_Tooltip_Reuse.Location = new Point(365, 342);
			UI_Tooltip_Reuse.Name = "UI_Tooltip_Reuse";
			UI_Tooltip_Reuse.Size = new Size(32, 17);
			UI_Tooltip_Reuse.TabIndex = 34;
			UI_Tooltip_Reuse.Text = "冷却";
			// 
			// UI_Tooltip_Casting
			// 
			UI_Tooltip_Casting.AutoSize = true;
			UI_Tooltip_Casting.ForeColor = Color.White;
			UI_Tooltip_Casting.Location = new Point(260, 342);
			UI_Tooltip_Casting.Name = "UI_Tooltip_Casting";
			UI_Tooltip_Casting.Size = new Size(32, 17);
			UI_Tooltip_Casting.TabIndex = 35;
			UI_Tooltip_Casting.Text = "施展";
			// 
			// UI_Tooltip_Scale
			// 
			UI_Tooltip_Scale.AutoSize = true;
			UI_Tooltip_Scale.ForeColor = Color.White;
			UI_Tooltip_Scale.Location = new Point(129, 342);
			UI_Tooltip_Scale.Name = "UI_Tooltip_Scale";
			UI_Tooltip_Scale.Size = new Size(32, 17);
			UI_Tooltip_Scale.TabIndex = 36;
			UI_Tooltip_Scale.Text = "范围";
			// 
			// UI_Tooltip_Distance
			// 
			UI_Tooltip_Distance.AutoSize = true;
			UI_Tooltip_Distance.ForeColor = Color.White;
			UI_Tooltip_Distance.Location = new Point(26, 342);
			UI_Tooltip_Distance.Name = "UI_Tooltip_Distance";
			UI_Tooltip_Distance.Size = new Size(32, 17);
			UI_Tooltip_Distance.TabIndex = 37;
			UI_Tooltip_Distance.Text = "距离";
			// 
			// Distance
			// 
			Distance.AutoSize = true;
			Distance.ForeColor = Color.White;
			Distance.Location = new Point(26, 386);
			Distance.Name = "Distance";
			Distance.Size = new Size(32, 17);
			Distance.TabIndex = 38;
			Distance.Text = "测试";
			// 
			// Casting
			// 
			Casting.AutoSize = true;
			Casting.ForeColor = Color.White;
			Casting.Location = new Point(261, 386);
			Casting.Name = "Casting";
			Casting.Size = new Size(27, 17);
			Casting.TabIndex = 39;
			Casting.Text = "0秒";
			// 
			// STANCE_Panel
			// 
			STANCE_Panel.Location = new Point(6, 502);
			STANCE_Panel.Margin = new Padding(3, 2, 3, 2);
			STANCE_Panel.Name = "STANCE_Panel";
			STANCE_Panel.Size = new Size(419, 53);
			STANCE_Panel.TabIndex = 40;
			// 
			// SkillPreview
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			BackColor = Color.Transparent;
			Controls.Add(STANCE_Panel);
			Controls.Add(Casting);
			Controls.Add(Distance);
			Controls.Add(UI_Tooltip_Distance);
			Controls.Add(UI_Tooltip_Scale);
			Controls.Add(UI_Tooltip_Casting);
			Controls.Add(UI_Tooltip_Reuse);
			Controls.Add(scale);
			Controls.Add(reuse);
			Controls.Add(SkillGatherType);
			Controls.Add(CONDITION_Panel);
			Controls.Add(DamageRatePvp);
			Controls.Add(DamageRateStandardStats);
			Controls.Add(SUB_Panel);
			Controls.Add(M2_Panel);
			Controls.Add(M1_Panel);
			Controls.Add(SkillIcon);
			Controls.Add(SkillName);
			Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			Margin = new Padding(4, 3, 4, 3);
			Name = "SkillPreview";
			Size = new Size(428, 557);
			((System.ComponentModel.ISupportInitialize)SkillIcon).EndInit();
			((System.ComponentModel.ISupportInitialize)SkillGatherType).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private ItemNamePanel SkillName;
		private ItemIconCell SkillIcon;
		private SkillTooltipPanel M1_Panel;
		private SkillTooltipPanel M2_Panel;
		private SkillTooltipPanel SUB_Panel;
		private SkillTooltipPanel CONDITION_Panel;
		private System.Windows.Forms.Label DamageRateStandardStats;
		private System.Windows.Forms.Label DamageRatePvp;
		private System.Windows.Forms.PictureBox SkillGatherType;
		private System.Windows.Forms.Label reuse;
		private System.Windows.Forms.Label scale;
		private System.Windows.Forms.Label UI_Tooltip_Reuse;
		private System.Windows.Forms.Label UI_Tooltip_Casting;
		private System.Windows.Forms.Label UI_Tooltip_Scale;
		private System.Windows.Forms.Label UI_Tooltip_Distance;
		private System.Windows.Forms.Label Distance;
		private System.Windows.Forms.Label Casting;
		private SkillTooltipPanel STANCE_Panel;
	}
}
