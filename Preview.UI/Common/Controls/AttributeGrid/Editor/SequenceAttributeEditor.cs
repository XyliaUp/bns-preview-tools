using System.Windows;
using System.Windows.Controls.Primitives;
using HandyControl.Controls;

namespace Xylia.Preview.UI.Controls;
internal class SequenceAttributeEditor(List<string> sequence) : PropertyEditorBase
{
	public override FrameworkElement CreateElement(PropertyItem propertyItem) => new System.Windows.Controls.ComboBox
	{
		IsEnabled = !propertyItem.IsReadOnly,
		ItemsSource = sequence,
	};

	public override DependencyProperty GetDependencyProperty() => Selector.SelectedValueProperty;
}