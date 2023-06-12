using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.Designer;

namespace Xylia.Preview.GameUI.Controls
{
	[Designer(typeof(FixedHeightDesigner))]
	public partial class ItemShowCell : UserControl
	{
		#region Constructor
		public ItemShowCell()
		{
			InitializeComponent();
			this.AutoSize = false;

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			this.Refresh();
		}

		public ItemShowCell(Item ItemData, bool UseExtra = false) : this() => LoadData(ItemData, UseExtra);


		public void LoadData(Item ItemData, bool UseExtra = false) => this.LoadData(ItemData, UseExtra ? ItemData.IconExtra() : ItemData.Icon());

		public void LoadData(Item ItemData, Bitmap Icon = null)
		{
			this.ItemData = ItemData;

			this.IconCell.Image = Icon;
			this.NameCell.Text = ItemData.Name2;
			this.NameCell.TagImage = ItemData.TagIconGrade;
			this.NameCell.ItemGrade = ItemData.ItemGrade;
		}
		#endregion


		#region Fields
		public Item ItemData
		{
			get => (Item)this.IconCell.ObjectRef;
			set => this.IconCell.ObjectRef = value;
		}


		[Category("Data"), Description("物品名称")]
		public string ItemName
		{
			get => this.NameCell.Text;
			set => this.NameCell.Text = value;
		}

		[Category("Data"), Description("物品品质")]
		public byte ItemGrade
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
				this.Refresh();
			}
		}

		[Category("Data"), Description("物品图标尺寸")]
		public new int Scale
		{
			get => this.IconCell.Scale;
			set
			{
				this.IconCell.Scale = value;
				Refresh();
			}
		}


		private int m_HeightDiff { get; set; } = 0;

		/// <summary>
		/// 文本高度偏移
		/// </summary>
		[Category("Data"), Description("文本高度差")]
		public int HeightDiff
		{
			get => this.m_HeightDiff;
			set
			{
				this.m_HeightDiff = value;
				this.Refresh();
			}
		}


		private bool m_ReserveIconSpace { get; set; } = true;

		/// <summary>
		/// 在图标不存在的情况下, 仍然保留图标的空间
		/// </summary>
		[Category("Data"), Description("在图标不存在的情况下, 仍然保留图标的空间")]
		public bool ReserveIconSpace
		{
			get => m_ReserveIconSpace;
			set => m_ReserveIconSpace = value;
		}
		#endregion

		#region Functions
		/// <summary>
		/// 获得中心Y位置
		/// </summary>
		private int GetCenterLoY => (this.Scale - this.NameCell.Height) / 2;

		public override void Refresh()
		{
			//获得名称的Y坐标
			int NameLoY = GetCenterLoY + HeightDiff;

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

			this.Width = this.NameCell.Location.X + this.NameCell.Width;
			this.Height = Math.Max(this.IconCell.Scale - 1, this.NameCell.Location.Y + this.NameCell.Height);
		}

		/// <summary>
		/// 名称改变时重新设置宽度
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemName_NameChanged(object sender, EventArgs e) => this.Width = this.NameCell.Location.X + this.NameCell.Width;

		private void ItemName_DoubleClick(object sender, EventArgs e) => this.ItemData.PreviewShow(this);
		#endregion
	}
}