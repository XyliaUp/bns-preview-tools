using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Searcher
{
	partial class SearcherResult
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearcherResult));
			ListPreview = new ListPreview();
			Menu = new ContextMenuStrip(components);
			MenuItem_Filter = new ToolStripMenuItem();
			Menu.SuspendLayout();
			SuspendLayout();
			// 
			// ListPreview
			// 
			resources.ApplyResources(ListPreview, "ListPreview");
			ListPreview.BackColor = Color.Transparent;
			ListPreview.ContextMenuStrip = Menu;
			ListPreview.ForeColor = Color.White;
			ListPreview.Name = "ListPreview";
			ListPreview.DrawItem += ListPreview_DrawItem;
			ListPreview.SelectItem += ListPreview_SelectItem;
			// 
			// Menu
			// 
			resources.ApplyResources(Menu, "Menu");
			Menu.Items.AddRange(new ToolStripItem[] { MenuItem_Filter });
			Menu.Name = "Menu";
			// 
			// MenuItem_Filter
			// 
			resources.ApplyResources(MenuItem_Filter, "MenuItem_Filter");
			MenuItem_Filter.Name = "MenuItem_Filter";
			MenuItem_Filter.Click += MenuItem_Filter_Click;
			// 
			// SearcherResult
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Black;
			ForeColor = Color.White;
			Controls.Add(ListPreview);
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			MaximizeBox = false;
			Name = "SearcherResult";
			Shown += SearcherResult_Shown;
			Menu.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		public ListPreview ListPreview;
		private ContextMenuStrip Menu;
		private ToolStripMenuItem MenuItem_Filter;
	}
}