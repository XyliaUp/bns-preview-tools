using System.Drawing.Drawing2D;

using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.UI.Custom.Controls.List;
public partial class ListItemCell : UserControl
{
	public ListItemCell(Item data)
	{
		InitializeComponent();
		SetStyle(ControlStyles.UserPaint, true);

		this.data = data;
	}

	protected ListItemCell() { }



	#region Paint
	private bool _paint;

	private Item data;
	private string job;

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);

		if (!_paint)
		{
			_paint = true;

			job = data.EquipJobCheck
				.Where(seq => seq != JobSeq.JobNone)
				.Select(t => t.GetDescription()).Aggregate(",");


			this.ItemShow.LoadData(data, true);
		}


		Color c1 = Color.FromArgb(25, 34, 48);
		Color c2 = Color.FromArgb(28, 36, 50);
		
		float Half = this.ClientRectangle.Width / 2;
		e.Graphics.FillRectangle(new LinearGradientBrush(this.ClientRectangle, c1, c2, LinearGradientMode.Horizontal), new RectangleF(new PointF(0, this.ClientRectangle.Bottom - 2), new SizeF(Half, 2)));
		e.Graphics.FillRectangle(new LinearGradientBrush(this.ClientRectangle, c2, c1, LinearGradientMode.Horizontal), new RectangleF(new PointF(Half, this.ClientRectangle.Bottom - 2), new SizeF(Half, 2)));


		e.Graphics.DrawString(job, this.Font, new SolidBrush(this.ForeColor), new RectangleF(), new StringFormat { Alignment = StringAlignment.Far });
	}
	#endregion
}