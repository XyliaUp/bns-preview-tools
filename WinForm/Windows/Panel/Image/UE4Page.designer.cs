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
			Label1 = new Label();
			Path_OutDir = new TextBox();
			ucBtnFillet1 = new HZH_Controls.Controls.UCBtnFillet();
			checkBox1 = new CheckBox();
			Selector = new ComboBox();
			SuspendLayout();
			// 
			// Btn_Output
			// 
			Btn_Output.BtnBackColor = Color.Empty;
			Btn_Output.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			Btn_Output.BtnForeColor = Color.FromArgb(192, 192, 255);
			Btn_Output.ConerRadius = 8;
			Btn_Output.Cursor = Cursors.Hand;
			Btn_Output.DialogResult = DialogResult.None;
			Btn_Output.EnabledMouseEffect = false;
			Btn_Output.FillColor = Color.White;
			resources.ApplyResources(Btn_Output, "Btn_Output");
			Btn_Output.IsRadius = true;
			Btn_Output.IsShowRect = true;
			Btn_Output.IsShowTips = false;
			Btn_Output.Name = "Btn_Output";
			Btn_Output.RectColor = Color.FromArgb(192, 192, 255);
			Btn_Output.RectWidth = 1;
			Btn_Output.TabStop = false;
			Btn_Output.TipsColor = Color.FromArgb(232, 30, 99);
			Btn_Output.TipsText = "";
			Btn_Output.Click += Btn_Output_BtnClick;
			// 
			// Label1
			// 
			resources.ApplyResources(Label1, "Label1");
			Label1.BackColor = Color.Transparent;
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
			ucBtnFillet1.BackColor = Color.Transparent;
			ucBtnFillet1.BtnFont = new Font("微软雅黑", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
			ucBtnFillet1.BtnImage = (Image)resources.GetObject("ucBtnFillet1.BtnImage");
			ucBtnFillet1.ConerRadius = 10;
			ucBtnFillet1.FillColor = Color.Transparent;
			resources.ApplyResources(ucBtnFillet1, "ucBtnFillet1");
			ucBtnFillet1.IsRadius = true;
			ucBtnFillet1.IsShowRect = true;
			ucBtnFillet1.Name = "ucBtnFillet1";
			ucBtnFillet1.RectColor = Color.FromArgb(220, 220, 220);
			ucBtnFillet1.RectWidth = 1;
			ucBtnFillet1.Click += ucBtnFillet1_BtnClick;
			// 
			// checkBox1
			// 
			resources.ApplyResources(checkBox1, "checkBox1");
			checkBox1.BackColor = Color.Transparent;
			checkBox1.Checked = true;
			checkBox1.CheckState = CheckState.Checked;
			checkBox1.Name = "checkBox1";
			checkBox1.UseVisualStyleBackColor = false;
			// 
			// Selector
			// 
			Selector.DrawMode = DrawMode.OwnerDrawFixed;
			Selector.FormattingEnabled = true;
			resources.ApplyResources(Selector, "Selector");
			Selector.Items.AddRange(new object[] { resources.GetString("Selector.Items"), resources.GetString("Selector.Items1"), resources.GetString("Selector.Items2"), resources.GetString("Selector.Items3"), resources.GetString("Selector.Items4"), resources.GetString("Selector.Items5"), resources.GetString("Selector.Items6"), resources.GetString("Selector.Items7"), resources.GetString("Selector.Items8"), resources.GetString("Selector.Items9"), resources.GetString("Selector.Items10") });
			Selector.Name = "Selector";
			Selector.DrawItem += Selector_DrawItem;
			// 
			// UE4Page
			// 
			AllowDrop = true;
			AutoScaleMode = AutoScaleMode.Inherit;
			BackColor = Color.White;
			Controls.Add(checkBox1);
			Controls.Add(ucBtnFillet1);
			Controls.Add(Label1);
			Controls.Add(Selector);
			Controls.Add(Path_OutDir);
			Controls.Add(Btn_Output);
			Name = "UE4Page";
			resources.ApplyResources(this, "$this");
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private HZH_Controls.Controls.UCBtnExt Btn_Output;
		private System.Windows.Forms.Label Label1;
		private System.Windows.Forms.TextBox Path_OutDir;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet1;
		private System.Windows.Forms.CheckBox checkBox1;
		private ComboBox Selector;
	}
}