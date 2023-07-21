using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class ProcessComparisonCell
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessComparisonCell));
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.itemIconCell2 = new Xylia.Preview.UI.Custom.Controls.ItemIconCell();
			this.itemIconCell1 = new Xylia.Preview.UI.Custom.Controls.ItemIconCell();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(65, 11);
			this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(33, 30);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox3.TabIndex = 2;
			this.pictureBox3.TabStop = false;
			// 
			// itemIconCell2
			// 
			this.itemIconCell2.BackColor = System.Drawing.Color.Transparent;
			this.itemIconCell2.ForeColor = System.Drawing.Color.Black;
			this.itemIconCell2.FrameImage = null;
			this.itemIconCell2.FrameType = true;
			this.itemIconCell2.Image = ((System.Drawing.Image)(resources.GetObject("itemIconCell2.Image")));
			this.itemIconCell2.Location = new System.Drawing.Point(112, 0);
			this.itemIconCell2.Margin = new System.Windows.Forms.Padding(4);
			this.itemIconCell2.Name = "itemIconCell2";
			this.itemIconCell2.Scale = 52;
			this.itemIconCell2.ShowFrameImage = true;
			this.itemIconCell2.ShowStackCount = true;
			this.itemIconCell2.ShowStackCountOnlyOne = true;
			this.itemIconCell2.Size = new System.Drawing.Size(52, 52);
			this.itemIconCell2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.itemIconCell2.StackCount = 1;
			this.itemIconCell2.TabIndex = 7;
			this.itemIconCell2.TabStop = false;
			// 
			// itemIconCell1
			// 
			this.itemIconCell1.BackColor = System.Drawing.Color.Transparent;
			this.itemIconCell1.ForeColor = System.Drawing.Color.Black;
			this.itemIconCell1.FrameImage = null;
			this.itemIconCell1.FrameType = true;
			this.itemIconCell1.Image = ((System.Drawing.Image)(resources.GetObject("itemIconCell1.Image")));
			this.itemIconCell1.Location = new System.Drawing.Point(0, 0);
			this.itemIconCell1.Margin = new System.Windows.Forms.Padding(4);
			this.itemIconCell1.Name = "itemIconCell1";
			this.itemIconCell1.Scale = 52;
			this.itemIconCell1.ShowFrameImage = true;
			this.itemIconCell1.ShowStackCount = true;
			this.itemIconCell1.ShowStackCountOnlyOne = true;
			this.itemIconCell1.Size = new System.Drawing.Size(52, 52);
			this.itemIconCell1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.itemIconCell1.StackCount = 1;
			this.itemIconCell1.TabIndex = 6;
			this.itemIconCell1.TabStop = false;
			// 
			// ProcessComparisonCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.itemIconCell2);
			this.Controls.Add(this.itemIconCell1);
			this.Controls.Add(this.pictureBox3);
			this.ForeColor = System.Drawing.Color.Black;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ProcessComparisonCell";
			this.Size = new System.Drawing.Size(168, 56);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemIconCell1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox pictureBox3;
		private ItemIconCell itemIconCell1;
		private ItemIconCell itemIconCell2;
	}
}
