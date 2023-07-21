namespace Xylia.Preview.UI.Custom.Controls
{
	partial class TitlePanel
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
			this.lbl_Title = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl_Title
			// 
			this.lbl_Title.AutoSize = true;
			this.lbl_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(99)))));
			this.lbl_Title.Location = new System.Drawing.Point(0, 0);
			this.lbl_Title.Name = "lbl_Title";
			this.lbl_Title.Size = new System.Drawing.Size(58, 21);
			this.lbl_Title.TabIndex = 0;
			this.lbl_Title.Text = "信息组";
			// 
			// TitlePanel
			// 
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lbl_Title);
			this.ForeColor = System.Drawing.Color.Black;
			this.Name = "TitlePanel";
			this.Size = new System.Drawing.Size(292, 52);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lbl_Title;
	}
}
