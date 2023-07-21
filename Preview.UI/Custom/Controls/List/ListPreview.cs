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

		this.Scroll += new((s, e) => PerformLayout());
		this.MouseWheel += new((s, e) =>
		{
			var value = this.VerticalScroll.Value - e.Delta / 5;
			if (value >= this.VerticalScroll.Minimum && value <= this.VerticalScroll.Maximum)
			{
				this.VerticalScroll.Value = value;
				this.OnScroll(new ScrollEventArgs(ScrollEventType.SmallIncrement, value));

				this.Invalidate();
			}
		});
	}

	protected override Point ScrollToControl(Control activeControl) => this.AutoScrollPosition;
	#endregion


	#region Event
	public event PaintEventHandler DrawItem;

	public event EventHandler SelectItem;
	#endregion




	#region Items
	public ObjectCollection Items;

	/// <summary>
	///  A collection that stores objects.
	/// </summary>
	[ListBindable(false)]
	public class ObjectCollection : List<object>
	{
		private readonly ListPreview _owner;

		public ObjectCollection(ListPreview owner)
		{
			ArgumentNullException.ThrowIfNull(owner);
			_owner = owner;
		}

		/// <summary>
		///  Initializes a new instance of ListBox.ObjectCollection based on another ListBox.ObjectCollection.
		/// </summary>
		public ObjectCollection(ListPreview owner, ObjectCollection value)
			: this(owner)
		{
			ArgumentNullException.ThrowIfNull(value);

			AddRange(value);
		}

		/// <summary>
		///  Initializes a new instance of ListBox.ObjectCollection containing any array of objects.
		/// </summary>
		public ObjectCollection(ListPreview owner, object[] value)
			: this(owner)
		{
			ArgumentNullException.ThrowIfNull(value);

			AddRange(value);
		}
	}


	public int ItemHeight = 30;
	#endregion

	#region Pages
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int MaxItemNum { get; set; } = 100;

	private int PageIndex = 1;

	private int PageCount;


	private void PrevPage(object sender, EventArgs e)
	{
		if (PageIndex - 1 <= 0) return;

		PageIndex--;
		this.RefreshList();
	}

	private void NextPage(object sender, EventArgs e)
	{
		if (PageIndex + 1 > PageCount) return;

		PageIndex++;
		this.RefreshList();
	}
	#endregion



	#region OnPaint 
	List<KeyValuePair<object, int>> tempObj = new();

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);

		for (int i = 0; i < tempObj.Count; i++)
		{
			var item = tempObj[i].Key;

			var RECT = GetItemRectangle(i);
			if (RECT.Width == 0 && RECT.Height == 0) continue;

			if (DrawItem != null) DrawItem?.Invoke(item, new PaintEventArgs(e.Graphics, RECT));
			else e.Graphics.DrawString(item.ToString(), this.Font, new SolidBrush(this.ForeColor), RECT);
		}
	}

	public void RefreshList()
	{
		base.Refresh();

		this.SuspendLayout();
		this.Controls.Clear();

		//PageIndex = 1;
		VerticalScroll.Value = 0;
		tempObj.Clear();


		#region data 
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
				this.Invoke(() =>
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

		#region page
		// TODO: change to use paint
		if (PageCount > 1)
		{
			y += 10;
			var pageSelector = new PageSelector { Text = $"{PageIndex} / {PageCount}" };
			pageSelector.Location = new Point((this.Width - pageSelector.Width) / 2, y);
			pageSelector.PrevSeleted += PrevPage;
			pageSelector.NextSeleted += NextPage;
			this.Invoke(() => this.Controls.Add(pageSelector));

			y = pageSelector.Bottom;
		}
		#endregion


		// source: https://source.dot.net/#System.Windows.Forms/System/Windows/Forms/ScrollableControl.cs,320
		this.ResumeLayout(true);
		this.VerticalScroll.Visible = true;
		this.VerticalScroll.Maximum = y;
	}

	private Rectangle GetItemRectangle(int index)
	{
		var y = tempObj[index].Value - this.VerticalScroll.Value;
		if (y < 0 || y > this.ClientRectangle.Width) return default;

		return new Rectangle()
		{
			X = 0,
			Y = y,
			Width = this.Width,
			Height = ItemHeight
		};
	}
	#endregion


	protected override void OnDoubleClick(EventArgs e)
	{
		var point = this.PointToClient(MousePosition);
		for (int index = 0; index < tempObj.Count; index++)
		{
			var RECT = GetItemRectangle(index);
			if (point.Y > RECT.Y && point.Y < RECT.Y + RECT.Height)
				SelectItem?.Invoke(tempObj[index].Key, new());
		}
	}
}