using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	partial class SkillTraitPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillTraitPreview));
			traitTier1 = new TraitTier();
			ucBtnExt1 = new HZH_Controls.Controls.UCBtnExt();
			TooltipEffectDescription = new ContentPanel();
			TooltipTrainDescription = new ContentPanel();
			TooltipTrainName = new ContentPanel();
			Select_Job = new HZH_Controls.Controls.UCCombox();
			Select_JobStyle = new HZH_Controls.Controls.UCCombox();
			SuspendLayout();
			// 
			// traitTier1
			// 
			resources.ApplyResources(traitTier1, "traitTier1");
			traitTier1.BackColor = Color.Transparent;
			traitTier1.Name = "traitTier1";
			traitTier1.Variation1 = null;
			traitTier1.Variation2 = null;
			traitTier1.Variation3 = null;
			// 
			// ucBtnExt1
			// 
			resources.ApplyResources(ucBtnExt1, "ucBtnExt1");
			ucBtnExt1.BtnBackColor = Color.Empty;
			ucBtnExt1.BtnFont = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt1.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt1.ConerRadius = 8;
			ucBtnExt1.Cursor = Cursors.Hand;
			ucBtnExt1.DialogResult = DialogResult.None;
			ucBtnExt1.EnabledMouseEffect = false;
			ucBtnExt1.FillColor = Color.Black;
			ucBtnExt1.IsRadius = true;
			ucBtnExt1.IsShowRect = true;
			ucBtnExt1.IsShowTips = false;
			ucBtnExt1.Name = "ucBtnExt1";
			ucBtnExt1.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt1.RectWidth = 1;
			ucBtnExt1.TabStop = false;
			ucBtnExt1.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt1.TipsText = "";
			ucBtnExt1.Click += ucBtnExt1_BtnClick;
			// 
			// TooltipEffectDescription
			// 
			resources.ApplyResources(TooltipEffectDescription, "TooltipEffectDescription");
			TooltipEffectDescription.BackColor = Color.Transparent;
			TooltipEffectDescription.ForeColor = Color.White;
			TooltipEffectDescription.Name = "TooltipEffectDescription";
			// 
			// TooltipTrainDescription
			// 
			resources.ApplyResources(TooltipTrainDescription, "TooltipTrainDescription");
			TooltipTrainDescription.BackColor = Color.Transparent;
			TooltipTrainDescription.ForeColor = Color.White;
			TooltipTrainDescription.Name = "TooltipTrainDescription";
			// 
			// TooltipTrainName
			// 
			resources.ApplyResources(TooltipTrainName, "TooltipTrainName");
			TooltipTrainName.BackColor = Color.Transparent;
			TooltipTrainName.ForeColor = Color.FromArgb(235, 215, 83);
			TooltipTrainName.Name = "TooltipTrainName";
			// 
			// Select_Job
			// 
			resources.ApplyResources(Select_Job, "Select_Job");
			Select_Job.BackColor = Color.Transparent;
			Select_Job.BoxStyle = ComboBoxStyle.DropDownList;
			Select_Job.ConerRadius = 10;
			Select_Job.DropPanelHeight = -1;
			Select_Job.ForeColor = Color.White;
			Select_Job.IsRadius = true;
			Select_Job.IsShowRect = true;
			Select_Job.ItemWidth = 40;
			Select_Job.Name = "Select_Job";
			Select_Job.RectColor = Color.Gray;
			Select_Job.RectWidth = 1;
			Select_Job.SelectedIndex = -1;
			Select_Job.TextAlign = HorizontalAlignment.Center;
			Select_Job.TextValue = "职业";
			Select_Job.TriangleColor = Color.FromArgb(255, 128, 128);
			Select_Job.SelectedChangedEvent += Select_Job_SelectedChangedEvent;
			// 
			// Select_JobStyle
			// 
			resources.ApplyResources(Select_JobStyle, "Select_JobStyle");
			Select_JobStyle.BackColor = Color.Transparent;
			Select_JobStyle.BoxStyle = ComboBoxStyle.DropDownList;
			Select_JobStyle.ConerRadius = 10;
			Select_JobStyle.DropPanelHeight = -1;
			Select_JobStyle.ForeColor = Color.White;
			Select_JobStyle.IsRadius = true;
			Select_JobStyle.IsShowRect = true;
			Select_JobStyle.ItemWidth = 40;
			Select_JobStyle.Name = "Select_JobStyle";
			Select_JobStyle.RectColor = Color.Gray;
			Select_JobStyle.RectWidth = 1;
			Select_JobStyle.SelectedIndex = -1;
			Select_JobStyle.TextAlign = HorizontalAlignment.Center;
			Select_JobStyle.TextValue = "派系";
			Select_JobStyle.TriangleColor = Color.FromArgb(255, 128, 128);
			Select_JobStyle.SelectedChangedEvent += Select_JobStyle_SelectedChangedEvent;
			// 
			// SkillTraitPreview
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Black;
			Controls.Add(Select_JobStyle);
			Controls.Add(Select_Job);
			Controls.Add(TooltipTrainName);
			Controls.Add(TooltipTrainDescription);
			Controls.Add(TooltipEffectDescription);
			Controls.Add(ucBtnExt1);
			Controls.Add(traitTier1);
			DoubleBuffered = true;
			ForeColor = Color.DimGray;
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Name = "SkillTraitPreview";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TraitTier traitTier1;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt1;
		public ContentPanel TooltipEffectDescription;
		public ContentPanel TooltipTrainDescription;
		public ContentPanel TooltipTrainName;
		private HZH_Controls.Controls.UCCombox Select_Job;
		private HZH_Controls.Controls.UCCombox Select_JobStyle;
	}
}

