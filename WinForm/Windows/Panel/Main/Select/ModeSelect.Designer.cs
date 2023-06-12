namespace Xylia.Match
{
	partial class ModeSelect
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModeSelect));
			pictureBox1 = new System.Windows.Forms.PictureBox();
			pictureBox2 = new System.Windows.Forms.PictureBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			pictureBox3 = new System.Windows.Forms.PictureBox();
			pictureBox4 = new System.Windows.Forms.PictureBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = System.Drawing.Color.FromArgb(247, 247, 247);
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			toolTip1.SetToolTip(pictureBox1, resources.GetString("pictureBox1.ToolTip"));
			pictureBox1.Click += PictureBox1_Click;
			// 
			// pictureBox2
			// 
			resources.ApplyResources(pictureBox2, "pictureBox2");
			pictureBox2.Name = "pictureBox2";
			pictureBox2.TabStop = false;
			toolTip1.SetToolTip(pictureBox2, resources.GetString("pictureBox2.ToolTip"));
			pictureBox2.Click += PictureBox2_Click;
			// 
			// pictureBox3
			// 
			resources.ApplyResources(pictureBox3, "pictureBox3");
			pictureBox3.Name = "pictureBox3";
			pictureBox3.TabStop = false;
			toolTip1.SetToolTip(pictureBox3, resources.GetString("pictureBox3.ToolTip"));
			// 
			// pictureBox4
			// 
			pictureBox4.BackColor = System.Drawing.Color.White;
			resources.ApplyResources(pictureBox4, "pictureBox4");
			pictureBox4.Name = "pictureBox4";
			pictureBox4.TabStop = false;
			toolTip1.SetToolTip(pictureBox4, resources.GetString("pictureBox4.ToolTip"));
			// 
			// label2
			// 
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			label2.Click += PictureBox1_Click;
			// 
			// label3
			// 
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			label3.Click += PictureBox2_Click;
			// 
			// ModeSelect
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(pictureBox2);
			Controls.Add(pictureBox1);
			FrmTitle = "选择导出模式";
			HelpButton = true;
			IsFullSize = false;
			KeyPreview = true;
			MaximizeBox = false;
			Name = "ModeSelect";
			Load += Select_Load;
			Controls.SetChildIndex(pictureBox1, 0);
			Controls.SetChildIndex(pictureBox2, 0);
			Controls.SetChildIndex(label2, 0);
			Controls.SetChildIndex(label3, 0);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}