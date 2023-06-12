using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class SlateScrollCell
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlateScrollCell));
			this.lbl_AbilityInfo = new System.Windows.Forms.Label();
			this.Panel_ItemInfo = new Xylia.Preview.GameUI.Controls.ItemShowCell();
			this.SuspendLayout();
			// 
			// lbl_AbilityInfo
			// 
			this.lbl_AbilityInfo.AutoSize = true;
			this.lbl_AbilityInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_AbilityInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(255)))), ((int)(((byte)(119)))));
			this.lbl_AbilityInfo.Location = new System.Drawing.Point(268, 4);
			this.lbl_AbilityInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_AbilityInfo.Name = "lbl_AbilityInfo";
			this.lbl_AbilityInfo.Size = new System.Drawing.Size(51, 20);
			this.lbl_AbilityInfo.TabIndex = 7;
			this.lbl_AbilityInfo.Text = "能力值";
			// 
			// Panel_ItemInfo
			// 
			//this.Panel_ItemInfo.alias = null;
			this.Panel_ItemInfo.BackColor = System.Drawing.Color.Transparent;
			this.Panel_ItemInfo.ForeColor = System.Drawing.Color.Black;
			this.Panel_ItemInfo.HeightDiff = 0;
			this.Panel_ItemInfo.ItemGrade = 7;
			this.Panel_ItemInfo.ItemIcon = ((System.Drawing.Bitmap)(resources.GetObject("Panel_ItemInfo.ItemIcon")));
			this.Panel_ItemInfo.ItemName = "雪峰之觉醒燃烧";
			this.Panel_ItemInfo.Location = new System.Drawing.Point(1, 1);
			this.Panel_ItemInfo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Panel_ItemInfo.Name = "Panel_ItemInfo";
			this.Panel_ItemInfo.ReserveIconSpace = false;
			this.Panel_ItemInfo.Scale = 25;
			this.Panel_ItemInfo.Size = new System.Drawing.Size(167, 34);
			this.Panel_ItemInfo.TabIndex = 5;
			this.Panel_ItemInfo.TagImage = null;
			// 
			// SlateScrollCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lbl_AbilityInfo);
			this.Controls.Add(this.Panel_ItemInfo);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SlateScrollCell";
			this.Size = new System.Drawing.Size(345, 34);
			this.Load += new System.EventHandler(this.RewardCell_Load);
			this.SizeChanged += new System.EventHandler(this.SlateScrollCell_SizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private ItemShowCell Panel_ItemInfo;
		private System.Windows.Forms.Label lbl_AbilityInfo;
	}
}
