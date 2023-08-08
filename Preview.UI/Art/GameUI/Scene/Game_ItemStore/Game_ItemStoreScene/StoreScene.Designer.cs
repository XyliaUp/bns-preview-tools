using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	partial class StoreScene
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
			components = new System.ComponentModel.Container();
			TreeNode treeNode1 = new TreeNode("商店");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoreScene));
			TreeView = new Windows.Controls.TreeView();
			MenuStrip = new ContextMenuStrip(components);
			ModifyFilterRule = new ToolStripMenuItem();
			CancelFilter = new ToolStripMenuItem();
			ListPreview = new ListPreview();
			MenuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// TreeView
			// 
			TreeView.BackColor = Color.FromArgb(227, 251, 244);
			TreeView.BorderStyle = BorderStyle.None;
			TreeView.ContextMenuStrip = MenuStrip;
			TreeView.Dock = DockStyle.Left;
			TreeView.DrawMode = TreeViewDrawMode.OwnerDrawAll;
			TreeView.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
			TreeView.HotTracking = true;
			TreeView.Indent = 20;
			TreeView.ItemHeight = 30;
			TreeView.Location = new Point(0, 0);
			TreeView.Margin = new Padding(4);
			TreeView.Name = "TreeView";
			treeNode1.Name = "RootNode";
			treeNode1.Text = "商店";
			TreeView.Nodes.AddRange(new TreeNode[] { treeNode1 });
			TreeView.ShowLines = false;
			TreeView.ShowCount = false;
			TreeView.Size = new Size(401, 803);
			TreeView.TabIndex = 4;
			TreeView.AfterSelect += TreeView_AfterSelect;
			// 
			// MenuStrip
			// 
			MenuStrip.Items.AddRange(new ToolStripItem[] { ModifyFilterRule, CancelFilter });
			MenuStrip.Name = "Menu";
			MenuStrip.Size = new Size(149, 48);
			// 
			// ModifyFilterRule
			// 
			ModifyFilterRule.Image = (Image)resources.GetObject("ModifyFilterRule.Image");
			ModifyFilterRule.Name = "ModifyFilterRule";
			ModifyFilterRule.Size = new Size(148, 22);
			ModifyFilterRule.Text = "修改筛选条件";
			ModifyFilterRule.Click += ModifyFilterRule_Click;
			// 
			// CancelFilter
			// 
			CancelFilter.Image = (Image)resources.GetObject("CancelFilter.Image");
			CancelFilter.Name = "CancelFilter";
			CancelFilter.Size = new Size(148, 22);
			CancelFilter.Text = "取消筛选";
			CancelFilter.Click += CancelFilter_Click;
			// 
			// ListPreview
			// 
			ListPreview.AutoScroll = true;
			ListPreview.BackColor = Color.FromArgb(20, 26, 35);
			ListPreview.Dock = DockStyle.Right;
			ListPreview.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			ListPreview.ForeColor = Color.White;
			ListPreview.Location = new Point(409, 0);
			ListPreview.Margin = new Padding(4);
			ListPreview.Name = "ListPreview";
			ListPreview.Size = new Size(489, 803);
			ListPreview.TabIndex = 0;
			// 
			// StoreScene
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(20, 26, 35);
			ClientSize = new Size(898, 803);
			Controls.Add(ListPreview);
			Controls.Add(TreeView);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4);
			Name = "StoreScene";
			Text = "Store";
			FormClosing += Frm_FormClosing;
			Load += Frm_Load;
			SizeChanged += Frm_SizeChanged;
			MenuStrip.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		public ListPreview ListPreview;
		private System.Windows.Forms.ContextMenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ModifyFilterRule;
		private System.Windows.Forms.ToolStripMenuItem CancelFilter;
		public Windows.Controls.TreeView TreeView;
	}
}