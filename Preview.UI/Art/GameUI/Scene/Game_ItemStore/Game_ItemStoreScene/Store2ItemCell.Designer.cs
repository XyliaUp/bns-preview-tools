namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	partial class Store2ItemCell
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
			this.quotaTxt = new System.Windows.Forms.Label();
			this.BuyPriceCell = new BuyPriceCell();
			this.SuspendLayout();
			// 
			// ItemShow
			// 
			this.ItemShow.AutoSize = true;
			this.ItemShow.HeightDiff = -15;
			this.ItemShow.Size = new System.Drawing.Size(145, 56);
			// 
			// lbl_RightText
			// 
			this.lbl_RightText.Location = new System.Drawing.Point(341, 0);
			this.lbl_RightText.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.lbl_RightText.Size = new System.Drawing.Size(10, 60);
			this.lbl_RightText.Visible = false;
			// 
			// quotaTxt
			// 
			this.quotaTxt.AutoSize = true;
			this.quotaTxt.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.quotaTxt.ForeColor = System.Drawing.Color.White;
			this.quotaTxt.Location = new System.Drawing.Point(58, 29);
			this.quotaTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.quotaTxt.Name = "quotaTxt";
			this.quotaTxt.Size = new System.Drawing.Size(103, 20);
			this.quotaTxt.TabIndex = 5;
			this.quotaTxt.Text = "[限购政策信息]";
			this.quotaTxt.Visible = false;
			// 
			// Store2ItemCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.quotaTxt); 
			this.Controls.Add(this.BuyPriceCell);
			this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
			this.Name = "Store2ItemCell";
			this.Size = new System.Drawing.Size(351, 60);
			this.SizeChanged += new System.EventHandler(this.Store2ItemCell_SizeChanged);
			this.Controls.SetChildIndex(this.ItemShow, 0);
			this.Controls.SetChildIndex(this.lbl_RightText, 0);
			this.Controls.SetChildIndex(this.quotaTxt, 0);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion
		private System.Windows.Forms.Label quotaTxt;
		private BuyPriceCell BuyPriceCell;
	}
}
