using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using HandyControl.Controls;

using Xylia.Preview.UI.Controls;

namespace Xylia.Preview.UI.Common.Controls.AttributeGrid.Editor;
public class IconAttributeEditor : ReferenceAttributeEditor
{
	public IconAttributeEditor() : base("IconTexture")
	{

	}

	public override FrameworkElement CreateElement(PropertyItem propertyItem)
	{
		// element
		var element = new IconPicker
		{
			IsEnabled = !propertyItem.IsReadOnly,
		};

		// set source
		BindingOperations.SetBinding(element, ItemsControl.ItemsSourceProperty,
			new Binding($"Records")
			{
				Source = ReferedTable,
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
			});

		return element;
	}
}