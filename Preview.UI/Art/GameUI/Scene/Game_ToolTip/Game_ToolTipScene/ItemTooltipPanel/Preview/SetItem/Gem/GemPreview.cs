using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	[DesignTimeVisible(false)]
	public partial class GemPreview : UserControl
	{
		public GemPreview()
		{
			InitializeComponent();
		}


		[Category("Data"), Description("")]
		public Bitmap Meta1 { set => this.Circle.Meta1 = value; get => this.Circle.Meta1; }

		[Category("Data"), Description("")]
		public Bitmap Meta2 { set => this.Circle.Meta2 = value; get => this.Circle.Meta2; }

		[Category("Data"), Description("")]
		public Bitmap Meta3 { set => this.Circle.Meta3 = value; get => this.Circle.Meta3; }

		[Category("Data"), Description("")]
		public Bitmap Meta4 { set => this.Circle.Meta4 = value; get => this.Circle.Meta4; }

		[Category("Data"), Description("")]
		public Bitmap Meta5 { set => this.Circle.Meta5 = value; get => this.Circle.Meta5; }

		[Category("Data"), Description("")]
		public Bitmap Meta6 { set => this.Circle.Meta6 = value; get => this.Circle.Meta6; }

		[Category("Data"), Description("")]
		public Bitmap Meta7 { set => this.Circle.Meta7 = value; get => this.Circle.Meta7; }

		[Category("Data"), Description("")]
		public Bitmap Meta8 { set => this.Circle.Meta8 = value; get => this.Circle.Meta8; }


		/// <summary>
		/// 设置公共品质
		/// </summary>
		[Category("Meta"), Description("")]
		public byte PublicGrade
		{
			get => this.itemNameCell1.ItemGrade;
			set => this.itemNameCell1.ItemGrade = this.itemNameCell2.ItemGrade = this.itemNameCell3.ItemGrade =
				   this.itemNameCell4.ItemGrade = this.itemNameCell5.ItemGrade = this.itemNameCell6.ItemGrade =
				   this.itemNameCell7.ItemGrade = this.itemNameCell8.ItemGrade = value;
		}
	}
}
