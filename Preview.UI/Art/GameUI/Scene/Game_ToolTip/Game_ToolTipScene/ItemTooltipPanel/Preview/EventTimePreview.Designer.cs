namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class EventTimePreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventTimePreview));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.EventTime_FixedDate = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 25);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(19, 23);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// EventTime_FixedDate
			// 
			this.EventTime_FixedDate.AutoSize = true;
			this.EventTime_FixedDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.EventTime_FixedDate.ForeColor = System.Drawing.Color.White;
			this.EventTime_FixedDate.Location = new System.Drawing.Point(35, 26);
			this.EventTime_FixedDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.EventTime_FixedDate.Name = "EventTime_FixedDate";
			this.EventTime_FixedDate.Size = new System.Drawing.Size(152, 20);
			this.EventTime_FixedDate.TabIndex = 3;
			this.EventTime_FixedDate.Text = "EventTime_FixedDate";
			// 
			// EventTimePreview
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.EventTime_FixedDate);
			this.Controls.Add(this.pictureBox1);
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "EventTimePreview";
			this.Size = new System.Drawing.Size(191, 52);
			this.Title = "EventName";
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.EventTime_FixedDate, 0);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label EventTime_FixedDate;
	}
}
