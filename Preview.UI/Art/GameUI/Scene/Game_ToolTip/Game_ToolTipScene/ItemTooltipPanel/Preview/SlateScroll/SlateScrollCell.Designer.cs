using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class SlateScrollCell
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlateScrollCell));
			lbl_AbilityInfo = new Label();
			Panel_ItemInfo = new ItemShowCell();
			SuspendLayout();
			// 
			// lbl_AbilityInfo
			// 
			lbl_AbilityInfo.AutoSize = true;
			lbl_AbilityInfo.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			lbl_AbilityInfo.ForeColor = Color.FromArgb(88, 255, 119);
			lbl_AbilityInfo.Location = new Point(268, 4);
			lbl_AbilityInfo.Margin = new Padding(4, 0, 4, 0);
			lbl_AbilityInfo.Name = "lbl_AbilityInfo";
			lbl_AbilityInfo.Size = new Size(52, 20);
			lbl_AbilityInfo.TabIndex = 7;
			lbl_AbilityInfo.Text = "Ability";
			// 
			// Panel_ItemInfo
			// 
			Panel_ItemInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			Panel_ItemInfo.BackColor = Color.Transparent;
			Panel_ItemInfo.ForeColor = Color.Black;
			Panel_ItemInfo.HeightDiff = 0;
			Panel_ItemInfo.ItemData = null;
			Panel_ItemInfo.ItemGrade = 7;
			Panel_ItemInfo.ItemIcon = (Bitmap)resources.GetObject("Panel_ItemInfo.ItemIcon");
			Panel_ItemInfo.ItemName = "SlateName";
			Panel_ItemInfo.Location = new Point(1, 1);
			Panel_ItemInfo.Margin = new Padding(5, 6, 5, 6);
			Panel_ItemInfo.Name = "Panel_ItemInfo";
			Panel_ItemInfo.ReserveIconSpace = false;
			Panel_ItemInfo.Scale = 25;
			Panel_ItemInfo.Size = new Size(115, 34);
			Panel_ItemInfo.TabIndex = 5;
			Panel_ItemInfo.TagImage = null;
			// 
			// SlateScrollCell
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Transparent;
			Controls.Add(lbl_AbilityInfo);
			Controls.Add(Panel_ItemInfo);
			Margin = new Padding(4);
			Name = "SlateScrollCell";
			Size = new Size(345, 34);
			Load += RewardCell_Load;
			SizeChanged += SlateScrollCell_SizeChanged;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private ItemShowCell Panel_ItemInfo;
		private System.Windows.Forms.Label lbl_AbilityInfo;
	}
}
