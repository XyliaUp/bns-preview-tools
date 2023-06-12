namespace Xylia.Preview.GameUI.Scene.Game_RandomStore
{
	partial class Game_RandomStoreScene
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_RandomStoreScene));
			this.SuspendLayout();
			// 
			// ListPreview
			// 
			this.ListPreview.Location = new System.Drawing.Point(365, 0);
			this.ListPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ListPreview.Size = new System.Drawing.Size(391, 606);
			// 
			// TreeView
			// 
			this.TreeView.LineColor = System.Drawing.Color.Black;
			this.TreeView.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.TreeView.Size = new System.Drawing.Size(351, 606);
			// 
			// RandomStoreListScene
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(26)))), ((int)(((byte)(35)))));
			this.ClientSize = new System.Drawing.Size(756, 606);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "RandomStoreListScene";
			this.Text = "聚灵阁奖池";
			//this.Load += new System.EventHandler(this.RandomStoreListScene_Load);
			this.ResumeLayout(false);

		}

		#endregion
	}
}