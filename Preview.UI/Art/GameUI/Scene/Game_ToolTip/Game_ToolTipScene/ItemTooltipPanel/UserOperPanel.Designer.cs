
namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class UserOperPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserOperPanel));
			EquipmentGuide = new PictureBox();
			pictureBox2 = new PictureBox();
			ToolTip = new ToolTip(components);
			ModelViwer = new PictureBox();
			((System.ComponentModel.ISupportInitialize)EquipmentGuide).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)ModelViwer).BeginInit();
			SuspendLayout();
			// 
			// EquipmentGuide
			// 
			EquipmentGuide.Image = (Image)resources.GetObject("EquipmentGuide.Image");
			EquipmentGuide.Location = new Point(9, 72);
			EquipmentGuide.Margin = new Padding(4);
			EquipmentGuide.Name = "EquipmentGuide";
			EquipmentGuide.Size = new Size(32, 32);
			EquipmentGuide.SizeMode = PictureBoxSizeMode.StretchImage;
			EquipmentGuide.TabIndex = 0;
			EquipmentGuide.TabStop = false;
			ToolTip.SetToolTip(EquipmentGuide, "提炼");
			EquipmentGuide.Visible = false;
			EquipmentGuide.Click += EquipmentGuide_Click;
			// 
			// pictureBox2
			// 
			pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
			pictureBox2.Location = new Point(9, 7);
			pictureBox2.Margin = new Padding(4);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new Size(32, 32);
			pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox2.TabIndex = 1;
			pictureBox2.TabStop = false;
			ToolTip.SetToolTip(pictureBox2, "更多信息");
			pictureBox2.Visible = false;
			pictureBox2.Click += pictureBox2_Click;
			// 
			// ModelViwer
			// 
			ModelViwer.Image = (Image)resources.GetObject("ModelViwer.Image");
			ModelViwer.Location = new Point(9, 136);
			ModelViwer.Margin = new Padding(4);
			ModelViwer.Name = "ModelViwer";
			ModelViwer.Size = new Size(32, 32);
			ModelViwer.SizeMode = PictureBoxSizeMode.StretchImage;
			ModelViwer.TabIndex = 2;
			ModelViwer.TabStop = false;
			ToolTip.SetToolTip(ModelViwer, "Debug");
			ModelViwer.Visible = false;
			ModelViwer.Click += ModelViwer_Click;
			// 
			// UserOperPanel
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Black;
			ClientSize = new Size(53, 192);
			Controls.Add(ModelViwer);
			Controls.Add(pictureBox2);
			Controls.Add(EquipmentGuide);
			DoubleBuffered = true;
			FormBorderStyle = FormBorderStyle.None;
			KeyPreview = true;
			Margin = new Padding(4);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "UserOperPanel";
			Opacity = 0.8D;
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "UserOperScene";
			ToolTip.SetToolTip(this, "           用户操作功能\r\n可通过 Ctrl+G 控制关闭/开启");
			Load += UserOperScene_Load;
			VisibleChanged += UserOperPanel_VisibleChanged;
			((System.ComponentModel.ISupportInitialize)EquipmentGuide).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)ModelViwer).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.PictureBox EquipmentGuide;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.ToolTip ToolTip;
		private PictureBox ModelViwer;
	}
}