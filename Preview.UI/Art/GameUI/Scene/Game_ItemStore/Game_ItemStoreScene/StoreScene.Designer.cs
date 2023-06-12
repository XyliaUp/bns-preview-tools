using Xylia.Preview.GameUI.Controls;

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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("商店");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoreScene));
			this.TreeView = new Xylia.Windows.Controls.TreeView();
			this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ModifyFilterRule = new System.Windows.Forms.ToolStripMenuItem();
			this.CancelFilter = new System.Windows.Forms.ToolStripMenuItem();
			this.ListPreview = new Xylia.Preview.GameUI.Controls.ListPreview();
			this.MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// TreeView
			// 
			this.TreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(251)))), ((int)(((byte)(244)))));
			this.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TreeView.ContextMenuStrip = this.MenuStrip;
			this.TreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.TreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
			this.TreeView.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.TreeView.HotTracking = true;
			this.TreeView.Indent = 20;
			this.TreeView.ItemHeight = 30;
			this.TreeView.Location = new System.Drawing.Point(0, 0);
			this.TreeView.Margin = new System.Windows.Forms.Padding(4);
			this.TreeView.Name = "TreeView";
			treeNode1.Name = "RootNode";
			treeNode1.Text = "商店";
			this.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.TreeView.ShowLines = false;
			this.TreeView.Size = new System.Drawing.Size(401, 803);
			this.TreeView.TabIndex = 4;
			this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
			// 
			// MenuStrip
			// 
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyFilterRule,
            this.CancelFilter});
			this.MenuStrip.Name = "Menu";
			this.MenuStrip.Size = new System.Drawing.Size(149, 48);
			// 
			// ModifyFilterRule
			// 
			this.ModifyFilterRule.Image = ((System.Drawing.Image)(resources.GetObject("ModifyFilterRule.Image")));
			this.ModifyFilterRule.Name = "ModifyFilterRule";
			this.ModifyFilterRule.Size = new System.Drawing.Size(148, 22);
			this.ModifyFilterRule.Text = "修改筛选条件";
			this.ModifyFilterRule.Click += new System.EventHandler(this.ModifyFilterRule_Click);
			// 
			// CancelFilter
			// 
			this.CancelFilter.Image = ((System.Drawing.Image)(resources.GetObject("CancelFilter.Image")));
			this.CancelFilter.Name = "CancelFilter";
			this.CancelFilter.Size = new System.Drawing.Size(148, 22);
			this.CancelFilter.Text = "取消筛选";
			this.CancelFilter.Click += new System.EventHandler(this.CancelFilter_Click);
			// 
			// ListPreview
			// 
			this.ListPreview.MaxCellNum = 0;
			this.ListPreview.AutoScroll = true;
			this.ListPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(26)))), ((int)(((byte)(35)))));
			this.ListPreview.Dock = System.Windows.Forms.DockStyle.Right;
			this.ListPreview.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ListPreview.Location = new System.Drawing.Point(409, 0);
			this.ListPreview.Margin = new System.Windows.Forms.Padding(4);
			this.ListPreview.Name = "ListPreview";
			this.ListPreview.Size = new System.Drawing.Size(489, 803);
			this.ListPreview.TabIndex = 0;
			// 
			// StoreScene
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(26)))), ((int)(((byte)(35)))));
			this.ClientSize = new System.Drawing.Size(898, 803);
			this.Controls.Add(this.ListPreview);
			this.Controls.Add(this.TreeView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "StoreScene";
			this.Text = "Store";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Store2Frm_FormClosing);
			this.Load += new System.EventHandler(this.Store2Frm_Load);
			this.SizeChanged += new System.EventHandler(this.Frm_SizeChanged);
			this.MenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public ListPreview ListPreview;
		private System.Windows.Forms.ContextMenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ModifyFilterRule;
		private System.Windows.Forms.ToolStripMenuItem CancelFilter;
		public Windows.Controls.TreeView TreeView;
	}
}