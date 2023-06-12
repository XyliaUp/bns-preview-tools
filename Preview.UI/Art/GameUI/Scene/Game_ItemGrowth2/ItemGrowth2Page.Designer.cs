using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	partial class ItemGrowth2Page
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
			this.label1 = new System.Windows.Forms.Label();
			this.ResultWeaponPreview = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.ResultWeaponPreview();
			this.GrowthState = new Xylia.Preview.GameUI.Controls.ContentPanel();
			((System.ComponentModel.ISupportInitialize)(this.MyWeapon_Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(332, 17);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 21);
			this.label1.TabIndex = 17;
			this.label1.Text = "目标装备";
			// 
			// ResultWeaponPreview
			// 
			this.ResultWeaponPreview.AutoSize = true;
			this.ResultWeaponPreview.BackColor = System.Drawing.Color.Transparent;
			this.ResultWeaponPreview.Location = new System.Drawing.Point(182, 44);
			this.ResultWeaponPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ResultWeaponPreview.Name = "ResultWeaponPreview";
			this.ResultWeaponPreview.Size = new System.Drawing.Size(497, 131);
			this.ResultWeaponPreview.TabIndex = 0;
			this.ResultWeaponPreview.ResultItemChanged += new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.ResultWeaponPreview.ResultItemChangedHandle(this.ResultWeaponPreview_ResultItemChanged);
			// 
			// GrowthState
			// 
			this.GrowthState.BackColor = System.Drawing.Color.Transparent;
			this.GrowthState.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GrowthState.FontName = null;
			this.GrowthState.ForeColor = System.Drawing.Color.White;
			this.GrowthState.Location = new System.Drawing.Point(295, 261);
			this.GrowthState.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
			this.GrowthState.Name = "GrowthState";
			this.GrowthState.TabIndex = 26;
			this.GrowthState.Text = "<font name=\"00008130.ItemGrowth_Transform_28\">进化</font>";
			// 
			// ItemGrowth2Page
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.Controls.Add(this.GrowthState);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ResultWeaponPreview);
			this.Name = "ItemGrowth2Page";
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.MyWeapon_Icon, 0);
			this.Controls.SetChildIndex(this.MyWeapon_Name, 0);
			this.Controls.SetChildIndex(this.MoneyCostPreview, 0);
			this.Controls.SetChildIndex(this.FixedIngredientPreview, 0);
			this.Controls.SetChildIndex(this.SubIngredientTitle, 0);
			this.Controls.SetChildIndex(this.SubIngredientPreview, 0);
			this.Controls.SetChildIndex(this.WarningPreview, 0);
			this.Controls.SetChildIndex(this.ResultWeaponPreview, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.MyWeapon_Title, 0);
			this.Controls.SetChildIndex(this.GrowthState, 0);
			((System.ComponentModel.ISupportInitialize)(this.MyWeapon_Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
	
		private System.Windows.Forms.Label label1;
		protected ResultWeaponPreview ResultWeaponPreview;
		protected Controls.ContentPanel GrowthState;
	}
}
