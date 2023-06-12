namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class DurabilityPreview
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的Functions - 不要修改
		/// 使用代码编辑器修改此Functions的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.lbl_Trade_Account = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.pictureBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbl_Trade_Account
			// 
			this.lbl_Trade_Account.AutoSize = true;
			this.lbl_Trade_Account.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Trade_Account.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl_Trade_Account.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_Trade_Account.ForeColor = System.Drawing.Color.White;
			this.lbl_Trade_Account.Location = new System.Drawing.Point(0, 0);
			this.lbl_Trade_Account.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_Trade_Account.Name = "lbl_Trade_Account";
			this.lbl_Trade_Account.Size = new System.Drawing.Size(51, 20);
			this.lbl_Trade_Account.TabIndex = 9;
			this.lbl_Trade_Account.Text = "耐久度";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.pictureBox1.Controls.Add(this.label1);
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
			this.pictureBox1.Location = new System.Drawing.Point(67, 0);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(303, 26);
			this.pictureBox1.TabIndex = 10;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(78, 1);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 17);
			this.label1.TabIndex = 11;
			this.label1.Text = "100 / 100";
			// 
			// DurabilityPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.lbl_Trade_Account);
			this.ForeColor = System.Drawing.Color.White;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "DurabilityPreview";
			this.Size = new System.Drawing.Size(370, 26);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.pictureBox1.ResumeLayout(false);
			this.pictureBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_Trade_Account;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
	}
}
