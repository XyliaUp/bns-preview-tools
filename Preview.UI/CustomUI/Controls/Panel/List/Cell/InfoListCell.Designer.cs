namespace Xylia.Preview.GameUI.Controls.List
{
	partial class InfoListCell
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
			this.lbl_LeftText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl_RightText
			// 
			this.lbl_RightText.Location = new System.Drawing.Point(394, 0);
			this.lbl_RightText.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbl_RightText.Size = new System.Drawing.Size(97, 75);
			// 
			// lbl_LeftText
			// 
			this.lbl_LeftText.AutoSize = true;
			this.lbl_LeftText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_LeftText.ForeColor = System.Drawing.Color.Magenta;
			this.lbl_LeftText.Location = new System.Drawing.Point(4, 21);
			this.lbl_LeftText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_LeftText.Name = "lbl_LeftText";
			this.lbl_LeftText.Size = new System.Drawing.Size(76, 21);
			this.lbl_LeftText.TabIndex = 5;
			this.lbl_LeftText.Text = "NPC名称";
			this.lbl_LeftText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// InfoCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lbl_LeftText);
			this.ForeColor = System.Drawing.Color.Black;
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "InfoCell";
			this.ShowRightText = true;
			this.Size = new System.Drawing.Size(491, 75);
			this.Controls.SetChildIndex(this.lbl_RightText, 0);
			this.Controls.SetChildIndex(this.lbl_LeftText, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lbl_LeftText;
	}
}
