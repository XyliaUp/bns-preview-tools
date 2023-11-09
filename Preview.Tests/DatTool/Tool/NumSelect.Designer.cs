namespace Xylia.Preview.Tests.DatTool
{
	partial class NumSelect
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
			StartVal = new NumericUpDown();
			EndVal = new NumericUpDown();
			Btn_Confirm = new Button();
			label4 = new Label();
			label5 = new Label();
			radioButton1 = new RadioButton();
			radioButton2 = new RadioButton();
			radioButton3 = new RadioButton();
			comboBox1 = new ComboBox();
			((System.ComponentModel.ISupportInitialize)StartVal).BeginInit();
			((System.ComponentModel.ISupportInitialize)EndVal).BeginInit();
			SuspendLayout();
			// 
			// StartVal
			// 
			StartVal.Location = new Point(77, 123);
			StartVal.Margin = new Padding(4);
			StartVal.Maximum = new decimal(new int[] { 1215752191, 23, 0, 0 });
			StartVal.Name = "StartVal";
			StartVal.Size = new Size(106, 23);
			StartVal.TabIndex = 0;
			// 
			// EndVal
			// 
			EndVal.Location = new Point(77, 160);
			EndVal.Margin = new Padding(4);
			EndVal.Maximum = new decimal(new int[] { 1215752191, 23, 0, 0 });
			EndVal.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			EndVal.Name = "EndVal";
			EndVal.Size = new Size(106, 23);
			EndVal.TabIndex = 1;
			EndVal.Value = new decimal(new int[] { 1, 0, 0, 0 });
			// 
			// Btn_Confirm
			// 
			Btn_Confirm.DialogResult = DialogResult.OK;
			Btn_Confirm.Location = new Point(303, 138);
			Btn_Confirm.Margin = new Padding(4);
			Btn_Confirm.Name = "Btn_Confirm";
			Btn_Confirm.Size = new Size(110, 45);
			Btn_Confirm.TabIndex = 2;
			Btn_Confirm.Text = "确定";
			Btn_Confirm.UseVisualStyleBackColor = true;
			Btn_Confirm.Click += Btn_Confirm_Click;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			label4.Location = new Point(5, 126);
			label4.Margin = new Padding(4, 0, 4, 0);
			label4.Name = "label4";
			label4.Size = new Size(56, 17);
			label4.TabIndex = 11;
			label4.Text = "起始索引";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			label5.Location = new Point(5, 161);
			label5.Margin = new Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new Size(56, 17);
			label5.TabIndex = 12;
			label5.Text = "结束索引";
			// 
			// radioButton1
			// 
			radioButton1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton1.Location = new Point(5, 53);
			radioButton1.Margin = new Padding(4);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new Size(98, 21);
			radioButton1.TabIndex = 19;
			radioButton1.TabStop = true;
			radioButton1.Text = "显示起始信息";
			radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			radioButton2.AutoSize = true;
			radioButton2.Location = new Point(5, 82);
			radioButton2.Margin = new Padding(4);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new Size(62, 21);
			radioButton2.TabIndex = 20;
			radioButton2.Text = "不显示";
			radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton3
			// 
			radioButton3.AutoSize = true;
			radioButton3.Location = new Point(111, 53);
			radioButton3.Margin = new Padding(4);
			radioButton3.Name = "radioButton3";
			radioButton3.Size = new Size(111, 21);
			radioButton3.TabIndex = 21;
			radioButton3.Text = "start-show形式";
			radioButton3.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(5, 12);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(271, 25);
			comboBox1.TabIndex = 22;
			// 
			// NumSelect
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(447, 221);
			Controls.Add(comboBox1);
			Controls.Add(radioButton3);
			Controls.Add(radioButton2);
			Controls.Add(radioButton1);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(Btn_Confirm);
			Controls.Add(EndVal);
			Controls.Add(StartVal);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(4);
			Name = "NumSelect";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "数值选择";
			Load += NumSelect_Load;
			((System.ComponentModel.ISupportInitialize)StartVal).EndInit();
			((System.ComponentModel.ISupportInitialize)EndVal).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button Btn_Confirm;
		public NumericUpDown StartVal;
		public NumericUpDown EndVal;
		private Label label4;
		private Label label5;
		private RadioButton radioButton1;
		private RadioButton radioButton2;
		private RadioButton radioButton3;
		private ComboBox comboBox1;
	}
}