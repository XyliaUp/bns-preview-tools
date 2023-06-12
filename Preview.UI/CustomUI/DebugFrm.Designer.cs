using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.Tests
{
	partial class DebugFrm
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
			this.components = new System.ComponentModel.Container();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.contentPanel1 = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.contentPanel2 = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(237, 23);
			this.textBox1.TabIndex = 0;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// contentPanel1
			// 
			this.contentPanel1.BackColor = System.Drawing.Color.Transparent;
			this.contentPanel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.contentPanel1.FontName = null;
			this.contentPanel1.ForeColor = System.Drawing.Color.White;
			this.contentPanel1.Location = new System.Drawing.Point(12, 55);
			this.contentPanel1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.contentPanel1.Name = "contentPanel1";
			this.contentPanel1.TabIndex = 2;
			this.contentPanel1.Text = "# 测试功能";
			// 
			// contentPanel2
			// 
			this.contentPanel2.BackColor = System.Drawing.Color.Transparent;
			this.contentPanel2.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.contentPanel2.FontName = null;
			this.contentPanel2.ForeColor = System.Drawing.Color.White;
			this.contentPanel2.Location = new System.Drawing.Point(32, 84);
			this.contentPanel2.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.contentPanel2.Name = "contentPanel2";
			this.contentPanel2.TabIndex = 3;
			this.contentPanel2.Text = "百炼锤1个 ~ 2个";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 136);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 89);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// DebugFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(548, 304);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.contentPanel2);
			this.Controls.Add(this.contentPanel1);
			this.Controls.Add(this.textBox1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.DimGray;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximumSize = new System.Drawing.Size(2147483647, 850);
			this.Name = "DebugFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Debug";
			this.Load += new System.EventHandler(this.DebugFrm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }


		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ListBox listBox1;
		private ContentPanel contentPanel1;
		private ContentPanel contentPanel2;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

