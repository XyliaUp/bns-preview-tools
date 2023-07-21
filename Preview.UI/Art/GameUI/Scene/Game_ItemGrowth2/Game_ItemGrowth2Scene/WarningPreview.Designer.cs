using Xylia.Preview.UI.Custom.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	partial class WarningPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningPreview));
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.panelContent1 = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(0, -1);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(31, 27);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 20;
			this.pictureBox2.TabStop = false;
			// 
			// panelContent1
			// 
			this.panelContent1.BackColor = System.Drawing.Color.Transparent;
			//this.panelContent1.BasicLineHeight = 19;
			this.panelContent1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.panelContent1.ForeColor = System.Drawing.Color.White;
			this.panelContent1.Location = new System.Drawing.Point(39, 3);
			this.panelContent1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
			this.panelContent1.Name = "panelContent1";
			this.panelContent1.TabIndex = 19;
			this.panelContent1.Text = "提示消息";
			// 
			// WarningPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.panelContent1);
			this.Controls.Add(this.pictureBox2);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "WarningPreview";
			this.Size = new System.Drawing.Size(103, 30);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public ContentPanel panelContent1;
		private System.Windows.Forms.PictureBox pictureBox2;
	}
}
