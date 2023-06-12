namespace Xylia.Preview.GameUI.Controls.PanelEx.ScrollBar
{
	partial class ScrollBar
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源, 为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary>
		/// 设计器支持所需的Functions - 不要
		/// 使用代码编辑器修改此Functions的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScrollBar));
			this.SlideBar = new System.Windows.Forms.PictureBox();
			this._RelaPanel = new System.Windows.Forms.Panel();
			this.BottomBtn = new System.Windows.Forms.PictureBox();
			this.TopBtn = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.SlideBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TopBtn)).BeginInit();
			this.SuspendLayout();
			// 
			// SlideBar
			// 
			this.SlideBar.BackColor = System.Drawing.Color.Transparent;
			this.SlideBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SlideBar.BackgroundImage")));
			this.SlideBar.Location = new System.Drawing.Point(3, 28);
			this.SlideBar.Name = "SlideBar";
			this.SlideBar.Size = new System.Drawing.Size(15, 331);
			this.SlideBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.SlideBar.TabIndex = 0;
			this.SlideBar.TabStop = false;
			//this.SlideBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SlideBar_MouseDown);
			this.SlideBar.MouseEnter += new System.EventHandler(this.SlideBar_MouseEnter);
			this.SlideBar.MouseLeave += new System.EventHandler(this.SlideBar_MouseLeave);
			//this.SlideBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SlideBar_MouseUp);
			// 
			// _RelaPanel
			// 
			this._RelaPanel.BackColor = System.Drawing.Color.White;
			this._RelaPanel.Location = new System.Drawing.Point(0, 0);
			this._RelaPanel.Name = "_RelaPanel";
			this._RelaPanel.Size = new System.Drawing.Size(100, 415);
			this._RelaPanel.TabIndex = 0;
			// 
			// BottomBtn
			// 
			this.BottomBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomBtn.Image = ((System.Drawing.Image)(resources.GetObject("BottomBtn.Image")));
			this.BottomBtn.Location = new System.Drawing.Point(0, 393);
			this.BottomBtn.Name = "BottomBtn";
			this.BottomBtn.Size = new System.Drawing.Size(22, 22);
			this.BottomBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.BottomBtn.TabIndex = 1;
			this.BottomBtn.TabStop = false;
			//this.BottomBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BottomBtn_MouseClick);
			this.BottomBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BottomBtn_MouseDown);
			this.BottomBtn.MouseEnter += new System.EventHandler(this.BottomBtn_MouseEnter);
			this.BottomBtn.MouseLeave += new System.EventHandler(this.BottomBtn_MouseLeave);
			this.BottomBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BottomBtn_MouseUp);
			// 
			// TopBtn
			// 
			this.TopBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopBtn.Image = ((System.Drawing.Image)(resources.GetObject("TopBtn.Image")));
			this.TopBtn.Location = new System.Drawing.Point(0, 0);
			this.TopBtn.Name = "TopBtn";
			this.TopBtn.Size = new System.Drawing.Size(22, 22);
			this.TopBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.TopBtn.TabIndex = 2;
			this.TopBtn.TabStop = false;
			this.TopBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopBtn_MouseDown);
			this.TopBtn.MouseEnter += new System.EventHandler(this.TopBtn_MouseEnter);
			this.TopBtn.MouseLeave += new System.EventHandler(this.TopBtn_MouseLeave);
			this.TopBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopBtn_MouseUp);
			// 
			// ScrollBar
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.TopBtn);
			this.Controls.Add(this.BottomBtn);
			this.Controls.Add(this.SlideBar);
			this.Name = "ScrollBar";
			this.Size = new System.Drawing.Size(22, 415);
			this.SizeChanged += new System.EventHandler(this.ScrollBar_SizeChanged);
			this.VisibleChanged += new System.EventHandler(this.ScrollBar_VisibleChanged);
			this.HandleCreated += new System.EventHandler(this.This_Created);
			this.MouseEnter += new System.EventHandler(this.ScrollBar_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.ScrollBar_MouseLeave);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.This_MouseWheel);
			((System.ComponentModel.ISupportInitialize)(this.SlideBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TopBtn)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox SlideBar;
		private System.Windows.Forms.PictureBox BottomBtn;
		private System.Windows.Forms.PictureBox TopBtn;
	}
}
