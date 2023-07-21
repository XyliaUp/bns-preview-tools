using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	partial class EquipmentGuidePage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquipmentGuidePage));
			this.MyWeapon_Title = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.MyWeapon_Name = new Xylia.Preview.UI.Custom.Controls.ItemNamePanel();
			this.MyWeapon_Icon = new Xylia.Preview.UI.Custom.Controls.ItemIconCell();
			this.WarningPreview = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.WarningPreview();
			this.FixedIngredientTitle = new System.Windows.Forms.Label();
			this.SubIngredientTitle = new System.Windows.Forms.Label();
			this.feedItemIconCell1 = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.FeedItemIconCell();
			this.FixedIngredientPreview = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.FixedIngredientPreview();
			this.itemIconCell4 = new Xylia.Preview.UI.Custom.Controls.ItemIconCell();
			this.itemIconCell3 = new Xylia.Preview.UI.Custom.Controls.ItemIconCell();
			this.itemIconCell2 = new Xylia.Preview.UI.Custom.Controls.ItemIconCell();
			this.MoneyCostPreview = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.MoneyCostPreview();
			this.SubIngredientPreview = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.SubIngredientPreview();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MyWeapon_Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.feedItemIconCell1)).BeginInit();
			this.FixedIngredientPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell2)).BeginInit();
			this.SubIngredientPreview.SuspendLayout();
			this.SuspendLayout();
			// 
			// MyWeapon_Title
			// 
			this.MyWeapon_Title.AutoSize = true;
			this.MyWeapon_Title.BackColor = System.Drawing.Color.Transparent;
			this.MyWeapon_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.MyWeapon_Title.ForeColor = System.Drawing.Color.White;
			this.MyWeapon_Title.Location = new System.Drawing.Point(42, 17);
			this.MyWeapon_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.MyWeapon_Title.Name = "MyWeapon_Title";
			this.MyWeapon_Title.Size = new System.Drawing.Size(74, 21);
			this.MyWeapon_Title.TabIndex = 11;
			this.MyWeapon_Title.Text = "当前装备";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(28, 59);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(96, 72);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 22;
			this.pictureBox2.TabStop = false;
			// 
			// MyWeapon_Name
			// 
			this.MyWeapon_Name.BackColor = System.Drawing.Color.Transparent;
			this.MyWeapon_Name.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.MyWeapon_Name.ItemGrade = ((byte)(7));
			this.MyWeapon_Name.Location = new System.Drawing.Point(42, 136);
			this.MyWeapon_Name.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.MyWeapon_Name.Name = "MyWeapon_Name";
			this.MyWeapon_Name.Size = new System.Drawing.Size(63, 19);
			this.MyWeapon_Name.TabIndex = 23;
			this.MyWeapon_Name.TagImage = null;
			this.MyWeapon_Name.Text = "物品名称";
			// 
			// MyWeapon_Icon
			// 
			this.MyWeapon_Icon.BackColor = System.Drawing.Color.Transparent;
			this.MyWeapon_Icon.ForeColor = System.Drawing.Color.Black;
			this.MyWeapon_Icon.FrameImage = null;
			this.MyWeapon_Icon.FrameType = true;
			this.MyWeapon_Icon.Location = new System.Drawing.Point(44, 62);
			this.MyWeapon_Icon.Margin = new System.Windows.Forms.Padding(4);
			this.MyWeapon_Icon.Name = "MyWeapon_Icon";
			this.MyWeapon_Icon.Scale = 64;
			this.MyWeapon_Icon.ShowFrameImage = true;
			this.MyWeapon_Icon.ShowStackCount = false;
			this.MyWeapon_Icon.ShowStackCountOnlyOne = true;
			this.MyWeapon_Icon.Size = new System.Drawing.Size(64, 64);
			this.MyWeapon_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.MyWeapon_Icon.StackCount = 1;
			this.MyWeapon_Icon.TabIndex = 21;
			this.MyWeapon_Icon.TabStop = false;
			// 
			// WarningPreview
			// 
			this.WarningPreview.AutoSize = true;
			this.WarningPreview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.WarningPreview.BackColor = System.Drawing.Color.Transparent;
			this.WarningPreview.Location = new System.Drawing.Point(274, 651);
			this.WarningPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.WarningPreview.Name = "WarningPreview";
			this.WarningPreview.Size = new System.Drawing.Size(103, 30);
			this.WarningPreview.TabIndex = 20;
			this.WarningPreview.Text = "提示消息";
			this.WarningPreview.Visible = false;
			this.WarningPreview.TextChanged += new System.EventHandler(this.WarningPreview_TextChanged);
			// 
			// FixedIngredientTitle
			// 
			this.FixedIngredientTitle.AutoSize = true;
			this.FixedIngredientTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FixedIngredientTitle.ForeColor = System.Drawing.Color.White;
			this.FixedIngredientTitle.Location = new System.Drawing.Point(66, 442);
			this.FixedIngredientTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FixedIngredientTitle.Name = "FixedIngredientTitle";
			this.FixedIngredientTitle.Size = new System.Drawing.Size(42, 21);
			this.FixedIngredientTitle.TabIndex = 30;
			this.FixedIngredientTitle.Text = "材料";
			// 
			// SubIngredientTitle
			// 
			this.SubIngredientTitle.AutoSize = true;
			this.SubIngredientTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SubIngredientTitle.ForeColor = System.Drawing.Color.White;
			this.SubIngredientTitle.Location = new System.Drawing.Point(28, 355);
			this.SubIngredientTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SubIngredientTitle.Name = "SubIngredientTitle";
			this.SubIngredientTitle.Size = new System.Drawing.Size(42, 21);
			this.SubIngredientTitle.TabIndex = 29;
			this.SubIngredientTitle.Text = "祭品";
			// 
			// feedItemIconCell1
			// 
			this.feedItemIconCell1.BackColor = System.Drawing.Color.Transparent;
			this.feedItemIconCell1.ForeColor = System.Drawing.Color.Black;
			this.feedItemIconCell1.FrameImage = ((System.Drawing.Bitmap)(resources.GetObject("feedItemIconCell1.FrameImage")));
			this.feedItemIconCell1.FrameType = true;
			this.feedItemIconCell1.Location = new System.Drawing.Point(3, 3);
			this.feedItemIconCell1.Name = "feedItemIconCell1";
			this.feedItemIconCell1.Scale = 82;
			this.feedItemIconCell1.ShowFrameImage = false;
			this.feedItemIconCell1.ShowStackCount = false;
			this.feedItemIconCell1.ShowStackCountOnlyOne = false;
			this.feedItemIconCell1.Size = new System.Drawing.Size(82, 90);
			this.feedItemIconCell1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.feedItemIconCell1.StackCount = 0;
			this.feedItemIconCell1.TabIndex = 26;
			this.feedItemIconCell1.TabStop = false;
			this.feedItemIconCell1.Visible = false;
			// 
			// FixedIngredientPreview
			// 
			this.FixedIngredientPreview.AutoSize = true;
			this.FixedIngredientPreview.BackColor = System.Drawing.Color.Transparent;
			this.FixedIngredientPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.FixedIngredientPreview.Controls.Add(this.itemIconCell4);
			this.FixedIngredientPreview.Controls.Add(this.itemIconCell3);
			this.FixedIngredientPreview.Controls.Add(this.itemIconCell2);
			this.FixedIngredientPreview.Location = new System.Drawing.Point(254, 427);
			this.FixedIngredientPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.FixedIngredientPreview.Name = "FixedIngredientPreview";
			this.FixedIngredientPreview.Size = new System.Drawing.Size(165, 51);
			this.FixedIngredientPreview.TabIndex = 27;
			this.FixedIngredientPreview.DataLoaded += new EmptyHandler(this.FixedIngredientPreview_DataLoaded);
			// 
			// itemIconCell4
			// 
			this.itemIconCell4.BackColor = System.Drawing.Color.Transparent;
			this.itemIconCell4.ForeColor = System.Drawing.Color.Black;
			this.itemIconCell4.FrameImage = null;
			this.itemIconCell4.FrameType = true;
			this.itemIconCell4.Location = new System.Drawing.Point(117, 3);
			this.itemIconCell4.Name = "itemIconCell4";
			this.itemIconCell4.Scale = 45;
			this.itemIconCell4.ShowFrameImage = true;
			this.itemIconCell4.ShowStackCount = false;
			this.itemIconCell4.ShowStackCountOnlyOne = true;
			this.itemIconCell4.Size = new System.Drawing.Size(45, 45);
			this.itemIconCell4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.itemIconCell4.StackCount = 1;
			this.itemIconCell4.TabIndex = 2;
			this.itemIconCell4.TabStop = false;
			this.itemIconCell4.Visible = false;
			// 
			// itemIconCell3
			// 
			this.itemIconCell3.BackColor = System.Drawing.Color.Transparent;
			this.itemIconCell3.ForeColor = System.Drawing.Color.Black;
			this.itemIconCell3.FrameImage = null;
			this.itemIconCell3.FrameType = true;
			this.itemIconCell3.Location = new System.Drawing.Point(59, 3);
			this.itemIconCell3.Name = "itemIconCell3";
			this.itemIconCell3.Scale = 45;
			this.itemIconCell3.ShowFrameImage = true;
			this.itemIconCell3.ShowStackCount = false;
			this.itemIconCell3.ShowStackCountOnlyOne = true;
			this.itemIconCell3.Size = new System.Drawing.Size(45, 45);
			this.itemIconCell3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.itemIconCell3.StackCount = 1;
			this.itemIconCell3.TabIndex = 1;
			this.itemIconCell3.TabStop = false;
			this.itemIconCell3.Visible = false;
			// 
			// itemIconCell2
			// 
			this.itemIconCell2.BackColor = System.Drawing.Color.Transparent;
			this.itemIconCell2.ForeColor = System.Drawing.Color.Black;
			this.itemIconCell2.FrameImage = null;
			this.itemIconCell2.FrameType = true;
			this.itemIconCell2.Location = new System.Drawing.Point(2, 2);
			this.itemIconCell2.Name = "itemIconCell2";
			this.itemIconCell2.Scale = 45;
			this.itemIconCell2.ShowFrameImage = true;
			this.itemIconCell2.ShowStackCount = false;
			this.itemIconCell2.ShowStackCountOnlyOne = true;
			this.itemIconCell2.Size = new System.Drawing.Size(45, 45);
			this.itemIconCell2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.itemIconCell2.StackCount = 1;
			this.itemIconCell2.TabIndex = 0;
			this.itemIconCell2.TabStop = false;
			this.itemIconCell2.Visible = false;
			// 
			// MoneyCostPreview
			// 
			this.MoneyCostPreview.BackColor = System.Drawing.Color.Transparent;
			this.MoneyCostPreview.Location = new System.Drawing.Point(183, 559);
			this.MoneyCostPreview.Margin = new System.Windows.Forms.Padding(4);
			this.MoneyCostPreview.MaximumSize = new System.Drawing.Size(400, 0);
			this.MoneyCostPreview.MoneyCost = 0;
			this.MoneyCostPreview.Name = "MoneyCostPreview";
			this.MoneyCostPreview.Size = new System.Drawing.Size(334, 63);
			this.MoneyCostPreview.TabIndex = 28;
			this.MoneyCostPreview.UseDiscount = true;
			// 
			// SubIngredientPreview
			// 
			this.SubIngredientPreview.AutoSize = true;
			this.SubIngredientPreview.BackColor = System.Drawing.Color.Transparent;
			this.SubIngredientPreview.Controls.Add(this.feedItemIconCell1);
			this.SubIngredientPreview.Location = new System.Drawing.Point(298, 305);
			this.SubIngredientPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.SubIngredientPreview.Name = "SubIngredientPreview";
			this.SubIngredientPreview.Size = new System.Drawing.Size(88, 96);
			this.SubIngredientPreview.TabIndex = 31;
			this.SubIngredientPreview.RecipeChanged += new SubIngredientPreview.RecipeChangedHandle(SubIngredientPreview_RecipeChanged);
			this.SubIngredientPreview.DataLoaded += new EmptyHandler(this.SubIngredientPreview_DataLoaded);
			// 
			// EquipmentGuidePage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.SubIngredientPreview);
			this.Controls.Add(this.FixedIngredientTitle);
			this.Controls.Add(this.SubIngredientTitle);
			this.Controls.Add(this.FixedIngredientPreview);
			this.Controls.Add(this.MoneyCostPreview);
			this.Controls.Add(this.MyWeapon_Name);
			this.Controls.Add(this.MyWeapon_Icon);
			this.Controls.Add(this.WarningPreview);
			this.Controls.Add(this.MyWeapon_Title);
			this.Controls.Add(this.pictureBox2);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "EquipmentGuidePage";
			this.Size = new System.Drawing.Size(681, 702);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MyWeapon_Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.feedItemIconCell1)).EndInit();
			this.FixedIngredientPreview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell2)).EndInit();
			this.SubIngredientPreview.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public WarningPreview WarningPreview;
		private System.Windows.Forms.Label FixedIngredientTitle;
		private FeedItemIconCell feedItemIconCell1;
		private ItemIconCell itemIconCell4;
		private ItemIconCell itemIconCell3;
		private ItemIconCell itemIconCell2;
		protected MoneyCostPreview MoneyCostPreview;
		protected FixedIngredientPreview FixedIngredientPreview;
		protected SubIngredientPreview SubIngredientPreview;
		protected System.Windows.Forms.Label SubIngredientTitle;
		protected System.Windows.Forms.Label MyWeapon_Title;
		protected ItemIconCell MyWeapon_Icon;
		protected System.Windows.Forms.PictureBox pictureBox2;
		protected ItemNamePanel MyWeapon_Name;
	}
}