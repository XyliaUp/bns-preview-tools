namespace Xylia.Preview.GameUI.Scene.Searcher
{
	partial class SearcherDialog
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.Confirm = new HZH_Controls.Controls.UCBtnExt();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBox1.Location = new System.Drawing.Point(17, 46);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(400, 25);
			this.textBox1.TabIndex = 0;
			// 
			// Confirm
			// 
			this.Confirm.BtnBackColor = System.Drawing.Color.Empty;
			this.Confirm.BtnFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Confirm.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.Confirm.Text = "确定";
			this.Confirm.ConerRadius = 8;
			this.Confirm.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Confirm.EnabledMouseEffect = false;
			this.Confirm.FillColor = System.Drawing.Color.White;
			this.Confirm.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Confirm.IsRadius = true;
			this.Confirm.IsShowRect = true;
			this.Confirm.IsShowTips = false;
			this.Confirm.Location = new System.Drawing.Point(327, 87);
			this.Confirm.Margin = new System.Windows.Forms.Padding(0);
			this.Confirm.Name = "Confirm";
			this.Confirm.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.Confirm.RectWidth = 1;
			this.Confirm.Size = new System.Drawing.Size(90, 38);
			this.Confirm.TabIndex = 100;
			this.Confirm.TabStop = false;
			this.Confirm.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
			this.Confirm.TipsText = "";
			this.Confirm.Click += new System.EventHandler(this.Confirm_BtnClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(276, 20);
			this.label1.TabIndex = 101;
			this.label1.Text = "请输入筛选条件 (引用对象输入alias或者id)";
			// 
			// SearcherStore
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(438, 141);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Confirm);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SearcherStore";
			this.Padding = new System.Windows.Forms.Padding(23, 85, 23, 28);
			this.Text = "筛选";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private HZH_Controls.Controls.UCBtnExt Confirm;
		public System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
	}
}