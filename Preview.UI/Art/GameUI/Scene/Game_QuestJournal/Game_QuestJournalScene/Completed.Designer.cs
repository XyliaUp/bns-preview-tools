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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.TreeView = new Xylia.Windows.Controls.TreeView();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.BackColor = System.Drawing.Color.White;
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.richTextBox1.Location = new System.Drawing.Point(290, 0);
			this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(487, 550);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			this.richTextBox1.ZoomFactor = 1.2F;
			this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Completed_KeyDown);
			// 
			// TreeView
			// 
			this.TreeView.BackColor = System.Drawing.Color.White;
			this.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.TreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
			this.TreeView.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.TreeView.HotTracking = true;
			this.TreeView.Indent = 20;
			this.TreeView.ItemHeight = 30;
			this.TreeView.Location = new System.Drawing.Point(0, 0);
			this.TreeView.Margin = new System.Windows.Forms.Padding(4);
			this.TreeView.Name = "TreeView";
			this.TreeView.ShowLines = false;
			this.TreeView.Size = new System.Drawing.Size(281, 550);
			this.TreeView.TabIndex = 2;
			this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView2_AfterSelect_1);
			// 
			// Completed
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(778, 550);
			this.Controls.Add(this.TreeView);
			this.Controls.Add(this.richTextBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Completed";
			this.Title = "前世红尘";
	
			this.Shown += new System.EventHandler(this.Completed_Shown);
			this.SizeChanged += new System.EventHandler(this.Completed_SizeChanged);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Completed_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.RichTextBox richTextBox1;
		private Xylia.Windows.Controls.TreeView TreeView;
	}
}