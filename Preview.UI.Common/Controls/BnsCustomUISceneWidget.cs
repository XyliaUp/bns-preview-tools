using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Xylia.Preview.UI.Controls;

[ContentProperty("Children")]
public class BnsCustomUISceneWidget : FrameworkElement
{
	#region Properties
	public string Activate { get; set; }
	#endregion

	#region Children
	public Collection<FrameworkElement> Children { get; } = [];

	protected override IEnumerator LogicalChildren => Children.GetEnumerator();

	protected override int VisualChildrenCount => Children.Count;  // 1 or 0

	protected override Visual GetVisualChild(int index)
	{
		return Children[index];
	}
	#endregion
}