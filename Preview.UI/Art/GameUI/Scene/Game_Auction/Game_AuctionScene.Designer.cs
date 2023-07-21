using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_Auction
{
	partial class Game_AuctionScene
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_AuctionScene));
			TreeView = new Windows.Controls.TreeView();
			ItemList = new ListPreview();
			loader = new PictureBox();
			panel1 = new Panel();
			chk_compare = new CheckBox();
			checkBox1 = new CheckBox();
			chk_Auctionable = new CheckBox();
			panel2 = new Panel();
			ItemPreview_Search = new HZH_Controls.Controls.UCTextBoxEx();
			ItemList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)loader).BeginInit();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			// 
			// TreeView
			// 
			TreeView.BackColor = Color.White;
			TreeView.BorderStyle = BorderStyle.None;
			resources.ApplyResources(TreeView, "TreeView");
			TreeView.DrawMode = TreeViewDrawMode.OwnerDrawAll;
			TreeView.HotTracking = true;
			TreeView.ItemHeight = 30;
			TreeView.Name = "TreeView";
			TreeView.ShowLines = false;
			TreeView.AfterSelect += LoadData;
			// 
			// ItemList
			// 
			resources.ApplyResources(ItemList, "ItemList");
			ItemList.BackColor = Color.FromArgb(20, 26, 35);
			ItemList.Controls.Add(loader);
			ItemList.ForeColor = Color.Blue;
			ItemList.Name = "ItemList";
			// 
			// loader
			// 
			resources.ApplyResources(loader, "loader");
			loader.Name = "loader";
			loader.TabStop = false;
			// 
			// panel1
			// 
			panel1.Controls.Add(chk_compare);
			panel1.Controls.Add(checkBox1);
			panel1.Controls.Add(chk_Auctionable);
			panel1.Controls.Add(ItemList);
			resources.ApplyResources(panel1, "panel1");
			panel1.Name = "panel1";
			// 
			// chk_compare
			// 
			resources.ApplyResources(chk_compare, "chk_compare");
			chk_compare.Name = "chk_compare";
			chk_compare.UseVisualStyleBackColor = true;
			chk_compare.CheckedChanged += chk_compare_CheckedChanged;
			// 
			// checkBox1
			// 
			resources.ApplyResources(checkBox1, "checkBox1");
			checkBox1.Name = "checkBox1";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += LoadData;
			// 
			// chk_Auctionable
			// 
			resources.ApplyResources(chk_Auctionable, "chk_Auctionable");
			chk_Auctionable.Name = "chk_Auctionable";
			chk_Auctionable.UseVisualStyleBackColor = true;
			chk_Auctionable.CheckedChanged += LoadData;
			// 
			// panel2
			// 
			panel2.Controls.Add(ItemPreview_Search);
			panel2.Controls.Add(TreeView);
			resources.ApplyResources(panel2, "panel2");
			panel2.Name = "panel2";
			// 
			// ItemPreview_Search
			// 
			ItemPreview_Search.BackColor = Color.Transparent;
			ItemPreview_Search.ConerRadius = 5;
			ItemPreview_Search.Cursor = Cursors.IBeam;
			ItemPreview_Search.DecLength = 2;
			ItemPreview_Search.FillColor = Color.Empty;
			ItemPreview_Search.FocusBorderColor = Color.FromArgb(255, 77, 59);
			resources.ApplyResources(ItemPreview_Search, "ItemPreview_Search");
			ItemPreview_Search.InputText = "";
			ItemPreview_Search.InputType = HZH_Controls.TextInputType.NotControl;
			ItemPreview_Search.IsFocusColor = true;
			ItemPreview_Search.IsRadius = true;
			ItemPreview_Search.IsShowClearBtn = true;
			ItemPreview_Search.IsShowKeyboard = false;
			ItemPreview_Search.IsShowRect = true;
			ItemPreview_Search.IsShowSearchBtn = true;
			ItemPreview_Search.KeyBoardType = HZH_Controls.Controls.KeyBoardType.Null;
			ItemPreview_Search.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
			ItemPreview_Search.MinValue = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
			ItemPreview_Search.Name = "ItemPreview_Search";
			ItemPreview_Search.PromptColor = Color.Gray;
			ItemPreview_Search.PromptFont = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
			ItemPreview_Search.PromptText = "";
			ItemPreview_Search.RectColor = Color.FromArgb(220, 220, 220);
			ItemPreview_Search.RectWidth = 1;
			ItemPreview_Search.RegexPattern = "";
			ItemPreview_Search.SearchClick += LoadData;
			// 
			// Game_AuctionScene
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panel2);
			Controls.Add(panel1);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			KeyPreview = true;
			MaximizeBox = false;
			Name = "Game_AuctionScene";
			KeyDown += Game_AuctionScene_KeyDown;
			ItemList.ResumeLayout(false);
			ItemList.PerformLayout();
			((System.ComponentModel.ISupportInitialize)loader).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Windows.Controls.TreeView TreeView;
		private ListPreview ItemList;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private HZH_Controls.Controls.UCTextBoxEx ItemPreview_Search;
		private System.Windows.Forms.CheckBox chk_Auctionable;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox chk_compare;
		private PictureBox loader;
	}
}