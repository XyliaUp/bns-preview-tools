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
			ucBtnExt1 = new HZH_Controls.Controls.UCBtnExt();
			ControlPanel = new Panel();
			JobSelector = new HZH_Controls.Controls.UCCombox();
			ControlPanel.SuspendLayout();
			SuspendLayout();
			// 
			// ListPreview
			// 
			resources.ApplyResources(ListPreview, "ListPreview");
			// 
			// TreeView
			// 
			TreeView.LineColor = Color.Black;
			resources.ApplyResources(TreeView, "TreeView");
			// 
			// ucBtnExt1
			// 
			ucBtnExt1.BtnBackColor = Color.Empty;
			ucBtnExt1.BtnFont = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt1.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt1.ConerRadius = 8;
			ucBtnExt1.Cursor = Cursors.Hand;
			ucBtnExt1.DialogResult = DialogResult.None;
			ucBtnExt1.EnabledMouseEffect = false;
			ucBtnExt1.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt1, "ucBtnExt1");
			ucBtnExt1.IsRadius = true;
			ucBtnExt1.IsShowRect = true;
			ucBtnExt1.IsShowTips = false;
			ucBtnExt1.Name = "ucBtnExt1";
			ucBtnExt1.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt1.RectWidth = 1;
			ucBtnExt1.TabStop = false;
			ucBtnExt1.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt1.TipsText = "";
			ucBtnExt1.Click += ucBtnExt1_BtnClick;
			// 
			// ControlPanel
			// 
			ControlPanel.Controls.Add(JobSelector);
			ControlPanel.Controls.Add(ucBtnExt1);
			resources.ApplyResources(ControlPanel, "ControlPanel");
			ControlPanel.Name = "ControlPanel";
			// 
			// JobSelector
			// 
			JobSelector.BackColor = Color.Transparent;
			JobSelector.BoxStyle = ComboBoxStyle.DropDownList;
			JobSelector.ConerRadius = 10;
			JobSelector.DropPanelHeight = -1;
			resources.ApplyResources(JobSelector, "JobSelector");
			JobSelector.ForeColor = Color.FromArgb(192, 192, 255);
			JobSelector.IsRadius = true;
			JobSelector.IsShowRect = true;
			JobSelector.ItemWidth = 40;
			JobSelector.Name = "JobSelector";
			JobSelector.RectColor = Color.FromArgb(240, 240, 240);
			JobSelector.RectWidth = 1;
			JobSelector.SelectedIndex = -1;
			JobSelector.Source.Add("剑士");
			JobSelector.TextAlign = HorizontalAlignment.Center;
			JobSelector.TextValue = "全部";
			JobSelector.TriangleColor = Color.FromArgb(192, 192, 255);
			JobSelector.SelectedChangedEvent += JobSelector_SelectedChangedEvent;
			// 
			// Game_ItemStoreScene
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(20, 26, 35);
			Controls.Add(ControlPanel);
			Name = "Game_ItemStoreScene";
			Load += Store2Scene_SizeChanged;
			SizeChanged += Store2Scene_SizeChanged;
			Controls.SetChildIndex(TreeView, 0);
			Controls.SetChildIndex(ListPreview, 0);
			Controls.SetChildIndex(ControlPanel, 0);
			ControlPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private HZH_Controls.Controls.UCBtnExt ucBtnExt9;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt1;
		private System.Windows.Forms.Panel ControlPanel;
		private HZH_Controls.Controls.UCCombox JobSelector;
	}
}