using System.ComponentModel;

using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls.Designer;
using Xylia.Preview.UI.Extension;

namespace Xylia.Preview.UI.Custom.Controls;

[Designer(typeof(FixedHeightDesigner))]
public partial class ItemShowCell : UserControl
{
	#region Constructor
	public ItemShowCell()
	{
		InitializeComponent();
		this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

		this.AutoSize = false;
		this.DoubleBuffered = true;
		this.ResizeRedraw = false;
	}



	public void LoadData(Item ItemData, bool UseExtra = false) => this.LoadData(ItemData, UseExtra ? ItemData.IconExtra() : ItemData.Icon());

	public void LoadData(Item ItemData, Bitmap Icon)
	{
		this.ItemData = ItemData;

		this.IconCell.Image = Icon;
		this.NameCell.Text = ItemData.Name2;
		this.NameCell.ItemGrade = ItemData.ItemGrade;
		this.NameCell.TagImage = ItemData.TagIconGrade;

		this.Invalidate();
	}
	#endregion

	#region Fields
	public override Size MaximumSize
	{
		get => base.MaximumSize;
		set
		{
			base.MaximumSize = value;
			NameCell.MaximumSize = new(value.Width - NameCell.Left, value.Height);
		}
	}


	public Item ItemData
	{
		get => (Item)this.IconCell.ObjectRef;
		set => this.IconCell.ObjectRef = value;
	}


	[Category("Data"), Description("物品名称")]
	public string ItemName
	{
		get => this.NameCell.Text;
		set
		{
			this.NameCell.Text = value;
			this.Invalidate();
		}
	}

	[Category("Data"), Description("物品品质")]
	public sbyte ItemGrade
	{
		get => this.NameCell.ItemGrade;
		set => this.NameCell.ItemGrade = value;
	}

	[Category("Data"), Description("是否使用标签")]
	public Bitmap TagImage
	{
		get => this.NameCell.TagImage;
		set => this.NameCell.TagImage = value;
	}


	[Category("Data"), Description("物品图标")]
	public Bitmap ItemIcon
	{
		get => (Bitmap)this.IconCell.Image;
		set
		{
			this.IconCell.Image = value;
			this.Invalidate();
		}
	}

	[Category("Data"), Description("物品图标尺寸")]
	public new int Scale
	{
		get => this.IconCell.Scale;
		set
		{
			this.IconCell.Scale = value;
			this.Invalidate();
		}
	}


	public int HeightDiff { get; set; }

	public bool ReserveIconSpace { get; set; }
	#endregion


	#region Functions
	protected override void OnPaint(PaintEventArgs e)
	{
		int NameLoY = (this.Scale - this.NameCell.Height) / 2 + HeightDiff;

		if (!ReserveIconSpace && this.ItemIcon is null)
		{
			this.IconCell.Visible = false;
			this.NameCell.Location = new Point(0, NameLoY);
		}
		else
		{
			this.IconCell.Visible = true;
			this.NameCell.Location = new Point(this.IconCell.Scale + 2, NameLoY);
		}

		this.Width = this.NameCell.Right;
		this.Height = Math.Max(this.IconCell.Scale - 1, this.NameCell.Location.Y + this.NameCell.Height);
	}

	private void ItemName_DoubleClick(object sender, EventArgs e) => this.ItemData.PreviewShow(this);
	#endregion
}