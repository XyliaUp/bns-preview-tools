
namespace Xylia.Preview.GameUI.Controls.List
{
	partial class ListCell
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
			this.lbl_RightText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl_RightText
			// 
			this.lbl_RightText.Dock = System.Windows.Forms.DockStyle.Right;
			this.lbl_RightText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_RightText.ForeColor = System.Drawing.Color.Magenta;
			this.lbl_RightText.Location = new System.Drawing.Point(365, 0);
			this.lbl_RightText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_RightText.Name = "lbl_RightText";
			this.lbl_RightText.Size = new System.Drawing.Size(83, 68);
			this.lbl_RightText.TabIndex = 6;
			this.lbl_RightText.Text = "地图名称";
			this.lbl_RightText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_RightText.Visible = false;
			// 
			// ListCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lbl_RightText);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ListCell";
			this.Size = new System.Drawing.Size(448, 68);
			this.ResumeLayout(false);

		}
		#endregion

		public System.Windows.Forms.Label lbl_RightText;
	}
}
