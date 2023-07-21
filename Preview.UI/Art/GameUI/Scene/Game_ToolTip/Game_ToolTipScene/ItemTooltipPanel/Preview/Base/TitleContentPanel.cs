using System.ComponentModel;
using System.Drawing.Design;

namespace Xylia.Preview.UI.Custom.Controls;
public partial class TitleContentPanel : TitlePanel
{
	public TitleContentPanel() => InitializeComponent();


	public TitleContentPanel(string Title, string Content) : this()
	{
		this.Title = Title;
		this.Text = Content;
	}


	[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
	public override string Text
	{
		get => this.ContentPanel.Text;
		set => this.ContentPanel.Text = value;
	}


	public override void Refresh()
	{
		this.Height = this.ContentPanel.Bottom;
	}
}