namespace Xylia.Preview.GameUI.Controls
{
	partial class ListPreview
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
			this.components = new System.ComponentModel.Container();
			this.MainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SaveAsImage = new System.Windows.Forms.ToolStripMenuItem();
			this.pageSelector = new Xylia.Preview.GameUI.Controls.PageSelector();
			this.MainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveAsImage});
			this.MainMenu.Name = "Menu";
			this.MainMenu.Size = new System.Drawing.Size(137, 26);
			// 
			// SaveAsImage
			// 
			this.SaveAsImage.Name = "SaveAsImage";
			this.SaveAsImage.Size = new System.Drawing.Size(136, 22);
			this.SaveAsImage.Text = "存储为图片";
			this.SaveAsImage.Click += new System.EventHandler(this.SaveAsImage_Click);
			// 
			// pageSelector
			// 
			this.pageSelector.AutoSize = true;
			this.pageSelector.Visible = false;
			this.pageSelector.BackColor = System.Drawing.Color.Transparent;
			this.pageSelector.Location = new System.Drawing.Point(85, 173);
			this.pageSelector.Margin = new System.Windows.Forms.Padding(4);
			this.pageSelector.Name = "pageSelector";
			this.pageSelector.Size = new System.Drawing.Size(111, 21);
			this.pageSelector.TabIndex = 1;
			this.pageSelector.PrevSeleted += new System.EventHandler(this.pageSelector_PrevSeleted);
			this.pageSelector.NextSeleted += new System.EventHandler(this.pageSelector_NextSeleted);
			// 
			// ListPreview
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pageSelector);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Name = "ListPreview";
			this.Size = new System.Drawing.Size(292, 198);
			this.MainMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.ToolStripMenuItem SaveAsImage;
		public System.Windows.Forms.ContextMenuStrip MainMenu;
		private PageSelector pageSelector;
	}
}