using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Controls.List
{
	partial class ItemListCell
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
		protected void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemListCell));
			this.ItemShow = new Xylia.Preview.GameUI.Controls.ItemShowCell();
			this.SuspendLayout();
			// 
			// lbl_RightText
			// 
			this.lbl_RightText.Location = new System.Drawing.Point(260, 0);
			this.lbl_RightText.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbl_RightText.Size = new System.Drawing.Size(58, 47);
			// 
			// ItemShow
			// 
			this.ItemShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ItemShow.BackColor = System.Drawing.Color.Transparent;
			this.ItemShow.ForeColor = System.Drawing.Color.Black;
			this.ItemShow.HeightDiff = 0;
			this.ItemShow.ItemData = null;
			this.ItemShow.ItemGrade = ((byte)(7));
			this.ItemShow.ItemIcon = ((System.Drawing.Bitmap)(resources.GetObject("ItemShow.ItemIcon")));
			this.ItemShow.ItemName = "ItemName";
			this.ItemShow.Location = new System.Drawing.Point(0, 0);
			this.ItemShow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.ItemShow.Name = "ItemShow";
			this.ItemShow.ReserveIconSpace = true;
			this.ItemShow.Scale = 48;
			this.ItemShow.Size = new System.Drawing.Size(135, 47);
			this.ItemShow.TabIndex = 26;
			this.ItemShow.TagImage = null;
			// 
			// ItemListCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.ItemShow);
			this.ForeColor = System.Drawing.Color.Black;
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "ItemListCell";
			this.ShowRightText = true;
			this.Size = new System.Drawing.Size(318, 47);
			this.Controls.SetChildIndex(this.lbl_RightText, 0);
			this.Controls.SetChildIndex(this.ItemShow, 0);
			this.ResumeLayout(false);

		}

		#endregion

		public ItemShowCell ItemShow;
	}
}
