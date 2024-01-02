using System.Collections;
using System.Windows;
using System.Windows.Markup;

namespace Xylia.Preview.UI.Controls;

[ContentProperty("Items")]
public class BnsCustomWindowWidget : Window, IBnsCustomBaseWidget
{
	#region Constructors
	private readonly Anchor ItemsPanel;

	public BnsCustomWindowWidget()
	{
		this.Content = this.ItemsPanel = new Anchor();

		this.ResizeMode = ResizeMode.NoResize;
		this.WindowStyle = WindowStyle.ToolWindow;
	}
	#endregion

	#region Properies
	public IList Items => ItemsPanel.Children;
	#endregion
}