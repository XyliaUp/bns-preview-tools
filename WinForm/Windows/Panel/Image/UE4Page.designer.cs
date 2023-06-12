using Xylia.Preview.Common.Extension;

namespace Xylia.Match.Windows.Panel
{
	partial class UE4Page
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UE4Page));
			Btn_Output = new HZH_Controls.Controls.UCBtnExt();
			Selector = new HZH_Controls.Controls.UCCombox();
			Label1 = new System.Windows.Forms.Label();
			Path_OutDir = new System.Windows.Forms.TextBox();
			ucBtnFillet1 = new HZH_Controls.Controls.UCBtnFillet();
			checkBox1 = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			// 
			// Btn_Output
			// 
			Btn_Output.BtnBackColor = System.Drawing.Color.Empty;
			Btn_Output.BtnFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			Btn_Output.BtnForeColor = System.Drawing.Color.FromArgb(192, 192, 255);
			Btn_Output.Text = "输出";
			Btn_Output.ConerRadius = 8;
			Btn_Output.Cursor = System.Windows.Forms.Cursors.Hand;
			Btn_Output.DialogResult = System.Windows.Forms.DialogResult.None;
			Btn_Output.EnabledMouseEffect = false;
			Btn_Output.FillColor = System.Drawing.Color.White;
			resources.ApplyResources(Btn_Output, "Btn_Output");
			Btn_Output.IsRadius = true;
			Btn_Output.IsShowRect = true;
			Btn_Output.IsShowTips = false;
			Btn_Output.Name = "Btn_Output";
			Btn_Output.RectColor = System.Drawing.Color.FromArgb(192, 192, 255);
			Btn_Output.RectWidth = 1;
			Btn_Output.TabStop = false;
			Btn_Output.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
			Btn_Output.TipsText = "";
			Btn_Output.Click += Btn_Output_BtnClick;
			// 
			// Selector
			// 
			Selector.BackColor = System.Drawing.Color.RosyBrown;
			Selector.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			Selector.ConerRadius = 10;
			Selector.DropPanelHeight = -1;
			resources.ApplyResources(Selector, "Selector");
			Selector.IsRadius = true;
			Selector.IsShowRect = true;
			Selector.ItemWidth = 30;
			Selector.Name = "Selector";
			Selector.RectColor = System.Drawing.Color.RosyBrown;
			Selector.RectWidth = 1;
			Selector.SelectedIndex = -1;
			Selector.Source.Add("GameUI/Resource/GameUI_Icon");
			Selector.Source.Add("GameUI/Resource/GameUI_Icon2nd");
			Selector.Source.Add("GameUI/Resource/GameUI_Icon3rd");
			Selector.Source.Add("GameUI/Resource/GameUI_Icon4th");
			Selector.Source.Add("GameUI/Resource/GameUI_Icon5th");
			Selector.Source.Add("GameUI/Resource/GameUI_Icon6th");
			Selector.Source.Add("GameUI/Resource/GameUI_TitleImage");
			Selector.Source.Add("GameUI/Resource/GameUI_ImageSet");
			Selector.Source.Add("GameUI/Resource/GameUI_ImageSet_R");
			Selector.Source.Add("BNSR/Content/Art/UI/V2/Resource");
			Selector.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			Selector.TextValue = "";
			Selector.TriangleColor = System.Drawing.Color.FromArgb(255, 128, 128);
			// 
			// Label1
			// 
			resources.ApplyResources(Label1, "Label1");
			Label1.BackColor = System.Drawing.Color.Transparent;
			Label1.Name = "Label1";
			// 
			// Path_OutDir
			// 
			resources.ApplyResources(Path_OutDir, "Path_OutDir");
			Path_OutDir.Name = "Path_OutDir";
			Path_OutDir.TextChanged += Path_OutDir_TextChanged;
			// 
			// ucBtnFillet1
			// 
			ucBtnFillet1.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet1.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet1.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet1.BtnImage");
			ucBtnFillet1.Text = "选择";
			ucBtnFillet1.ConerRadius = 10;
			ucBtnFillet1.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet1, "ucBtnFillet1");
			ucBtnFillet1.IsRadius = true;
			ucBtnFillet1.IsShowRect = true;
			ucBtnFillet1.Name = "ucBtnFillet1";
			ucBtnFillet1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet1.RectWidth = 1;
			ucBtnFillet1.Click += ucBtnFillet1_BtnClick;
			// 
			// checkBox1
			// 
			resources.ApplyResources(checkBox1, "checkBox1");
			checkBox1.BackColor = System.Drawing.Color.Transparent;
			checkBox1.Checked = true;
			checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox1.Name = "checkBox1";
			checkBox1.UseVisualStyleBackColor = false;
			// 
			// UE4Page
			// 
			AllowDrop = true;
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			BackColor = System.Drawing.Color.White;
			Controls.Add(checkBox1);
			Controls.Add(ucBtnFillet1);
			Controls.Add(Label1);
			Controls.Add(Path_OutDir);
			Controls.Add(Selector);
			Controls.Add(Btn_Output);
			Name = "UE4Page";
			resources.ApplyResources(this, "$this");
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private HZH_Controls.Controls.UCBtnExt Btn_Output;
		private HZH_Controls.Controls.UCCombox Selector;
		private System.Windows.Forms.Label Label1;
		private System.Windows.Forms.TextBox Path_OutDir;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet1;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}