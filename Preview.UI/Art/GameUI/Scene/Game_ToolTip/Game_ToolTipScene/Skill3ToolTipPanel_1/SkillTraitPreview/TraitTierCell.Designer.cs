using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	partial class TraitTierCell
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
			this.TraitName = new ContentPanel();
			this.TraitIcon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.TraitIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// TraitName
			// 
			this.TraitName.BackColor = System.Drawing.Color.Transparent;
			this.TraitName.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TraitName.ForeColor = System.Drawing.Color.White;
			this.TraitName.Location = new System.Drawing.Point(76, 24);
			this.TraitName.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.TraitName.Name = "TraitName";
			this.TraitName.TabIndex = 3;
			this.TraitName.Text = "特性名称";
			// 
			// TraitIcon
			// 
			this.TraitIcon.Image = global::Xylia.Preview.UI.Resources.Resource_Common.ItemIcon;
			this.TraitIcon.Location = new System.Drawing.Point(2, 2);
			this.TraitIcon.Name = "TraitIcon";
			this.TraitIcon.Size = new System.Drawing.Size(64, 64);
			this.TraitIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.TraitIcon.TabIndex = 2;
			this.TraitIcon.TabStop = false;
			// 
			// TraitTierCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.TraitName);
			this.Controls.Add(this.TraitIcon);
			this.Name = "TraitTierCell";
			this.Size = new System.Drawing.Size(200, 69);
			((System.ComponentModel.ISupportInitialize)(this.TraitIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ContentPanel TraitName;
		private System.Windows.Forms.PictureBox TraitIcon;
	}
}
