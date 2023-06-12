namespace Xylia.Preview.GameUI.Controls
{
	partial class ContentPanel
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
			this.SuspendLayout();
			// 
			// PanelContent
			// 
			//this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ForeColor = System.Drawing.Color.White;
			this.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.Name = "PanelContent";
			this.Size = new System.Drawing.Size(123, 20);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelContent_Paint);
			this.DoubleClick += new System.EventHandler(this.PanelContent_DoubleClick);
			this.ResumeLayout(false);
		}

		#endregion
	}
}
