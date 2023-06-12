using System.ComponentModel;

namespace Xylia.Preview.GameUI.Controls.List
{
	public partial class InfoListCell : ListCell
	{
		public InfoListCell() => InitializeComponent();

		[Category("Data"), Description("左侧文本")]
		public string LeftText { get => this.lbl_LeftText.Text; set => this.lbl_LeftText.Text = value; }
	}
}