
namespace Xylia.Preview.GameUI.Scene.Game_Intension
{
	partial class IntensionResetConfirmPanel
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
			this.components = new System.ComponentModel.Container();
			this.AcquirableOptionListTitle = new System.Windows.Forms.Label();
			this.CurrentOptionListTitle = new System.Windows.Forms.Label();
			this.CurrentOptionList = new OptionList();
			this.AcquirableOptionList = new OptionList();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.MyWeapon_Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// MoneyCostPreview
			// 
			this.MoneyCostPreview.UseDiscount = false;
			// 
			// SubIngredientPreview
			// 
			this.SubIngredientPreview.RecipeChanged += new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.SubIngredientPreview.RecipeChangedHandle(this.SubIngredientPreview_RecipeChanged);
			// 
			// MyWeapon_Title
			// 
			this.MyWeapon_Title.Visible = false;
			// 
			// MyWeapon_Icon
			// 
			this.MyWeapon_Icon.Visible = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Visible = false;
			// 
			// MyWeapon_Name
			// 
			this.MyWeapon_Name.Visible = false;
			// 
			// AcquirableOptionListTitle
			// 
			this.AcquirableOptionListTitle.AutoSize = true;
			this.AcquirableOptionListTitle.ForeColor = System.Drawing.Color.White;
			this.AcquirableOptionListTitle.Location = new System.Drawing.Point(357, 8);
			this.AcquirableOptionListTitle.Name = "AcquirableOptionListTitle";
			this.AcquirableOptionListTitle.Size = new System.Drawing.Size(104, 17);
			this.AcquirableOptionListTitle.TabIndex = 33;
			this.AcquirableOptionListTitle.Text = "可获得的强化效果";
			this.ToolTip.SetToolTip(this.AcquirableOptionListTitle, "显示当前强化阶段的强化效果\n若无则显示获得阶段的强化效果");
			// 
			// CurrentOptionListTitle
			// 
			this.CurrentOptionListTitle.AutoSize = true;
			this.CurrentOptionListTitle.ForeColor = System.Drawing.Color.White;
			this.CurrentOptionListTitle.Location = new System.Drawing.Point(13, 8);
			this.CurrentOptionListTitle.Name = "CurrentOptionListTitle";
			this.CurrentOptionListTitle.Size = new System.Drawing.Size(128, 17);
			this.CurrentOptionListTitle.TabIndex = 35;
			this.CurrentOptionListTitle.Text = "现在应用中的强化效果";
			// 
			// CurrentOptionList
			// 
			this.CurrentOptionList.AutoScroll = true;
			this.CurrentOptionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CurrentOptionList.ForeColor = System.Drawing.Color.White;
			this.CurrentOptionList.Location = new System.Drawing.Point(13, 28);
			this.CurrentOptionList.Name = "CurrentOptionList";
			this.CurrentOptionList.Size = new System.Drawing.Size(313, 247);
			this.CurrentOptionList.TabIndex = 36;
			this.CurrentOptionList.SelectedItemChanged += new System.EventHandler(this.CurrentOptionList_SelectedItemChanged);
			// 
			// AcquirableOptionList
			// 
			this.AcquirableOptionList.AutoScroll = true;
			this.AcquirableOptionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AcquirableOptionList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(19)))), ((int)(((byte)(5)))));
			this.AcquirableOptionList.Location = new System.Drawing.Point(357, 28);
			this.AcquirableOptionList.Name = "AcquirableOptionList";
			this.AcquirableOptionList.Size = new System.Drawing.Size(313, 247);
			this.AcquirableOptionList.TabIndex = 37;
			// 
			// ToolTip
			// 
			this.ToolTip.AutoPopDelay = 5000;
			this.ToolTip.InitialDelay = 500;
			this.ToolTip.IsBalloon = true;
			this.ToolTip.ReshowDelay = 0;
			// 
			// IntensionResetConfirmPanel
			// 
			this.Controls.Add(this.AcquirableOptionList);
			this.Controls.Add(this.CurrentOptionList);
			this.Controls.Add(this.CurrentOptionListTitle);
			this.Controls.Add(this.AcquirableOptionListTitle);
			this.Name = "IntensionResetConfirmPanel";
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.MyWeapon_Title, 0);
			this.Controls.SetChildIndex(this.MyWeapon_Icon, 0);
			this.Controls.SetChildIndex(this.MyWeapon_Name, 0);
			this.Controls.SetChildIndex(this.WarningPreview, 0);
			this.Controls.SetChildIndex(this.MoneyCostPreview, 0);
			this.Controls.SetChildIndex(this.FixedIngredientPreview, 0);
			this.Controls.SetChildIndex(this.SubIngredientTitle, 0);
			this.Controls.SetChildIndex(this.SubIngredientPreview, 0);
			this.Controls.SetChildIndex(this.AcquirableOptionListTitle, 0);
			this.Controls.SetChildIndex(this.CurrentOptionListTitle, 0);
			this.Controls.SetChildIndex(this.CurrentOptionList, 0);
			this.Controls.SetChildIndex(this.AcquirableOptionList, 0);
			((System.ComponentModel.ISupportInitialize)(this.MyWeapon_Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.Label AcquirableOptionListTitle;
		private System.Windows.Forms.Label CurrentOptionListTitle;
		private OptionList CurrentOptionList;
		private OptionList AcquirableOptionList;
		private System.Windows.Forms.ToolTip ToolTip;
	}
}
