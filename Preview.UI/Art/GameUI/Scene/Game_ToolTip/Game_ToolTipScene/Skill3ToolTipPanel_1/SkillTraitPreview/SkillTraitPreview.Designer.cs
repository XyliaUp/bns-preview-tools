namespace Xylia.Preview.GameUI.Scene.Skill
{
    partial class SkillTraitPreview
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的Functions - 不要修改
        /// 使用代码编辑器修改此Functions的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.traitTier1 = new Xylia.Preview.GameUI.Scene.Skill.TraitTier();
			this.ucBtnExt1 = new HZH_Controls.Controls.UCBtnExt();
			this.TooltipEffectDescription = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.TooltipTrainDescription = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.TooltipTrainName = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.Select_Job = new HZH_Controls.Controls.UCCombox();
			this.Select_JobStyle = new HZH_Controls.Controls.UCCombox();
			this.SuspendLayout();
			// 
			// traitTier1
			// 
			this.traitTier1.AutoSize = true;
			this.traitTier1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.traitTier1.BackColor = System.Drawing.Color.Transparent;
			this.traitTier1.Location = new System.Drawing.Point(2, 90);
			this.traitTier1.Name = "traitTier1";
			this.traitTier1.Size = new System.Drawing.Size(626, 74);
			this.traitTier1.TabIndex = 0;
			this.traitTier1.Variation1 = null;
			this.traitTier1.Variation2 = null;
			this.traitTier1.Variation3 = null;
			this.traitTier1.Visible = false;
			// 
			// ucBtnExt1
			// 
			this.ucBtnExt1.BtnBackColor = System.Drawing.Color.Empty;
			this.ucBtnExt1.BtnFont = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ucBtnExt1.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.ucBtnExt1.Text = "技能图标";
			this.ucBtnExt1.ConerRadius = 8;
			this.ucBtnExt1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnExt1.EnabledMouseEffect = false;
			this.ucBtnExt1.FillColor = System.Drawing.Color.Black;
			this.ucBtnExt1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.ucBtnExt1.IsRadius = true;
			this.ucBtnExt1.IsShowRect = true;
			this.ucBtnExt1.IsShowTips = false;
			this.ucBtnExt1.Location = new System.Drawing.Point(537, 536);
			this.ucBtnExt1.Margin = new System.Windows.Forms.Padding(0);
			this.ucBtnExt1.Name = "ucBtnExt1";
			this.ucBtnExt1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.ucBtnExt1.RectWidth = 1;
			this.ucBtnExt1.Size = new System.Drawing.Size(77, 41);
			this.ucBtnExt1.TabIndex = 119;
			this.ucBtnExt1.TabStop = false;
			this.ucBtnExt1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
			this.ucBtnExt1.TipsText = "";
			this.ucBtnExt1.Click += new System.EventHandler(this.ucBtnExt1_BtnClick);
			// 
			// TooltipEffectDescription
			// 
			this.TooltipEffectDescription.BackColor = System.Drawing.Color.Transparent;
			this.TooltipEffectDescription.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TooltipEffectDescription.FontName = null;
			this.TooltipEffectDescription.ForeColor = System.Drawing.Color.White;
			this.TooltipEffectDescription.Location = new System.Drawing.Point(641, 106);
			this.TooltipEffectDescription.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.TooltipEffectDescription.Name = "TooltipEffectDescription";
			this.TooltipEffectDescription.TabIndex = 120;
			this.TooltipEffectDescription.Text = "EffectDescription";
			// 
			// TooltipTrainDescription
			// 
			this.TooltipTrainDescription.BackColor = System.Drawing.Color.Transparent;
			this.TooltipTrainDescription.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TooltipTrainDescription.FontName = null;
			this.TooltipTrainDescription.ForeColor = System.Drawing.Color.White;
			this.TooltipTrainDescription.Location = new System.Drawing.Point(641, 81);
			this.TooltipTrainDescription.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.TooltipTrainDescription.Name = "TooltipTrainDescription";
			this.TooltipTrainDescription.TabIndex = 121;
			this.TooltipTrainDescription.Text = "TrainDescription";
			// 
			// TooltipTrainName
			// 
			this.TooltipTrainName.BackColor = System.Drawing.Color.Transparent;
			this.TooltipTrainName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TooltipTrainName.FontName = null;
			this.TooltipTrainName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(215)))), ((int)(((byte)(83)))));
			this.TooltipTrainName.Location = new System.Drawing.Point(641, 47);
			this.TooltipTrainName.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.TooltipTrainName.Name = "TooltipTrainName";
			this.TooltipTrainName.TabIndex = 122;
			this.TooltipTrainName.Text = "TrainName";
			// 
			// Select_Job
			// 
			this.Select_Job.BackColor = System.Drawing.Color.Transparent;
			this.Select_Job.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Select_Job.ConerRadius = 10;
			this.Select_Job.DropPanelHeight = -1;
			this.Select_Job.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Select_Job.ForeColor = System.Drawing.Color.White;
			this.Select_Job.IsRadius = true;
			this.Select_Job.IsShowRect = true;
			this.Select_Job.ItemWidth = 40;
			this.Select_Job.Location = new System.Drawing.Point(8, 8);
			this.Select_Job.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.Select_Job.Name = "Select_Job";
			this.Select_Job.RectColor = System.Drawing.Color.Gray;
			this.Select_Job.RectWidth = 1;
			this.Select_Job.SelectedIndex = -1;
			this.Select_Job.Size = new System.Drawing.Size(97, 32);
			this.Select_Job.TabIndex = 123;
			this.Select_Job.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Select_Job.TextValue = "职业";
			this.Select_Job.TriangleColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.Select_Job.SelectedChangedEvent += new System.EventHandler(this.Select_Job_SelectedChangedEvent);
			// 
			// Select_JobStyle
			// 
			this.Select_JobStyle.BackColor = System.Drawing.Color.Transparent;
			this.Select_JobStyle.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Select_JobStyle.ConerRadius = 10;
			this.Select_JobStyle.DropPanelHeight = -1;
			this.Select_JobStyle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Select_JobStyle.ForeColor = System.Drawing.Color.White;
			this.Select_JobStyle.IsRadius = true;
			this.Select_JobStyle.IsShowRect = true;
			this.Select_JobStyle.ItemWidth = 40;
			this.Select_JobStyle.Location = new System.Drawing.Point(124, 8);
			this.Select_JobStyle.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.Select_JobStyle.Name = "Select_JobStyle";
			this.Select_JobStyle.RectColor = System.Drawing.Color.Gray;
			this.Select_JobStyle.RectWidth = 1;
			this.Select_JobStyle.SelectedIndex = -1;
			this.Select_JobStyle.Size = new System.Drawing.Size(97, 32);
			this.Select_JobStyle.TabIndex = 124;
			this.Select_JobStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Select_JobStyle.TextValue = "派系";
			this.Select_JobStyle.TriangleColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.Select_JobStyle.SelectedChangedEvent += new System.EventHandler(this.Select_JobStyle_SelectedChangedEvent);
			// 
			// SkillTraitPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(920, 590);
			this.Controls.Add(this.Select_JobStyle);
			this.Controls.Add(this.Select_Job);
			this.Controls.Add(this.TooltipTrainName);
			this.Controls.Add(this.TooltipTrainDescription);
			this.Controls.Add(this.TooltipEffectDescription);
			this.Controls.Add(this.ucBtnExt1);
			this.Controls.Add(this.traitTier1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.DimGray;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SkillTraitPreview";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "武功特性";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private TraitTier traitTier1;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt1;
		public Controls.ContentPanel TooltipEffectDescription;
		public Controls.ContentPanel TooltipTrainDescription;
		public Controls.ContentPanel TooltipTrainName;
		private HZH_Controls.Controls.UCCombox Select_Job;
		private HZH_Controls.Controls.UCCombox Select_JobStyle;
	}
}

