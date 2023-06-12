using Xylia.Preview.GameUI.Controls.PanelEx;
using Xylia.Preview.GameUI.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	partial class WarningPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningPreview));
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.panelContent1 = new Xylia.Preview.GameUI.Controls.ContentPanel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(0, -1);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(31, 27);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 20;
			this.pictureBox2.TabStop = false;
			// 
			// panelContent1
			// 
			this.panelContent1.BackColor = System.Drawing.Color.Transparent;
			//this.panelContent1.BasicLineHeight = 19;
			this.panelContent1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.panelContent1.ForeColor = System.Drawing.Color.White;
			this.panelContent1.Location = new System.Drawing.Point(39, 3);
			this.panelContent1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
			this.panelContent1.Name = "panelContent1";
			this.panelContent1.TabIndex = 19;
			this.panelContent1.Text = "提示消息";
			// 
			// WarningPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.panelContent1);
			this.Controls.Add(this.pictureBox2);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "WarningPreview";
			this.Size = new System.Drawing.Size(103, 30);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public ContentPanel panelContent1;
		private System.Windows.Forms.PictureBox pictureBox2;
	}
}
