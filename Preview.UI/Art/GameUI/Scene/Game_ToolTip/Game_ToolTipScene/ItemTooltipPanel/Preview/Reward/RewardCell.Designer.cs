using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class RewardCell
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RewardCell));
			this.m_Type = new System.Windows.Forms.PictureBox();
			this.lbl_Count = new System.Windows.Forms.Label();
			this.ItemShow = new Xylia.Preview.GameUI.Controls.ItemShowCell();
			((System.ComponentModel.ISupportInitialize)(this.m_Type)).BeginInit();
			this.SuspendLayout();
			// 
			// m_Type
			// 
			this.m_Type.BackColor = System.Drawing.Color.Transparent;
			this.m_Type.Image = ((System.Drawing.Image)(resources.GetObject("m_Type.Image")));
			this.m_Type.Location = new System.Drawing.Point(212, 4);
			this.m_Type.Margin = new System.Windows.Forms.Padding(4);
			this.m_Type.Name = "m_Type";
			this.m_Type.Size = new System.Drawing.Size(64, 25);
			this.m_Type.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_Type.TabIndex = 3;
			this.m_Type.TabStop = false;
			this.m_Type.Visible = false;
			// 
			// lbl_Count
			// 
			this.lbl_Count.AutoSize = true;
			this.lbl_Count.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Count.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_Count.ForeColor = System.Drawing.Color.White;
			this.lbl_Count.Location = new System.Drawing.Point(132, 3);
			this.lbl_Count.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_Count.Name = "lbl_Count";
			this.lbl_Count.Size = new System.Drawing.Size(56, 21);
			this.lbl_Count.TabIndex = 4;
			this.lbl_Count.Text = "1~8个";
			// 
			// ItemShow
			// 
			//this.ItemShow.alias = null;
			this.ItemShow.BackColor = System.Drawing.Color.Transparent;
			this.ItemShow.ForeColor = System.Drawing.Color.Black;
			this.ItemShow.HeightDiff = 0;
			this.ItemShow.ItemGrade = 7;
			this.ItemShow.ItemIcon = ((System.Drawing.Bitmap)(resources.GetObject("ItemShow.ItemIcon")));
			this.ItemShow.ItemName = "ItemName";
			this.ItemShow.Location = new System.Drawing.Point(1, 1);
			this.ItemShow.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ItemShow.Name = "ItemShow";
			this.ItemShow.ReserveIconSpace = false;
			this.ItemShow.Scale = 28;
			this.ItemShow.Size = new System.Drawing.Size(141, 26);
			this.ItemShow.TabIndex = 5;
			this.ItemShow.TagImage = null;
			// 
			// RewardCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lbl_Count);
			this.Controls.Add(this.ItemShow);
			this.Controls.Add(this.m_Type);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "RewardCell";
			this.Size = new System.Drawing.Size(292, 33);
			this.Load += new System.EventHandler(this.RewardCell_Load);
			((System.ComponentModel.ISupportInitialize)(this.m_Type)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.PictureBox m_Type;
		private System.Windows.Forms.Label lbl_Count;
		private ItemShowCell ItemShow;
	}
}
