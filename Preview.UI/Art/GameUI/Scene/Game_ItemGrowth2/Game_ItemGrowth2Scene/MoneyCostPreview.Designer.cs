
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	partial class MoneyCostPreview
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.priceCell2 = new Xylia.Preview.GameUI.Controls.PriceCell();
			this.priceCell1 = new Xylia.Preview.GameUI.Controls.PriceCell();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(2, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 21);
			this.label2.TabIndex = 5;
			this.label2.Text = "基本费用";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(2, 35);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 21);
			this.label1.TabIndex = 6;
			this.label1.Text = "会员折扣";
			// 
			// priceCell2
			// 
			this.priceCell2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.priceCell2.BackColor = System.Drawing.Color.Transparent;
			this.priceCell2.CurrencyCount = 3008888;
			this.priceCell2.CurrencyType = Xylia.Preview.GameUI.Controls.Currency.CurrencyType.Money;
			this.priceCell2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.priceCell2.FontStyle = System.Drawing.FontStyle.Regular;
			this.priceCell2.ForeColor = System.Drawing.Color.White;
			this.priceCell2.Location = new System.Drawing.Point(180, 37);
			this.priceCell2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.priceCell2.Name = "priceCell2";
			this.priceCell2.Size = new System.Drawing.Size(123, 19);
			this.priceCell2.TabIndex = 8;
			this.priceCell2.Tooltip = null;
			// 
			// priceCell1
			// 
			this.priceCell1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.priceCell1.BackColor = System.Drawing.Color.Transparent;
			this.priceCell1.CurrencyCount = 3000;
			this.priceCell1.CurrencyType = Xylia.Preview.GameUI.Controls.Currency.CurrencyType.Money;
			this.priceCell1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.priceCell1.FontStyle = System.Drawing.FontStyle.Strikeout;
			this.priceCell1.ForeColor = System.Drawing.Color.White;
			this.priceCell1.Location = new System.Drawing.Point(265, 0);
			this.priceCell1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.priceCell1.Name = "priceCell1";
			this.priceCell1.Size = new System.Drawing.Size(38, 19);
			this.priceCell1.TabIndex = 7;
			this.priceCell1.Tooltip = null;
			// 
			// MoneyCostPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.priceCell2);
			this.Controls.Add(this.priceCell1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MoneyCostPreview";
			this.Size = new System.Drawing.Size(314, 62);
			this.SizeChanged += new System.EventHandler(this.MoneyCostPreview_SizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private PriceCell priceCell1;
		private PriceCell priceCell2;
	}
}
