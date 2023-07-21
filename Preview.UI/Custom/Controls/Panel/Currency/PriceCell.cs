using System.ComponentModel;

using Xylia.Preview.Common.Arg;
using Xylia.Preview.UI.Custom.Controls.Currency;
using Xylia.Preview.UI.Custom.Controls.Designer;
using Xylia.Preview.UI.Resources;

namespace Xylia.Preview.UI.Custom.Controls;
[Designer(typeof(FixedHeightDesigner))]
public partial class PriceCell : Panel
{
	#region Constructor
	public PriceCell()
	{
		InitializeComponent();

		this.BackColor = Color.Transparent;
		this.DoubleBuffered = true;
		this.ForeColor = Color.White;
		this.ResizeRedraw = false;

		this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
	}
	#endregion


	#region Fields
	[Description("描述信息")]
	public string Tooltip { get; set; }

	public FontStyle FontStyle { get; set; }

	public int CurrencyCount { get; set; }

	public CurrencyType CurrencyType { get; set; } = CurrencyType.Money;
	#endregion


	#region Functions
	protected override void OnPaint(PaintEventArgs e)
	{
		int LoX = 0;

		if (!string.IsNullOrEmpty(this.Tooltip))
		{
			e.Graphics.DrawString(this.Tooltip, this.Font, new SolidBrush(this.ForeColor), new Point(LoX, 0));
			LoX = (int)(e.Graphics.MeasureString(this.Tooltip, this.Font).Width - 4F);
		}


		if (this.CurrencyType == CurrencyType.Money)
		{
			if (this.CurrencyCount == 0) this.CreateMeta(e.Graphics, 0, Resource_BNSR.GameUI_Coin_Bronze, ref LoX);
			else
			{
				var money = new Money(this.CurrencyCount);
				if (money.Gold != 0) this.CreateMeta(e.Graphics, money.Gold, Resource_BNSR.GameUI_Coin_Gold, ref LoX);
				if (money.Silver != 0) this.CreateMeta(e.Graphics, money.Silver, Resource_BNSR.GameUI_Coin_Silver, ref LoX);
				if (money.Copper != 0) this.CreateMeta(e.Graphics, money.Copper, Resource_BNSR.GameUI_Coin_Bronze, ref LoX);
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

	private void CreateMeta(Graphics g, int Count, Image Icon, ref int LoX)
	{
		Size RealSize = new(24, 24);
		g.DrawString(Count.ToString(), this.Font, new SolidBrush(this.ForeColor), new Point(LoX, 0));

		//测量长度
		int StrLength = (int)(g.MeasureString(Count.ToString(), this.Font).Width - 4F);

		if ((FontStyle & FontStyle.Strikeout) == FontStyle.Strikeout)
		{
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

		LoX += RealSize.Width;
	}


	public override void Refresh()
	{
		base.Refresh();
		this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
	}
	#endregion
}