namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class SetItemPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetItemPreview));
			this.lbl_Title = new System.Windows.Forms.Label();
			this.SetItemEffect_Title = new System.Windows.Forms.Label();
			this.itemShowCell2 = new Xylia.Preview.GameUI.Controls.ItemShowCell();
			this.GemPreview = new Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.GemPreview();
			this.JobStyleSelect = new Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview.JobStyleSelect();
			this.SuspendLayout();
			// 
			// lbl_Title
			// 
			this.lbl_Title.AutoSize = true;
			this.lbl_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_Title.ForeColor = System.Drawing.Color.White;
			this.lbl_Title.Location = new System.Drawing.Point(4, 0);
			this.lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_Title.Name = "lbl_Title";
			this.lbl_Title.Size = new System.Drawing.Size(114, 21);
			this.lbl_Title.TabIndex = 2;
			this.lbl_Title.Text = "套装名称 (0/0)";
			// 
			// SetItemEffect_Title
			// 
			this.SetItemEffect_Title.AutoSize = true;
			this.SetItemEffect_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SetItemEffect_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(99)))));
			this.SetItemEffect_Title.Location = new System.Drawing.Point(0, 257);
			this.SetItemEffect_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SetItemEffect_Title.Name = "SetItemEffect_Title";
			this.SetItemEffect_Title.Size = new System.Drawing.Size(74, 21);
			this.SetItemEffect_Title.TabIndex = 3;
			this.SetItemEffect_Title.Text = "套装效果";
			// 
			// itemShowCell2
			// 
			this.itemShowCell2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.itemShowCell2.BackColor = System.Drawing.Color.Transparent;
			this.itemShowCell2.ForeColor = System.Drawing.Color.Black;
			this.itemShowCell2.HeightDiff = 0;
			this.itemShowCell2.ItemData = null;
			this.itemShowCell2.ItemGrade = ((byte)(7));
			this.itemShowCell2.ItemIcon = ((System.Drawing.Bitmap)(resources.GetObject("itemShowCell2.ItemIcon")));
			this.itemShowCell2.ItemName = "ItemName";
			this.itemShowCell2.Location = new System.Drawing.Point(8, 34);
			this.itemShowCell2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.itemShowCell2.Name = "itemShowCell2";
			this.itemShowCell2.ReserveIconSpace = true;
			this.itemShowCell2.Scale = 32;
			this.itemShowCell2.Size = new System.Drawing.Size(146, 33);
			this.itemShowCell2.TabIndex = 10;
			this.itemShowCell2.TagImage = null;
			this.itemShowCell2.Visible = false;
			// 
			// GemPreview
			// 
			this.GemPreview.AutoSize = true;
			this.GemPreview.BackColor = System.Drawing.Color.Transparent;
			this.GemPreview.Location = new System.Drawing.Point(7, 35);
			this.GemPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.GemPreview.Meta1 = null;
			this.GemPreview.Meta2 = null;
			this.GemPreview.Meta3 = null;
			this.GemPreview.Meta4 = null;
			this.GemPreview.Meta5 = null;
			this.GemPreview.Meta6 = null;
			this.GemPreview.Meta7 = null;
			this.GemPreview.Meta8 = null;
			this.GemPreview.Name = "GemPreview";
			this.GemPreview.PublicGrade = ((byte)(7));
			this.GemPreview.Size = new System.Drawing.Size(334, 214);
			this.GemPreview.TabIndex = 9;
			// 
			// JobStyleSelect
			// 
			this.JobStyleSelect.AutoSize = true;
			this.JobStyleSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.JobStyleSelect.BackColor = System.Drawing.Color.Transparent;
			this.JobStyleSelect.Location = new System.Drawing.Point(296, 244);
			this.JobStyleSelect.Name = "JobStyleSelect";
			this.JobStyleSelect.Size = new System.Drawing.Size(73, 34);
			this.JobStyleSelect.TabIndex = 11;
			// 
			// SetItemPreview
			// 
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.JobStyleSelect);
			this.Controls.Add(this.itemShowCell2);
			this.Controls.Add(this.SetItemEffect_Title);
			this.Controls.Add(this.lbl_Title);
			this.Controls.Add(this.GemPreview);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SetItemPreview";
			this.Size = new System.Drawing.Size(372, 281);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_Title;
		private System.Windows.Forms.Label SetItemEffect_Title;
		private GemPreview GemPreview;
		private Xylia.Preview.GameUI.Controls.ItemShowCell itemShowCell2;
		private Preview.JobStyleSelect JobStyleSelect;
	}
}
