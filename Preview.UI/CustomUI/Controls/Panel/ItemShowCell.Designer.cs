namespace Xylia.Preview.GameUI.Controls
{
	partial class ItemShowCell
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
			this.NameCell = new Xylia.Preview.GameUI.Controls.ItemNameCell();
			this.IconCell = new Xylia.Preview.GameUI.Controls.ItemIconCell();
			((System.ComponentModel.ISupportInitialize)(this.IconCell)).BeginInit();
			this.SuspendLayout();
			// 
			// NameCell
			// 
			this.NameCell.AutoSize = true;
			this.NameCell.BackColor = System.Drawing.Color.Transparent;
			this.NameCell.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameCell.ItemGrade = ((byte)(7));
			this.NameCell.Location = new System.Drawing.Point(63, 16);
			this.NameCell.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.NameCell.Name = "NameCell";
			this.NameCell.Size = new System.Drawing.Size(85, 21);
			this.NameCell.TabIndex = 1;
			this.NameCell.TagImage = null;
			this.NameCell.Text = "ItemName";
			this.NameCell.NameChanged += new System.EventHandler(this.ItemName_NameChanged);
			this.NameCell.DoubleClick += new System.EventHandler(this.ItemName_DoubleClick);
			// 
			// IconCell
			// 
			this.IconCell.BackColor = System.Drawing.Color.Transparent;
			this.IconCell.ForeColor = System.Drawing.Color.Black;
			this.IconCell.FrameImage = null;
			this.IconCell.FrameType = true;
			this.IconCell.Location = new System.Drawing.Point(0, 0);
			this.IconCell.Margin = new System.Windows.Forms.Padding(4);
			this.IconCell.Name = "IconCell";
			this.IconCell.Scale = 52;
			this.IconCell.ShowFrameImage = true;
			this.IconCell.ShowStackCount = false;
			this.IconCell.ShowStackCountOnlyOne = true;
			this.IconCell.Size = new System.Drawing.Size(52, 52);
			this.IconCell.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.IconCell.StackCount = 1;
			this.IconCell.TabIndex = 2;
			this.IconCell.TabStop = false;
			// 
			// ItemShowCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.IconCell);
			this.Controls.Add(this.NameCell);
			this.ForeColor = System.Drawing.Color.Black;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ItemShowCell";
			this.Size = new System.Drawing.Size(154, 56);

			((System.ComponentModel.ISupportInitialize)(this.IconCell)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public ItemIconCell IconCell;
		public ItemNameCell NameCell;
	}
}
