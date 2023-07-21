namespace Xylia.Preview.UI.Custom.Controls
{
	partial class TitleContentPanel
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
		private void InitializeComponent()
		{
			this.ContentPanel = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// ContentPanel
			// 
			this.ContentPanel.BackColor = System.Drawing.Color.Transparent;
			this.ContentPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ContentPanel.ForeColor = System.Drawing.Color.White;
			this.ContentPanel.Location = new System.Drawing.Point(7, 26);
			this.ContentPanel.Margin = new System.Windows.Forms.Padding(5);
			this.ContentPanel.Name = "ContentPanel";
			this.ContentPanel.TabIndex = 1;
			this.ContentPanel.Text = "消息文本";
			// 
			// TitleContentPanel
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.ContentPanel);
			this.Name = "TitleContentPanel";
			this.Size = new System.Drawing.Size(359, 52);
			this.Title = "标题";
			this.Controls.SetChildIndex(this.ContentPanel, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public ContentPanel ContentPanel;
	}
}
