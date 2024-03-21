using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Xylia.Preview.UI.Controls;
/// <summary>
/// 
/// </summary>
[ContentProperty("Children")]
public class BnsCustomUISceneWidget : FrameworkElement
{
	#region Properties
	public Collection<FrameworkElement> Children { get; } = [];

	public object? Activate { get; set; }
	#endregion

	#region Children
	protected override IEnumerator LogicalChildren => Children.GetEnumerator();

	protected override int VisualChildrenCount => Activate is null ? 0 : 1;

	protected override Visual? GetVisualChild(int index) => Activate as Visual;
	#endregion
}