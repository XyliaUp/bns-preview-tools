using System.ComponentModel;

using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls.List;

namespace Xylia.Preview.UI.Custom.Controls;
public partial class ListPreview : Panel
{
	#region Constructor
	public ListPreview()
	{
		this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
		this.InitializeComponent();
	}
	#endregion

	#region Event
	public event PaintEventHandler DrawItem;

	public event EventHandler SelectItem;
	#endregion


	#region Items
	public List<object> Items;

	public int ItemHeight = 30;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int MaxItemNum { get; set; } = 0;
	#endregion

	#region Pages
	public int PageIndex = 1;
	public int PageCount;

	public bool HasPrevPage => PageIndex - 1 > 0;
	public bool HasNextPage => PageIndex + 1 <= PageCount;

	protected override Point ScrollToControl(Control activeControl) => this.AutoScrollPosition;
	#endregion


	#region Paint 
	readonly List<KeyValuePair<object, int>> tempObj = new();

	public void RefreshList(bool SwitchPage = false)
	{
		#region init
		this.SuspendLayout();
		this.Controls.Clear();
		tempObj.Clear();
		#endregion

		#region data 
		if (!SwitchPage) PageIndex = 1;

		IEnumerable<object> data = Items;
		if (MaxItemNum > 0)
		{
			data = data.Skip(MaxItemNum * (PageIndex - 1)).Take(MaxItemNum);
			PageCount = (int)Math.Ceiling((float)Items.Count / MaxItemNum);
		}
		#endregion

		#region item
		int y = 0;
		for (int i = 0; i < data.Count(); i++)
		{
			var item = data.ElementAt(i);

			if (item is Control ctl)
			{
				ctl.Location = new Point(0, y);
				ctl.Width = this.Width - 20;

				y = ctl.Bottom + 5;

				this.BeginInvoke(() => this.Controls.Add(ctl));
			}
			else if (item is Item record)
			{
				this.BeginInvoke(() =>
				{
					var cell = new ListItemCell(record);
					cell.Location = new Point(0, y);
					y = cell.Bottom + 5;

					this.Controls.Add(cell);
				});
			}
			else
			{
				tempObj.Add(new(item, y));
				y += ItemHeight;
			}
		}
		#endregion

		this.VerticalScroll.Minimum = 0;
		this.VerticalScroll.Maximum = Math.Max(0, y - this.ClientRectangle.Height);
		this.VerticalScroll.Value = 0;
		this.VerticalScroll.Visible = true;
		this.ResumeLayout(true);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);

		// set default item draw event
		DrawItem ??= new PaintEventHandler((o, e) => e.Graphics.DrawString(o.ToString(), this.Font, new SolidBrush(this.ForeColor), e.ClipRectangle));

		// execute
		e.Graphics.TranslateTransform(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);
		foreach (var item in tempObj)
		{
			if (item.Value < this.VerticalScroll.Value ||
				item.Value > this.VerticalScroll.Value + this.ClientRectangle.Height + ItemHeight) continue;

			DrawItem.Invoke(item.Key, new PaintEventArgs(e.Graphics, new Rectangle(0, item.Value, this.Width, ItemHeight)));
		}
	}
	#endregion

	#region Click 	
	protected override void OnDoubleClick(EventArgs e)
	{
		// get real point
		var point = this.PointToClient(MousePosition);
		point.Y += this.VerticalScroll.Value;

		// execute
		foreach (var item in tempObj)
		{
			if (item.Value < this.VerticalScroll.Value ||
				item.Value > this.VerticalScroll.Value + this.ClientRectangle.Height + ItemHeight) continue;

			if (point.Y > item.Value && point.Y < item.Value + ItemHeight)
				SelectItem?.Invoke(item.Key, new());
		}
	}
	#endregion

	#region Scroll
	protected override void OnMouseWheel(MouseEventArgs e)
	{
		base.OnMouseWheel(e);

		if (!VerticalScroll.Visible) return;

		var value = VerticalScroll.Value - e.Delta / 5;
		if (value >= VerticalScroll.Minimum && value <= VerticalScroll.Maximum)
		{
			this.VerticalScroll.Value = value;
			this.OnScroll(new ScrollEventArgs(ScrollEventType.SmallIncrement, value));

			this.Invalidate();
		}
	}

	protected override void OnScroll(ScrollEventArgs se)
	{
		base.OnScroll(se);
		PerformLayout();
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		this.RefreshList(true);
	}
	#endregion
}