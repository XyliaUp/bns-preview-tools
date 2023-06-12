using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.Designer;

namespace Xylia.Preview.GameUI.Controls
{
	[OnlyAutoSize(true)]
	[Designer(typeof(FixedDesigner))]
	public partial class ItemNameCell : Panel
	{
		#region Constructor
		public ItemNameCell()
		{
			InitializeComponent();

			CheckForIllegalCrossThreadCalls = false;

			//初始状态
			this.BackColor = Color.Transparent;
			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

			this.ResizeRedraw = false;
		}
		#endregion

		#region Event
		/// <summary>
		/// 名称改变Event
		/// </summary>
		public event EventHandler NameChanged;
		#endregion

		#region Fields
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
		[Category("Appearance"), Description("物品名称")]
		public override string Text
		{
			get => base.Text;
			set
			{
				base.Text = value;
				this.Refresh();

				//委托Event
				this.NameChanged?.Invoke(null, null);
			}
		}


		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color ForeColor => m_itemgrade.ItemGrade();



		private byte m_itemgrade = 6;

		[Category("Data"), Description("物品品质")]
		public byte ItemGrade
		{
			get => m_itemgrade;
			set
			{
				m_itemgrade = value;
				this.Refresh();
			}
		}


		private Bitmap m_TagImage;
		[Category("Data"), Description("标签图片")]
		public Bitmap TagImage
		{
			get => this.m_TagImage;
			set
			{
				this.m_TagImage = value;
				this.Refresh();
			}
		}

		/// <summary>
		/// 物品别名
		/// </summary>
		public BaseRecord ObjectRef;
		#endregion

		#region Functions
		float ExpectHeight, ExpectWidth;

		protected override void OnPaint(PaintEventArgs e)
		{
			#region Initialize
			ExpectWidth = ExpectHeight = 0;

			int MaxWidth = 0;
			float LocX = 0, LocY = 0;

			if (this.MaximumSize.Width != 0) MaxWidth = this.MaximumSize.Width;
			else if (this.Parent != null && this.Parent.MaximumSize.Width != 0) MaxWidth = this.Parent.MaximumSize.Width - this.Left;
			#endregion

			#region 绘制文本
			var param = new ExecuteParam(this);
			foreach (var Info in ContentPanel.SplitMultiLine(param, this.Text, MaxWidth, ref LocX, ref LocY))
			{
				ExpectWidth = Math.Max(ExpectWidth, (int)Math.Ceiling(LocX + 4.0F));
				e.Graphics?.DrawString(Info.text, this.Font, new SolidBrush(this.ForeColor), Info.point);
			}

			if (LocY != 0) ExpectWidth = Math.Max(ExpectWidth, MaxWidth);
			ExpectHeight = (int)LocY + this.Font.Height;
			#endregion

			#region 绘制附加图片
			if (this.TagImage != null)
			{
				//图片渲染前景色
				var Img = this.TagImage.ChangeColor(this.ForeColor);

				//绘制时图片高度
				int TarHeight = this.Font.Height - 7;
				int TarWidth = TarHeight * Img.Width / Img.Height;

				//设置绘制位置
				var Rectangle = new RectangleF(LocX, LocY + (this.Font.Height - TarHeight) / 2, TarWidth, TarHeight);

				ExpectWidth = Math.Max(ExpectWidth, Rectangle.X + Rectangle.Width);
				ExpectHeight = Math.Max(ExpectHeight, Rectangle.Y + Rectangle.Height);

				e.Graphics?.DrawImage(Img, Rectangle);
			}
			#endregion



			if (this.AutoSize)
			{
				this.Height = (int)Math.Ceiling(ExpectHeight);
				this.Width = (int)Math.Ceiling(ExpectWidth);
			}
		}

		public override void Refresh()
		{
			base.Refresh();

			//如果没有load, 则使用空指针方式计算控件大小信息
			this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
		}

		public void OnDoubleClick(object sender, EventArgs e) => ObjectRef.PreviewShow(this);
		#endregion
	}
}