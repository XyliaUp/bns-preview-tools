using Xylia.Preview.Common.Extension;

namespace Xylia.Match.Windows.Panel
{
	partial class IconPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconPage));
			Folder = new System.Windows.Forms.FolderBrowserDialog();
			tabControl1 = new System.Windows.Forms.TabControl();
			TabPage1 = new System.Windows.Forms.TabPage();
			groupBox2 = new System.Windows.Forms.GroupBox();
			Button9 = new System.Windows.Forms.Button();
			groupBox1 = new System.Windows.Forms.GroupBox();
			checkBox1 = new System.Windows.Forms.CheckBox();
			Switch_Mode = new HZH_Controls.Controls.UCSwitch();
			Label5 = new System.Windows.Forms.Label();
			FormatSelect = new HZH_Controls.Controls.UCCombox();
			Button1 = new System.Windows.Forms.Button();
			Button2 = new System.Windows.Forms.Button();
			Btn_Search_3 = new System.Windows.Forms.Button();
			Label3 = new System.Windows.Forms.Label();
			TextBox1 = new System.Windows.Forms.TextBox();
			Btn_Search_2 = new System.Windows.Forms.Button();
			Label2 = new System.Windows.Forms.Label();
			Path_ResultPath = new System.Windows.Forms.TextBox();
			Label1 = new System.Windows.Forms.Label();
			Btn_Search_1 = new System.Windows.Forms.Button();
			Path_GameFolder = new System.Windows.Forms.TextBox();
			Footer = new HZH_Controls.Controls.UCSplitLabel();
			TabPage2 = new System.Windows.Forms.TabPage();
			ComboBox3 = new HZH_Controls.Controls.UCCombox();
			ComboBox2 = new HZH_Controls.Controls.UCCombox();
			ComboBox1 = new HZH_Controls.Controls.UCCombox();
			Radio_128px = new HZH_Controls.Controls.UCRadioButton();
			Radio_64px = new HZH_Controls.Controls.UCRadioButton();
			ucCheckBox1 = new HZH_Controls.Controls.UCCheckBox();
			Label4 = new System.Windows.Forms.Label();
			Button4 = new System.Windows.Forms.Button();
			ImageCompose_Reset = new System.Windows.Forms.Button();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			GemPage = new System.Windows.Forms.TabPage();
			ucSwitch1 = new HZH_Controls.Controls.UCSwitch();
			Label6 = new System.Windows.Forms.Label();
			Button7 = new System.Windows.Forms.Button();
			Button6 = new System.Windows.Forms.Button();
			GemCircle = new Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell.GemCircle();
			Menu = new System.Windows.Forms.ContextMenuStrip(components);
			DEBUG = new System.Windows.Forms.ToolStripMenuItem();
			Tip = new System.Windows.Forms.ToolTip(components);
			SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			Open = new System.Windows.Forms.OpenFileDialog();
			tabControl1.SuspendLayout();
			TabPage1.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox1.SuspendLayout();
			TabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			GemPage.SuspendLayout();
			Menu.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl1
			// 
			tabControl1.AllowDrop = true;
			tabControl1.Controls.Add(TabPage1);
			tabControl1.Controls.Add(TabPage2);
			tabControl1.Controls.Add(GemPage);
			resources.ApplyResources(tabControl1, "tabControl1");
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			// 
			// TabPage1
			// 
			TabPage1.AllowDrop = true;
			TabPage1.BackColor = System.Drawing.Color.White;
			TabPage1.Controls.Add(groupBox2);
			TabPage1.Controls.Add(groupBox1);
			TabPage1.Controls.Add(Btn_Search_3);
			TabPage1.Controls.Add(Label3);
			TabPage1.Controls.Add(TextBox1);
			TabPage1.Controls.Add(Btn_Search_2);
			TabPage1.Controls.Add(Label2);
			TabPage1.Controls.Add(Path_ResultPath);
			TabPage1.Controls.Add(Label1);
			TabPage1.Controls.Add(Btn_Search_1);
			TabPage1.Controls.Add(Path_GameFolder);
			TabPage1.Controls.Add(Footer);
			resources.ApplyResources(TabPage1, "TabPage1");
			TabPage1.Name = "TabPage1";
			TabPage1.DragDrop += IconOperator_DragDrop;
			TabPage1.DragEnter += IconOperator_DragEnter;
			// 
			// groupBox2
			// 
			groupBox2.BackColor = System.Drawing.Color.Transparent;
			groupBox2.Controls.Add(Button9);
			resources.ApplyResources(groupBox2, "groupBox2");
			groupBox2.Name = "groupBox2";
			groupBox2.TabStop = false;
			// 
			// Button9
			// 
			resources.ApplyResources(Button9, "Button9");
			Button9.Name = "Button9";
			Button9.Click += Button9_Click;
			// 
			// groupBox1
			// 
			groupBox1.BackColor = System.Drawing.Color.Transparent;
			groupBox1.Controls.Add(checkBox1);
			groupBox1.Controls.Add(Switch_Mode);
			groupBox1.Controls.Add(Label5);
			groupBox1.Controls.Add(FormatSelect);
			groupBox1.Controls.Add(Button1);
			groupBox1.Controls.Add(Button2);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			// 
			// checkBox1
			// 
			resources.ApplyResources(checkBox1, "checkBox1");
			checkBox1.BackColor = System.Drawing.Color.Transparent;
			checkBox1.Checked = true;
			checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox1.Name = "checkBox1";
			checkBox1.UseVisualStyleBackColor = false;
			checkBox1.CheckedChanged += checkBox1_CheckedChanged;
			checkBox1.MouseEnter += Switch_HasBG_MouseEnter;
			checkBox1.MouseLeave += Switch_HasBG_MouseLeave;
			// 
			// Switch_Mode
			// 
			Switch_Mode.BackColor = System.Drawing.Color.Transparent;
			Switch_Mode.Checked = true;
			Switch_Mode.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
			Switch_Mode.FalseTextColr = System.Drawing.Color.White;
			resources.ApplyResources(Switch_Mode, "Switch_Mode");
			Switch_Mode.ForeColor = System.Drawing.Color.Black;
			Switch_Mode.Name = "Switch_Mode";
			Switch_Mode.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
			Switch_Mode.Texts = (new string[] { "过滤列表", "按照列表" });
			Switch_Mode.TrueColor = System.Drawing.Color.FromArgb(255, 192, 192);
			Switch_Mode.TrueTextColr = System.Drawing.Color.Black;
			Switch_Mode.CheckedChanged += Switch_Mode_CheckedChanged;
			Switch_Mode.MouseEnter += Switch_Mode_MouseEnter;
			Switch_Mode.MouseLeave += Switch_HasBG_MouseLeave;
			// 
			// Label5
			// 
			resources.ApplyResources(Label5, "Label5");
			Label5.Name = "Label5";
			Label5.MouseEnter += FormatSelect_MouseEnter;
			Label5.MouseLeave += Switch_HasBG_MouseLeave;
			// 
			// FormatSelect
			// 
			FormatSelect.BackColor = System.Drawing.Color.Transparent;
			FormatSelect.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			FormatSelect.ConerRadius = 10;
			FormatSelect.DropPanelHeight = -1;
			resources.ApplyResources(FormatSelect, "FormatSelect");
			FormatSelect.IsRadius = true;
			FormatSelect.IsShowRect = true;
			FormatSelect.ItemWidth = 40;
			FormatSelect.Name = "FormatSelect";
			FormatSelect.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			FormatSelect.RectWidth = 1;
			FormatSelect.SelectedIndex = -1;
			FormatSelect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			FormatSelect.TextValue = "[id]";
			FormatSelect.TriangleColor = System.Drawing.Color.FromArgb(255, 128, 128);
			FormatSelect.TextChangedEvent += FormatSelect_TextChanged;
			FormatSelect.MouseEnter += FormatSelect_MouseEnter;
			FormatSelect.MouseLeave += Switch_HasBG_MouseLeave;
			// 
			// Button1
			// 
			resources.ApplyResources(Button1, "Button1");
			Button1.Name = "Button1";
			Button1.Click += Button1_Click;
			Button1.MouseEnter += Button1_MouseEnter;
			Button1.MouseLeave += Switch_HasBG_MouseLeave;
			// 
			// Button2
			// 
			resources.ApplyResources(Button2, "Button2");
			Button2.Name = "Button2";
			Button2.Click += Button2_Click;
			// 
			// Btn_Search_3
			// 
			resources.ApplyResources(Btn_Search_3, "Btn_Search_3");
			Btn_Search_3.Name = "Btn_Search_3";
			Btn_Search_3.Click += Btn_Search_3_Click;
			// 
			// Label3
			// 
			resources.ApplyResources(Label3, "Label3");
			Label3.Name = "Label3";
			// 
			// TextBox1
			// 
			resources.ApplyResources(TextBox1, "TextBox1");
			TextBox1.Name = "TextBox1";
			TextBox1.TextChanged += TextBox1_TextChanged;
			// 
			// Btn_Search_2
			// 
			resources.ApplyResources(Btn_Search_2, "Btn_Search_2");
			Btn_Search_2.Name = "Btn_Search_2";
			Btn_Search_2.Click += Btn_Search_2_Click;
			// 
			// Label2
			// 
			resources.ApplyResources(Label2, "Label2");
			Label2.Name = "Label2";
			// 
			// Path_ResultPath
			// 
			resources.ApplyResources(Path_ResultPath, "Path_ResultPath");
			Path_ResultPath.Name = "Path_ResultPath";
			Path_ResultPath.TextChanged += Path_ResultPath_TextChanged;
			// 
			// Label1
			// 
			resources.ApplyResources(Label1, "Label1");
			Label1.Name = "Label1";
			// 
			// Btn_Search_1
			// 
			resources.ApplyResources(Btn_Search_1, "Btn_Search_1");
			Btn_Search_1.Name = "Btn_Search_1";
			Btn_Search_1.Click += Btn_Search_1_Click;
			// 
			// Path_GameFolder
			// 
			resources.ApplyResources(Path_GameFolder, "Path_GameFolder");
			Path_GameFolder.Name = "Path_GameFolder";
			Path_GameFolder.TextChanged += Path_GameFolder_TextChanged;
			// 
			// Footer
			// 
			Footer.AutoEllipsis = true;
			resources.ApplyResources(Footer, "Footer");
			Footer.BackColor = System.Drawing.Color.Transparent;
			Footer.LineColor = System.Drawing.Color.White;
			Footer.Name = "Footer";
			// 
			// TabPage2
			// 
			TabPage2.AllowDrop = true;
			TabPage2.BackColor = System.Drawing.Color.White;
			TabPage2.Controls.Add(ComboBox3);
			TabPage2.Controls.Add(ComboBox2);
			TabPage2.Controls.Add(ComboBox1);
			TabPage2.Controls.Add(Radio_128px);
			TabPage2.Controls.Add(Radio_64px);
			TabPage2.Controls.Add(ucCheckBox1);
			TabPage2.Controls.Add(Label4);
			TabPage2.Controls.Add(Button4);
			TabPage2.Controls.Add(ImageCompose_Reset);
			TabPage2.Controls.Add(pictureBox1);
			resources.ApplyResources(TabPage2, "TabPage2");
			TabPage2.Name = "TabPage2";
			TabPage2.DragDrop += IconOperator_DragDrop;
			TabPage2.DragEnter += IconOperator_DragEnter;
			// 
			// ComboBox3
			// 
			ComboBox3.BackColor = System.Drawing.Color.Transparent;
			ComboBox3.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			ComboBox3.ConerRadius = 10;
			ComboBox3.DropPanelHeight = -1;
			resources.ApplyResources(ComboBox3, "ComboBox3");
			ComboBox3.IsRadius = true;
			ComboBox3.IsShowRect = true;
			ComboBox3.ItemWidth = 40;
			ComboBox3.Name = "ComboBox3";
			ComboBox3.RectColor = System.Drawing.Color.FromArgb(240, 240, 240);
			ComboBox3.RectWidth = 1;
			ComboBox3.SelectedIndex = -1;
			ComboBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			ComboBox3.TextValue = "";
			ComboBox3.TriangleColor = System.Drawing.Color.FromArgb(255, 128, 128);
			ComboBox3.SelectedChangedEvent += ComboBox3_TextChanged;
			// 
			// ComboBox2
			// 
			ComboBox2.BackColor = System.Drawing.Color.Transparent;
			ComboBox2.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			ComboBox2.ConerRadius = 10;
			ComboBox2.DropPanelHeight = -1;
			resources.ApplyResources(ComboBox2, "ComboBox2");
			ComboBox2.IsRadius = true;
			ComboBox2.IsShowRect = true;
			ComboBox2.ItemWidth = 40;
			ComboBox2.Name = "ComboBox2";
			ComboBox2.RectColor = System.Drawing.Color.FromArgb(240, 240, 240);
			ComboBox2.RectWidth = 1;
			ComboBox2.SelectedIndex = -1;
			ComboBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			ComboBox2.TextValue = "";
			ComboBox2.TriangleColor = System.Drawing.Color.FromArgb(255, 128, 128);
			ComboBox2.SelectedChangedEvent += ComboBox2_TextChanged;
			// 
			// ComboBox1
			// 
			ComboBox1.BackColor = System.Drawing.Color.Transparent;
			ComboBox1.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			ComboBox1.ConerRadius = 10;
			ComboBox1.DropPanelHeight = -1;
			resources.ApplyResources(ComboBox1, "ComboBox1");
			ComboBox1.IsRadius = true;
			ComboBox1.IsShowRect = true;
			ComboBox1.ItemWidth = 40;
			ComboBox1.Name = "ComboBox1";
			ComboBox1.RectColor = System.Drawing.Color.FromArgb(240, 240, 240);
			ComboBox1.RectWidth = 1;
			ComboBox1.SelectedIndex = -1;
			ComboBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			ComboBox1.TextValue = "";
			ComboBox1.TriangleColor = System.Drawing.Color.FromArgb(255, 128, 128);
			ComboBox1.SelectedChangedEvent += ComboBox1_TextChanged;
			// 
			// Radio_128px
			// 
			Radio_128px.BackColor = System.Drawing.SystemColors.Window;
			Radio_128px.Checked = false;
			Radio_128px.GroupName = null;
			resources.ApplyResources(Radio_128px, "Radio_128px");
			Radio_128px.Name = "Radio_128px";
			Radio_128px.TextValue = "128 px";
			// 
			// Radio_64px
			// 
			Radio_64px.BackColor = System.Drawing.SystemColors.Window;
			Radio_64px.Checked = true;
			Radio_64px.GroupName = null;
			resources.ApplyResources(Radio_64px, "Radio_64px");
			Radio_64px.Name = "Radio_64px";
			Radio_64px.TextValue = "64   px";
			Radio_64px.CheckedChangeEvent += Radio_64px_CheckedChangeEvent;
			// 
			// ucCheckBox1
			// 
			ucCheckBox1.BackColor = System.Drawing.Color.Transparent;
			ucCheckBox1.Checked = true;
			resources.ApplyResources(ucCheckBox1, "ucCheckBox1");
			ucCheckBox1.Name = "ucCheckBox1";
			ucCheckBox1.TextValue = "使用新版本背景";
			ucCheckBox1.CheckedChangeEvent += ComboBox1_TextChanged;
			// 
			// Label4
			// 
			resources.ApplyResources(Label4, "Label4");
			Label4.Name = "Label4";
			// 
			// Button4
			// 
			resources.ApplyResources(Button4, "Button4");
			Button4.Name = "Button4";
			Button4.Click += Button4_Click;
			// 
			// ImageCompose_Reset
			// 
			resources.ApplyResources(ImageCompose_Reset, "ImageCompose_Reset");
			ImageCompose_Reset.Name = "ImageCompose_Reset";
			ImageCompose_Reset.Click += ImageCompose_Reset_Click;
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			// 
			// GemPage
			// 
			GemPage.AllowDrop = true;
			GemPage.BackColor = System.Drawing.Color.White;
			GemPage.Controls.Add(ucSwitch1);
			GemPage.Controls.Add(Label6);
			GemPage.Controls.Add(Button7);
			GemPage.Controls.Add(Button6);
			GemPage.Controls.Add(GemCircle);
			resources.ApplyResources(GemPage, "GemPage");
			GemPage.Name = "GemPage";
			GemPage.Click += GemPage_Click;
			GemPage.DragDrop += GemPage_DragDrop;
			GemPage.DragEnter += GemPage_DragEnter;
			// 
			// ucSwitch1
			// 
			ucSwitch1.BackColor = System.Drawing.Color.Transparent;
			ucSwitch1.Checked = true;
			ucSwitch1.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
			ucSwitch1.FalseTextColr = System.Drawing.Color.White;
			resources.ApplyResources(ucSwitch1, "ucSwitch1");
			ucSwitch1.Name = "ucSwitch1";
			ucSwitch1.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
			ucSwitch1.Texts = (new string[] { "背景 ", "无背景 " });
			ucSwitch1.TrueColor = System.Drawing.Color.FromArgb(255, 192, 192);
			ucSwitch1.TrueTextColr = System.Drawing.Color.Black;
			ucSwitch1.CheckedChanged += ucSwitch1_CheckedChanged;
			// 
			// Label6
			// 
			resources.ApplyResources(Label6, "Label6");
			Label6.Name = "Label6";
			// 
			// Button7
			// 
			resources.ApplyResources(Button7, "Button7");
			Button7.Name = "Button7";
			Button7.Click += Button7_Click;
			// 
			// Button6
			// 
			resources.ApplyResources(Button6, "Button6");
			Button6.Name = "Button6";
			Button6.Click += Button6_Click;
			// 
			// GemCircle
			// 
			resources.ApplyResources(GemCircle, "GemCircle");
			GemCircle.BackColor = System.Drawing.Color.Transparent;
			GemCircle.Meta1 = null;
			GemCircle.Meta2 = null;
			GemCircle.Meta3 = null;
			GemCircle.Meta4 = null;
			GemCircle.Meta5 = null;
			GemCircle.Meta6 = null;
			GemCircle.Meta7 = null;
			GemCircle.Meta8 = null;
			GemCircle.Name = "GemCircle";
			GemCircle.PartSel = Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell.GemCircle.PartSection.Part1;
			GemCircle.Transparent = false;
			GemCircle.WholeScale = 1F;
			GemCircle.SelectPartChanged += GemCircle_SelectPartChanged;
			// 
			// Menu
			// 
			Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { DEBUG });
			Menu.Name = "Menu";
			resources.ApplyResources(Menu, "Menu");
			// 
			// DEBUG
			// 
			DEBUG.Name = "DEBUG";
			resources.ApplyResources(DEBUG, "DEBUG");
			// 
			// IconPage
			// 
			AllowDrop = true;
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			BackColor = System.Drawing.Color.Transparent;
			Controls.Add(tabControl1);
			Name = "IconPage";
			resources.ApplyResources(this, "$this");
			Load += IconOperator_Load;
			DragDrop += IconOperator_DragDrop;
			DragEnter += IconOperator_DragEnter;
			tabControl1.ResumeLayout(false);
			TabPage1.ResumeLayout(false);
			TabPage1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			TabPage2.ResumeLayout(false);
			TabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			GemPage.ResumeLayout(false);
			GemPage.PerformLayout();
			Menu.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.FolderBrowserDialog Folder;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolTip Tip;
		private System.Windows.Forms.SaveFileDialog SaveFileDialog;
		private System.Windows.Forms.TabPage TabPage1;
		private System.Windows.Forms.Button Button2;
		private System.Windows.Forms.Label Label1;
		private System.Windows.Forms.Button Btn_Search_1;
		private System.Windows.Forms.TextBox Path_GameFolder;
		private System.Windows.Forms.Button Btn_Search_2;
		private System.Windows.Forms.Label Label2;
		private System.Windows.Forms.TextBox Path_ResultPath;
		private System.Windows.Forms.ContextMenuStrip Menu;
		private System.Windows.Forms.Button Btn_Search_3;
		private System.Windows.Forms.Label Label3;
		private System.Windows.Forms.TextBox TextBox1;
		private System.Windows.Forms.OpenFileDialog Open;
		private System.Windows.Forms.Button Button1;
		private System.Windows.Forms.TabPage TabPage2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button Button4;
		private System.Windows.Forms.Button ImageCompose_Reset;
		private System.Windows.Forms.Label Label4;
		private HZH_Controls.Controls.UCSplitLabel Footer;
		private HZH_Controls.Controls.UCSwitch Switch_Mode;
		private System.Windows.Forms.ToolStripMenuItem DEBUG;
		private HZH_Controls.Controls.UCCheckBox ucCheckBox1;
		private HZH_Controls.Controls.UCRadioButton Radio_128px;
		private HZH_Controls.Controls.UCRadioButton Radio_64px;
		private System.Windows.Forms.Label Label5;
		private System.Windows.Forms.TabPage GemPage;
		private Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell.GemCircle GemCircle;
		private System.Windows.Forms.Button Button6;
		private System.Windows.Forms.Button Button7;
		private System.Windows.Forms.Label Label6;
		private HZH_Controls.Controls.UCSwitch ucSwitch1;
		private HZH_Controls.Controls.UCCombox FormatSelect;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Button9;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private HZH_Controls.Controls.UCCombox ComboBox3;
		private HZH_Controls.Controls.UCCombox ComboBox2;
		private HZH_Controls.Controls.UCCombox ComboBox1;
	}
}