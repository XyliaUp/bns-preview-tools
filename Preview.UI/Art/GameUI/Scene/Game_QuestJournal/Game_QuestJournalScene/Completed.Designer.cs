using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class Completed
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Completed));
			TreeView = new Windows.Controls.TreeView();
			contentPanel1 = new ContentPanel();
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
			TreeView.AfterSelect += TreeView2_AfterSelect_1;
			// 
			// contentPanel1
			// 
			resources.ApplyResources(contentPanel1, "contentPanel1");
			contentPanel1.BackColor = Color.Transparent;
			contentPanel1.ForeColor = Color.Black;
			contentPanel1.Name = "contentPanel1";
			// 
			// Completed
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			Controls.Add(contentPanel1);
			Controls.Add(TreeView);
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			Name = "Completed";
			Title = "前世红尘";
			SizeChanged += Completed_SizeChanged;
			ResumeLayout(false);
		}

		#endregion
		private Xylia.Windows.Controls.TreeView TreeView;
		private ContentPanel contentPanel1;
	}
}