using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class SetItemTooltip
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetItemTooltip));
			lbl_Title = new Label();
			itemShowCell2 = new ItemShowCell();
			GemPreview = new GemPreview();
			JobStyleSelect = new Preview.JobStyleSelect();
			SetItemEffect_Title = new ContentPanel();
			SuspendLayout();
			// 
			// lbl_Title
			// 
			lbl_Title.AutoSize = true;
			lbl_Title.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lbl_Title.ForeColor = Color.White;
			lbl_Title.Location = new Point(4, 0);
			lbl_Title.Margin = new Padding(4, 0, 4, 0);
			lbl_Title.Name = "lbl_Title";
			lbl_Title.Size = new Size(114, 21);
			lbl_Title.TabIndex = 2;
			lbl_Title.Text = "套装名称 (0/0)";
			// 
			// itemShowCell2
			// 
			itemShowCell2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			itemShowCell2.BackColor = Color.Transparent;
			itemShowCell2.ForeColor = Color.Black;
			itemShowCell2.HeightDiff = 0;
			itemShowCell2.ItemData = null;
			itemShowCell2.ItemGrade = 7;
			itemShowCell2.ItemIcon = (Bitmap)resources.GetObject("itemShowCell2.ItemIcon");
			itemShowCell2.ItemName = "ItemName";
			itemShowCell2.Location = new Point(8, 34);
			itemShowCell2.Margin = new Padding(5, 6, 5, 6);
			itemShowCell2.Name = "itemShowCell2";
			itemShowCell2.ReserveIconSpace = true;
			itemShowCell2.Scale = 32;
			itemShowCell2.Size = new Size(146, 33);
			itemShowCell2.TabIndex = 10;
			itemShowCell2.TagImage = null;
			itemShowCell2.Visible = false;
			// 
			// GemPreview
			// 
			GemPreview.AutoSize = true;
			GemPreview.BackColor = Color.Transparent;
			GemPreview.Location = new Point(7, 35);
			GemPreview.Margin = new Padding(5, 6, 5, 6);
			GemPreview.Meta1 = null;
			GemPreview.Meta2 = null;
			GemPreview.Meta3 = null;
			GemPreview.Meta4 = null;
			GemPreview.Meta5 = null;
			GemPreview.Meta6 = null;
			GemPreview.Meta7 = null;
			GemPreview.Meta8 = null;
			GemPreview.Name = "GemPreview";
			GemPreview.PublicGrade = 7;
			GemPreview.Size = new Size(334, 214);
			GemPreview.TabIndex = 9;
			// 
			// JobStyleSelect
			// 
			JobStyleSelect.AutoSize = true;
			JobStyleSelect.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			JobStyleSelect.BackColor = Color.Transparent;
			JobStyleSelect.Location = new Point(296, 244);
			JobStyleSelect.Name = "JobStyleSelect";
			JobStyleSelect.Size = new Size(73, 34);
			JobStyleSelect.TabIndex = 11;
			JobStyleSelect.Visible = false;
			// 
			// SetItemEffect_Title
			// 
			SetItemEffect_Title.BackColor = Color.Transparent;
			SetItemEffect_Title.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
			SetItemEffect_Title.ForeColor = Color.White;
			SetItemEffect_Title.Location = new Point(4, 257);
			SetItemEffect_Title.Margin = new Padding(2, 5, 2, 5);
			SetItemEffect_Title.Name = "SetItemEffect_Title";
			SetItemEffect_Title.TabIndex = 12;
			SetItemEffect_Title.Text = "SetItemEffect.Title";
			// 
			// SetItemPreview
			// 
			AutoSize = true;
			BackColor = Color.Transparent;
			Controls.Add(SetItemEffect_Title);
			Controls.Add(JobStyleSelect);
			Controls.Add(itemShowCell2);
			Controls.Add(lbl_Title);
			Controls.Add(GemPreview);
			Margin = new Padding(4);
			Name = "SetItemPreview";
			Size = new Size(372, 281);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label lbl_Title;
		private GemPreview GemPreview;
		private ItemShowCell itemShowCell2; 
		private ContentPanel SetItemEffect_Title;
		private Preview.JobStyleSelect JobStyleSelect;
	}
}
