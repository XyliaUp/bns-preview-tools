using System.Windows;
using System.Windows.Controls.Primitives;

using HandyControl.Controls;

namespace Xylia.Preview.UI.Controls;
public class SequenceAttributeEditor : PropertyEditorBase
{
	public SequenceAttributeEditor(List<string> sequence)
	{
		Sequence = sequence;
	}

	public List<string> Sequence { get; set; }

	public override FrameworkElement CreateElement(PropertyItem propertyItem) => new System.Windows.Controls.ComboBox
	{
		IsEnabled = !propertyItem.IsReadOnly,
		ItemsSource = Sequence,
	};

	public override DependencyProperty GetDependencyProperty() => Selector.SelectedValueProperty;
}