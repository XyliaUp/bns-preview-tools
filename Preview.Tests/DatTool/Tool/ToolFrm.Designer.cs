using System.Drawing;
using System.Windows.Forms;

namespace Xylia.Preview.Tests.DatTool
{
	partial class ToolFrm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolFrm));
			ToolTip = new ToolTip(components);
			Txt_Decimal = new TextBox();
			Txt_HEX = new TextBox();
			TabControl1 = new TabControl();
			metroTabPage1 = new TabPage();
			radioButton2 = new RadioButton();
			radioButton1 = new RadioButton();
			label2 = new Label();
			lbl_Warning3 = new Label();
			Btn_DecimalToHex = new Button();
			Btn_HexToDecimal = new Button();
			metroTabPage3 = new TabPage();
			button4 = new Button();
			button3 = new Button();
			button8 = new Button();
			button7 = new Button();
			richTextBox1 = new RichTextBox();
			MainMenu = new ContextMenuStrip(components);
			复制ToolStripMenuItem = new ToolStripMenuItem();
			metroTabPage4 = new TabPage();
			numericUpDown3 = new NumericUpDown();
			button11 = new Button();
			numericUpDown2 = new NumericUpDown();
			radioButton4 = new RadioButton();
			radioButton3 = new RadioButton();
			numericUpDown1 = new NumericUpDown();
			button6 = new Button();
			richTextBox2 = new RichTextBox();
			richTextBox3 = new RichTextBox();
			Page2 = new TabPage();
			checkBox3 = new CheckBox();
			checkBox1 = new CheckBox();
			RuleText = new TextBox();
			Find = new PictureBox();
			lbl_Warning2 = new Label();
			lbl_length5 = new Label();
			lbl_length4 = new Label();
			Btn_Split = new Button();
			richOut = new RichTextBox();
			Btn_Compare = new Button();
			textBox4 = new TextBox();
			textBox3 = new TextBox();
			TabControl1.SuspendLayout();
			metroTabPage1.SuspendLayout();
			metroTabPage3.SuspendLayout();
			MainMenu.SuspendLayout();
			metroTabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			Page2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Find).BeginInit();
			SuspendLayout();
			// 
			// Txt_Decimal
			// 
			Txt_Decimal.Location = new Point(19, 57);
			Txt_Decimal.Margin = new Padding(3, 4, 3, 4);
			Txt_Decimal.Name = "Txt_Decimal";
			Txt_Decimal.Size = new Size(364, 23);
			Txt_Decimal.TabIndex = 27;
			ToolTip.SetToolTip(Txt_Decimal, "10进制");
			Txt_Decimal.TextChanged += Txt_Decimal_TextChanged;
			// 
			// Txt_HEX
			// 
			Txt_HEX.Location = new Point(19, 14);
			Txt_HEX.Margin = new Padding(3, 4, 3, 4);
			Txt_HEX.Name = "Txt_HEX";
			Txt_HEX.Size = new Size(364, 23);
			Txt_HEX.TabIndex = 25;
			Txt_HEX.Text = "00 e4 0b 54 02 00 00 00";
			ToolTip.SetToolTip(Txt_HEX, "16进制");
			Txt_HEX.TextChanged += Txt_HEX_TextChanged;
			// 
			// TabControl1
			// 
			TabControl1.Controls.Add(metroTabPage1);
			TabControl1.Controls.Add(metroTabPage3);
			TabControl1.Controls.Add(metroTabPage4);
			TabControl1.Controls.Add(Page2);
			TabControl1.Dock = DockStyle.Fill;
			TabControl1.Location = new Point(0, 0);
			TabControl1.Margin = new Padding(3, 4, 3, 4);
			TabControl1.Name = "TabControl1";
			TabControl1.SelectedIndex = 2;
			TabControl1.Size = new Size(893, 457);
			TabControl1.TabIndex = 23;
			// 
			// metroTabPage1
			// 
			metroTabPage1.Controls.Add(radioButton2);
			metroTabPage1.Controls.Add(radioButton1);
			metroTabPage1.Controls.Add(label2);
			metroTabPage1.Controls.Add(lbl_Warning3);
			metroTabPage1.Controls.Add(Btn_DecimalToHex);
			metroTabPage1.Controls.Add(Txt_Decimal);
			metroTabPage1.Controls.Add(Btn_HexToDecimal);
			metroTabPage1.Controls.Add(Txt_HEX);
			metroTabPage1.Location = new Point(4, 26);
			metroTabPage1.Margin = new Padding(3, 4, 3, 4);
			metroTabPage1.Name = "metroTabPage1";
			metroTabPage1.Size = new Size(885, 427);
			metroTabPage1.TabIndex = 0;
			metroTabPage1.Text = "主要";
			// 
			// radioButton2
			// 
			radioButton2.AutoSize = true;
			radioButton2.BackColor = Color.Transparent;
			radioButton2.Location = new Point(111, 116);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new Size(86, 21);
			radioButton2.TabIndex = 56;
			radioButton2.Text = "单字节转换";
			radioButton2.UseVisualStyleBackColor = false;
			// 
			// radioButton1
			// 
			radioButton1.AutoSize = true;
			radioButton1.BackColor = Color.Transparent;
			radioButton1.Checked = true;
			radioButton1.Location = new Point(22, 116);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new Size(86, 21);
			radioButton1.TabIndex = 55;
			radioButton1.TabStop = true;
			radioButton1.Text = "多字节转换";
			radioButton1.UseVisualStyleBackColor = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.BackColor = SystemColors.Window;
			label2.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(270, 85);
			label2.Name = "label2";
			label2.Size = new Size(55, 21);
			label2.TabIndex = 45;
			label2.Text = "label2";
			// 
			// lbl_Warning3
			// 
			lbl_Warning3.AutoSize = true;
			lbl_Warning3.BackColor = Color.Transparent;
			lbl_Warning3.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			lbl_Warning3.ForeColor = Color.Red;
			lbl_Warning3.Location = new Point(22, 153);
			lbl_Warning3.Name = "lbl_Warning3";
			lbl_Warning3.Size = new Size(65, 20);
			lbl_Warning3.TabIndex = 40;
			lbl_Warning3.Text = "提示信息";
			lbl_Warning3.Visible = false;
			lbl_Warning3.TextChanged += lbl_Warning3_TextChanged;
			// 
			// Btn_DecimalToHex
			// 
			Btn_DecimalToHex.Location = new Point(409, 53);
			Btn_DecimalToHex.Margin = new Padding(3, 4, 3, 4);
			Btn_DecimalToHex.Name = "Btn_DecimalToHex";
			Btn_DecimalToHex.Size = new Size(112, 32);
			Btn_DecimalToHex.TabIndex = 28;
			Btn_DecimalToHex.Text = "10进制转HEX";
			Btn_DecimalToHex.UseVisualStyleBackColor = true;
			Btn_DecimalToHex.Click += Btn_DecimalToHex_Click;
			// 
			// Btn_HexToDecimal
			// 
			Btn_HexToDecimal.Location = new Point(410, 9);
			Btn_HexToDecimal.Margin = new Padding(3, 4, 3, 4);
			Btn_HexToDecimal.Name = "Btn_HexToDecimal";
			Btn_HexToDecimal.Size = new Size(112, 32);
			Btn_HexToDecimal.TabIndex = 26;
			Btn_HexToDecimal.Text = "HEX转10进制";
			Btn_HexToDecimal.UseVisualStyleBackColor = true;
			Btn_HexToDecimal.Click += Btn_HexToDecimal_Click;
			// 
			// metroTabPage3
			// 
			metroTabPage3.Controls.Add(button4);
			metroTabPage3.Controls.Add(button3);
			metroTabPage3.Controls.Add(button8);
			metroTabPage3.Controls.Add(button7);
			metroTabPage3.Controls.Add(richTextBox1);
			metroTabPage3.Location = new Point(4, 26);
			metroTabPage3.Margin = new Padding(3, 4, 3, 4);
			metroTabPage3.Name = "metroTabPage3";
			metroTabPage3.Size = new Size(885, 427);
			metroTabPage3.TabIndex = 3;
			metroTabPage3.Text = "其他功能";
			// 
			// button4
			// 
			button4.BackColor = SystemColors.GradientActiveCaption;
			button4.Location = new Point(790, 108);
			button4.Margin = new Padding(3, 4, 3, 4);
			button4.Name = "button4";
			button4.Size = new Size(86, 34);
			button4.TabIndex = 41;
			button4.Text = "生成实枚举";
			button4.UseVisualStyleBackColor = false;
			button4.Click += button4_Click;
			// 
			// button3
			// 
			button3.BackColor = SystemColors.GradientActiveCaption;
			button3.Location = new Point(790, 66);
			button3.Margin = new Padding(3, 4, 3, 4);
			button3.Name = "button3";
			button3.Size = new Size(86, 34);
			button3.TabIndex = 40;
			button3.Text = "生成实字段";
			button3.UseVisualStyleBackColor = false;
			button3.Click += button3_Click;
			// 
			// button8
			// 
			button8.BackColor = SystemColors.GradientActiveCaption;
			button8.Location = new Point(790, 373);
			button8.Margin = new Padding(3, 4, 3, 4);
			button8.Name = "button8";
			button8.Size = new Size(86, 34);
			button8.TabIndex = 38;
			button8.Text = "标记转表格";
			button8.UseVisualStyleBackColor = false;
			button8.Click += button8_Click;
			// 
			// button7
			// 
			button7.BackColor = SystemColors.GradientActiveCaption;
			button7.Location = new Point(790, 331);
			button7.Margin = new Padding(3, 4, 3, 4);
			button7.Name = "button7";
			button7.Size = new Size(86, 34);
			button7.TabIndex = 37;
			button7.Text = "表格转标记";
			button7.UseVisualStyleBackColor = false;
			button7.Click += button7_Click;
			// 
			// richTextBox1
			// 
			richTextBox1.BackColor = Color.PapayaWhip;
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.ContextMenuStrip = MainMenu;
			richTextBox1.Dock = DockStyle.Fill;
			richTextBox1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			richTextBox1.Location = new Point(0, 0);
			richTextBox1.Margin = new Padding(3, 4, 3, 4);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(885, 427);
			richTextBox1.TabIndex = 31;
			richTextBox1.Text = "  <record name=\"TX_Gold_200716_001_Rare_0102_6\" id=\"686\" item=\"GB_General_Grocery_Coin_0067\" item-count=\"10\" item-price-money=\"100000\" />";
			richTextBox1.ZoomFactor = 1.3F;
			// 
			// MainMenu
			// 
			MainMenu.Items.AddRange(new ToolStripItem[] { 复制ToolStripMenuItem });
			MainMenu.Name = "contextMenuStrip1";
			MainMenu.RenderMode = ToolStripRenderMode.Professional;
			MainMenu.Size = new Size(101, 26);
			// 
			// 复制ToolStripMenuItem
			// 
			复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
			复制ToolStripMenuItem.Size = new Size(100, 22);
			复制ToolStripMenuItem.Text = "粘贴";
			复制ToolStripMenuItem.Click += 复制ToolStripMenuItem_Click;
			// 
			// metroTabPage4
			// 
			metroTabPage4.Controls.Add(numericUpDown3);
			metroTabPage4.Controls.Add(button11);
			metroTabPage4.Controls.Add(numericUpDown2);
			metroTabPage4.Controls.Add(radioButton4);
			metroTabPage4.Controls.Add(radioButton3);
			metroTabPage4.Controls.Add(numericUpDown1);
			metroTabPage4.Controls.Add(button6);
			metroTabPage4.Controls.Add(richTextBox2);
			metroTabPage4.Controls.Add(richTextBox3);
			metroTabPage4.Location = new Point(4, 26);
			metroTabPage4.Name = "metroTabPage4";
			metroTabPage4.Size = new Size(885, 427);
			metroTabPage4.TabIndex = 4;
			metroTabPage4.Text = "ItemSkill";
			// 
			// numericUpDown3
			// 
			numericUpDown3.Location = new Point(17, 393);
			numericUpDown3.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
			numericUpDown3.Name = "numericUpDown3";
			numericUpDown3.Size = new Size(73, 23);
			numericUpDown3.TabIndex = 38;
			// 
			// button11
			// 
			button11.Location = new Point(96, 369);
			button11.Margin = new Padding(3, 4, 3, 4);
			button11.Name = "button11";
			button11.Size = new Size(65, 33);
			button11.TabIndex = 37;
			button11.Text = "生成等级";
			button11.UseVisualStyleBackColor = true;
			button11.Click += button11_Click;
			// 
			// numericUpDown2
			// 
			numericUpDown2.Location = new Point(17, 366);
			numericUpDown2.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
			numericUpDown2.Name = "numericUpDown2";
			numericUpDown2.Size = new Size(73, 23);
			numericUpDown2.TabIndex = 36;
			// 
			// radioButton4
			// 
			radioButton4.AutoSize = true;
			radioButton4.BackColor = Color.Transparent;
			radioButton4.Checked = true;
			radioButton4.Location = new Point(624, 397);
			radioButton4.Name = "radioButton4";
			radioButton4.Size = new Size(93, 21);
			radioButton4.TabIndex = 35;
			radioButton4.TabStop = true;
			radioButton4.Text = "模式2 (手镯)";
			radioButton4.UseVisualStyleBackColor = false;
			// 
			// radioButton3
			// 
			radioButton3.AutoSize = true;
			radioButton3.BackColor = Color.Transparent;
			radioButton3.Location = new Point(624, 374);
			radioButton3.Name = "radioButton3";
			radioButton3.Size = new Size(105, 21);
			radioButton3.TabIndex = 24;
			radioButton3.Text = "模式1 (真气石)";
			radioButton3.UseVisualStyleBackColor = false;
			radioButton3.CheckedChanged += radioButton3_CheckedChanged;
			// 
			// numericUpDown1
			// 
			numericUpDown1.Location = new Point(806, 383);
			numericUpDown1.Name = "numericUpDown1";
			numericUpDown1.Size = new Size(73, 23);
			numericUpDown1.TabIndex = 34;
			numericUpDown1.Visible = false;
			// 
			// button6
			// 
			button6.Location = new Point(734, 378);
			button6.Margin = new Padding(3, 4, 3, 4);
			button6.Name = "button6";
			button6.Size = new Size(65, 33);
			button6.TabIndex = 33;
			button6.Text = "确定";
			button6.UseVisualStyleBackColor = true;
			button6.Click += button6_Click;
			// 
			// richTextBox2
			// 
			richTextBox2.BackColor = Color.PapayaWhip;
			richTextBox2.BorderStyle = BorderStyle.None;
			richTextBox2.ContextMenuStrip = MainMenu;
			richTextBox2.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			richTextBox2.Location = new Point(8, 4);
			richTextBox2.Margin = new Padding(3, 4, 3, 4);
			richTextBox2.Name = "richTextBox2";
			richTextBox2.Size = new Size(866, 162);
			richTextBox2.TabIndex = 32;
			richTextBox2.Text = "在此复制 ItemSkill 文本描述";
			richTextBox2.ZoomFactor = 1.3F;
			// 
			// richTextBox3
			// 
			richTextBox3.BackColor = Color.PapayaWhip;
			richTextBox3.BorderStyle = BorderStyle.None;
			richTextBox3.ContextMenuStrip = MainMenu;
			richTextBox3.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			richTextBox3.Location = new Point(8, 174);
			richTextBox3.Margin = new Padding(3, 4, 3, 4);
			richTextBox3.Name = "richTextBox3";
			richTextBox3.Size = new Size(866, 185);
			richTextBox3.TabIndex = 31;
			richTextBox3.Text = "在此复制 Effect 数据段";
			richTextBox3.ZoomFactor = 1.3F;
			// 
			// Page2
			// 
			Page2.Controls.Add(checkBox3);
			Page2.Controls.Add(checkBox1);
			Page2.Controls.Add(RuleText);
			Page2.Controls.Add(Find);
			Page2.Controls.Add(lbl_Warning2);
			Page2.Controls.Add(lbl_length5);
			Page2.Controls.Add(lbl_length4);
			Page2.Controls.Add(Btn_Split);
			Page2.Controls.Add(richOut);
			Page2.Controls.Add(Btn_Compare);
			Page2.Controls.Add(textBox4);
			Page2.Controls.Add(textBox3);
			Page2.Location = new Point(4, 26);
			Page2.Margin = new Padding(3, 4, 3, 4);
			Page2.Name = "Page2";
			Page2.Size = new Size(885, 427);
			Page2.TabIndex = 1;
			Page2.Text = "差异比对";
			// 
			// checkBox3
			// 
			checkBox3.AutoSize = true;
			checkBox3.BackColor = Color.White;
			checkBox3.Checked = true;
			checkBox3.CheckState = CheckState.Checked;
			checkBox3.Location = new Point(790, 116);
			checkBox3.Margin = new Padding(3, 4, 3, 4);
			checkBox3.Name = "checkBox3";
			checkBox3.Size = new Size(87, 21);
			checkBox3.TabIndex = 47;
			checkBox3.Text = "按字节处理";
			checkBox3.UseVisualStyleBackColor = false;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.BackColor = Color.White;
			checkBox1.Location = new Point(774, 284);
			checkBox1.Margin = new Padding(3, 4, 3, 4);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(123, 21);
			checkBox1.TabIndex = 45;
			checkBox1.Text = "逗号按字节型处理";
			checkBox1.UseVisualStyleBackColor = false;
			// 
			// RuleText
			// 
			RuleText.Location = new Point(450, 81);
			RuleText.Margin = new Padding(3, 4, 3, 4);
			RuleText.Name = "RuleText";
			RuleText.Size = new Size(212, 23);
			RuleText.TabIndex = 44;
			RuleText.TextChanged += RuleText_TextChanged;
			// 
			// Find
			// 
			Find.Location = new Point(701, 79);
			Find.Margin = new Padding(3, 4, 3, 4);
			Find.Name = "Find";
			Find.Size = new Size(29, 30);
			Find.SizeMode = PictureBoxSizeMode.StretchImage;
			Find.TabIndex = 42;
			Find.TabStop = false;
			Find.Click += Find_Click;
			// 
			// lbl_Warning2
			// 
			lbl_Warning2.AutoSize = true;
			lbl_Warning2.BackColor = Color.Transparent;
			lbl_Warning2.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			lbl_Warning2.ForeColor = Color.Red;
			lbl_Warning2.Location = new Point(3, 85);
			lbl_Warning2.Name = "lbl_Warning2";
			lbl_Warning2.Size = new Size(327, 20);
			lbl_Warning2.TabIndex = 40;
			lbl_Warning2.Text = "[提示]由于文本长度不一致，较长的部分将会被忽略";
			lbl_Warning2.Visible = false;
			// 
			// lbl_length5
			// 
			lbl_length5.AutoSize = true;
			lbl_length5.BackColor = Color.Transparent;
			lbl_length5.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			lbl_length5.ForeColor = Color.DodgerBlue;
			lbl_length5.Location = new Point(739, 48);
			lbl_length5.Name = "lbl_length5";
			lbl_length5.Size = new Size(59, 20);
			lbl_length5.TabIndex = 39;
			lbl_length5.Text = "长度：0";
			lbl_length5.TextChanged += Lbl_Length1_TextChanged;
			// 
			// lbl_length4
			// 
			lbl_length4.AutoSize = true;
			lbl_length4.BackColor = Color.Transparent;
			lbl_length4.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point);
			lbl_length4.ForeColor = Color.DodgerBlue;
			lbl_length4.Location = new Point(739, 11);
			lbl_length4.Name = "lbl_length4";
			lbl_length4.Size = new Size(59, 20);
			lbl_length4.TabIndex = 38;
			lbl_length4.Text = "长度：0";
			lbl_length4.TextChanged += Lbl_Length1_TextChanged;
			// 
			// Btn_Split
			// 
			Btn_Split.Location = new Point(774, 210);
			Btn_Split.Margin = new Padding(3, 4, 3, 4);
			Btn_Split.Name = "Btn_Split";
			Btn_Split.Size = new Size(78, 30);
			Btn_Split.TabIndex = 32;
			Btn_Split.Text = "4字节拆分";
			Btn_Split.UseVisualStyleBackColor = true;
			Btn_Split.Click += Btn_Split_Click;
			// 
			// richOut
			// 
			richOut.BackColor = Color.PapayaWhip;
			richOut.BorderStyle = BorderStyle.None;
			richOut.ContextMenuStrip = MainMenu;
			richOut.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			richOut.Location = new Point(3, 117);
			richOut.Margin = new Padding(3, 4, 3, 4);
			richOut.Name = "richOut";
			richOut.Size = new Size(765, 299);
			richOut.TabIndex = 30;
			richOut.Text = "";
			richOut.ZoomFactor = 1.3F;
			// 
			// Btn_Compare
			// 
			Btn_Compare.Location = new Point(799, 74);
			Btn_Compare.Margin = new Padding(3, 4, 3, 4);
			Btn_Compare.Name = "Btn_Compare";
			Btn_Compare.Size = new Size(78, 30);
			Btn_Compare.TabIndex = 11;
			Btn_Compare.Text = "比对差异";
			Btn_Compare.UseVisualStyleBackColor = true;
			Btn_Compare.Click += Btn_Compare_Click;
			// 
			// textBox4
			// 
			textBox4.Location = new Point(3, 48);
			textBox4.Margin = new Padding(3, 4, 3, 4);
			textBox4.Name = "textBox4";
			textBox4.Size = new Size(727, 23);
			textBox4.TabIndex = 10;
			textBox4.TextChanged += TextBox4_TextChanged;
			// 
			// textBox3
			// 
			textBox3.Location = new Point(3, 11);
			textBox3.Margin = new Padding(3, 4, 3, 4);
			textBox3.Name = "textBox3";
			textBox3.Size = new Size(727, 23);
			textBox3.TabIndex = 9;
			textBox3.TextChanged += TextBox3_TextChanged;
			// 
			// Frm1
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(893, 457);
			Controls.Add(TabControl1);
			Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Icon = (Icon)resources.GetObject("$this.Icon");
			KeyPreview = true;
			Margin = new Padding(3, 4, 3, 4);
			MaximizeBox = false;
			Name = "Frm1";
			StartPosition = FormStartPosition.CenterParent;
			Text = "开发者工具";
			TransparencyKey = Color.FromArgb(255, 192, 192);
			Shown += DevTools_Shown;
			TabControl1.ResumeLayout(false);
			metroTabPage1.ResumeLayout(false);
			metroTabPage1.PerformLayout();
			metroTabPage3.ResumeLayout(false);
			MainMenu.ResumeLayout(false);
			metroTabPage4.ResumeLayout(false);
			metroTabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
			Page2.ResumeLayout(false);
			Page2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)Find).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private ToolTip ToolTip;
		private TabControl TabControl1;
		private Button Btn_DecimalToHex;
		private TextBox Txt_Decimal;
		private Button Btn_HexToDecimal;
		private TextBox Txt_HEX;

		private TabPage metroTabPage1;
		private TabPage Page2;
		private Button Btn_Split;

		private RichTextBox richOut;
		private Button Btn_Compare;
		private TextBox textBox4;
		private TextBox textBox3;
		private Label lbl_length5;
		private Label lbl_length4;
		private Label lbl_Warning2;
		private Label lbl_Warning3;
		private ContextMenuStrip MainMenu;
		private ToolStripMenuItem 复制ToolStripMenuItem;
		private Label label2;
		private PictureBox Find;
		private TextBox RuleText;
		private CheckBox checkBox1;
		private TabPage metroTabPage3;
		private RichTextBox richTextBox1;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
		private CheckBox checkBox3;
		private TabPage metroTabPage4;
		private Button button6;
		private RichTextBox richTextBox2;
		private RichTextBox richTextBox3;
		private NumericUpDown numericUpDown1;
		private Button button7;
		private Button button8;
		private RadioButton radioButton4;
		private RadioButton radioButton3;
		private Button button11;
		private NumericUpDown numericUpDown2;
		private NumericUpDown numericUpDown3;
		private Button button3;
		private Button button4;
	}
}