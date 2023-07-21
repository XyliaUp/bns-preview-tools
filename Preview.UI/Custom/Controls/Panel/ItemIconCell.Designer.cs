using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Xylia.Preview.UI.Custom.Controls
{
	partial class ItemIconCell
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
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// ItemIconCell
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.ForeColor = System.Drawing.Color.Black;
			this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.Resize += new System.EventHandler(this.ItemIconCell_Resize);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}
