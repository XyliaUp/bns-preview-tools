using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Xylia.Preview.GameUI.Controls.List
{
	/// <summary>
	/// 列表x对象单元
	/// </summary>
	[DesignTimeVisible(false)]
	public partial class ListCell : UserControl	, ListData
	{
		#region Constructor
		public ListCell()
		{
			InitializeComponent();
			if (this.Site == null) this.lbl_RightText.Text = null;
		}
		#endregion

		#region Fields
		/// <summary>
		/// 显示右侧文本
		/// </summary>
		public virtual bool ShowRightText { get => this.lbl_RightText.Visible; set => this.lbl_RightText.Visible = value; }

		/// <summary>
		/// 右侧文本
		/// </summary>
		[Category("Data"), Description("右侧文本")]
		public string RightText { get => this.lbl_RightText.Text; set => this.lbl_RightText.Text = value; }
		#endregion


		#region 重写Functions
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			//绘制分割符
			Color c1 = Color.FromArgb(25, 34, 48);
			Color c2 = Color.FromArgb(28, 36, 50);

			var Brush1 = new LinearGradientBrush(this.ClientRectangle, c1, c2, LinearGradientMode.Horizontal);      //渐变画刷1
			var Brush2 = new LinearGradientBrush(this.ClientRectangle, c2, c1, LinearGradientMode.Horizontal);      //渐变画刷2

			//计算宽度的一半
			float Half = this.ClientRectangle.Width / 2;

			e.Graphics.FillRectangle(Brush1, new RectangleF(new PointF(0, this.ClientRectangle.Bottom - 2), new SizeF(Half, 2)));
			e.Graphics.FillRectangle(Brush2, new RectangleF(new PointF(Half, this.ClientRectangle.Bottom - 2), new SizeF(Half, 2)));
		}

		public ListCell GetCell() => this;
		#endregion
	}


	public interface ListData
	{
		public ListCell GetCell();
	}
}