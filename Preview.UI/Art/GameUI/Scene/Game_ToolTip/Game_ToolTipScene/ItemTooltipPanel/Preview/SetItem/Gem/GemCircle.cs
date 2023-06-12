using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	[DesignTimeVisible(false)]
	public partial class GemCircle : UserControl
	{
		#region	Constructor
		public GemCircle()
		{
			InitializeComponent();

			this.DefaultScale = this.Width;
		}
		#endregion

		#region Fields
		private readonly int DefaultScale;

		private Bitmap BackGround  => (Bitmap)new ComponentResourceManager(typeof(GemCircle)).GetObject(nameof(this.Panel_BackGround) + ".Image");

		public Bitmap Image => (Bitmap)this.Panel_BackGround.Image;

		private Bitmap m_meta1 { get; set; }

		private Bitmap m_meta2 { get; set; }

		private Bitmap m_meta3 { get; set; }

		private Bitmap m_meta4 { get; set; }

		private Bitmap m_meta5 { get; set; }

		private Bitmap m_meta6 { get; set; }

		private Bitmap m_meta7 { get; set; }

		private Bitmap m_meta8 { get; set; }

		private float m_scale { get; set; } = 1.0F;

		private bool m_Transparent { get; set; }

		readonly List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();





		#region 元素成员
		[Category("Meta"), Description("")]
		public Bitmap Meta1
		{
			set
			{
				this.m_meta1 = value;
				this.Refresh();
			}

			get => m_meta1;
		}

		[Category("Meta"), Description("")]
		public Bitmap Meta2
		{
			set
			{
				this.m_meta2 = value;
				this.Refresh();
			}

			get => m_meta2;
		}

		[Category("Meta"), Description("")]
		public Bitmap Meta3
		{
			set
			{
				this.m_meta3 = value;
				this.Refresh();
			}

			get => m_meta3;
		}

		[Category("Meta"), Description("")]
		public Bitmap Meta4
		{
			set
			{
				this.m_meta4 = value;
				this.Refresh();
			}

			get => m_meta4;
		}

		[Category("Meta"), Description("")]
		public Bitmap Meta5
		{
			set
			{
				this.m_meta5 = value;
				this.Refresh();
			}

			get => m_meta5;
		}

		[Category("Meta"), Description("")]
		public Bitmap Meta6
		{
			set
			{
				this.m_meta6 = value;
				this.Refresh();
			}

			get => m_meta6;
		}

		[Category("Meta"), Description("")]
		public Bitmap Meta7
		{
			set
			{
				this.m_meta7 = value;
				this.Refresh();
			}

			get => m_meta7;
		}

		[Category("Meta"), Description("")]
		public Bitmap Meta8
		{
			set
			{
				this.m_meta8 = value;
				this.Refresh();
			}

			get => m_meta8;
		}
		#endregion


		/// <summary>
		/// 整体缩放控制
		/// </summary>
		/// <returns></returns>
		[Category("Meta"), Description("整体缩放到原来的 X分之一")]
		public float WholeScale
		{
			get => this.m_scale;
			set
			{
				if (value <= 1) this.m_scale = 1.0F;
				else
				{
					this.Panel_BackGround.SizeMode = PictureBoxSizeMode.StretchImage;
					this.Height = this.Panel_BackGround.Height = this.Width = this.Panel_BackGround.Width = (int)(DefaultScale / (this.m_scale = value));

					this.Refresh();
				}
			}
		}

		public bool Transparent
		{
			get => this.m_Transparent;
			set
			{
				this.m_Transparent = value;
				this.Refresh();
			}
		}


		/// <summary>
		/// 选择的部分
		/// </summary>
		[Category("Meta"), Description("")]
		public PartSection PartSel
		{
			get => m_PartSel;
			set
			{
				m_PartSel = value;

				SelectPartChanged?.Invoke(null, new EventArgs());

				this.Refresh();
			}
		}

		PartSection m_PartSel { get; set; } = PartSection.Init;

		public enum PartSection
		{
			/// <summary>
			/// Initialize
			/// </summary>
			Init,

			None,

			Part1,
			Part2,
			Part3,
			Part4,
			Part5,
			Part6,
			Part7,
			Part8,
		}

		public static Dictionary<PartSection, string> PartConvert = new Dictionary<PartSection, string>()
		{
			{ PartSection.Init , "未选择" },{ PartSection.None , "超出边界" },
			{ PartSection.Part1 , "1号位" },{ PartSection.Part2 , "2号位" },{ PartSection.Part3 , "3号位" },{ PartSection.Part4 , "4号位" },
			{ PartSection.Part5 , "5号位" },{ PartSection.Part6 , "6号位" },{ PartSection.Part7 , "7号位" },{ PartSection.Part8 , "8号位" },
		};



		#endregion

		#region Functions
		public event EventHandler SelectPartChanged;

		/// <summary>
		/// 渲染
		/// </summary>
		public override void Refresh()
		{
			Bitmap Panel = Transparent ? new Bitmap(this.BackGround.Width, this.BackGround.Height) : this.BackGround;

			if (Panel != null)
			{
				Graphics _g = Graphics.FromImage(Panel);

				DrawPart(_g, Meta1, PartSel == PartSection.Part1, Panel.Width, 1, 0);
				DrawPart(_g, Meta2, PartSel == PartSection.Part2, Panel.Width, 2, 0);
				DrawPart(_g, Meta3, PartSel == PartSection.Part3, Panel.Width, 2, 1);
				DrawPart(_g, Meta4, PartSel == PartSection.Part4, Panel.Width, 2, 2);
				DrawPart(_g, Meta5, PartSel == PartSection.Part5, Panel.Width, 1, 2);
				DrawPart(_g, Meta6, PartSel == PartSection.Part6, Panel.Width, 0, 2);
				DrawPart(_g, Meta7, PartSel == PartSection.Part7, Panel.Width, 0, 1);
				DrawPart(_g, Meta8, PartSel == PartSection.Part8, Panel.Width, 0, 0);


				_g.Save();

				this.Panel_BackGround.Image = PartSel == PartSection.None ? Stroke(Panel, true) : Panel;
			}
		}


		private void GemCircle_Load(object sender, EventArgs e)
		{

		}

		private void Panel_BackGround_DoubleClick(object sender, EventArgs e)
		{
			//this.Meta4 = this.Meta1;
		}

		/// <summary>
		/// 鼠标点击Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Panel_BackGround_MouseClick(object sender, MouseEventArgs e)
		{
			//计算单位值
			float Unit = this.Panel_BackGround.Width / 4;

			//转换为 中心单位坐标系
			PointF Point = new PointF(e.Location.X / Unit - 2, 2 - e.Location.Y / Unit);


			float Line1 = 2 * Point.X;
			float Line2 = Point.X / 2;
			float Line3 = Point.X / -2;
			float Line4 = -2 * Point.X;

			if (Point.Y >= Line1 && Point.Y > Line4 && Point.Y <= 2) PartSel = PartSection.Part1;
			else if (Point.Y >= Line2 && Point.Y < Line1 && Point.Y <= -Point.X + 3) PartSel = PartSection.Part2;
			else if (Point.Y >= Line3 && Point.Y < Line2 && Point.X <= 2) PartSel = PartSection.Part3;
			else if (Point.Y >= Line4 && Point.Y < Line3 && Point.Y >= Point.X - 3) PartSel = PartSection.Part4;
			else if (Point.Y <= Line1 && Point.Y < Line4 && Point.Y >= -2) PartSel = PartSection.Part5;
			else if (Point.Y <= Line2 && Point.Y > Line1 && Point.Y >= -Point.X - 3) PartSel = PartSection.Part6;
			else if (Point.Y <= Line3 && Point.Y > Line2 && Point.X >= -2) PartSel = PartSection.Part7;
			else if (Point.Y <= Line4 && Point.Y > Line3 && Point.Y <= Point.X + 3) PartSel = PartSection.Part8;
			else PartSel = PartSection.None;
		}

		public void DrawPart(Graphics g, Bitmap bitmap, bool Selected, float Width, int ParaX, int ParaY)
		{
			if (bitmap != null)
			{

				if (Selected) bitmap = Stroke(bitmap);

				float Unit = (float)Width / 4;
				float UnitDouble = (float)Width / 2;

				//计算坐标偏移量
				float OffsetX = ParaX * Unit;
				float OffsetY = ParaY * Unit;


				//解决外部图片引用空白衔接问题
				if (ParaX == 1 && ParaY == 0) OffsetX += 0.4F;

				g.DrawImage(bitmap, OffsetX, OffsetY, UnitDouble, UnitDouble);
			}
		}

		/// <summary>
		/// 描边
		/// </summary>
		/// <param name="bitmap"></param>
		/// <param name="IsBackground"></param>
		/// <returns></returns>
		public Bitmap Stroke(Bitmap bitmap, bool IsBackground = false)
		{
			//重新实例化, 防止修改原数据
			bitmap = new Bitmap(bitmap);

			//背景框范围选择有问题, 暂时调整为不处理
			if (IsBackground) return bitmap;

			GraphicsPath GP = new GraphicsPath();
			Color C = Color.FromArgb(0, 0, 0, 0);

			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					// 这点不透明而且左右上下四点至少有一点是透明的, 那这点就是边缘
					if (bitmap.GetPixel(i, j) != C && (i > 0 && bitmap.GetPixel(i - 1, j) == C || i < bitmap.Width - 1 && bitmap.GetPixel(i + 1, j) == C || j > 0 && bitmap.GetPixel(i, j - 1) == C || j < bitmap.Height - 1 && bitmap.GetPixel(i, j + 1) == C))
					{
						if (IsBackground) GP.AddRectangle(new Rectangle(new Point(i + 1, j), new Size(3, 3)));
						else GP.AddRectangle(new Rectangle(new Point(i, j), new Size(2, 2)));
					}
				}
			}


			using (Graphics G = Graphics.FromImage(bitmap)) G.DrawPath(Pens.Blue, GP);
			graphicsPaths.Add(GP);

			return bitmap;
		}

		public void Clear()
		{
			this.Meta1 = this.Meta2 = this.Meta3 = this.Meta4 = this.Meta5 = this.Meta6 = this.Meta7 = this.Meta8 = null;
		}
		#endregion
	}
}