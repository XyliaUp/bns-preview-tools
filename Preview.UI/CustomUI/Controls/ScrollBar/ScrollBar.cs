using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Xylia.Preview.GameUI.Controls.Designer;

namespace Xylia.Preview.GameUI.Controls.PanelEx.ScrollBar
{
	[DesignTimeVisible(false)]
	[Designer(typeof(FixedWidthDesigner))]
	[ToolboxBitmap(typeof(HScrollBar))]
	public partial class ScrollBar : UserControl
	{
		#region Constructor
		public ScrollBar()
		{
			InitializeComponent();

			CheckForIllegalCrossThreadCalls = false;
			this.BackColor = Color.Transparent;
		}
		#endregion

		#region Private Fields
		private Panel _RelaPanel = null;

		private bool SlideBarFlag = false;
		private bool ScrollBarFlag = false;
		private int SlideBarOpacity = 255;
		#endregion

		#region  Fields
		[Browsable(true)]
		[Category("Appearance")]
		[Description("滑动条最短长度")]
		public int MinHeight { get; set; } = 30;

		[Browsable(true)]
		[Category("Appearance")]
		[Description("要滚动的Panel")]
		public Panel RelaPanel
		{
			get => _RelaPanel;
			set
			{
				this._RelaPanel = value;

				this.AdjustPanelVScroll();
				this.Wake();
			}
		}


		public int SlideStep => RelaPanel.VerticalScroll.SmallChange;

		public bool NeedSleep { get; set; }

		public bool Active { get; set; }

		/// <summary>
		/// 需要显示滚动条
		/// </summary>
		public bool HasScrollBar => RelaPanel.Height != RelaPanel.DisplayRectangle.Height;
		#endregion



		/// <summary>
		/// 唤醒
		/// </summary>
		private void Wake()
		{
			this.ResizeSlideBar();
			this.LocateSlideBar();

			this.SlideBarOpacity = 255;
		}

		/// <summary>
		/// 适配垂直滚动条
		/// </summary>
		private void AdjustPanelVScroll()
		{
			//要求刷新大小
			this.ResizeSlideBar();

			//修改状态
			this.SlideBar.Visible = this.TopBtnEnabled = this.BottomBtnEnabled = this.HasScrollBar;
			if (!this.HasScrollBar)
			{
				return;
			}
		}



		#region 创建绑定
		private void This_Created(object sender, EventArgs e)
		{
			RelaPanel.MouseWheel += new MouseEventHandler(This_RelaPanel_MouseWheel);
			RelaPanel.MouseEnter += new EventHandler(ScrollBar_MouseEnter);
			RelaPanel.MouseLeave += new EventHandler(ScrollBar_MouseLeave);
			RelaPanel.ControlAdded += new ControlEventHandler(This_RelaPanel_ControlAdded);
			RelaPanel.ControlRemoved += new ControlEventHandler(This_RelaPanel_ControlRemoved);
		}

		private void ScrollBar_MouseEnter(object sender, EventArgs e)
		{
			if (!this.Active) return;

			this.SlideBarFlag = true;
			this.Wake();
		}

		private void ScrollBar_MouseLeave(object sender, EventArgs e)
		{
			if (!Active) return;
			ScrollBarFlag = false;
		}

		private void This_RelaPanel_MouseWheel(object sender, MouseEventArgs e)
		{
			if (!this.Active) return;

			this.Wake();
			this.OnMouseWheel(e);
			this.SlideBar_Sleep();
		}

		private void This_RelaPanel_ControlAdded(object sender, ControlEventArgs e)
		{
			this.AdjustPanelVScroll();
		}

		private void This_RelaPanel_ControlRemoved(object sender, ControlEventArgs e)
		{
			this.AdjustPanelVScroll();
		}

		/// <summary>
		/// 鼠标滚动Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void This_MouseWheel(object sender, MouseEventArgs e)
		{
			if (!this.Active) return;
			this.AdjustPanelVScroll();
		}
		#endregion

		#region 方向按钮控制
		private System.Timers.Timer TopBtnInMouse;

		private bool TopBtnEnabled
		{
			get => this.TopBtn.Enabled;
			set
			{
				this.TopBtn.Enabled = value;
				this.TopBtn.Image = value ? BarImage.vertical_decrement_arrow : BarImage.vertical_decrement_arrow_disabled;
			}
		}

		private void TopBtn_MouseEnter(object sender, EventArgs e)
		{
			//设置图标
			if (this.TopBtnEnabled) this.TopBtn.Image = BarImage.vertical_decrement_arrow_hover;
		}

		private void TopBtn_MouseLeave(object sender, EventArgs e)
		{
			if (this.TopBtnEnabled) this.TopBtn.Image = BarImage.vertical_decrement_arrow;
		}

		private void TopBtn_MouseDown(object sender, MouseEventArgs e)
		{
			this.TopBtnInMouse = new System.Timers.Timer(5) { Enabled = true };
			this.TopBtnInMouse.Elapsed += (o, s) =>
			{
				if (RelaPanel.VerticalScroll.Value != RelaPanel.VerticalScroll.Minimum)
				{
					this.RelaPanel.VerticalScroll.Value -= 10;
					this.LocateSlideBar();
				}
			};
		}

