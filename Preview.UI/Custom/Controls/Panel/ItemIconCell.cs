using System.ComponentModel;
using System.Drawing.Drawing2D;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.UI.Custom.Controls.Designer;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.Game_ItemGrowth2;
using Xylia.Preview.UI.Extension;

namespace Xylia.Preview.UI.Custom.Controls;

[Designer(typeof(FixedHeightDesigner))]
public partial class ItemIconCell : PictureBox
{
	#region Constructor
	public ItemIconCell()
	{
		InitializeComponent();

		this.Scale = 45;

		this.DoubleBuffered = true;
		this.ResizeRedraw = true;
		this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
	}
	#endregion

	#region 隐藏不需要的属性
	[Browsable(false)]
	[Obsolete]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new int Width => base.Width;

	[Browsable(false)]
	[Obsolete]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new int Height => base.Height;
	#endregion


	#region Fields
	public BaseRecord ObjectRef;

	/// <summary>
	/// 框架图标
	/// </summary>
	[Category("Apperance"), Description("框架图标")]
	public virtual Bitmap FrameImage { get; set; }

	/// <summary>
	/// 框架图标显示类型
	/// flase=仅覆盖 | true=计算大小
	/// </summary>
	[Category("Apperance"), Description("框架图标显示类型")]
	public bool FrameType { get; set; } = true;

	[Category("Apperance"), Description("显示框架图标")]
	public bool ShowFrameImage { get; set; } = true;

	/// <summary>
	/// 图片比例
	/// </summary>
	[Category("Apperance"), Description("比例")]
	public new int Scale
	{
		get => base.Width;
		set
		{
			base.Width = base.Height = value;
			this.Refresh();
		}
	}
	#endregion

	#region ExtraIcon
	private Dictionary<DrawLocation, Bitmap> ExtraIcon { get; set; } = new();

	public void SetExtraImg(DrawLocation location, Bitmap bitmap)
	{
		if (bitmap is null) return;
		ExtraIcon[location] = bitmap;

		this.Refresh();
	}

	public Bitmap GetExtraImg(DrawLocation location) => ExtraIcon.ContainsKey(location) ? ExtraIcon[location] : null;
	#endregion

	#region StackCount
	/// <summary>
	/// 堆叠数量
	/// </summary>
	[Category("Data"), Description("物品数量")]
	public int StackCount { get; set; }

	/// <summary>
	/// 显示物品数量
	/// </summary>
	[Category("Data"), Description("显示物品数量")]
	public bool ShowStackCount { get; set; } = false;

	/// <summary>
	/// 显示物品数量
	/// </summary>
	[Category("Data"), Description("当数量为1时, 是否显示物品数量")]
	public bool ShowStackCountOnlyOne { get; set; } = true;
	#endregion



