
namespace Xylia.Preview.UI.Custom.Controls
{
	partial class TestTooltip2
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
			this.contentPanel = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// contentPanel
			// 
			this.contentPanel.BackColor = System.Drawing.Color.Transparent;
			this.contentPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.contentPanel.ForeColor = System.Drawing.Color.White;
			this.contentPanel.Location = new System.Drawing.Point(0, 0);
			this.contentPanel.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.contentPanel.Name = "contentPanel";
			this.contentPanel.TabIndex = 0;
			this.contentPanel.Text = "测试";
			// 
			// TestTooltip2
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(355, 73);
			this.Controls.Add(this.contentPanel);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TestTooltip2";
			this.Opacity = 0.8D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.VisibleChanged += new System.EventHandler(this.Frm_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ContentPanel contentPanel;
	}
}