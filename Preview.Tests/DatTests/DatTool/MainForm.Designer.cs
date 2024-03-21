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
			ToolStripMenuItem_NumSelect = new ToolStripMenuItem();
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
			tabPage2 = new TabPage();
			radioButton2 = new RadioButton();
			radioButton1 = new RadioButton();
			Convert_Warning = new Label();
			Btn_DecimalToHex = new Button();
			Convert_Decimal = new TextBox();
			Btn_HexToDecimal = new Button();
			Convert_Hex = new TextBox();
			tabPage3 = new TabPage();
			Btn_Split = new Button();
			button5 = new Button();
			button12 = new Button();
			button15 = new Button();
			button16 = new Button();
			richTextBox1 = new RichTextBox();
			groupBox2.SuspendLayout();
			MainMenu.SuspendLayout();
			ModifyBin.SuspendLayout();
			groupBox5.SuspendLayout();
			groupBox4.SuspendLayout();
			ModifyData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
			tabControl1.SuspendLayout();
			tabPage2.SuspendLayout();
			tabPage3.SuspendLayout();
			SuspendLayout();
			// 
			// richOut
			// 
			richOut.BackColor = Color.White;
			richOut.BorderStyle = BorderStyle.None;
			richOut.Dock = DockStyle.Fill;
			richOut.Font = new Font("微软雅黑", 10.5F);
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
			button39.Font = new Font("微软雅黑", 11.25F);
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
			MainMenu.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_NumSelect });
			MainMenu.Name = "MainMenu";
			MainMenu.Size = new Size(125, 26);
			// 
			// ToolStripMenuItem_NumSelect
			// 
			ToolStripMenuItem_NumSelect.Name = "ToolStripMenuItem_NumSelect";
			ToolStripMenuItem_NumSelect.Size = new Size(124, 22);
			ToolStripMenuItem_NumSelect.Text = "属性生成";
			ToolStripMenuItem_NumSelect.Click += ToolStripMenuItem_NumSelect_Click;
			// 
			// ModifyBin
			// 
			ModifyBin.Controls.Add(groupBox5);
			ModifyBin.Controls.Add(groupBox4);
			ModifyBin.Location = new Point(4, 26);
			ModifyBin.Margin = new Padding(4);
			ModifyBin.Name = "ModifyBin";
			ModifyBin.Padding = new Padding(4);
			ModifyBin.Size = new Size(873, 270);
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
			groupBox5.Location = new Point(4, 135);
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
			label2.Font = new Font("微软雅黑", 10F);
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
			label1.Font = new Font("微软雅黑", 10F);
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
			label3.Font = new Font("微软雅黑", 10F);
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
			ModifyData.Size = new Size(873, 270);
			ModifyData.TabIndex = 0;
			ModifyData.Text = "Dat File";
			ModifyData.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Checked = true;
			checkBox1.CheckState = CheckState.Checked;
			checkBox1.Font = new Font("微软雅黑", 10F);
			checkBox1.Location = new Point(137, 179);
			checkBox1.Margin = new Padding(4);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(98, 24);
			checkBox1.TabIndex = 25;
			checkBox1.Text = "第三方封包";
			checkBox1.UseVisualStyleBackColor = true;
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
			label6.Font = new Font("微软雅黑", 12F);
			label6.Location = new Point(486, 203);
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
			trackBar1.Value = 1;
			trackBar1.ValueChanged += trackBar1_ValueChanged;
			// 
			// lbDat
			// 
			lbDat.AutoSize = true;
			lbDat.Font = new Font("微软雅黑", 10F);
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
			txbDatFile.Font = new Font("微软雅黑", 10F);
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
			txbRpFolder.Font = new Font("微软雅黑", 10F);
			txbRpFolder.Location = new Point(120, 60);
			txbRpFolder.Margin = new Padding(4);
			txbRpFolder.Name = "txbRpFolder";
			txbRpFolder.Size = new Size(507, 25);
			txbRpFolder.TabIndex = 4;
			txbRpFolder.DoubleClick += DoubleClickPath;
			// 
			// cB_output
			// 
			cB_output.AutoSize = true;
			cB_output.Checked = true;
			cB_output.CheckState = CheckState.Checked;
			cB_output.Font = new Font("微软雅黑", 10F);
			cB_output.Location = new Point(16, 135);
			cB_output.Margin = new Padding(4);
			cB_output.Name = "cB_output";
			cB_output.Size = new Size(154, 24);
			cB_output.TabIndex = 14;
			cB_output.Text = "自动获得文件夹位置";
			cB_output.UseVisualStyleBackColor = true;
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
			Cb_back.Font = new Font("微软雅黑", 10F);
			Cb_back.Location = new Point(16, 179);
			Cb_back.Margin = new Padding(4);
			Cb_back.Name = "Cb_back";
			Cb_back.Size = new Size(98, 24);
			Cb_back.TabIndex = 20;
			Cb_back.Text = "备份原文件";
			Cb_back.UseVisualStyleBackColor = true;
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
			lbRfolder.Font = new Font("微软雅黑", 10F);
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
			tabControl1.Controls.Add(tabPage2);
			tabControl1.Controls.Add(tabPage3);
			tabControl1.Dock = DockStyle.Top;
			tabControl1.Font = new Font("微软雅黑", 9F);
			tabControl1.Location = new Point(0, 0);
			tabControl1.Margin = new Padding(4);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(881, 300);
			tabControl1.TabIndex = 24;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(radioButton2);
			tabPage2.Controls.Add(radioButton1);
			tabPage2.Controls.Add(Convert_Warning);
			tabPage2.Controls.Add(Btn_DecimalToHex);
			tabPage2.Controls.Add(Convert_Decimal);
			tabPage2.Controls.Add(Btn_HexToDecimal);
			tabPage2.Controls.Add(Convert_Hex);
			tabPage2.Location = new Point(4, 26);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3);
			tabPage2.Size = new Size(873, 270);
			tabPage2.TabIndex = 12;
			tabPage2.Text = "Convert";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			radioButton2.AutoSize = true;
			radioButton2.BackColor = Color.Transparent;
			radioButton2.Location = new Point(102, 109);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new Size(86, 21);
			radioButton2.TabIndex = 64;
			radioButton2.Text = "单字节转换";
			radioButton2.UseVisualStyleBackColor = false;
			// 
			// radioButton1
			// 
			radioButton1.AutoSize = true;
			radioButton1.BackColor = Color.Transparent;
			radioButton1.Checked = true;
			radioButton1.Location = new Point(13, 109);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new Size(86, 21);
			radioButton1.TabIndex = 63;
			radioButton1.TabStop = true;
			radioButton1.Text = "多字节转换";
			radioButton1.UseVisualStyleBackColor = false;
			// 
			// Convert_Warning
			// 
			Convert_Warning.AutoSize = true;
			Convert_Warning.BackColor = Color.Transparent;
			Convert_Warning.Font = new Font("微软雅黑", 10F);
			Convert_Warning.ForeColor = Color.Red;
			Convert_Warning.Location = new Point(13, 146);
			Convert_Warning.Name = "Convert_Warning";
			Convert_Warning.Size = new Size(65, 20);
			Convert_Warning.TabIndex = 61;
			Convert_Warning.Text = "提示信息";
			// 
			// Btn_DecimalToHex
			// 
			Btn_DecimalToHex.Location = new Point(400, 46);
			Btn_DecimalToHex.Margin = new Padding(3, 4, 3, 4);
			Btn_DecimalToHex.Name = "Btn_DecimalToHex";
			Btn_DecimalToHex.Size = new Size(112, 32);
			Btn_DecimalToHex.TabIndex = 60;
			Btn_DecimalToHex.Text = "10进制转HEX";
			Btn_DecimalToHex.UseVisualStyleBackColor = true;
			Btn_DecimalToHex.Click += Btn_DecimalToHex_Click;
			// 
			// Convert_Decimal
			// 
			Convert_Decimal.Location = new Point(10, 50);
			Convert_Decimal.Margin = new Padding(3, 4, 3, 4);
			Convert_Decimal.Name = "Convert_Decimal";
			Convert_Decimal.Size = new Size(364, 23);
			Convert_Decimal.TabIndex = 59;
			// 
			// Btn_HexToDecimal
			// 
			Btn_HexToDecimal.Location = new Point(401, 2);
			Btn_HexToDecimal.Margin = new Padding(3, 4, 3, 4);
			Btn_HexToDecimal.Name = "Btn_HexToDecimal";
			Btn_HexToDecimal.Size = new Size(112, 32);
			Btn_HexToDecimal.TabIndex = 58;
			Btn_HexToDecimal.Text = "HEX转10进制";
			Btn_HexToDecimal.UseVisualStyleBackColor = true;
			Btn_HexToDecimal.Click += Btn_HexToDecimal_Click;
			// 
			// Convert_Hex
			// 
			Convert_Hex.Location = new Point(10, 7);
			Convert_Hex.Margin = new Padding(3, 4, 3, 4);
			Convert_Hex.Name = "Convert_Hex";
			Convert_Hex.Size = new Size(364, 23);
			Convert_Hex.TabIndex = 57;
			Convert_Hex.Text = "00 e4 0b 54 02 00 00 00";
			// 
			// tabPage3
			// 
			tabPage3.Controls.Add(Btn_Split);
			tabPage3.Controls.Add(button5);
			tabPage3.Controls.Add(button12);
			tabPage3.Controls.Add(button15);
			tabPage3.Controls.Add(button16);
			tabPage3.Controls.Add(richTextBox1);
			tabPage3.Location = new Point(4, 26);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new Padding(3);
			tabPage3.Size = new Size(873, 270);
			tabPage3.TabIndex = 13;
			tabPage3.Text = "Else";
			tabPage3.UseVisualStyleBackColor = true;
			// 
			// Btn_Split
			// 
			Btn_Split.Location = new Point(764, 191);
			Btn_Split.Margin = new Padding(3, 4, 3, 4);
			Btn_Split.Name = "Btn_Split";
			Btn_Split.Size = new Size(83, 30);
			Btn_Split.TabIndex = 47;
			Btn_Split.Text = "4字节拆分";
			Btn_Split.UseVisualStyleBackColor = true;
			Btn_Split.Click += Btn_Split_Click;
			// 
			// button5
			// 
			button5.Location = new Point(764, 44);
			button5.Margin = new Padding(3, 4, 3, 4);
			button5.Name = "button5";
			button5.Size = new Size(86, 34);
			button5.TabIndex = 46;
			button5.Text = "生成实枚举";
			button5.UseVisualStyleBackColor = false;
			button5.Click += button5_Click;
			// 
			// button12
			// 
			button12.Location = new Point(764, 4);
			button12.Margin = new Padding(3, 4, 3, 4);
			button12.Name = "button12";
			button12.Size = new Size(86, 34);
			button12.TabIndex = 45;
			button12.Text = "生成实字段";
			button12.UseVisualStyleBackColor = false;
			button12.Click += button12_Click;
			// 
			// button15
			// 
			button15.Location = new Point(764, 135);
			button15.Margin = new Padding(3, 4, 3, 4);
			button15.Name = "button15";
			button15.Size = new Size(86, 34);
			button15.TabIndex = 44;
			button15.Text = "标记转表格";
			button15.UseVisualStyleBackColor = false;
			button15.Click += button15_Click;
			// 
			// button16
			// 
			button16.Location = new Point(764, 93);
			button16.Margin = new Padding(3, 4, 3, 4);
			button16.Name = "button16";
			button16.Size = new Size(86, 34);
			button16.TabIndex = 43;
			button16.Text = "表格转标记";
			button16.UseVisualStyleBackColor = false;
			button16.Click += button16_Click;
			// 
			// richTextBox1
			// 
			richTextBox1.BackColor = Color.PapayaWhip;
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.ContextMenuStrip = MainMenu;
			richTextBox1.Dock = DockStyle.Fill;
			richTextBox1.Font = new Font("微软雅黑", 9F);
			richTextBox1.Location = new Point(3, 3);
			richTextBox1.Margin = new Padding(3, 4, 3, 4);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(867, 264);
			richTextBox1.TabIndex = 42;
			richTextBox1.Text = "  <record name=\"TX_Gold_200716_001_Rare_0102_6\" id=\"686\" item=\"GB_General_Grocery_Coin_0067\" item-count=\"10\" item-price-money=\"100000\" />";
			richTextBox1.ZoomFactor = 1.3F;
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
			ModifyBin.ResumeLayout(false);
			groupBox5.ResumeLayout(false);
			groupBox5.PerformLayout();
			groupBox4.ResumeLayout(false);
			groupBox4.PerformLayout();
			ModifyData.ResumeLayout(false);
			ModifyData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
			tabControl1.ResumeLayout(false);
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			tabPage3.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private RichTextBox richOut;
		private GroupBox groupBox2;
		private ContextMenuStrip MainMenu;
		internal Button button39;
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
		private Button button16;
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
		private TabPage tabPage2;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
		private Label Convert_Warning;
		private Button Btn_DecimalToHex;
		private TextBox Convert_Decimal;
		private Button Btn_HexToDecimal;
		private TextBox Convert_Hex;
		private TabPage tabPage3;
		private Button button5;
		private Button button12;
		private Button button15;
		private RichTextBox richTextBox1;
		private Button Btn_Split;
	}
}