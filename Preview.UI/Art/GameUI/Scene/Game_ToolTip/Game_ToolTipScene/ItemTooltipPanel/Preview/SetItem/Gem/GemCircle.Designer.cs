namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class GemCircle
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GemCircle));
			this.Panel_BackGround = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.Panel_BackGround)).BeginInit();
			this.SuspendLayout();
			// 
			// Panel_BackGround
			// 
			this.Panel_BackGround.BackColor = System.Drawing.Color.Transparent;
			this.Panel_BackGround.Image = ((System.Drawing.Image)(resources.GetObject("Panel_BackGround.Image")));
			this.Panel_BackGround.Location = new System.Drawing.Point(0, 0);
			this.Panel_BackGround.Margin = new System.Windows.Forms.Padding(4);
			this.Panel_BackGround.Name = "Panel_BackGround";
			this.Panel_BackGround.Size = new System.Drawing.Size(240, 240);
			this.Panel_BackGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.Panel_BackGround.TabIndex = 0;
			this.Panel_BackGround.TabStop = false;
			this.Panel_BackGround.DoubleClick += new System.EventHandler(this.Panel_BackGround_DoubleClick);
			this.Panel_BackGround.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel_BackGround_MouseClick);
			// 
			// GemCircle
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Panel_BackGround);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "GemCircle";
			this.Size = new System.Drawing.Size(244, 244);
			this.Load += new System.EventHandler(this.GemCircle_Load);
			((System.ComponentModel.ISupportInitialize)(this.Panel_BackGround)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox Panel_BackGround;
	}
}
