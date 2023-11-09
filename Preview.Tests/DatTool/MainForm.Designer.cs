using System.Drawing;
using System.Windows.Forms;

namespace Xylia.Preview.Tests.DatTool
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			richOut = new RichTextBox();
			groupBox2 = new GroupBox();
			button39 = new Button();
			MainMenu = new ContextMenuStrip(components);
			MenuItem_DevTools = new ToolStripMenuItem();
			ToolStripMenuItem_NumSelect = new ToolStripMenuItem();
			重新排列ToolStripMenuItem = new ToolStripMenuItem();
			xml操作ToolStripMenuItem = new ToolStripMenuItem();
			列出所有属性ToolStripMenuItem = new ToolStripMenuItem();
			列出指定属性范围ToolStripMenuItem = new ToolStripMenuItem();
			合并文档ToolStripMenuItem = new ToolStripMenuItem();
			tabPage1 = new TabPage();
			Compare_Diff = new Button();
			textBox6 = new TextBox();
			label8 = new Label();
			button17 = new Button();
			checkBox14 = new CheckBox();
			button11 = new Button();
			checkBox12 = new CheckBox();
			checkBox11 = new CheckBox();
			textBox2 = new TextBox();
			textBox4 = new TextBox();
			label4 = new Label();
			button9 = new Button();
			label5 = new Label();
			button10 = new Button();
			Page_Region = new TabPage();
			label13 = new Label();
			label12 = new Label();
			Region_YMax_input = new TextBox();
			Region_YMin_input = new TextBox();
			Region_XMax_input = new TextBox();
			Region_XMin_input = new TextBox();
			button36 = new Button();
			label27 = new Label();
			Txt_Cterrain_Path = new TextBox();
			textBox12 = new TextBox();
			Txt_Region_Path = new TextBox();
			Txt_Zone = new TextBox();
			button37 = new Button();
			label33 = new Label();
			button32 = new Button();
			label31 = new Label();
			button31 = new Button();
			label29 = new Label();
			tabPage5 = new TabPage();
			groupBox3 = new GroupBox();
			button13 = new Button();
			label11 = new Label();
			button14 = new Button();
			textBox8 = new TextBox();
			ModifyBin = new TabPage();
			groupBox5 = new GroupBox();
			textBox3 = new TextBox();
			label2 = new Label();
			button4 = new Button();
			button6 = new Button();
			button7 = new Button();
			textBox1 = new TextBox();
			label1 = new Label();
			button8 = new Button();
			groupBox4 = new GroupBox();
			label3 = new Label();
			checkBox13 = new CheckBox();
			button2 = new Button();
			Txt_Bin_Data = new TextBox();
			button1 = new Button();
			ModifyData = new TabPage();
			checkBox1 = new CheckBox();
			button3 = new Button();
			label6 = new Label();
			trackBar1 = new TrackBar();
			lbDat = new Label();
			bntSearchDat = new Button();
			txbDatFile = new TextBox();
			txbRpFolder = new TextBox();
			cB_output = new CheckBox();
			bntUnpack = new Button();
			Cb_back = new CheckBox();
			btnRepack = new Button();
			lbRfolder = new Label();
			bntSearchOut = new Button();
			tabControl1 = new TabControl();
			groupBox2.SuspendLayout();
			MainMenu.SuspendLayout();
			tabPage1.SuspendLayout();
			Page_Region.SuspendLayout();
			tabPage5.SuspendLayout();
			groupBox3.SuspendLayout();
			ModifyBin.SuspendLayout();
			groupBox5.SuspendLayout();
			groupBox4.SuspendLayout();
			ModifyData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
			tabControl1.SuspendLayout();
			SuspendLayout();
			// 
			// richOut
			// 
			richOut.BackColor = Color.White;
			richOut.BorderStyle = BorderStyle.None;
			richOut.Dock = DockStyle.Fill;
			richOut.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
			richOut.HideSelection = false;
			richOut.Location = new Point(4, 20);
			richOut.Margin = new Padding(4);
			richOut.Name = "richOut";
			richOut.ReadOnly = true;
			richOut.Size = new Size(858, 232);
			richOut.TabIndex = 21;
			richOut.Text = "";
			richOut.ZoomFactor = 1.101F;
			// 
			// groupBox2
			// 
			groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupBox2.BackColor = Color.Transparent;
			groupBox2.Controls.Add(button39);
			groupBox2.Controls.Add(richOut);
			groupBox2.Location = new Point(10, 308);
			groupBox2.Margin = new Padding(4);
			groupBox2.Name = "groupBox2";
			groupBox2.Padding = new Padding(4);
			groupBox2.Size = new Size(866, 256);
			groupBox2.TabIndex = 23;
			groupBox2.TabStop = false;
			groupBox2.Text = "日志";
			// 
			// button39
			// 
			button39.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			button39.Location = new Point(781, 20);
			button39.Margin = new Padding(4);
			button39.Name = "button39";
			button39.Size = new Size(75, 40);
			button39.TabIndex = 22;
			button39.Text = "清 空";
			button39.UseVisualStyleBackColor = true;
			button39.Click += ClearLog;
			// 
			// MainMenu
			// 
			MainMenu.Items.AddRange(new ToolStripItem[] { MenuItem_DevTools, ToolStripMenuItem_NumSelect, 重新排列ToolStripMenuItem, xml操作ToolStripMenuItem });
			MainMenu.Name = "MainMenu";
			MainMenu.Size = new Size(175, 92);
			// 
			// MenuItem_DevTools
			// 
			MenuItem_DevTools.Name = "MenuItem_DevTools";
			MenuItem_DevTools.Size = new Size(174, 22);
			MenuItem_DevTools.Text = "开发工具";
			MenuItem_DevTools.Click += MenuItem_DevTools_Click;
			// 
			// ToolStripMenuItem_NumSelect
			// 
			ToolStripMenuItem_NumSelect.Name = "ToolStripMenuItem_NumSelect";
			ToolStripMenuItem_NumSelect.Size = new Size(174, 22);
			ToolStripMenuItem_NumSelect.Text = "属性生成";
			ToolStripMenuItem_NumSelect.Click += ToolStripMenuItem_NumSelect_Click;
			// 
			// 重新排列ToolStripMenuItem
			// 
			重新排列ToolStripMenuItem.Name = "重新排列ToolStripMenuItem";
			重新排列ToolStripMenuItem.Size = new Size(174, 22);
			重新排列ToolStripMenuItem.Text = "重新排列XML文件";
			重新排列ToolStripMenuItem.Click += 重新排列ToolStripMenuItem_Click;
			// 
			// xml操作ToolStripMenuItem
			// 
			xml操作ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 列出所有属性ToolStripMenuItem, 列出指定属性范围ToolStripMenuItem, 合并文档ToolStripMenuItem });
			xml操作ToolStripMenuItem.Name = "xml操作ToolStripMenuItem";
			xml操作ToolStripMenuItem.Size = new Size(174, 22);
			xml操作ToolStripMenuItem.Text = "Xml操作";
			// 
			// 列出所有属性ToolStripMenuItem
			// 
			列出所有属性ToolStripMenuItem.Name = "列出所有属性ToolStripMenuItem";
			列出所有属性ToolStripMenuItem.Size = new Size(172, 22);
			列出所有属性ToolStripMenuItem.Text = "列出所有属性";
			列出所有属性ToolStripMenuItem.Click += 列出所有属性ToolStripMenuItem_Click;
			// 
			// 列出指定属性范围ToolStripMenuItem
			// 
			列出指定属性范围ToolStripMenuItem.Name = "列出指定属性范围ToolStripMenuItem";
			列出指定属性范围ToolStripMenuItem.Size = new Size(172, 22);
			列出指定属性范围ToolStripMenuItem.Text = "列出指定属性范围";
			列出指定属性范围ToolStripMenuItem.Click += 列出指定属性范围ToolStripMenuItem_Click;
			// 
			// 合并文档ToolStripMenuItem
			// 
			合并文档ToolStripMenuItem.Name = "合并文档ToolStripMenuItem";
			合并文档ToolStripMenuItem.Size = new Size(172, 22);
			合并文档ToolStripMenuItem.Text = "合并文档";
			合并文档ToolStripMenuItem.Click += 合并文档ToolStripMenuItem_Click;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(Compare_Diff);
			tabPage1.Controls.Add(textBox6);
			tabPage1.Controls.Add(label8);
			tabPage1.Controls.Add(button17);
			tabPage1.Controls.Add(checkBox14);
			tabPage1.Controls.Add(button11);
			tabPage1.Controls.Add(checkBox12);
			tabPage1.Controls.Add(checkBox11);
			tabPage1.Controls.Add(textBox2);
			tabPage1.Controls.Add(textBox4);
			tabPage1.Controls.Add(label4);
			tabPage1.Controls.Add(button9);
			tabPage1.Controls.Add(label5);
			tabPage1.Controls.Add(button10);
			tabPage1.Location = new Point(4, 26);
			tabPage1.Margin = new Padding(4);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(4);
			tabPage1.Size = new Size(873, 273);
			tabPage1.TabIndex = 11;
			tabPage1.Text = "合并数据源";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// Compare_Diff
			// 
			Compare_Diff.Location = new Point(741, 75);
			Compare_Diff.Margin = new Padding(4);
			Compare_Diff.Name = "Compare_Diff";
			Compare_Diff.Size = new Size(95, 37);
			Compare_Diff.TabIndex = 113;
			Compare_Diff.Text = "比对差异";
			Compare_Diff.UseVisualStyleBackColor = true;
			// 
			// textBox6
			// 
			textBox6.Location = new Point(111, 106);
			textBox6.Margin = new Padding(4);
			textBox6.Name = "textBox6";
			textBox6.Size = new Size(518, 23);
			textBox6.TabIndex = 112;
			textBox6.TextChanged += SaveConfig;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(20, 111);
			label8.Margin = new Padding(4, 0, 4, 0);
			label8.Name = "label8";
			label8.Size = new Size(56, 17);
			label8.TabIndex = 111;
			label8.Text = "公共配置";
			// 
			// button17
			// 
			button17.Location = new Point(646, 100);
			button17.Margin = new Padding(4);
			button17.Name = "button17";
			button17.Size = new Size(75, 34);
			button17.TabIndex = 110;
			button17.Text = "浏览";
			button17.UseVisualStyleBackColor = true;
			button17.Click += button17_Click;
			// 
			// checkBox14
			// 
			checkBox14.AutoSize = true;
			checkBox14.Checked = true;
			checkBox14.CheckState = CheckState.Checked;
			checkBox14.Location = new Point(582, 192);
			checkBox14.Margin = new Padding(4);
			checkBox14.Name = "checkBox14";
			checkBox14.Size = new Size(132, 21);
			checkBox14.TabIndex = 109;
			checkBox14.Text = "只合并服务端Fields";
			checkBox14.UseVisualStyleBackColor = true;
			checkBox14.CheckedChanged += checkBox14_CheckedChanged;
			// 
			// button11
			// 
			button11.Location = new Point(741, 11);
			button11.Margin = new Padding(4);
			button11.Name = "button11";
			button11.Size = new Size(95, 44);
			button11.TabIndex = 108;
			button11.Text = "执行";
			button11.UseVisualStyleBackColor = true;
			button11.Click += button11_Click;
			// 
			// checkBox12
			// 
			checkBox12.AutoSize = true;
			checkBox12.Checked = true;
			checkBox12.CheckState = CheckState.Checked;
			checkBox12.Location = new Point(582, 163);
			checkBox12.Margin = new Padding(4);
			checkBox12.Name = "checkBox12";
			checkBox12.Size = new Size(101, 21);
			checkBox12.TabIndex = 107;
			checkBox12.Text = "alias作为主键";
			checkBox12.UseVisualStyleBackColor = true;
			checkBox12.CheckedChanged += checkBox12_CheckedChanged;
			// 
			// checkBox11
			// 
			checkBox11.AutoSize = true;
			checkBox11.Checked = true;
			checkBox11.CheckState = CheckState.Checked;
			checkBox11.Location = new Point(703, 163);
			checkBox11.Margin = new Padding(4);
			checkBox11.Name = "checkBox11";
			checkBox11.Size = new Size(168, 21);
			checkBox11.TabIndex = 106;
			checkBox11.Text = "重复Fields应用合并源数据";
			checkBox11.UseVisualStyleBackColor = true;
			checkBox11.CheckedChanged += SaveCheckStatus;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(111, 57);
			textBox2.Margin = new Padding(4);
			textBox2.MaxLength = 999999999;
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(518, 23);
			textBox2.TabIndex = 105;
			textBox2.TextChanged += textBox2_TextChanged;
			textBox2.DoubleClick += DoubleClickPath;
			// 
			// textBox4
			// 
			textBox4.Location = new Point(111, 16);
			textBox4.Margin = new Padding(4);
			textBox4.Name = "textBox4";
			textBox4.Size = new Size(518, 23);
			textBox4.TabIndex = 102;
			textBox4.TextChanged += SaveConfig;
			textBox4.DoubleClick += DoubleClickPath;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(10, 60);
			label4.Margin = new Padding(4, 0, 4, 0);
			label4.Name = "label4";
			label4.Size = new Size(68, 17);
			label4.TabIndex = 104;
			label4.Text = "合并源文件";
			// 
			// button9
			// 
			button9.Location = new Point(645, 52);
			button9.Margin = new Padding(4);
			button9.Name = "button9";
			button9.Size = new Size(75, 34);
			button9.TabIndex = 103;
			button9.Text = "浏览";
			button9.UseVisualStyleBackColor = true;
			button9.Click += button9_Click;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(10, 20);
			label5.Margin = new Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new Size(80, 17);
			label5.TabIndex = 101;
			label5.Text = "主要数据文件";
			// 
			// button10
			// 
			button10.Location = new Point(646, 11);
			button10.Margin = new Padding(4);
			button10.Name = "button10";
			button10.Size = new Size(75, 34);
			button10.TabIndex = 100;
			button10.Text = "浏览";
			button10.UseVisualStyleBackColor = true;
			button10.Click += button10_Click;
			// 
			// Page_Region
			// 
			Page_Region.Controls.Add(label13);
			Page_Region.Controls.Add(label12);
			Page_Region.Controls.Add(Region_YMax_input);
			Page_Region.Controls.Add(Region_YMin_input);
			Page_Region.Controls.Add(Region_XMax_input);
			Page_Region.Controls.Add(Region_XMin_input);
			Page_Region.Controls.Add(button36);
			Page_Region.Controls.Add(label27);
			Page_Region.Controls.Add(Txt_Cterrain_Path);
			Page_Region.Controls.Add(textBox12);
			Page_Region.Controls.Add(Txt_Region_Path);
			Page_Region.Controls.Add(Txt_Zone);
			Page_Region.Controls.Add(button37);
			Page_Region.Controls.Add(label33);
			Page_Region.Controls.Add(button32);
			Page_Region.Controls.Add(label31);
			Page_Region.Controls.Add(button31);
			Page_Region.Controls.Add(label29);
			Page_Region.Location = new Point(4, 26);
			Page_Region.Margin = new Padding(4);
			Page_Region.Name = "Page_Region";
			Page_Region.Size = new Size(873, 273);
			Page_Region.TabIndex = 10;
			Page_Region.Text = "区域数据";
			Page_Region.UseVisualStyleBackColor = true;
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			label13.Location = new Point(727, 65);
			label13.Margin = new Padding(4, 0, 4, 0);
			label13.Name = "label13";
			label13.Size = new Size(50, 20);
			label13.TabIndex = 83;
			label13.Text = "XMax";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			label12.Location = new Point(728, 24);
			label12.Margin = new Padding(4, 0, 4, 0);
			label12.Name = "label12";
			label12.Size = new Size(47, 20);
			label12.TabIndex = 82;
			label12.Text = "XMin";
			// 
			// Region_YMax_input
			// 
			Region_YMax_input.Location = new Point(789, 142);
			Region_YMax_input.Margin = new Padding(4);
			Region_YMax_input.Name = "Region_YMax_input";
			Region_YMax_input.Size = new Size(66, 23);
			Region_YMax_input.TabIndex = 81;
			// 
			// Region_YMin_input
			// 
			Region_YMin_input.Location = new Point(789, 101);
			Region_YMin_input.Margin = new Padding(4);
			Region_YMin_input.Name = "Region_YMin_input";
			Region_YMin_input.Size = new Size(66, 23);
			Region_YMin_input.TabIndex = 80;
			// 
			// Region_XMax_input
			// 
			Region_XMax_input.Location = new Point(789, 60);
			Region_XMax_input.Margin = new Padding(4);
			Region_XMax_input.Name = "Region_XMax_input";
			Region_XMax_input.Size = new Size(66, 23);
			Region_XMax_input.TabIndex = 79;
			// 
			// Region_XMin_input
			// 
			Region_XMin_input.Location = new Point(789, 19);
			Region_XMin_input.Margin = new Padding(4);
			Region_XMin_input.Name = "Region_XMin_input";
			Region_XMin_input.Size = new Size(66, 23);
			Region_XMin_input.TabIndex = 78;
			// 
			// button36
			// 
			button36.Location = new Point(322, 175);
			button36.Margin = new Padding(4);
			button36.Name = "button36";
			button36.Size = new Size(88, 33);
			button36.TabIndex = 75;
			button36.Text = "修改区域id";
			button36.UseVisualStyleBackColor = true;
			button36.Click += button36_Click;
			// 
			// label27
			// 
			label27.AutoSize = true;
			label27.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			label27.Location = new Point(33, 129);
			label27.Margin = new Padding(4, 0, 4, 0);
			label27.Name = "label27";
			label27.Size = new Size(39, 20);
			label27.TabIndex = 74;
			label27.Text = "文件";
			// 
			// Txt_Cterrain_Path
			// 
			Txt_Cterrain_Path.Location = new Point(96, 133);
			Txt_Cterrain_Path.Margin = new Padding(4);
			Txt_Cterrain_Path.Name = "Txt_Cterrain_Path";
			Txt_Cterrain_Path.Size = new Size(510, 23);
			Txt_Cterrain_Path.TabIndex = 73;
			Txt_Cterrain_Path.TextChanged += SaveConfig;
			Txt_Cterrain_Path.DoubleClick += DoubleClickPath;
			// 
			// textBox12
			// 
			textBox12.Location = new Point(96, 175);
			textBox12.Margin = new Padding(4);
			textBox12.Name = "textBox12";
			textBox12.Size = new Size(195, 23);
			textBox12.TabIndex = 70;
			// 
			// Txt_Region_Path
			// 
			Txt_Region_Path.Location = new Point(94, 24);
			Txt_Region_Path.Margin = new Padding(4);
			Txt_Region_Path.Name = "Txt_Region_Path";
			Txt_Region_Path.Size = new Size(512, 23);
			Txt_Region_Path.TabIndex = 66;
			Txt_Region_Path.TextChanged += SaveConfig;
			Txt_Region_Path.DoubleClick += DoubleClickPath;
			// 
			// Txt_Zone
			// 
			Txt_Zone.Location = new Point(96, 66);
			Txt_Zone.Margin = new Padding(4);
			Txt_Zone.Name = "Txt_Zone";
			Txt_Zone.Size = new Size(195, 23);
			Txt_Zone.TabIndex = 0;
			// 
			// button37
			// 
			button37.Location = new Point(616, 129);
			button37.Margin = new Padding(4);
			button37.Name = "button37";
			button37.Size = new Size(82, 37);
			button37.TabIndex = 72;
			button37.Text = "浏览";
			button37.UseVisualStyleBackColor = true;
			button37.Click += button37_Click;
			// 
			// label33
			// 
			label33.AutoSize = true;
			label33.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			label33.Location = new Point(31, 176);
			label33.Margin = new Padding(4, 0, 4, 0);
			label33.Name = "label33";
			label33.Size = new Size(46, 20);
			label33.TabIndex = 71;
			label33.Text = "Zone";
			// 
			// button32
			// 
			button32.Location = new Point(322, 66);
			button32.Margin = new Padding(4);
			button32.Name = "button32";
			button32.Size = new Size(88, 33);
			button32.TabIndex = 69;
			button32.Text = "修改区域id";
			button32.UseVisualStyleBackColor = true;
			button32.Click += button32_Click;
			// 
			// label31
			// 
			label31.AutoSize = true;
			label31.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			label31.Location = new Point(31, 20);
			label31.Margin = new Padding(4, 0, 4, 0);
			label31.Name = "label31";
			label31.Size = new Size(39, 20);
			label31.TabIndex = 68;
			label31.Text = "文件";
			// 
			// button31
			// 
			button31.Location = new Point(614, 20);
			button31.Margin = new Padding(4);
			button31.Name = "button31";
			button31.Size = new Size(82, 37);
			button31.TabIndex = 65;
			button31.Text = "浏览";
			button31.UseVisualStyleBackColor = true;
			button31.Click += button31_Click;
			// 
			// label29
			// 
			label29.AutoSize = true;
			label29.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			label29.Location = new Point(31, 67);
			label29.Margin = new Padding(4, 0, 4, 0);
			label29.Name = "label29";
			label29.Size = new Size(46, 20);
			label29.TabIndex = 1;
			label29.Text = "Zone";
			// 
			// tabPage5
			// 
			tabPage5.Controls.Add(groupBox3);
			tabPage5.Location = new Point(4, 26);
			tabPage5.Margin = new Padding(4);
			tabPage5.Name = "tabPage5";
			tabPage5.Padding = new Padding(4);
			tabPage5.Size = new Size(873, 273);
			tabPage5.TabIndex = 5;
			tabPage5.Text = "服务端data";
			tabPage5.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(button13);
			groupBox3.Controls.Add(label11);
			groupBox3.Controls.Add(button14);
			groupBox3.Controls.Add(textBox8);
			groupBox3.Dock = DockStyle.Top;
			groupBox3.Location = new Point(4, 4);
			groupBox3.Margin = new Padding(4);
			groupBox3.Name = "groupBox3";
			groupBox3.Padding = new Padding(4);
			groupBox3.Size = new Size(865, 69);
			groupBox3.TabIndex = 72;
			groupBox3.TabStop = false;
			groupBox3.Text = "加解密操作";
			// 
			// button13
			// 
			button13.Location = new Point(690, 21);
			button13.Margin = new Padding(4);
			button13.Name = "button13";
			button13.Size = new Size(75, 34);
			button13.TabIndex = 24;
			button13.Text = "解密";
			button13.UseVisualStyleBackColor = true;
			button13.Click += button13_Click;
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			label11.Location = new Point(4, 28);
			label11.Margin = new Padding(4, 0, 4, 0);
			label11.Name = "label11";
			label11.Size = new Size(65, 20);
			label11.TabIndex = 30;
			label11.Text = "解包目录";
			// 
			// button14
			// 
			button14.Location = new Point(773, 21);
			button14.Margin = new Padding(4);
			button14.Name = "button14";
			button14.Size = new Size(75, 34);
			button14.TabIndex = 28;
			button14.Text = "加密";
			button14.UseVisualStyleBackColor = true;
			button14.Click += button14_Click;
			// 
			// textBox8
			// 
			textBox8.Location = new Point(99, 27);
			textBox8.Margin = new Padding(4);
			textBox8.Name = "textBox8";
			textBox8.Size = new Size(578, 23);
			textBox8.TabIndex = 26;
			textBox8.Text = "D:\\剑灵_NT\\contents\\Local\\TENCENT\\data\\xml.dat.files";
			textBox8.TextChanged += textBox8_TextChanged;
			textBox8.DoubleClick += DoubleClickPath;
			// 
			// ModifyBin
			// 
			ModifyBin.Controls.Add(groupBox5);
			ModifyBin.Controls.Add(groupBox4);
			ModifyBin.Location = new Point(4, 26);
			ModifyBin.Margin = new Padding(4);
			ModifyBin.Name = "ModifyBin";
			ModifyBin.Padding = new Padding(4);
			ModifyBin.Size = new Size(873, 273);
			ModifyBin.TabIndex = 3;
			ModifyBin.Text = "Bin File";
			ModifyBin.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			groupBox5.Controls.Add(textBox3);
			groupBox5.Controls.Add(label2);
			groupBox5.Controls.Add(button4);
			groupBox5.Controls.Add(button6);
			groupBox5.Controls.Add(button7);
			groupBox5.Controls.Add(textBox1);
			groupBox5.Controls.Add(label1);
			groupBox5.Controls.Add(button8);
			groupBox5.Dock = DockStyle.Bottom;
			groupBox5.Location = new Point(4, 138);
			groupBox5.Margin = new Padding(4);
			groupBox5.Name = "groupBox5";
			groupBox5.Padding = new Padding(4);
			groupBox5.Size = new Size(865, 131);
			groupBox5.TabIndex = 95;
			groupBox5.TabStop = false;
			groupBox5.Text = "反序列";
			// 
			// textBox3
			// 
			textBox3.Location = new Point(89, 73);
			textBox3.Margin = new Padding(4);
			textBox3.Name = "textBox3";
			textBox3.Size = new Size(530, 23);
			textBox3.TabIndex = 103;
			textBox3.Text = "D:\\资源\\客户端相关\\Auto\\data";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(7, 73);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(65, 20);
			label2.TabIndex = 102;
			label2.Text = "输出目录";
			// 
			// button4
			// 
			button4.Location = new Point(648, 73);
			button4.Margin = new Padding(4);
			button4.Name = "button4";
			button4.Size = new Size(75, 30);
			button4.TabIndex = 101;
			button4.Text = "浏览";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// button6
			// 
			button6.Location = new Point(757, 73);
			button6.Margin = new Padding(4);
			button6.Name = "button6";
			button6.Size = new Size(92, 37);
			button6.TabIndex = 100;
			button6.Text = "Table Test";
			button6.UseVisualStyleBackColor = true;
			button6.Click += button6_Click;
			// 
			// button7
			// 
			button7.Location = new Point(757, 25);
			button7.Margin = new Padding(4);
			button7.Name = "button7";
			button7.Size = new Size(92, 37);
			button7.TabIndex = 99;
			button7.Text = "Offset Test";
			button7.UseVisualStyleBackColor = true;
			button7.Click += button7_Click;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(89, 28);
			textBox1.Margin = new Padding(4);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(530, 23);
			textBox1.TabIndex = 91;
			textBox1.Text = "..\\..\\..\\..\\Preview.Core\\Data\\Definition\\AbnormalCamera.xml";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(7, 30);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(51, 20);
			label1.TabIndex = 90;
			label1.Text = "表定义";
			// 
			// button8
			// 
			button8.Location = new Point(647, 25);
			button8.Margin = new Padding(4);
			button8.Name = "button8";
			button8.Size = new Size(75, 30);
			button8.TabIndex = 89;
			button8.Text = "浏览";
			button8.UseVisualStyleBackColor = true;
			button8.Click += button8_Click;
			// 
			// groupBox4
			// 
			groupBox4.Controls.Add(label3);
			groupBox4.Controls.Add(checkBox13);
			groupBox4.Controls.Add(button2);
			groupBox4.Controls.Add(Txt_Bin_Data);
			groupBox4.Controls.Add(button1);
			groupBox4.Dock = DockStyle.Top;
			groupBox4.Location = new Point(4, 4);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new Size(865, 117);
			groupBox4.TabIndex = 94;
			groupBox4.TabStop = false;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(6, 24);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(65, 20);
			label3.TabIndex = 63;
			label3.Text = "游戏目录";
			// 
			// checkBox13
			// 
			checkBox13.AutoSize = true;
			checkBox13.Location = new Point(16, 71);
			checkBox13.Margin = new Padding(4);
			checkBox13.Name = "checkBox13";
			checkBox13.Size = new Size(117, 21);
			checkBox13.TabIndex = 93;
			checkBox13.Text = "直接处理bin文件";
			checkBox13.UseVisualStyleBackColor = true;
			checkBox13.CheckedChanged += SaveCheckStatus;
			// 
			// button2
			// 
			button2.Location = new Point(647, 17);
			button2.Margin = new Padding(4);
			button2.Name = "button2";
			button2.Size = new Size(75, 34);
			button2.TabIndex = 62;
			button2.Text = "浏览";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// Txt_Bin_Data
			// 
			Txt_Bin_Data.Location = new Point(89, 23);
			Txt_Bin_Data.Margin = new Padding(4);
			Txt_Bin_Data.Name = "Txt_Bin_Data";
			Txt_Bin_Data.Size = new Size(530, 23);
			Txt_Bin_Data.TabIndex = 64;
			Txt_Bin_Data.TextChanged += SaveConfig;
			Txt_Bin_Data.DoubleClick += DoubleClickPath;
			// 
			// button1
			// 
			button1.Location = new Point(757, 17);
			button1.Margin = new Padding(4);
			button1.Name = "button1";
			button1.Size = new Size(75, 34);
			button1.TabIndex = 65;
			button1.Text = "提取";
			button1.UseVisualStyleBackColor = true;
			button1.Click += Button1_Click;
			// 
			// ModifyData
			// 
			ModifyData.Controls.Add(checkBox1);
			ModifyData.Controls.Add(button3);
			ModifyData.Controls.Add(label6);
			ModifyData.Controls.Add(trackBar1);
			ModifyData.Controls.Add(lbDat);
			ModifyData.Controls.Add(bntSearchDat);
			ModifyData.Controls.Add(txbDatFile);
			ModifyData.Controls.Add(txbRpFolder);
			ModifyData.Controls.Add(cB_output);
			ModifyData.Controls.Add(bntUnpack);
			ModifyData.Controls.Add(Cb_back);
			ModifyData.Controls.Add(btnRepack);
			ModifyData.Controls.Add(lbRfolder);
			ModifyData.Controls.Add(bntSearchOut);
			ModifyData.Location = new Point(4, 26);
			ModifyData.Margin = new Padding(4);
			ModifyData.Name = "ModifyData";
			ModifyData.Padding = new Padding(4);
			ModifyData.Size = new Size(873, 273);
			ModifyData.TabIndex = 0;
			ModifyData.Text = "Dat File";
			ModifyData.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Checked = true;
			checkBox1.CheckState = CheckState.Checked;
			checkBox1.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox1.Location = new Point(137, 179);
			checkBox1.Margin = new Padding(4);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(98, 24);
			checkBox1.TabIndex = 25;
			checkBox1.Text = "第三方封包";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += SaveCheckStatus;
			// 
			// button3
			// 
			button3.Location = new Point(733, 60);
			button3.Margin = new Padding(4);
			button3.Name = "button3";
			button3.Size = new Size(75, 34);
			button3.TabIndex = 24;
			button3.Text = "patch";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label6.Location = new Point(606, 203);
			label6.Margin = new Padding(4, 0, 4, 0);
			label6.Name = "label6";
			label6.Size = new Size(256, 21);
			label6.TabIndex = 23;
			label6.Text = "->       处理速度变慢，压缩率提高";
			// 
			// trackBar1
			// 
			trackBar1.BackColor = SystemColors.Window;
			trackBar1.Location = new Point(486, 179);
			trackBar1.Margin = new Padding(4);
			trackBar1.Maximum = 3;
			trackBar1.Name = "trackBar1";
			trackBar1.Size = new Size(376, 45);
			trackBar1.TabIndex = 22;
			trackBar1.ValueChanged += trackBar1_ValueChanged;
			// 
			// lbDat
			// 
			lbDat.AutoSize = true;
			lbDat.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			lbDat.Location = new Point(13, 14);
			lbDat.Margin = new Padding(4, 0, 4, 0);
			lbDat.Name = "lbDat";
			lbDat.Size = new Size(66, 20);
			lbDat.TabIndex = 17;
			lbDat.Text = ".dat 文件";
			// 
			// bntSearchDat
			// 
			bntSearchDat.Location = new Point(644, 13);
			bntSearchDat.Margin = new Padding(4);
			bntSearchDat.Name = "bntSearchDat";
			bntSearchDat.Size = new Size(75, 34);
			bntSearchDat.TabIndex = 0;
			bntSearchDat.Text = "选择";
			bntSearchDat.UseVisualStyleBackColor = true;
			bntSearchDat.Click += bntSearchDat_Click;
			// 
			// txbDatFile
			// 
			txbDatFile.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			txbDatFile.Location = new Point(120, 13);
			txbDatFile.Margin = new Padding(4);
			txbDatFile.Name = "txbDatFile";
			txbDatFile.Size = new Size(507, 25);
			txbDatFile.TabIndex = 3;
			txbDatFile.TextChanged += txbDatFile_TextChanged;
			txbDatFile.DoubleClick += DoubleClickPath;
			// 
			// txbRpFolder
			// 
			txbRpFolder.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			txbRpFolder.Location = new Point(120, 60);
			txbRpFolder.Margin = new Padding(4);
			txbRpFolder.Name = "txbRpFolder";
			txbRpFolder.Size = new Size(507, 25);
			txbRpFolder.TabIndex = 4;
			txbRpFolder.TextChanged += txbRpFolder_TextChanged;
			txbRpFolder.DoubleClick += DoubleClickPath;
			// 
			// cB_output
			// 
			cB_output.AutoSize = true;
			cB_output.Checked = true;
			cB_output.CheckState = CheckState.Checked;
			cB_output.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			cB_output.Location = new Point(16, 135);
			cB_output.Margin = new Padding(4);
			cB_output.Name = "cB_output";
			cB_output.Size = new Size(154, 24);
			cB_output.TabIndex = 14;
			cB_output.Text = "自动获得文件夹位置";
			cB_output.UseVisualStyleBackColor = true;
			cB_output.CheckedChanged += SaveCheckStatus;
			// 
			// bntUnpack
			// 
			bntUnpack.Location = new Point(636, 125);
			bntUnpack.Margin = new Padding(4);
			bntUnpack.Name = "bntUnpack";
			bntUnpack.Size = new Size(75, 34);
			bntUnpack.TabIndex = 2;
			bntUnpack.Text = "解包";
			bntUnpack.UseVisualStyleBackColor = true;
			bntUnpack.Click += BntStart_Click;
			// 
			// Cb_back
			// 
			Cb_back.AutoSize = true;
			Cb_back.Checked = true;
			Cb_back.CheckState = CheckState.Checked;
			Cb_back.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			Cb_back.Location = new Point(16, 179);
			Cb_back.Margin = new Padding(4);
			Cb_back.Name = "Cb_back";
			Cb_back.Size = new Size(98, 24);
			Cb_back.TabIndex = 20;
			Cb_back.Text = "备份原文件";
			Cb_back.UseVisualStyleBackColor = true;
			Cb_back.CheckedChanged += SaveCheckStatus;
			// 
			// btnRepack
			// 
			btnRepack.Location = new Point(733, 125);
			btnRepack.Margin = new Padding(4);
			btnRepack.Name = "btnRepack";
			btnRepack.Size = new Size(75, 34);
			btnRepack.TabIndex = 15;
			btnRepack.Text = "封包";
			btnRepack.UseVisualStyleBackColor = true;
			btnRepack.Click += btnRepack_Click;
			// 
			// lbRfolder
			// 
			lbRfolder.AutoSize = true;
			lbRfolder.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			lbRfolder.Location = new Point(13, 62);
			lbRfolder.Margin = new Padding(4, 0, 4, 0);
			lbRfolder.Name = "lbRfolder";
			lbRfolder.Size = new Size(65, 20);
			lbRfolder.TabIndex = 18;
			lbRfolder.Text = "解包目录";
			// 
			// bntSearchOut
			// 
			bntSearchOut.Location = new Point(644, 60);
			bntSearchOut.Margin = new Padding(4);
			bntSearchOut.Name = "bntSearchOut";
			bntSearchOut.Size = new Size(75, 34);
			bntSearchOut.TabIndex = 1;
			bntSearchOut.Text = "选择";
			bntSearchOut.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			tabControl1.Controls.Add(ModifyData);
			tabControl1.Controls.Add(ModifyBin);
			tabControl1.Controls.Add(tabPage5);
			tabControl1.Controls.Add(Page_Region);
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Dock = DockStyle.Top;
			tabControl1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			tabControl1.Location = new Point(0, 0);
			tabControl1.Margin = new Padding(4);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(881, 303);
			tabControl1.TabIndex = 24;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(881, 574);
			ContextMenuStrip = MainMenu;
			Controls.Add(tabControl1);
			Controls.Add(groupBox2);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Icon = (Icon)resources.GetObject("$this.Icon");
			KeyPreview = true;
			Margin = new Padding(4);
			MaximizeBox = false;
			Name = "MainForm";
			Text = "Test GUI";
			groupBox2.ResumeLayout(false);
			MainMenu.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			Page_Region.ResumeLayout(false);
			Page_Region.PerformLayout();
			tabPage5.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			ModifyBin.ResumeLayout(false);
			groupBox5.ResumeLayout(false);
			groupBox5.PerformLayout();
			groupBox4.ResumeLayout(false);
			groupBox4.PerformLayout();
			ModifyData.ResumeLayout(false);
			ModifyData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
			tabControl1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private RichTextBox richOut;
		private GroupBox groupBox2;
		private ContextMenuStrip MainMenu;
		private ToolStripMenuItem MenuItem_DevTools;
		private ToolStripMenuItem 重新排列ToolStripMenuItem;
		private ToolStripMenuItem xml操作ToolStripMenuItem;
		private ToolStripMenuItem 列出所有属性ToolStripMenuItem;
		private ToolStripMenuItem 合并文档ToolStripMenuItem;
		internal Button button39;
		private TabPage tabPage1;
		private Button button11;
		private CheckBox checkBox12;
		private CheckBox checkBox11;
		private TextBox textBox2;
		private TextBox textBox4;
		private Label label4;
		private Button button9;
		private Label label5;
		private Button button10;
		private TabPage Page_Region;
		private Button button36;
		private Label label27;
		private TextBox Txt_Cterrain_Path;
		private TextBox textBox12;
		private TextBox Txt_Region_Path;
		private TextBox Txt_Zone;
		private Button button37;
		private Label label33;
		private Button button32;
		private Label label31;
		private Button button31;
		private Label label29;
		private TabPage tabPage5;
		private GroupBox groupBox3;
		private Button button13;
		private Label label11;
		private Button button14;
		private TextBox textBox8;
		private TabPage ModifyBin;
		private Button button38;
		private Button HeadDump;
		private TextBox Txt_Bin_Data;
		private Button button1;
		private Label label3;
		private Button button2;
		private CheckBox Chk_Backup;
		private TabPage ModifyData;
		private Label label6;
		private TrackBar trackBar1;
		private Label lbDat;
		private Button bntSearchDat;
		private TextBox txbDatFile;
		private TextBox txbRpFolder;
		private CheckBox cB_output;
		private Button bntUnpack;
		private CheckBox Cb_back;
		private Button btnRepack;
		private Label lbRfolder;
		private Button bntSearchOut;
		private TabControl tabControl1;
		private Button button3;
		private CheckBox checkBox1;
		private Button button7;
		private CheckBox checkBox13;
		private TextBox textBox6;
		private Label label8;
		private Button button17;
		private CheckBox checkBox14;
		private ToolStripMenuItem 列出指定属性范围ToolStripMenuItem;
		private Button button16;
		private Button Compare_Diff;
		private Label label13;
		private Label label12;
		private TextBox Region_YMax_input;
		private TextBox Region_YMin_input;
		private TextBox Region_XMax_input;
		private TextBox Region_XMin_input;
		private GroupBox groupBox4;
		private GroupBox groupBox5;
		private Button button6;
		private TextBox textBox1;
		private Label label1;
		private Button button8;
		private ToolStripMenuItem ToolStripMenuItem_NumSelect;
		private TextBox textBox3;
		private Label label2;
		private Button button4;
	}
}