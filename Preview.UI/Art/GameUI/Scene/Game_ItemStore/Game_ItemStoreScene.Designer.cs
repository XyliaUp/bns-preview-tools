using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	partial class Game_ItemStoreScene
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_ItemStoreScene));
			this.ucBtnExt1 = new HZH_Controls.Controls.UCBtnExt();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.JobSelector = new HZH_Controls.Controls.UCCombox();
			this.ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ListPreview
			// 
			this.ListPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ListPreview.Location = new System.Drawing.Point(399, 45);
			this.ListPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ListPreview.Size = new System.Drawing.Size(401, 570);
			// 
			// TreeView
			// 
			this.TreeView.LineColor = System.Drawing.Color.Black;
			this.TreeView.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.TreeView.Size = new System.Drawing.Size(399, 615);
			// 
			// ucBtnExt1
			// 
			this.ucBtnExt1.BtnBackColor = System.Drawing.Color.Empty;
			this.ucBtnExt1.BtnFont = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ucBtnExt1.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.ucBtnExt1.Text = "出售NPC";
			this.ucBtnExt1.ConerRadius = 8;
			this.ucBtnExt1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnExt1.EnabledMouseEffect = false;
			this.ucBtnExt1.FillColor = System.Drawing.Color.White;
			this.ucBtnExt1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.ucBtnExt1.IsRadius = true;
			this.ucBtnExt1.IsShowRect = true;
			this.ucBtnExt1.IsShowTips = false;
			this.ucBtnExt1.Location = new System.Drawing.Point(198, 6);
			this.ucBtnExt1.Margin = new System.Windows.Forms.Padding(0);
			this.ucBtnExt1.Name = "ucBtnExt1";
			this.ucBtnExt1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.ucBtnExt1.RectWidth = 1;
			this.ucBtnExt1.Size = new System.Drawing.Size(82, 33);
			this.ucBtnExt1.TabIndex = 118;
			this.ucBtnExt1.TabStop = false;
			this.ucBtnExt1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
			this.ucBtnExt1.TipsText = "";
			this.ucBtnExt1.Visible = false;
			this.ucBtnExt1.Click += new System.EventHandler(this.ucBtnExt1_BtnClick);
			// 
			// ControlPanel
			// 
			this.ControlPanel.Controls.Add(this.JobSelector);
			this.ControlPanel.Controls.Add(this.ucBtnExt1);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ControlPanel.Location = new System.Drawing.Point(399, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(401, 44);
			this.ControlPanel.TabIndex = 119;
			// 
			// JobSelector
			// 
			this.JobSelector.BackColor = System.Drawing.Color.Transparent;
			this.JobSelector.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.JobSelector.ConerRadius = 10;
			this.JobSelector.DropPanelHeight = -1;
			this.JobSelector.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.JobSelector.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.JobSelector.IsRadius = true;
			this.JobSelector.IsShowRect = true;
			this.JobSelector.ItemWidth = 40;
			this.JobSelector.Location = new System.Drawing.Point(296, 6);
			this.JobSelector.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.JobSelector.Name = "JobSelector";
			this.JobSelector.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.JobSelector.RectWidth = 1;
			this.JobSelector.SelectedIndex = -1;
			this.JobSelector.Size = new System.Drawing.Size(97, 33);
			this.JobSelector.Source.Add("剑士");
			this.JobSelector.TabIndex = 119;
			this.JobSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.JobSelector.TextValue = "全部";
			this.JobSelector.TriangleColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.JobSelector.SelectedChangedEvent += new System.EventHandler(this.JobSelector_SelectedChangedEvent);
			// 
			// Game_ItemStoreScene
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(26)))), ((int)(((byte)(35)))));
			this.ClientSize = new System.Drawing.Size(800, 615);
			this.Controls.Add(this.ControlPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "Game_ItemStoreScene";
			this.Text = "Store2";
			this.Load += new System.EventHandler(this.Store2Scene_Load);
			this.SizeChanged += new System.EventHandler(this.Store2Scene_SizeChanged);
			this.Controls.SetChildIndex(this.TreeView, 0);
			this.Controls.SetChildIndex(this.ListPreview, 0);
			this.Controls.SetChildIndex(this.ControlPanel, 0);
			this.ControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private HZH_Controls.Controls.UCBtnExt ucBtnExt9;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt1;
		private System.Windows.Forms.Panel ControlPanel;
		private HZH_Controls.Controls.UCCombox JobSelector;
	}
}