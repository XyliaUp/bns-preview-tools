using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Custom.Controls.Currency;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardCell
{
	partial class PriceBase
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
			this.priceCell1 = new Xylia.Preview.UI.Custom.Controls.PriceCell();
			this.SuspendLayout();
			// 
			// panelContent1
			// 
			this.panelContent1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(206)))), ((int)(((byte)(238)))));
			this.panelContent1.Visible = false;
			// 
			// priceCell1
			// 
			this.priceCell1.BackColor = System.Drawing.Color.Transparent;
			this.priceCell1.CurrencyCount = ((int)(1ul));
			this.priceCell1.CurrencyType = CurrencyType.Money;
			this.priceCell1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.priceCell1.FontStyle = System.Drawing.FontStyle.Regular;
			this.priceCell1.ForeColor = System.Drawing.Color.White;
			this.priceCell1.Location = new System.Drawing.Point(123, -1);
			this.priceCell1.Margin = new System.Windows.Forms.Padding(5);
			this.priceCell1.Name = "priceCell1";
			this.priceCell1.Size = new System.Drawing.Size(30, 19);
			this.priceCell1.TabIndex = 27;
			// 
			// PriceBase
			// 

			this.Controls.Add(this.priceCell1);
			this.Name = "PriceBase";
			this.Size = new System.Drawing.Size(325, 23);
			this.Title = "钱币";
			this.UseBasicPanel = false;
			this.Controls.SetChildIndex(this.panelContent1, 0);
			this.Controls.SetChildIndex(this.priceCell1, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public PriceCell priceCell1;
	}
}
