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
			colorDialog1 = new ColorDialog();
			toolTip1 = new ToolTip(components);
			FolderTabPage = new TabPage();
			ucBtnFillet2 = new HZH_Controls.Controls.UCBtnFillet();
			lbl_Region = new Label();
			GRoot_Path = new TextBox();
			GRoot_Note = new Label();
			Faster_Folder_Path = new TextBox();
			Faster_Folder_Note = new Label();
			ucBtnFillet1 = new HZH_Controls.Controls.UCBtnFillet();
			SettingsTabControl = new TabControl();
			OptionTabPage = new TabPage();
			cmb_DataTestMode = new HZH_Controls.Controls.UCCombox();
			lbl_DataTestMode = new Label();
			cmb_ClipboardMode = new HZH_Controls.Controls.UCCombox();
			lbl_ClipboardMode = new Label();
			Folder = new FolderBrowserDialog();
			FolderTabPage.SuspendLayout();
			SettingsTabControl.SuspendLayout();
			OptionTabPage.SuspendLayout();
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
			ucBtnFillet2.BackColor = Color.Transparent;
			ucBtnFillet2.BtnFont = new Font("微软雅黑", 13F, FontStyle.Regular, GraphicsUnit.Pixel);
			ucBtnFillet2.BtnImage = (Image)resources.GetObject("ucBtnFillet2.BtnImage");
			ucBtnFillet2.ConerRadius = 10;
			ucBtnFillet2.FillColor = Color.Transparent;
			ucBtnFillet2.IsRadius = true;
			ucBtnFillet2.IsShowRect = true;
			ucBtnFillet2.Name = "ucBtnFillet2";
			ucBtnFillet2.RectColor = Color.FromArgb(220, 220, 220);
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
			ucBtnFillet1.BackColor = Color.Transparent;
			ucBtnFillet1.BtnFont = new Font("微软雅黑", 13F, FontStyle.Regular, GraphicsUnit.Pixel);
			ucBtnFillet1.BtnImage = (Image)resources.GetObject("ucBtnFillet1.BtnImage");
			ucBtnFillet1.ConerRadius = 10;
			ucBtnFillet1.FillColor = Color.Transparent;
			ucBtnFillet1.IsRadius = true;
			ucBtnFillet1.IsShowRect = true;
			ucBtnFillet1.Name = "ucBtnFillet1";
			ucBtnFillet1.RectColor = Color.FromArgb(220, 220, 220);
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
			OptionTabPage.Controls.Add(cmb_DataTestMode);
			OptionTabPage.Controls.Add(lbl_DataTestMode);
			OptionTabPage.Controls.Add(cmb_ClipboardMode);
			OptionTabPage.Controls.Add(lbl_ClipboardMode);
			OptionTabPage.Name = "OptionTabPage";
			toolTip1.SetToolTip(OptionTabPage, resources.GetString("OptionTabPage.ToolTip"));
			OptionTabPage.UseVisualStyleBackColor = true;
			// 
			// cmb_DataTestMode
			// 
			resources.ApplyResources(cmb_DataTestMode, "cmb_DataTestMode");
			cmb_DataTestMode.BackColor = Color.Transparent;
			cmb_DataTestMode.BoxStyle = ComboBoxStyle.DropDownList;
			cmb_DataTestMode.ConerRadius = 10;
			cmb_DataTestMode.DropPanelHeight = -1;
			cmb_DataTestMode.IsRadius = true;
			cmb_DataTestMode.IsShowRect = true;
			cmb_DataTestMode.ItemWidth = 40;
			cmb_DataTestMode.Name = "cmb_DataTestMode";
			cmb_DataTestMode.RectColor = Color.Black;
			cmb_DataTestMode.RectWidth = 1;
			cmb_DataTestMode.SelectedIndex = -1;
			cmb_DataTestMode.Source.Add("None");
			cmb_DataTestMode.Source.Add("Dump used table");
			cmb_DataTestMode.Source.Add("Dump full table");
			cmb_DataTestMode.TextAlign = HorizontalAlignment.Center;
			cmb_DataTestMode.TextValue = "";
			toolTip1.SetToolTip(cmb_DataTestMode, resources.GetString("cmb_DataTestMode.ToolTip"));
			cmb_DataTestMode.TriangleColor = Color.FromArgb(255, 128, 128);
			cmb_DataTestMode.SelectedChangedEvent += cmb_DataTestMode_SelectedChangedEvent;
			// 
			// lbl_DataTestMode
			// 
			resources.ApplyResources(lbl_DataTestMode, "lbl_DataTestMode");
			lbl_DataTestMode.ForeColor = Color.Red;
			lbl_DataTestMode.Name = "lbl_DataTestMode";
			toolTip1.SetToolTip(lbl_DataTestMode, resources.GetString("lbl_DataTestMode.ToolTip"));
			// 
			// cmb_ClipboardMode
			// 
			resources.ApplyResources(cmb_ClipboardMode, "cmb_ClipboardMode");
			cmb_ClipboardMode.BackColor = Color.Transparent;
			cmb_ClipboardMode.BoxStyle = ComboBoxStyle.DropDownList;
			cmb_ClipboardMode.ConerRadius = 10;
			cmb_ClipboardMode.DropPanelHeight = -1;
			cmb_ClipboardMode.IsRadius = true;
			cmb_ClipboardMode.IsShowRect = true;
			cmb_ClipboardMode.ItemWidth = 40;
			cmb_ClipboardMode.Name = "cmb_ClipboardMode";
			cmb_ClipboardMode.RectColor = Color.Black;
			cmb_ClipboardMode.RectWidth = 1;
			cmb_ClipboardMode.SelectedIndex = -1;
			cmb_ClipboardMode.Source.Add("Trimmed");
			cmb_ClipboardMode.Source.Add("Regular");
			cmb_ClipboardMode.Source.Add("Source");
			cmb_ClipboardMode.TextAlign = HorizontalAlignment.Center;
			cmb_ClipboardMode.TextValue = "";
			toolTip1.SetToolTip(cmb_ClipboardMode, resources.GetString("cmb_ClipboardMode.ToolTip"));
			cmb_ClipboardMode.TriangleColor = Color.FromArgb(255, 128, 128);
			cmb_ClipboardMode.SelectedChangedEvent += cmb_ClipboardMode_SelectedChangedEvent;
			// 
			// lbl_ClipboardMode
			// 
			resources.ApplyResources(lbl_ClipboardMode, "lbl_ClipboardMode");
			lbl_ClipboardMode.Name = "lbl_ClipboardMode";
			toolTip1.SetToolTip(lbl_ClipboardMode, resources.GetString("lbl_ClipboardMode.ToolTip"));
			// 
			// Folder
			// 
			resources.ApplyResources(Folder, "Folder");
			// 
			// SettingsForm
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(SettingsTabControl);
			FormBorderStyle = FormBorderStyle.FixedDialog;
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
			OptionTabPage.PerformLayout();
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
		private Label lbl_ClipboardMode;
		private HZH_Controls.Controls.UCCombox cmb_ClipboardMode;
		private Label lbl_DataTestMode;
		private HZH_Controls.Controls.UCCombox cmb_DataTestMode;
	}
}