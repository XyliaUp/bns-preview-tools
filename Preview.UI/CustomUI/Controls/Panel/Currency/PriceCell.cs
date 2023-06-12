using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Xylia.Preview.GameUI.Controls.Currency;
using Xylia.Preview.GameUI.Controls.Designer;
using Xylia.Preview.Resources;

namespace Xylia.Preview.GameUI.Controls
{
	/// <summary>
	/// 出售价格控件
	/// </summary>
	[Designer(typeof(FixedHeightDesigner))]
	public partial class PriceCell : Panel
	{
		#region Constructor
		public PriceCell()
		{
			InitializeComponent();

			this.BackColor = Color.Transparent;
			this.ForeColor = Color.White;
			//this.AutoSize = false;

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
		}
		#endregion


		#region Fields
		/// <summary>
		/// 描述信息
		/// </summary>
		[Category("Appearance"), Description("描述信息")]
		public string Tooltip { get; set; }

		[Category("Appearance"), Description("字体风格")]
		public FontStyle FontStyle { get; set; }



		private int m_count = 1;

		[Category("Data"), Description("货币数量")]
		public int CurrencyCount
		{
			get => m_count;
			set
			{
				m_count = value;
				this.StartPaint();
			}
		}



		private CurrencyType m_currencyType = CurrencyType.Money;

		[Category("Data"), Description("货币类型")]
		public CurrencyType CurrencyType
		{
			get => m_currencyType;
			set
			{
				m_currencyType = value;
				this.StartPaint();
			}
		}
		#endregion

		#region Functions
		/// <summary>
		/// 开始进行绘制
		/// </summary>
		private void StartPaint()
		{
			this.Refresh();
			this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//记录当前位置
			int LoX = 0;


			if (!string.IsNullOrWhiteSpace(this.Tooltip))
			{
				//创建标签行
				e.Graphics.DrawString(this.Tooltip, this.Font, new SolidBrush(this.ForeColor), new Point(LoX, 0));

				//测量长度
				LoX = (int)(e.Graphics.MeasureString(this.Tooltip, this.Font).Width - 4F);
			}


			if (this.CurrencyType == CurrencyType.Money)
			{
				//计算各部分数量
				if (this.CurrencyCount == 0) this.CreateMeta(e.Graphics, 0, Resource_BNSR.GameUI_Coin_Bronze, ref LoX);
				else
				{
					float TxtPadding = 0F;
					int IconPadding = 0;

					var MoneyConvert = new MoneyConvert(this.CurrencyCount);
					if (MoneyConvert.Gold != 0) this.CreateMeta(e.Graphics, MoneyConvert.Gold, Resource_BNSR.GameUI_Coin_Gold, ref LoX, TxtPadding, IconPadding);
					if (MoneyConvert.Silver != 0) this.CreateMeta(e.Graphics, MoneyConvert.Silver, Resource_BNSR.GameUI_Coin_Silver, ref LoX, TxtPadding, IconPadding);
					if (MoneyConvert.Copper != 0) this.CreateMeta(e.Graphics, MoneyConvert.Copper, Resource_BNSR.GameUI_Coin_Bronze, ref LoX, TxtPadding, IconPadding);
				}

				this.Height = 23;
			}
			else
			{
				this.CreateMeta(e.Graphics, this.CurrencyCount, this.CurrencyType.GetCurrencyIcon(), ref LoX);
				this.Height = 23;
			}

			this.Width = LoX;
		}

		/// <summary>
		/// 生成货币单元
		/// </summary>
		public void CreateMeta(Graphics g, int Count, Image Icon, ref int LoX, float TxtPadding = 0, int IconPadding = 0, Size? IconSize = null)
		{
			//如果未传递, 则使用常用图标Size
			Size RealSize = IconSize ?? new Size(24, 24);

			//创建标签行
			g.DrawString(Count.ToString(), this.Font, new SolidBrush(this.ForeColor), new Point(LoX, 0));

			//测量长度
			int StrLength = (int)(g.MeasureString(Count.ToString(), this.Font).Width - 4F + TxtPadding);

			if ((FontStyle & FontStyle.Strikeout) == FontStyle.Strikeout)
			{
				//计算删除线位置
				int LineLocY = this.Font.Height / 2 + 1;
				g.DrawLine(new Pen(this.ForeColor, 1.5F), new Point(LoX, LineLocY), new Point(LoX + StrLength, LineLocY));
			}


			//判断图标的显示形式, 否则会出现小图标被拉伸的问题
			if (Icon.Width < RealSize.Width && Icon.Height < RealSize.Height)
			{
				RealSize = Icon.Size;
			}


			LoX += StrLength;

			//绘制图像
			g.DrawImage(Icon, new Rectangle(new Point(LoX, 0), RealSize));

			LoX += RealSize.Width + IconPadding;
		}
		#endregion
	}
}