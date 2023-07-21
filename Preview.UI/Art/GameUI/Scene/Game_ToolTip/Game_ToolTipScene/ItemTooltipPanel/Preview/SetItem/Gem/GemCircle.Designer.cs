namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class GemCircle
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GemCircle));
			this.Panel_BackGround = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.Panel_BackGround)).BeginInit();
			this.SuspendLayout();
			// 
			// Panel_BackGround
			// 
			this.Panel_BackGround.BackColor = System.Drawing.Color.Transparent;
			this.Panel_BackGround.Image = ((System.Drawing.Image)(resources.GetObject("Panel_BackGround.Image")));
			this.Panel_BackGround.Location = new System.Drawing.Point(0, 0);
			this.Panel_BackGround.Margin = new System.Windows.Forms.Padding(4);
			this.Panel_BackGround.Name = "Panel_BackGround";
			this.Panel_BackGround.Size = new System.Drawing.Size(240, 240);
			this.Panel_BackGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.Panel_BackGround.TabIndex = 0;
			this.Panel_BackGround.TabStop = false;
			this.Panel_BackGround.DoubleClick += new System.EventHandler(this.Panel_BackGround_DoubleClick);
			this.Panel_BackGround.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel_BackGround_MouseClick);
			// 
			// GemCircle
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Panel_BackGround);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "GemCircle";
			this.Size = new System.Drawing.Size(244, 244);
			this.Load += new System.EventHandler(this.GemCircle_Load);
			((System.ComponentModel.ISupportInitialize)(this.Panel_BackGround)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox Panel_BackGround;
	}
}
