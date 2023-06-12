using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	partial class SkillPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillPreview));
			this.SkillName = new Xylia.Preview.GameUI.Controls.ItemNameCell();
			this.SkillIcon = new Xylia.Preview.GameUI.Controls.ItemIconCell();
			this.M1_Panel = new Xylia.Preview.GameUI.Scene.Skill.SkillTooltipPanel();
			this.M2_Panel = new Xylia.Preview.GameUI.Scene.Skill.SkillTooltipPanel();
			this.SUB_Panel = new Xylia.Preview.GameUI.Scene.Skill.SkillTooltipPanel();
			this.CONDITION_Panel = new Xylia.Preview.GameUI.Scene.Skill.SkillTooltipPanel();
			this.DamageRateStandardStats = new System.Windows.Forms.Label();
			this.DamageRatePvp = new System.Windows.Forms.Label();
			this.SkillGatherType = new System.Windows.Forms.PictureBox();
			this.reuse = new System.Windows.Forms.Label();
			this.scale = new System.Windows.Forms.Label();
			this.UI_Tooltip_Reuse = new System.Windows.Forms.Label();
			this.UI_Tooltip_Casting = new System.Windows.Forms.Label();
			this.UI_Tooltip_Scale = new System.Windows.Forms.Label();
			this.UI_Tooltip_Distance = new System.Windows.Forms.Label();
			this.Distance = new System.Windows.Forms.Label();
			this.Casting = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.SkillIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SkillGatherType)).BeginInit();
			this.SuspendLayout();
			// 
			// SkillName
			// 
			this.SkillName.AutoSize = true;
			this.SkillName.BackColor = System.Drawing.Color.Transparent;
			this.SkillName.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SkillName.ItemGrade = ((byte)(4));
			this.SkillName.Location = new System.Drawing.Point(3, 0);
			this.SkillName.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
			this.SkillName.Name = "SkillName";
			this.SkillName.Size = new System.Drawing.Size(107, 28);
			this.SkillName.TabIndex = 24;
			this.SkillName.TagImage = null;
			this.SkillName.Text = "SkillName";
			// 
			// SkillIcon
			// 
			this.SkillIcon.BackColor = System.Drawing.Color.Transparent;
			this.SkillIcon.ForeColor = System.Drawing.Color.Black;
			this.SkillIcon.FrameImage = null;
			this.SkillIcon.FrameType = true;
			this.SkillIcon.Location = new System.Drawing.Point(6, 28);
			this.SkillIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SkillIcon.Name = "SkillIcon";
			this.SkillIcon.Scale = 90;
			this.SkillIcon.ShowFrameImage = true;
			this.SkillIcon.ShowStackCount = false;
			this.SkillIcon.ShowStackCountOnlyOne = true;
			this.SkillIcon.Size = new System.Drawing.Size(90, 90);
			this.SkillIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.SkillIcon.StackCount = 0;
			this.SkillIcon.TabIndex = 25;
			this.SkillIcon.TabStop = false;
			// 
			// M1_Panel
			// 
			this.M1_Panel.Location = new System.Drawing.Point(99, 28);
			this.M1_Panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.M1_Panel.Name = "M1_Panel";
			this.M1_Panel.Size = new System.Drawing.Size(326, 36);
			this.M1_Panel.TabIndex = 26;
			// 
			// M2_Panel
			// 
			this.M2_Panel.Location = new System.Drawing.Point(99, 78);
			this.M2_Panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.M2_Panel.Name = "M2_Panel";
			this.M2_Panel.Size = new System.Drawing.Size(326, 41);
			this.M2_Panel.TabIndex = 27;
			// 
			// SUB_Panel
			// 
			this.SUB_Panel.Location = new System.Drawing.Point(6, 137);
			this.SUB_Panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SUB_Panel.Name = "SUB_Panel";
			this.SUB_Panel.Size = new System.Drawing.Size(419, 115);
			this.SUB_Panel.TabIndex = 28;
			// 
			// CONDITION_Panel
			// 
			this.CONDITION_Panel.Location = new System.Drawing.Point(6, 437);
			this.CONDITION_Panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.CONDITION_Panel.Name = "CONDITION_Panel";
			this.CONDITION_Panel.Size = new System.Drawing.Size(419, 84);
			this.CONDITION_Panel.TabIndex = 29;
			// 
			// DamageRateStandardStats
			// 
			this.DamageRateStandardStats.AutoSize = true;
			this.DamageRateStandardStats.ForeColor = System.Drawing.Color.White;
			this.DamageRateStandardStats.Location = new System.Drawing.Point(99, 281);
			this.DamageRateStandardStats.Name = "DamageRateStandardStats";
			this.DamageRateStandardStats.Size = new System.Drawing.Size(39, 17);
			this.DamageRateStandardStats.TabIndex = 29;
			this.DamageRateStandardStats.Text = "1.000";
			// 
			// DamageRatePvp
			// 
			this.DamageRatePvp.AutoSize = true;
			this.DamageRatePvp.ForeColor = System.Drawing.Color.White;
			this.DamageRatePvp.Location = new System.Drawing.Point(316, 281);
			this.DamageRatePvp.Name = "DamageRatePvp";
			this.DamageRatePvp.Size = new System.Drawing.Size(39, 17);
			this.DamageRatePvp.TabIndex = 30;
			this.DamageRatePvp.Text = "1.000";
			// 
			// SkillGatherType
			// 
			this.SkillGatherType.BackColor = System.Drawing.Color.Transparent;
			this.SkillGatherType.Image = ((System.Drawing.Image)(resources.GetObject("SkillGatherType.Image")));
			this.SkillGatherType.Location = new System.Drawing.Point(121, 368);
			this.SkillGatherType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SkillGatherType.Name = "SkillGatherType";
			this.SkillGatherType.Size = new System.Drawing.Size(50, 50);
			this.SkillGatherType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.SkillGatherType.TabIndex = 31;
			this.SkillGatherType.TabStop = false;
			// 
			// Reuse
			// 
			this.reuse.AutoSize = true;
			this.reuse.ForeColor = System.Drawing.Color.White;
			this.reuse.Location = new System.Drawing.Point(368, 386);
			this.reuse.Name = "Reuse";
			this.reuse.Size = new System.Drawing.Size(27, 17);
			this.reuse.TabIndex = 32;
			this.reuse.Text = "0秒";
			// 
			// Scale
			// 
			this.scale.AutoSize = true;
			this.scale.ForeColor = System.Drawing.Color.White;
			this.scale.Location = new System.Drawing.Point(130, 386);
			this.scale.Name = "Scale";
			this.scale.Size = new System.Drawing.Size(32, 17);
			this.scale.TabIndex = 33;
			this.scale.Text = "测试";
			// 
			// UI_Tooltip_Reuse
			// 
			this.UI_Tooltip_Reuse.AutoSize = true;
			this.UI_Tooltip_Reuse.ForeColor = System.Drawing.Color.White;
			this.UI_Tooltip_Reuse.Location = new System.Drawing.Point(365, 342);
			this.UI_Tooltip_Reuse.Name = "UI_Tooltip_Reuse";
			this.UI_Tooltip_Reuse.Size = new System.Drawing.Size(32, 17);
			this.UI_Tooltip_Reuse.TabIndex = 34;
			this.UI_Tooltip_Reuse.Text = "冷却";
			// 
			// UI_Tooltip_Casting
			// 
			this.UI_Tooltip_Casting.AutoSize = true;
			this.UI_Tooltip_Casting.ForeColor = System.Drawing.Color.White;
			this.UI_Tooltip_Casting.Location = new System.Drawing.Point(260, 342);
			this.UI_Tooltip_Casting.Name = "UI_Tooltip_Casting";
			this.UI_Tooltip_Casting.Size = new System.Drawing.Size(32, 17);
			this.UI_Tooltip_Casting.TabIndex = 35;
			this.UI_Tooltip_Casting.Text = "施展";
			// 
			// UI_Tooltip_Scale
			// 
			this.UI_Tooltip_Scale.AutoSize = true;
			this.UI_Tooltip_Scale.ForeColor = System.Drawing.Color.White;
			this.UI_Tooltip_Scale.Location = new System.Drawing.Point(129, 342);
			this.UI_Tooltip_Scale.Name = "UI_Tooltip_Scale";
			this.UI_Tooltip_Scale.Size = new System.Drawing.Size(32, 17);
			this.UI_Tooltip_Scale.TabIndex = 36;
			this.UI_Tooltip_Scale.Text = "范围";
			// 
			// UI_Tooltip_Distance
			// 
			this.UI_Tooltip_Distance.AutoSize = true;
			this.UI_Tooltip_Distance.ForeColor = System.Drawing.Color.White;
			this.UI_Tooltip_Distance.Location = new System.Drawing.Point(26, 342);
			this.UI_Tooltip_Distance.Name = "UI_Tooltip_Distance";
			this.UI_Tooltip_Distance.Size = new System.Drawing.Size(32, 17);
			this.UI_Tooltip_Distance.TabIndex = 37;
			this.UI_Tooltip_Distance.Text = "距离";
			// 
			// Distance
			// 
			this.Distance.AutoSize = true;
			this.Distance.ForeColor = System.Drawing.Color.White;
			this.Distance.Location = new System.Drawing.Point(26, 386);
			this.Distance.Name = "Distance";
			this.Distance.Size = new System.Drawing.Size(32, 17);
			this.Distance.TabIndex = 38;
			this.Distance.Text = "测试";
			// 
			// Casting
			// 
			this.Casting.AutoSize = true;
			this.Casting.ForeColor = System.Drawing.Color.White;
			this.Casting.Location = new System.Drawing.Point(261, 386);
			this.Casting.Name = "Casting";
			this.Casting.Size = new System.Drawing.Size(27, 17);
			this.Casting.TabIndex = 39;
			this.Casting.Text = "0秒";
			// 
			// SkillPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Casting);
			this.Controls.Add(this.Distance);
			this.Controls.Add(this.UI_Tooltip_Distance);
			this.Controls.Add(this.UI_Tooltip_Scale);
			this.Controls.Add(this.UI_Tooltip_Casting);
			this.Controls.Add(this.UI_Tooltip_Reuse);
			this.Controls.Add(this.scale);
			this.Controls.Add(this.reuse);
			this.Controls.Add(this.SkillGatherType);
			this.Controls.Add(this.CONDITION_Panel);
			this.Controls.Add(this.DamageRatePvp);
			this.Controls.Add(this.DamageRateStandardStats);
			this.Controls.Add(this.SUB_Panel);
			this.Controls.Add(this.M2_Panel);
			this.Controls.Add(this.M1_Panel);
			this.Controls.Add(this.SkillIcon);
			this.Controls.Add(this.SkillName);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "SkillPreview";
			this.Size = new System.Drawing.Size(428, 523);
			((System.ComponentModel.ISupportInitialize)(this.SkillIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SkillGatherType)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private ItemNameCell SkillName;
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
	}
}