	#region Functions
	private void ItemIconCell_Resize(object sender, EventArgs e)
	{
		if (this is not FeedItemIconCell)
			base.Width = base.Height = this.Scale;
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		#region 计算基本信息
		Size ImgSize;
		PointF ImgTopLeft;
		PointF ImgBottomRight;

		if (this is FeedItemIconCell)
		{
			//获取缩放比例
			float Ratio = (float)base.Height / this.FrameImage.Height;
			base.Width = (int)(this.FrameImage.Width * Ratio);

			int Left = 13, Right = 13, Top = 20, Bottom = 14;

			ImgTopLeft = new PointF(Ratio * Left, Ratio * Top);
			ImgBottomRight = new PointF(Ratio * (this.FrameImage.Width - Right), Ratio * (this.FrameImage.Height - Bottom));
			ImgSize = new Size((int)(Ratio * (this.FrameImage.Width - Left - Right)), (int)(Ratio * (this.FrameImage.Height - Bottom - Top)));
		}
		else
		{
			int Border = 0;
			if (this.FrameImage != null && FrameType)
			{
				//需按照比例缩放框架图片
				var Ratio = (float)this.FrameImage.Width / this.Scale;

				//左边框中点首个透明度图层为0的对象的位置作为边界大小
				for (int x = 0; x < this.Scale; x++)
				{
					var Pixel = this.FrameImage.GetPixel((int)(Ratio * x), (int)(Ratio * this.Scale) / 2);
					if (Pixel.A == 0)
					{
						Border = x;
						break;
					}
				}
			}

			ImgTopLeft = new PointF(Border, Border);
			ImgBottomRight = new PointF(this.Scale - Border, this.Scale - Border);

			var ImgScale = this.Scale - 2 * Border;
			ImgSize = new Size(ImgScale, ImgScale);
		}
		#endregion


		#region 绘制一般内容
		//图片
		var Img = this.Image /*?? Resource_Common.ItemIcon*/;
		if (Img != null) pe.Graphics.DrawImage(Img, new Rectangle((int)ImgTopLeft.X, (int)ImgTopLeft.Y, ImgSize.Width, ImgSize.Height));

		//背景层
		if (this.FrameImage != null && this.ShowFrameImage)
			pe.Graphics.DrawImage(this.FrameImage, new Rectangle(0, 0, base.Width, base.Height));

		//堆叠数量
		if (ShowStackCount && (ShowStackCountOnlyOne || this.StackCount != 1))
			DrawStackCount(pe.Graphics, this.StackCount, ImgBottomRight);
		#endregion

		#region 绘制附加图片
		foreach (var Extra in ExtraIcon)
		{
			if (Extra.Value is null) continue;

			var AttachImg = Extra.Value.Thumbnail(ImgSize.Width, ImgSize.Height);

			float LocationX = 0;
			float LocationY = 0;

			#region 根据附加类型计算位置
			switch (Extra.Key)
			{
				case DrawLocation.Centre:
					LocationX = (ImgBottomRight.X - AttachImg.Width) / 2;
					LocationY = (ImgBottomRight.Y - AttachImg.Height) / 2;
					break;

				case DrawLocation.TopLeft:
					LocationX = ImgTopLeft.X;
					LocationY = ImgTopLeft.Y;
					break;

				case DrawLocation.TopRight:
					LocationX = ImgBottomRight.X - AttachImg.Width;
					LocationY = 0;
					break;

				case DrawLocation.BottomLeft:
					LocationX = 0;
					LocationY = ImgBottomRight.Y - AttachImg.Height;
					break;

				case DrawLocation.BottomRight:
					LocationX = ImgBottomRight.X - AttachImg.Width;
					LocationY = ImgBottomRight.Y - AttachImg.Height;
					break;
			}
			#endregion

			pe.Graphics.DrawImage(Extra.Value, new Rectangle((int)LocationX, (int)LocationY, AttachImg.Width, AttachImg.Height));
		}
		#endregion
	}

	private static void DrawStackCount(Graphics g, int StackCount, PointF Border)
	{
		var Txt = StackCount.ToString();

		#region 处理字体大小程度
		var CurFont = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);

		if (StackCount > 9999) CurFont = new Font(CurFont.FontFamily, CurFont.Size - 2);
		else if (StackCount > 999) CurFont = new Font(CurFont.FontFamily, CurFont.Size - 1);
		else if (StackCount > 99) CurFont = new Font(CurFont.FontFamily, CurFont.Size);
		else if (StackCount > 9) CurFont = new Font(CurFont.FontFamily, CurFont.Size);
		else CurFont = new Font(CurFont.FontFamily, CurFont.Size);
		#endregion


		var TxtSize = Txt.MeasureString(CurFont);
		var Rect = new RectangleF(Border.X - TxtSize.Width - 2, Border.Y - CurFont.Height, TxtSize.Width, CurFont.Height);

		using var path = GetStringPath(Txt, g.DpiY, Rect, CurFont, StringFormat.GenericTypographic);

		//设置字体质量
		g.SmoothingMode = SmoothingMode.AntiAlias;

		//绘制轮廓（描边）
		var StrokePen = new Pen(Color.Black, 1.7F);
		g.DrawPath(StrokePen, path);

		//填充轮廓（填充）
		g.FillPath(Brushes.White, path);
	}

	public static GraphicsPath GetStringPath(string s, float dpi, RectangleF rect, Font font, StringFormat format)
	{
		var path = new GraphicsPath();
		float emSize = dpi * font.SizeInPoints / 72;

		path.AddString(s, font.FontFamily, (int)font.Style, emSize, rect, format);

		return path;
	}




	protected override void OnDoubleClick(EventArgs e) => ObjectRef.PreviewShow(this);
	#endregion
}