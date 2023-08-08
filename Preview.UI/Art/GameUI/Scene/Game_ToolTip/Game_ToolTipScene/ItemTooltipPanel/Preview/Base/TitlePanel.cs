using Xylia.Preview.UI.Interface;

namespace Xylia.Preview.UI.Custom.Controls;
public partial class TitlePanel : PreviewControl
{
	public TitlePanel() => InitializeComponent();

	public string Title { get => this.lbl_Title.Text; set => this.lbl_Title.Text = value; }
}