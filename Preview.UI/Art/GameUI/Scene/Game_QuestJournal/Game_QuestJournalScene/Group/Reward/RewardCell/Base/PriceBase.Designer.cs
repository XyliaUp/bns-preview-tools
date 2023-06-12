
namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardCell
{
	partial class PriceBase
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
			this.priceCell1 = new Xylia.Preview.GameUI.Controls.PriceCell();
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
			this.priceCell1.CurrencyType = GameUI.Controls.Currency.CurrencyType.Money;
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
		public Controls.PriceCell priceCell1;
	}
}