		private void TopBtn_MouseUp(object sender, MouseEventArgs e)
		{
			this.TopBtnInMouse?.Stop();
		}




		private System.Timers.Timer BottomBtnInMouse;

		private bool BottomBtnEnabled
		{
			get => this.BottomBtn.Enabled;
			set
			{
				this.BottomBtn.Enabled = value;
				this.BottomBtn.Image = value ? BarImage.vertical_increment_arrow : BarImage.vertical_increment_arrow_disabled;
			}
		}

		private void BottomBtn_MouseEnter(object sender, EventArgs e)
		{
			if (this.BottomBtnEnabled) this.BottomBtn.Image = BarImage.vertical_increment_arrow_hover;
		}

		private void BottomBtn_MouseLeave(object sender, EventArgs e)
		{
			if (this.BottomBtnEnabled) this.BottomBtn.Image = BarImage.vertical_increment_arrow;
		}

		private void BottomBtn_MouseDown(object sender, MouseEventArgs e)
		{
			this.BottomBtnInMouse = new System.Timers.Timer(5) { Enabled = true };
			this.BottomBtnInMouse.Elapsed += (o, s) =>
			{
				if (RelaPanel.VerticalScroll.Value != RelaPanel.VerticalScroll.Maximum)
				{
					this.RelaPanel.VerticalScroll.Value += 10;
					this.LocateSlideBar();
				}
			};
		}

		private void BottomBtn_MouseUp(object sender, MouseEventArgs e)
		{
			this.BottomBtnInMouse?.Stop();
		}
		#endregion

		#region ScrollBar
		private void ScrollBar_SizeChanged(object sender, EventArgs e)
		{
			//SlideBar.Width = this.Width;
		}

		private void ScrollBar_VisibleChanged(object sender, EventArgs e)
		{
			Active = this.Visible;
			if (Active) this.Wake();
		}
		#endregion

		#region SlideBar (滑块)
		private void SlideBar_Sleep()
		{
			if (!Active) return;
		}

		private void SlideBar_MouseEnter(object sender, EventArgs e)
		{
			if (!Active) return;

			this.ScrollBarFlag = true;
			this.Wake();
		}

		private void SlideBar_MouseLeave(object sender, EventArgs e)
		{
			if (!Active) return;
			this.SlideBarFlag = false;
		}
		#endregion




		#region SlideBar位置&大小控制
		/// <summary>
		/// 滑块起始位置
		/// </summary>
		private int SlideTop => this.TopBtn.Bottom + 3;

		/// <summary>
		/// 滑块终止位置
		/// </summary>
		private int SlideBottom => this.BottomBtn.Top - 3;

		/// <summary>
		/// 滑块高度
		/// </summary>
		private int SlideHeight => this.SlideBottom - this.SlideTop;

		/// <summary>
		/// 滑块X坐标
		/// </summary>
		private int SlideLeft => (this.TopBtn.Width - this.SlideBar.Width + 1) / 2;


		/// <summary>
		/// 复位
		/// </summary>
		public void Reset()
		{
			this.SlideBar.Location = new Point(this.SlideLeft, this.SlideTop);
		}

		/// <summary>
		/// 计算滑块大小
		/// </summary>
		public void ResizeSlideBar()
		{
			//高度比率
			double rate = (double)RelaPanel.Height / RelaPanel.DisplayRectangle.Height;

			//修改位置
			this.SlideBar.Height = Math.Max((int)(rate * this.SlideHeight), this.MinHeight);

			#region 生成滑块图片
			int LocY = 0;
			Bitmap bitmap = new Bitmap(this.SlideBar.Width, this.SlideBar.Height);
			Graphics g = Graphics.FromImage(bitmap);

			var TopImg = BarImage.vertical_thumb_top;
			var MiddleImg = BarImage.vertical_thumb_bg;
			var BottomImg = BarImage.vertical_thumb_bottom;

			//绘制头段
			g.DrawImage(TopImg, new PointF(0, 0));
			LocY += TopImg.Height;

			//绘制中段
			int MiddleHeight = this.SlideBar.Height - BarImage.vertical_thumb_bottom.Height;
			while (LocY < MiddleHeight)
			{
				g.DrawImage(MiddleImg, new PointF(0, LocY));
				LocY += MiddleImg.Height;
			}

			//绘制底段
			g.DrawImage(BottomImg, new PointF(0, LocY));

			this.SlideBar.Image = bitmap;
			#endregion
		}

		/// <summary>
		/// 定位滑块
		/// </summary>
		private void LocateSlideBar()
		{
			//高度比率
			double rate = (double)RelaPanel.VerticalScroll.Value / (RelaPanel.VerticalScroll.Maximum - RelaPanel.Height);

			//修改位置							  
			this.SlideBar.Location = new Point(this.SlideLeft, this.SlideTop + (int)(rate * (this.SlideHeight - this.SlideBar.Height)));
		}
		#endregion
	}
}
