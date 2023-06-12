namespace Xylia.Preview.GameUI.Controls
{
	partial class TitleContentPanel
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
			this.ContentPanel = new Xylia.Preview.GameUI.Controls.ContentPanel();
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
			this.ContentPanel.Text = "<font name=\"00008130.UI.Label_LightYellow_12\">消息文本</font>";
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
