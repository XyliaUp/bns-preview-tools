using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class DescPanel
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
			this.Content = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// Content
			// 
			this.Content.BackColor = System.Drawing.Color.Transparent;
			this.Content.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Content.ForeColor = System.Drawing.Color.White;
			this.Content.Location = new System.Drawing.Point(26, 32);
			this.Content.Margin = new System.Windows.Forms.Padding(2, 7, 2, 7);
			this.Content.Name = "Content";
			this.Content.TabIndex = 8;
			this.Content.Text = "PanelContent";
			// 
			// DescPanel
			// 
			//this.AutoSize = false;
			this.Controls.Add(this.Content);
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "DescPanel";
			this.Size = new System.Drawing.Size(160, 64);
			this.Title = "内容";
			this.Controls.SetChildIndex(this.Content, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ContentPanel Content;
	}
}
