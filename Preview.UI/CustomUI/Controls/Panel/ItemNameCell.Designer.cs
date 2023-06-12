namespace Xylia.Preview.GameUI.Controls
{
	partial class ItemNameCell
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
			// ItemNameCell
			// 
			//this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
			//this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "ItemNameCell";
			this.Size = new System.Drawing.Size(20, 20);
			//this.Load += new System.EventHandler(this.ItemNameCell_Load);
			this.DoubleClick += new System.EventHandler(this.OnDoubleClick);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
