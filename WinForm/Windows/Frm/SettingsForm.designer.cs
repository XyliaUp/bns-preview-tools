namespace Xylia.Match.Windows.Forms
{
	partial class SettingsForm
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			colorDialog1 = new System.Windows.Forms.ColorDialog();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			FolderTabPage = new System.Windows.Forms.TabPage();
			ucBtnFillet2 = new HZH_Controls.Controls.UCBtnFillet();
			lbl_Region = new System.Windows.Forms.Label();
			GRoot_Path = new System.Windows.Forms.TextBox();
			GRoot_Note = new System.Windows.Forms.Label();
			Faster_Folder_Path = new System.Windows.Forms.TextBox();
			Faster_Folder_Note = new System.Windows.Forms.Label();
			ucBtnFillet1 = new HZH_Controls.Controls.UCBtnFillet();
			SettingsTabControl = new System.Windows.Forms.TabControl();
			OptionTabPage = new System.Windows.Forms.TabPage();
			groupBox1 = new System.Windows.Forms.GroupBox();
			Preview_DataTest = new System.Windows.Forms.TrackBar();
			label1 = new System.Windows.Forms.Label();
			Folder = new System.Windows.Forms.FolderBrowserDialog();
			FolderTabPage.SuspendLayout();
			SettingsTabControl.SuspendLayout();
			OptionTabPage.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Preview_DataTest).BeginInit();
			SuspendLayout();
			// 
			// colorDialog1
			// 
			colorDialog1.AnyColor = true;
			colorDialog1.FullOpen = true;
			// 
			// FolderTabPage
			// 
			resources.ApplyResources(FolderTabPage, "FolderTabPage");
			FolderTabPage.Controls.Add(ucBtnFillet2);
			FolderTabPage.Controls.Add(lbl_Region);
			FolderTabPage.Controls.Add(GRoot_Path);
			FolderTabPage.Controls.Add(GRoot_Note);
			FolderTabPage.Controls.Add(Faster_Folder_Path);
			FolderTabPage.Controls.Add(Faster_Folder_Note);
			FolderTabPage.Controls.Add(ucBtnFillet1);
			FolderTabPage.Name = "FolderTabPage";
			toolTip1.SetToolTip(FolderTabPage, resources.GetString("FolderTabPage.ToolTip"));
			FolderTabPage.UseVisualStyleBackColor = true;
			// 
			// ucBtnFillet2
			// 
			resources.ApplyResources(ucBtnFillet2, "ucBtnFillet2");
			ucBtnFillet2.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet2.BtnFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet2.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet2.BtnImage");
			ucBtnFillet2.Text = "选择";
			ucBtnFillet2.ConerRadius = 10;
			ucBtnFillet2.FillColor = System.Drawing.Color.Transparent;
			ucBtnFillet2.IsRadius = true;
			ucBtnFillet2.IsShowRect = true;
			ucBtnFillet2.Name = "ucBtnFillet2";
			ucBtnFillet2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet2.RectWidth = 1;
			toolTip1.SetToolTip(ucBtnFillet2, resources.GetString("ucBtnFillet2.ToolTip"));
			ucBtnFillet2.Click += button1_Click;
			// 
			// lbl_Region
			// 
			resources.ApplyResources(lbl_Region, "lbl_Region");
			lbl_Region.Name = "lbl_Region";
			toolTip1.SetToolTip(lbl_Region, resources.GetString("lbl_Region.ToolTip"));
			// 
			// GRoot_Path
			// 
			resources.ApplyResources(GRoot_Path, "GRoot_Path");
			GRoot_Path.Name = "GRoot_Path";
			toolTip1.SetToolTip(GRoot_Path, resources.GetString("GRoot_Path.ToolTip"));
			GRoot_Path.TextChanged += GRoot_Path_TextChanged;
			// 
			// GRoot_Note
			// 
			resources.ApplyResources(GRoot_Note, "GRoot_Note");
			GRoot_Note.Name = "GRoot_Note";
			toolTip1.SetToolTip(GRoot_Note, resources.GetString("GRoot_Note.ToolTip"));
			// 
			// Faster_Folder_Path
			// 
			resources.ApplyResources(Faster_Folder_Path, "Faster_Folder_Path");
			Faster_Folder_Path.Name = "Faster_Folder_Path";
			toolTip1.SetToolTip(Faster_Folder_Path, resources.GetString("Faster_Folder_Path.ToolTip"));
			Faster_Folder_Path.TextChanged += Faster_Folder_Path_TextChanged;
			// 
			// Faster_Folder_Note
			// 
			resources.ApplyResources(Faster_Folder_Note, "Faster_Folder_Note");
			Faster_Folder_Note.Name = "Faster_Folder_Note";
			toolTip1.SetToolTip(Faster_Folder_Note, resources.GetString("Faster_Folder_Note.ToolTip"));
			// 
			// ucBtnFillet1
			// 
			resources.ApplyResources(ucBtnFillet1, "ucBtnFillet1");
			ucBtnFillet1.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet1.BtnFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet1.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet1.BtnImage");
			ucBtnFillet1.Text = "选择";
			ucBtnFillet1.ConerRadius = 10;
			ucBtnFillet1.FillColor = System.Drawing.Color.Transparent;
			ucBtnFillet1.IsRadius = true;
			ucBtnFillet1.IsShowRect = true;
			ucBtnFillet1.Name = "ucBtnFillet1";
			ucBtnFillet1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet1.RectWidth = 1;
			toolTip1.SetToolTip(ucBtnFillet1, resources.GetString("ucBtnFillet1.ToolTip"));
			ucBtnFillet1.Click += Faster_Folder_Btn_Click;
			// 
			// SettingsTabControl
			// 
			resources.ApplyResources(SettingsTabControl, "SettingsTabControl");
			SettingsTabControl.Controls.Add(FolderTabPage);
			SettingsTabControl.Controls.Add(OptionTabPage);
			SettingsTabControl.Multiline = true;
			SettingsTabControl.Name = "SettingsTabControl";
			SettingsTabControl.SelectedIndex = 0;
			toolTip1.SetToolTip(SettingsTabControl, resources.GetString("SettingsTabControl.ToolTip"));
			// 
			// OptionTabPage
			// 
			resources.ApplyResources(OptionTabPage, "OptionTabPage");
			OptionTabPage.Controls.Add(groupBox1);
			OptionTabPage.Name = "OptionTabPage";
			toolTip1.SetToolTip(OptionTabPage, resources.GetString("OptionTabPage.ToolTip"));
			OptionTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Controls.Add(Preview_DataTest);
			groupBox1.Controls.Add(label1);
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			toolTip1.SetToolTip(groupBox1, resources.GetString("groupBox1.ToolTip"));
			groupBox1.Enter += groupBox1_Enter;
			// 
			// Preview_DataTest
			// 
			resources.ApplyResources(Preview_DataTest, "Preview_DataTest");
			Preview_DataTest.BackColor = System.Drawing.Color.White;
			Preview_DataTest.LargeChange = 1;
			Preview_DataTest.Maximum = 2;
			Preview_DataTest.Name = "Preview_DataTest";
			toolTip1.SetToolTip(Preview_DataTest, resources.GetString("Preview_DataTest.ToolTip"));
			Preview_DataTest.Scroll += Preview_DataTest_Scroll;
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			toolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip"));
			// 
			// Folder
			// 
			resources.ApplyResources(Folder, "Folder");
			// 
			// SettingsForm
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(SettingsTabControl);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SettingsForm";
			toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
			FormClosed += SettingsForm_FormClosed;
			Load += SettingsForm_Load;
			MouseEnter += SettingsForm_MouseEnter;
			FolderTabPage.ResumeLayout(false);
			FolderTabPage.PerformLayout();
			SettingsTabControl.ResumeLayout(false);
			OptionTabPage.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)Preview_DataTest).EndInit();
			ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.FolderBrowserDialog Folder;
		private System.Windows.Forms.TabPage FolderTabPage;
		private System.Windows.Forms.Label lbl_Region;
		private System.Windows.Forms.TextBox GRoot_Path;
		private System.Windows.Forms.Label GRoot_Note;
		private System.Windows.Forms.TextBox Faster_Folder_Path;
		private System.Windows.Forms.Label Faster_Folder_Note;
		private System.Windows.Forms.TabControl SettingsTabControl;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet1;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet2;
		private System.Windows.Forms.TabPage OptionTabPage;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar Preview_DataTest;
	}
}