using System.ComponentModel;
using System.Drawing.Design;

namespace Xylia.Preview.GameUI.Controls
{
	/// <summary>
	/// 带标题的控件块
	/// </summary>
	public partial class TitleContentPanel : TitlePanel
	{
		public TitleContentPanel() => InitializeComponent();


		public TitleContentPanel(string Title, string Content) : this()
		{
			this.Title = Title;
			this.Text = Content;
		}


		[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
		[Category("外观"), Description("内容")]
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
}