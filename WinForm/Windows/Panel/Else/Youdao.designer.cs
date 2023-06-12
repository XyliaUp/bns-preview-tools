namespace Xylia.Match.Windows.Panel
{
	partial class Youdao
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源, 为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的Functions - 不要修改
		/// 使用代码编辑器修改此Functions的内容。
		/// </summary>
		private void InitializeComponent()
		{
			textBox1 = new System.Windows.Forms.TextBox();
			Btn_Start = new System.Windows.Forms.Button();
			textBox3 = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			Copy = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			Open = new System.Windows.Forms.OpenFileDialog();
			checkBox1 = new System.Windows.Forms.CheckBox();
			textBox2 = new System.Windows.Forms.TextBox();
			Copy2 = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Location = new System.Drawing.Point(120, 13);
			textBox1.Margin = new System.Windows.Forms.Padding(4);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(447, 23);
			textBox1.TabIndex = 0;
			textBox1.MouseDoubleClick += TextBox1_MouseDoubleClick;
			// 
			// Btn_Start
			// 
			Btn_Start.Location = new System.Drawing.Point(617, 13);
			Btn_Start.Margin = new System.Windows.Forms.Padding(4);
			Btn_Start.Name = "Btn_Start";
			Btn_Start.Size = new System.Drawing.Size(88, 33);
			Btn_Start.TabIndex = 1;
			Btn_Start.Text = "转换";
			Btn_Start.UseVisualStyleBackColor = true;
			Btn_Start.Click += Btn_Start_Click;
			// 
			// textBox3
			// 
			textBox3.Location = new System.Drawing.Point(120, 64);
			textBox3.Margin = new System.Windows.Forms.Padding(4);
			textBox3.Name = "textBox3";
			textBox3.ReadOnly = true;
			textBox3.Size = new System.Drawing.Size(447, 23);
			textBox3.TabIndex = 3;
			textBox3.MouseDoubleClick += TextBox3_MouseDoubleClick;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(9, 68);
			label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(32, 17);
			label2.TabIndex = 5;
			label2.Text = "直链";
			// 
			// Copy
			// 
			Copy.Location = new System.Drawing.Point(617, 74);
			Copy.Margin = new System.Windows.Forms.Padding(4);
			Copy.Name = "Copy";
			Copy.Size = new System.Drawing.Size(88, 33);
			Copy.TabIndex = 9;
			Copy.Text = "复制";
			Copy.UseVisualStyleBackColor = true;
			Copy.Click += Button1_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(22, 177);
			label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(56, 17);
			label3.TabIndex = 10;
			label3.Text = "文件信息";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(9, 17);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(92, 17);
			label1.TabIndex = 11;
			label1.Text = "有道云分享链接";
			// 
			// Open
			// 
			Open.CheckFileExists = false;
			Open.CheckPathExists = false;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Checked = true;
			checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox1.Location = new System.Drawing.Point(12, 129);
			checkBox1.Margin = new System.Windows.Forms.Padding(4);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new System.Drawing.Size(75, 21);
			checkBox1.TabIndex = 15;
			checkBox1.Text = "结果拆分";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
			// 
			// textBox2
			// 
			textBox2.Location = new System.Drawing.Point(120, 123);
			textBox2.Margin = new System.Windows.Forms.Padding(4);
			textBox2.Name = "textBox2";
			textBox2.ReadOnly = true;
			textBox2.Size = new System.Drawing.Size(447, 23);
			textBox2.TabIndex = 16;
			// 
			// Copy2
			// 
			Copy2.Location = new System.Drawing.Point(617, 123);
			Copy2.Margin = new System.Windows.Forms.Padding(4);
			Copy2.Name = "Copy2";
			Copy2.Size = new System.Drawing.Size(88, 33);
			Copy2.TabIndex = 17;
			Copy2.Text = "复制";
			Copy2.UseVisualStyleBackColor = true;
			// 
			// Youdao
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Copy2);
			Controls.Add(textBox2);
			Controls.Add(checkBox1);
			Controls.Add(label1);
			Controls.Add(label3);
			Controls.Add(Copy);
			Controls.Add(label2);
			Controls.Add(textBox3);
			Controls.Add(Btn_Start);
			Controls.Add(textBox1);
			Margin = new System.Windows.Forms.Padding(4);
			Name = "Youdao";
			Size = new System.Drawing.Size(766, 249);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button Btn_Start;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button Copy;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog Open;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button Copy2;
	}
}

