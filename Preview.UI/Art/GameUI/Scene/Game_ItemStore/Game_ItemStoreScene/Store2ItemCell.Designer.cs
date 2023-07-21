namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	partial class Store2ItemCell
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
		private new void InitializeComponent()
		{
			this.QuotaText = new System.Windows.Forms.Label();
			this.BuyPriceCell = new BuyPriceCell();
			this.SuspendLayout();
			// 
			// QuotaText
			// 
			this.QuotaText.AutoSize = true;
			this.QuotaText.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.QuotaText.ForeColor = System.Drawing.Color.White;
			this.QuotaText.Location = new System.Drawing.Point(58, 29);
			this.QuotaText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.QuotaText.Name = "QuotaText";
			this.QuotaText.Size = new System.Drawing.Size(103, 20);
			this.QuotaText.TabIndex = 5;
			this.QuotaText.Text = "[限购政策信息]";
			this.QuotaText.Visible = false;
			// 
			// Store2ItemCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.QuotaText);
			this.Controls.Add(this.BuyPriceCell);
			this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
			this.Name = "Store2ItemCell";
			this.Size = new System.Drawing.Size(351, 60);
			this.SizeChanged += new System.EventHandler(this.Store2ItemCell_SizeChanged);
			this.Controls.SetChildIndex(this.ItemShow, 0);
			this.Controls.SetChildIndex(this.QuotaText, 0);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion
		private System.Windows.Forms.Label QuotaText;
		private BuyPriceCell BuyPriceCell;
	}
}
