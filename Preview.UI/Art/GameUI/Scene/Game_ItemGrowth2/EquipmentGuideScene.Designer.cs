
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.ItemGrowth.Scene
{
	partial class EquipmentGuideScene
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
			this.TabControl = new System.Windows.Forms.TabControl();
			this.Page_ItemGrowth2 = new System.Windows.Forms.TabPage();
			this.equipmentGuidePage1 = new Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.EquipmentGuidePage();
			this.TabControl.SuspendLayout();
			this.Page_ItemGrowth2.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.Page_ItemGrowth2);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 0);
			this.TabControl.Margin = new System.Windows.Forms.Padding(4);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(698, 743);
			this.TabControl.TabIndex = 5;
			// 
			// Page_ItemGrowth2
			// 
			this.Page_ItemGrowth2.BackColor = System.Drawing.Color.Black;
			this.Page_ItemGrowth2.Controls.Add(this.equipmentGuidePage1);
			this.Page_ItemGrowth2.Location = new System.Drawing.Point(4, 26);
			this.Page_ItemGrowth2.Margin = new System.Windows.Forms.Padding(4);
			this.Page_ItemGrowth2.Name = "Page_ItemGrowth2";
			this.Page_ItemGrowth2.Padding = new System.Windows.Forms.Padding(4);
			this.Page_ItemGrowth2.Size = new System.Drawing.Size(690, 713);
			this.Page_ItemGrowth2.TabIndex = 0;
			this.Page_ItemGrowth2.Text = "成长";
			// 
			// equipmentGuidePage1
			// 
			this.equipmentGuidePage1.BackColor = System.Drawing.Color.Transparent;
			this.equipmentGuidePage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.equipmentGuidePage1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.equipmentGuidePage1.Location = new System.Drawing.Point(4, 4);
			this.equipmentGuidePage1.Margin = new System.Windows.Forms.Padding(4);
			this.equipmentGuidePage1.MyWeapon = null;
			this.equipmentGuidePage1.Name = "equipmentGuidePage1";
			this.equipmentGuidePage1.Size = new System.Drawing.Size(682, 705);
			this.equipmentGuidePage1.TabIndex = 0;
			this.equipmentGuidePage1.Visible = false;
			// 
			// EquipmentGuideScene
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(698, 743);
			this.Controls.Add(this.TabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "EquipmentGuideScene";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Title = "装备管理";
			this.Shown += new System.EventHandler(this.EquipmentGuideScene_Shown);
			this.TabControl.ResumeLayout(false);
			this.Page_ItemGrowth2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.TabPage Page_ItemGrowth2;
		private EquipmentGuidePage equipmentGuidePage1;
	}
}